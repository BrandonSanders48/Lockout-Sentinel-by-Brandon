<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class email
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(email))
        GroupBox1 = New GroupBox()
        Label1 = New Label()
        TextBox1 = New TextBox()
        GroupBox2 = New GroupBox()
        Label6 = New Label()
        TextBox6 = New TextBox()
        CheckBox2 = New CheckBox()
        Label5 = New Label()
        TextBox5 = New TextBox()
        Label4 = New Label()
        TextBox4 = New TextBox()
        Label3 = New Label()
        TextBox3 = New TextBox()
        Label2 = New Label()
        TextBox2 = New TextBox()
        Button1 = New Button()
        GroupBox3 = New GroupBox()
        CheckBox1 = New CheckBox()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Location = New Point(12, 60)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(308, 84)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 23)
        Label1.Name = "Label1"
        Label1.Size = New Size(125, 15)
        Label1.TabIndex = 0
        Label1.Text = "Notification Recipient:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(14, 41)
        TextBox1.Name = "TextBox1"
        TextBox1.PlaceholderText = "johndoe@emailexample.com"
        TextBox1.Size = New Size(288, 23)
        TextBox1.TabIndex = 1
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label6)
        GroupBox2.Controls.Add(TextBox6)
        GroupBox2.Controls.Add(CheckBox2)
        GroupBox2.Controls.Add(Label5)
        GroupBox2.Controls.Add(TextBox5)
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Controls.Add(TextBox4)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(TextBox3)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(TextBox2)
        GroupBox2.Location = New Point(12, 145)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(308, 215)
        GroupBox2.TabIndex = 2
        GroupBox2.TabStop = False
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(6, 69)
        Label6.Name = "Label6"
        Label6.Size = New Size(39, 15)
        Label6.TabIndex = 11
        Label6.Text = "Email:"
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(6, 87)
        TextBox6.Name = "TextBox6"
        TextBox6.PlaceholderText = "jdoe@example.com"
        TextBox6.Size = New Size(141, 23)
        TextBox6.TabIndex = 12
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(16, 183)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(92, 19)
        CheckBox2.TabIndex = 10
        CheckBox2.Text = "SSL/StartTLS"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(6, 123)
        Label5.Name = "Label5"
        Label5.Size = New Size(60, 15)
        Label5.TabIndex = 8
        Label5.Text = "Password:"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(6, 141)
        TextBox5.Name = "TextBox5"
        TextBox5.PasswordChar = "*"c
        TextBox5.Size = New Size(141, 23)
        TextBox5.TabIndex = 9
        TextBox5.UseSystemPasswordChar = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(153, 69)
        Label4.Name = "Label4"
        Label4.Size = New Size(63, 15)
        Label4.TabIndex = 6
        Label4.Text = "Username:"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(153, 87)
        TextBox4.Name = "TextBox4"
        TextBox4.PlaceholderText = "jdoe"
        TextBox4.Size = New Size(141, 23)
        TextBox4.TabIndex = 7
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(153, 15)
        Label3.Name = "Label3"
        Label3.Size = New Size(66, 15)
        Label3.TabIndex = 4
        Label3.Text = "SMTP Port:"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(153, 33)
        TextBox3.Name = "TextBox3"
        TextBox3.PlaceholderText = "25"
        TextBox3.Size = New Size(141, 23)
        TextBox3.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(6, 15)
        Label2.Name = "Label2"
        Label2.Size = New Size(35, 15)
        Label2.TabIndex = 2
        Label2.Text = "Host:"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(6, 33)
        TextBox2.Name = "TextBox2"
        TextBox2.PlaceholderText = "smtp.example.com"
        TextBox2.Size = New Size(141, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(245, 366)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "Save"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(CheckBox1)
        GroupBox3.Location = New Point(12, 6)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(308, 52)
        GroupBox3.TabIndex = 2
        GroupBox3.TabStop = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(16, 22)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(201, 19)
        CheckBox1.TabIndex = 3
        CheckBox1.Text = "Send email on new lockout event"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' email
        ' 
        AcceptButton = Button1
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(330, 392)
        Controls.Add(GroupBox3)
        Controls.Add(Button1)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "email"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Email Configuration"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox6 As TextBox
End Class
