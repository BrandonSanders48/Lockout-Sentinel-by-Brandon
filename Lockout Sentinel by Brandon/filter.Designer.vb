<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class filter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(filter))
        treeViewAD = New TreeView()
        Button1 = New Button()
        SuspendLayout()
        ' 
        ' treeViewAD
        ' 
        treeViewAD.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        treeViewAD.Location = New Point(0, 0)
        treeViewAD.Name = "treeViewAD"
        treeViewAD.Size = New Size(461, 393)
        treeViewAD.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Location = New Point(318, 399)
        Button1.Name = "Button1"
        Button1.Size = New Size(131, 23)
        Button1.TabIndex = 1
        Button1.Text = "Filter Selected OU's"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' filter
        ' 
        AcceptButton = Button1
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(461, 431)
        Controls.Add(Button1)
        Controls.Add(treeViewAD)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "filter"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Domain Filtering"
        ResumeLayout(False)
    End Sub

    Friend WithEvents treeViewAD As TreeView
    Friend WithEvents Button1 As Button
End Class
