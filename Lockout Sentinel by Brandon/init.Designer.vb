<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class init
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(init))
        Label1 = New Label()
        Label2 = New Label()
        Timer1 = New Timer(components)
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        PictureBox1 = New PictureBox()
        Panel1 = New Panel()
        LinkLabel1 = New LinkLabel()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 12F)
        Label1.ForeColor = SystemColors.ControlDarkDark
        Label1.Location = New Point(248, 125)
        Label1.Name = "Label1"
        Label1.Size = New Size(129, 28)
        Label1.TabIndex = 0
        Label1.Text = "Loading..."
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(101, 15)
        Label2.TabIndex = 1
        Label2.Text = "Version: 1.0.3 beta"
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 3000
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 24)
        Label3.Name = "Label3"
        Label3.Size = New Size(159, 15)
        Label3.TabIndex = 2
        Label3.Text = "Created by: Brandon Sanders"
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Segoe UI", 16F)
        Label4.ForeColor = Color.DarkOrange
        Label4.Location = New Point(5, 5)
        Label4.Name = "Label4"
        Label4.Size = New Size(209, 28)
        Label4.TabIndex = 3
        Label4.Text = "Lockout Sentinel"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(14, 35)
        Label5.Name = "Label5"
        Label5.Size = New Size(190, 86)
        Label5.TabIndex = 4
        Label5.Text = "An application designed to quickly monitor and manage locked-out Active Directory and Network Policy Server (NPS) users, with real-time alerts and user-friendly controls." & vbCrLf
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(248, 22)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(129, 98)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Gainsboro
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label4)
        Panel1.Location = New Point(12, 43)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(217, 131)
        Panel1.TabIndex = 6
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.ActiveLinkColor = Color.RoyalBlue
        LinkLabel1.AutoSize = True
        LinkLabel1.DisabledLinkColor = Color.RoyalBlue
        LinkLabel1.ForeColor = Color.RoyalBlue
        LinkLabel1.LinkColor = Color.RoyalBlue
        LinkLabel1.Location = New Point(355, 165)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(36, 15)
        LinkLabel1.TabIndex = 7
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Close"
        LinkLabel1.Visible = False
        LinkLabel1.VisitedLinkColor = Color.RoyalBlue
        ' 
        ' init
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(397, 186)
        ControlBox = False
        Controls.Add(LinkLabel1)
        Controls.Add(Panel1)
        Controls.Add(PictureBox1)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "init"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Welcome"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LinkLabel1 As LinkLabel
End Class
