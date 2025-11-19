Public Class lockOut
    Inherits Form

    Public Sub New()
        InitializeComponent()
        ' Cover all screens
        Dim allBounds As Rectangle = Screen.AllScreens.Select(Function(s) s.Bounds).Aggregate(Function(a, b) Rectangle.Union(a, b))
        Me.Bounds = allBounds

        ' Form settings
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = False
        ' Me.Owner = Form1
        Form1.BringToFront()
        Me.BackColor = Color.Black ' Needed for transparency key
        '  Me.TransparencyKey = Color.Black
        Me.Opacity = 0.2
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80 ' WS_EX_TOOLWINDOW - hides from Alt+Tab
            Return cp
        End Get
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub lockOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.TopMost = True
    End Sub

    Private Sub lockOut_Enter(sender As Object, e As EventArgs) Handles Me.Enter
        End
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Form1.TopMost = False
        Form1.TopMost = True
        Form1.BringToFront()
    End Sub
End Class