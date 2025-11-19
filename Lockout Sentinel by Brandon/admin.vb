Imports System.DirectoryServices.AccountManagement

Public Class admin
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not settings.ValidateName(TextBox1.Text, "Username") Then
            Exit Sub
        End If
        If CheckBox1.Checked = False Then
            My.Settings.user = Nothing
            My.Settings.pass = Nothing
        Else
            If TextBox2.Text <> "passwordisset" Then
                If Not Await ValidateDomainUser(My.Settings.domain, TextBox1.Text, TextBox2.Text) Then
                    Me.Show()
                    Me.BringToFront()
                    TextBox2.Text = Nothing
                    TextBox2.Focus()
                    Exit Sub
                Else
                    MessageBox.Show("Your account has been successfully validated.", "Domain Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                My.Settings.user = Form1.EncryptPassword(TextBox1.Text)
                My.Settings.pass = Form1.EncryptPassword(TextBox2.Text)
            End If
        End If
        My.Settings.admin = CheckBox1.Checked
        'My.Settings.Save()
        settings.Show()
        Me.Close()
    End Sub
    Private Sub ShowMessageBox(message As String, title As String)
        If Application.OpenForms.Count > 0 Then
            Dim form = Application.OpenForms(0)
            form.Invoke(Sub()
                            MessageBox.Show(form, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Sub)
        Else
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Async Function ValidateDomainUser(domain As String, username As String, password As String) As Task(Of Boolean)
        Return Await Task.Run(Function()
                                  Try
                                      Dim upnUsername As String = If(username.Contains("@"), username, $"{username}@{domain}")

                                      Using pc As New PrincipalContext(ContextType.Domain, domain, upnUsername, password)
                                          If Not pc.ValidateCredentials(upnUsername, password, ContextOptions.Negotiate) Then
                                              ShowMessageBox("Invalid username or password.", "Login Failed")
                                              Return False
                                          End If

                                          Dim samUsername As String = If(username.Contains("@"), username.Split("@"c)(0), username)

                                          Using user As UserPrincipal = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, samUsername)
                                              If user Is Nothing Then
                                                  ShowMessageBox($"User '{samUsername}' not found in Active Directory.", "Login Failed")
                                                  Return False
                                              End If
                                          End Using
                                      End Using

                                      Return True

                                  Catch ex As Exception
                                      ShowMessageBox("Error validating credentials: " & ex.Message, "Exception")
                                      Return False
                                  End Try
                              End Function)
    End Function

    Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = Form1.DecryptPassword(My.Settings.user)
        CheckBox1.Checked = My.Settings.admin
        If My.Settings.admin = True Then
            GroupBox2.Enabled = True
            TextBox2.Text = "passwordisset"
        Else
            GroupBox2.Enabled = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            GroupBox2.Enabled = True
        Else
            GroupBox2.Enabled = False
        End If
    End Sub

    Private Sub admin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        settings.Show()
        settings.BringToFront()
    End Sub

End Class