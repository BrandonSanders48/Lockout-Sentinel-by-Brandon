<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        ListBox1 = New ListBox()
        ListBox2 = New ListBox()
        Label1 = New Label()
        Label2 = New Label()
        Panel1 = New Panel()
        LinkLabel4 = New LinkLabel()
        LinkLabel1 = New LinkLabel()
        Label3 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        ProgressBar1 = New ProgressBar()
        Panel2 = New Panel()
        Label4 = New Label()
        Label7 = New Label()
        Panel3 = New Panel()
        Label5 = New Label()
        Label8 = New Label()
        Panel4 = New Panel()
        Label12 = New Label()
        Label10 = New Label()
        Label6 = New Label()
        Label9 = New Label()
        Timer1 = New Timer(components)
        LinkLabel2 = New LinkLabel()
        LinkLabel3 = New LinkLabel()
        GroupBox1 = New GroupBox()
        LinkLabel8 = New LinkLabel()
        LinkLabel7 = New LinkLabel()
        LinkLabel10 = New LinkLabel()
        LinkLabel9 = New LinkLabel()
        GroupBox2 = New GroupBox()
        Panel5 = New Panel()
        TextBox1 = New TextBox()
        LinkLabel5 = New LinkLabel()
        Label11 = New Label()
        Button5 = New Button()
        Panel7 = New Panel()
        LinkLabel11 = New LinkLabel()
        Label13 = New Label()
        Timer3 = New Timer(components)
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        Panel5.SuspendLayout()
        Panel7.SuspendLayout()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(31, 255)
        ListBox1.Name = "ListBox1"
        ListBox1.SelectionMode = SelectionMode.MultiSimple
        ListBox1.Size = New Size(245, 184)
        ListBox1.TabIndex = 0
        ' 
        ' ListBox2
        ' 
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(296, 255)
        ListBox2.Name = "ListBox2"
        ListBox2.SelectionMode = SelectionMode.MultiSimple
        ListBox2.Size = New Size(245, 184)
        ListBox2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = SystemColors.ControlDarkDark
        Label1.Location = New Point(31, 237)
        Label1.Name = "Label1"
        Label1.Size = New Size(175, 15)
        Label1.TabIndex = 2
        Label1.Text = "0 Locked Out AD User(s) Found:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = SystemColors.ControlDarkDark
        Label2.Location = New Point(296, 237)
        Label2.Name = "Label2"
        Label2.Size = New Size(181, 15)
        Label2.TabIndex = 4
        Label2.Text = "0 Locked Out NPS User(s) Found:"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Honeydew
        Panel1.Controls.Add(LinkLabel4)
        Panel1.Controls.Add(LinkLabel1)
        Panel1.Controls.Add(Label3)
        Panel1.Location = New Point(15, 23)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(552, 100)
        Panel1.TabIndex = 0
        ' 
        ' LinkLabel4
        ' 
        LinkLabel4.ActiveLinkColor = Color.Green
        LinkLabel4.AutoSize = True
        LinkLabel4.DisabledLinkColor = Color.Green
        LinkLabel4.ForeColor = Color.Green
        LinkLabel4.LinkColor = Color.Green
        LinkLabel4.Location = New Point(498, 7)
        LinkLabel4.Name = "LinkLabel4"
        LinkLabel4.Size = New Size(44, 15)
        LinkLabel4.TabIndex = 10
        LinkLabel4.TabStop = True
        LinkLabel4.Text = "Unlock"
        LinkLabel4.Visible = False
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.ActiveLinkColor = Color.Green
        LinkLabel1.AutoSize = True
        LinkLabel1.DisabledLinkColor = Color.Green
        LinkLabel1.ForeColor = Color.Green
        LinkLabel1.LinkColor = Color.Green
        LinkLabel1.Location = New Point(493, 7)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(49, 15)
        LinkLabel1.TabIndex = 9
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Settings"
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Segoe UI", 20F)
        Label3.ForeColor = Color.Green
        Label3.Location = New Point(14, 26)
        Label3.Name = "Label3"
        Label3.Size = New Size(524, 47)
        Label3.TabIndex = 8
        Label3.Text = """Passwords are hard, apparently!"""
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(201, 443)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 5
        Button1.Text = "Unlock"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(466, 443)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 6
        Button2.Text = "Unlock"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(-11, 475)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(604, 23)
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.TabIndex = 7
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.AliceBlue
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(Label7)
        Panel2.Location = New Point(31, 132)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(156, 88)
        Panel2.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Segoe UI", 12F)
        Label4.ForeColor = Color.RoyalBlue
        Label4.Location = New Point(3, 4)
        Label4.Name = "Label4"
        Label4.Size = New Size(150, 25)
        Label4.TabIndex = 10
        Label4.Text = "Computers"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.Font = New Font("Segoe UI", 18F)
        Label7.ForeColor = Color.RoyalBlue
        Label7.Location = New Point(3, 29)
        Label7.Name = "Label7"
        Label7.Size = New Size(150, 29)
        Label7.TabIndex = 11
        Label7.Text = "0"
        Label7.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.AliceBlue
        Panel3.Controls.Add(Label5)
        Panel3.Controls.Add(Label8)
        Panel3.Location = New Point(207, 132)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(156, 88)
        Panel3.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.Font = New Font("Segoe UI", 12F)
        Label5.ForeColor = Color.RoyalBlue
        Label5.Location = New Point(3, 4)
        Label5.Name = "Label5"
        Label5.Size = New Size(150, 25)
        Label5.TabIndex = 11
        Label5.Text = "Users"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Segoe UI", 18F)
        Label8.ForeColor = Color.RoyalBlue
        Label8.Location = New Point(3, 29)
        Label8.Name = "Label8"
        Label8.Size = New Size(150, 29)
        Label8.TabIndex = 12
        Label8.Text = "0"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.AliceBlue
        Panel4.Controls.Add(Label12)
        Panel4.Controls.Add(Label10)
        Panel4.Controls.Add(Label6)
        Panel4.Controls.Add(Label9)
        Panel4.Location = New Point(385, 132)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(156, 88)
        Panel4.TabIndex = 9
        ' 
        ' Label12
        ' 
        Label12.BackColor = Color.Transparent
        Label12.Font = New Font("Segoe UI", 16F)
        Label12.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label12.Location = New Point(131, -3)
        Label12.Name = "Label12"
        Label12.Size = New Size(28, 25)
        Label12.TabIndex = 14
        Label12.Text = "⟳"
        Label12.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label10
        ' 
        Label10.Font = New Font("Segoe UI", 8F)
        Label10.ForeColor = Color.RoyalBlue
        Label10.Location = New Point(3, 56)
        Label10.Name = "Label10"
        Label10.Size = New Size(150, 18)
        Label10.TabIndex = 13
        Label10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 12F)
        Label6.ForeColor = Color.RoyalBlue
        Label6.Location = New Point(27, 4)
        Label6.Name = "Label6"
        Label6.Size = New Size(93, 25)
        Label6.TabIndex = 12
        Label6.Text = "Last Update"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 18F)
        Label9.ForeColor = Color.RoyalBlue
        Label9.Location = New Point(3, 29)
        Label9.Name = "Label9"
        Label9.Size = New Size(150, 29)
        Label9.TabIndex = 12
        Label9.Text = "Never"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 45000
        ' 
        ' LinkLabel2
        ' 
        LinkLabel2.ActiveLinkColor = Color.LightSlateGray
        LinkLabel2.AutoSize = True
        LinkLabel2.DisabledLinkColor = Color.LightSlateGray
        LinkLabel2.ForeColor = Color.LightSlateGray
        LinkLabel2.LinkColor = Color.LightSlateGray
        LinkLabel2.Location = New Point(221, 237)
        LinkLabel2.Name = "LinkLabel2"
        LinkLabel2.Size = New Size(55, 15)
        LinkLabel2.TabIndex = 10
        LinkLabel2.TabStop = True
        LinkLabel2.Text = "Select All"
        LinkLabel2.UseWaitCursor = True
        LinkLabel2.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' LinkLabel3
        ' 
        LinkLabel3.ActiveLinkColor = Color.LightSlateGray
        LinkLabel3.AutoSize = True
        LinkLabel3.DisabledLinkColor = Color.LightSlateGray
        LinkLabel3.LinkColor = Color.LightSlateGray
        LinkLabel3.Location = New Point(486, 237)
        LinkLabel3.Name = "LinkLabel3"
        LinkLabel3.Size = New Size(55, 15)
        LinkLabel3.TabIndex = 11
        LinkLabel3.TabStop = True
        LinkLabel3.Text = "Select All"
        LinkLabel3.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' GroupBox1
        ' 
        GroupBox1.BackColor = SystemColors.Control
        GroupBox1.Controls.Add(LinkLabel8)
        GroupBox1.Controls.Add(LinkLabel7)
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Controls.Add(LinkLabel3)
        GroupBox1.Controls.Add(ListBox2)
        GroupBox1.Controls.Add(LinkLabel2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(Panel4)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Panel3)
        GroupBox1.Controls.Add(Panel1)
        GroupBox1.Controls.Add(Panel2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(ProgressBar1)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(LinkLabel10)
        GroupBox1.Controls.Add(LinkLabel9)
        GroupBox1.Location = New Point(4, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(577, 516)
        GroupBox1.TabIndex = 13
        GroupBox1.TabStop = False
        ' 
        ' LinkLabel8
        ' 
        LinkLabel8.ActiveLinkColor = Color.LightSlateGray
        LinkLabel8.AutoSize = True
        LinkLabel8.DisabledLinkColor = Color.LightSlateGray
        LinkLabel8.ForeColor = Color.LightSlateGray
        LinkLabel8.LinkColor = Color.LightSlateGray
        LinkLabel8.Location = New Point(296, 442)
        LinkLabel8.Name = "LinkLabel8"
        LinkLabel8.Size = New Size(79, 15)
        LinkLabel8.TabIndex = 16
        LinkLabel8.TabStop = True
        LinkLabel8.Text = "Hide Selected"
        LinkLabel8.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' LinkLabel7
        ' 
        LinkLabel7.ActiveLinkColor = Color.LightSlateGray
        LinkLabel7.AutoSize = True
        LinkLabel7.DisabledLinkColor = Color.LightSlateGray
        LinkLabel7.ForeColor = Color.LightSlateGray
        LinkLabel7.LinkColor = Color.LightSlateGray
        LinkLabel7.Location = New Point(31, 442)
        LinkLabel7.Name = "LinkLabel7"
        LinkLabel7.Size = New Size(79, 15)
        LinkLabel7.TabIndex = 15
        LinkLabel7.TabStop = True
        LinkLabel7.Text = "Hide Selected"
        LinkLabel7.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' LinkLabel10
        ' 
        LinkLabel10.ActiveLinkColor = Color.LightSlateGray
        LinkLabel10.AutoSize = True
        LinkLabel10.DisabledLinkColor = Color.LightSlateGray
        LinkLabel10.ForeColor = Color.LightSlateGray
        LinkLabel10.LinkColor = Color.LightSlateGray
        LinkLabel10.Location = New Point(473, 237)
        LinkLabel10.Name = "LinkLabel10"
        LinkLabel10.Size = New Size(68, 15)
        LinkLabel10.TabIndex = 18
        LinkLabel10.TabStop = True
        LinkLabel10.Text = "Deselect All"
        LinkLabel10.Visible = False
        LinkLabel10.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' LinkLabel9
        ' 
        LinkLabel9.ActiveLinkColor = Color.LightSlateGray
        LinkLabel9.AutoSize = True
        LinkLabel9.DisabledLinkColor = Color.LightSlateGray
        LinkLabel9.ForeColor = Color.LightSlateGray
        LinkLabel9.LinkColor = Color.LightSlateGray
        LinkLabel9.Location = New Point(207, 237)
        LinkLabel9.Name = "LinkLabel9"
        LinkLabel9.Size = New Size(68, 15)
        LinkLabel9.TabIndex = 17
        LinkLabel9.TabStop = True
        LinkLabel9.Text = "Deselect All"
        LinkLabel9.Visible = False
        LinkLabel9.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Panel5)
        GroupBox2.Location = New Point(4, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(577, 516)
        GroupBox2.TabIndex = 14
        GroupBox2.TabStop = False
        GroupBox2.Visible = False
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = SystemColors.ControlLight
        Panel5.Controls.Add(TextBox1)
        Panel5.Controls.Add(LinkLabel5)
        Panel5.Controls.Add(Label11)
        Panel5.Controls.Add(Button5)
        Panel5.Location = New Point(155, 167)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(265, 144)
        Panel5.TabIndex = 16
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(10, 58)
        TextBox1.Name = "TextBox1"
        TextBox1.PasswordChar = "*"c
        TextBox1.Size = New Size(220, 23)
        TextBox1.TabIndex = 17
        TextBox1.UseSystemPasswordChar = True
        ' 
        ' LinkLabel5
        ' 
        LinkLabel5.ActiveLinkColor = Color.RoyalBlue
        LinkLabel5.AutoSize = True
        LinkLabel5.DisabledLinkColor = Color.RoyalBlue
        LinkLabel5.ForeColor = Color.RoyalBlue
        LinkLabel5.LinkColor = Color.RoyalBlue
        LinkLabel5.Location = New Point(10, 116)
        LinkLabel5.Name = "LinkLabel5"
        LinkLabel5.Size = New Size(50, 15)
        LinkLabel5.TabIndex = 16
        LinkLabel5.TabStop = True
        LinkLabel5.Text = "Go back"
        LinkLabel5.VisitedLinkColor = Color.RoyalBlue
        ' 
        ' Label11
        ' 
        Label11.Location = New Point(10, 27)
        Label11.Name = "Label11"
        Label11.Size = New Size(237, 28)
        Label11.TabIndex = 15
        Label11.Text = "Please enter your pin to disable LockOut:"
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(172, 108)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 23)
        Button5.TabIndex = 13
        Button5.Text = "Unlock"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.MistyRose
        Panel7.Controls.Add(LinkLabel11)
        Panel7.Controls.Add(Label13)
        Panel7.Location = New Point(-17, 6)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(639, 18)
        Panel7.TabIndex = 18
        Panel7.Visible = False
        ' 
        ' LinkLabel11
        ' 
        LinkLabel11.ActiveLinkColor = Color.LightSlateGray
        LinkLabel11.AutoSize = True
        LinkLabel11.DisabledLinkColor = Color.LightSlateGray
        LinkLabel11.ForeColor = Color.LightSlateGray
        LinkLabel11.LinkColor = Color.LightSlateGray
        LinkLabel11.Location = New Point(583, 1)
        LinkLabel11.Name = "LinkLabel11"
        LinkLabel11.Size = New Size(14, 15)
        LinkLabel11.TabIndex = 20
        LinkLabel11.TabStop = True
        LinkLabel11.Text = "X"
        LinkLabel11.VisitedLinkColor = Color.LightSlateGray
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkRed
        Label13.Location = New Point(37, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(539, 18)
        Label13.TabIndex = 0
        Label13.Text = "Error Sending email"
        Label13.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Timer3
        ' 
        Timer3.Interval = 200
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(585, 493)
        Controls.Add(Panel7)
        Controls.Add(GroupBox1)
        Controls.Add(GroupBox2)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Lockout Sentinel by Brandon"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents LinkLabel4 As LinkLabel
    Friend WithEvents LinkLabel5 As LinkLabel
    Friend WithEvents LinkLabel8 As LinkLabel
    Friend WithEvents LinkLabel7 As LinkLabel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents LinkLabel10 As LinkLabel
    Friend WithEvents LinkLabel9 As LinkLabel
    Friend WithEvents Timer3 As Timer
    Friend WithEvents LinkLabel11 As LinkLabel
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBox1 As TextBox

End Class
