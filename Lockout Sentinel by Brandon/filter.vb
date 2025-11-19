Imports System.Collections.Specialized
Imports System.DirectoryServices
Imports System.DirectoryServices.ActiveDirectory
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class filter
    Private Sub filter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        treeViewAD.CheckBoxes = True
        treeViewAD.Nodes.Clear()

        Dim domain As String = My.Settings.domain.Trim()

        Dim username As String = Form1.DecryptPassword(My.Settings.user)
        Dim password As String = Form1.DecryptPassword(My.Settings.pass)



        Try
            Dim ldapPath As String = "LDAP://DC=" & domain & ",DC=local"
            Dim entry As DirectoryEntry

            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
                ' Use current Windows identity
                entry = New DirectoryEntry(ldapPath)
            Else
                ' Use specified credentials
                Dim formattedUsername As String = If(username.Contains("@") OrElse username.Contains("\"), username, domain & "\" & username)
                entry = New DirectoryEntry(ldapPath, formattedUsername, password)
            End If

            ' Attempt bind to confirm it works
            Dim native = entry.NativeObject

            Dim rootNode As New TreeNode(domain)
            rootNode.Tag = entry
            treeViewAD.Nodes.Add(rootNode)

            LoadChildOUs(entry, rootNode)
            rootNode.Expand()

            'CheckSavedOUs()
            If My.Settings.SelectedOUs Is Nothing OrElse My.Settings.SelectedOUs.Count = 0 Then
                If treeViewAD.Nodes.Count > 0 Then
                    treeViewAD.SelectedNode = treeViewAD.Nodes(0) ' Select the root node
                    treeViewAD.Select()
                    'treeViewAD.Nodes(0).Checked = True
                End If
            Else
                CheckSavedOUs()
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            settings.Show()
            Me.Close()
        End Try
    End Sub
    Private Sub LoadChildOUs(de As DirectoryEntry, parentNode As TreeNode)
        Try
            Dim searcher As New DirectorySearcher(de)
            searcher.Filter = "(objectCategory=organizationalUnit)"
            searcher.SearchScope = SearchScope.OneLevel

            For Each result As SearchResult In searcher.FindAll()
                Dim childDE As DirectoryEntry = result.GetDirectoryEntry()
                Dim ouName As String = childDE.Properties("name").Value.ToString()

                Dim ouNode As New TreeNode(ouName)
                ouNode.Tag = childDE
                parentNode.Nodes.Add(ouNode)

                LoadChildOUs(childDE, ouNode)
            Next
        Catch ex As Exception
            ' Silent fail on sub-OUs
        End Try
    End Sub
    Private Sub GetCheckedOUs()
        Dim checkedOUs As New List(Of String)
        GetCheckedOUsRecursive(treeViewAD.Nodes, checkedOUs)
        MessageBox.Show(String.Join(Environment.NewLine, checkedOUs), "Checked OUs")
    End Sub

    Private Sub GetCheckedOUsRecursive(nodes As TreeNodeCollection, checkedOUs As List(Of String))
        For Each node As TreeNode In nodes
            If node.Checked Then
                Dim de As DirectoryEntry = TryCast(node.Tag, DirectoryEntry)
                If de IsNot Nothing Then
                    checkedOUs.Add(de.Path)
                End If
            End If
            GetCheckedOUsRecursive(node.Nodes, checkedOUs)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveSelectedOUs()
        settings.Show()
        Me.Close()
    End Sub
    Private Sub SaveSelectedOUs()
        Dim checkedOUs As New StringCollection()
        SaveCheckedOUsRecursive(treeViewAD.Nodes, checkedOUs)

        My.Settings.SelectedOUs = checkedOUs
        'My.Settings.Save()

        'MessageBox.Show("Selected OUs saved to settings.")
    End Sub
    Private Sub SaveCheckedOUsRecursive(nodes As TreeNodeCollection, list As StringCollection)
        For Each node As TreeNode In nodes
            If node.Checked Then
                Dim de As DirectoryEntry = TryCast(node.Tag, DirectoryEntry)
                If de IsNot Nothing Then
                    list.Add(de.Path)
                End If
            End If
            SaveCheckedOUsRecursive(node.Nodes, list)
        Next
    End Sub
    Private Sub CheckSavedOUs()
        If My.Settings.SelectedOUs Is Nothing Then Exit Sub
        CheckSavedOUsRecursive(treeViewAD.Nodes, My.Settings.SelectedOUs)
    End Sub

    Private Sub CheckSavedOUsRecursive(nodes As TreeNodeCollection, savedPaths As StringCollection)
        For Each node As TreeNode In nodes
            Dim de As DirectoryEntry = TryCast(node.Tag, DirectoryEntry)
            If de IsNot Nothing AndAlso savedPaths.Contains(de.Path) Then
                node.Checked = True
            End If
            CheckSavedOUsRecursive(node.Nodes, savedPaths)
        Next
    End Sub

    Private Sub filter_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        settings.Show()
        settings.BringToFront()
    End Sub
End Class