<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class admin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(admin))
        GroupBox1 = New GroupBox()
        CheckBox1 = New CheckBox()
        Label1 = New Label()
        GroupBox2 = New GroupBox()
        Label3 = New Label()
        TextBox2 = New TextBox()
        Label2 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox1)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(7, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(264, 139)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(21, 102)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(186, 19)
        CheckBox1.TabIndex = 1
        CheckBox1.Text = "Specify Domain Admin Login?"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(6, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(252, 80)
        Label1.TabIndex = 0
        Label1.Text = "You can either run the program directly as a domain administrator, or specify a domain admin account to run it while logged in with a standard user account."
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(TextBox2)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(TextBox1)
        GroupBox2.Location = New Point(7, 139)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(264, 131)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(6, 71)
        Label3.Name = "Label3"
        Label3.Size = New Size(60, 15)
        Label3.TabIndex = 4
        Label3.Text = "Password:"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(6, 89)
        TextBox2.Name = "TextBox2"
        TextBox2.PasswordChar = "*"c
        TextBox2.Size = New Size(252, 23)
        TextBox2.TabIndex = 5
        TextBox2.UseSystemPasswordChar = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(6, 23)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 15)
        Label2.TabIndex = 2
        Label2.Text = "Username:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(6, 41)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(252, 23)
        TextBox1.TabIndex = 3
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(199, 276)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "Save"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' admin
        ' 
        AcceptButton = Button1
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(279, 307)
        Controls.Add(Button1)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "admin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Domain Admin Setup"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
End Class
