Public Class border
    Inherits Form

    Private Const BORDER_THICKNESS As Integer = 20
    Private flashCount As Integer = 0
    Public flashMax As Integer = 10
    Private flashTimer As Timer

    Public Sub New()
        InitializeComponent()
        ' Setup fullscreen border-only form
        Dim allBounds As Rectangle = Screen.AllScreens.Select(Function(s) s.Bounds).Aggregate(Function(a, b) Rectangle.Union(a, b))
        Me.Bounds = allBounds
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = True
        Me.ShowInTaskbar = False
        Me.BackColor = Color.Red
        Me.StartPosition = FormStartPosition.Manual

        ' Create hollow center by setting form region
        Dim innerRect As New Rectangle(
        BORDER_THICKNESS,
        BORDER_THICKNESS,
        Me.ClientSize.Width - 2 * BORDER_THICKNESS,
        Me.ClientSize.Height - 2 * BORDER_THICKNESS
        )
        Dim outerRegion As New Region(Me.ClientRectangle)
        Dim innerRegion As New Region(innerRect)
        outerRegion.Exclude(innerRegion)
        Me.Region = outerRegion

        ' Setup flash timer
        flashTimer = New Timer()
        flashTimer.Interval = 600 ' milliseconds
        AddHandler flashTimer.Tick, AddressOf FlashTimer_Tick
    End Sub
    ' Start flashing
    Public Sub StartFlashing(times As Integer)
        flashMax = times * 2 ' because each flash is on+off
        flashCount = 0
        Me.Show()
        flashTimer.Start()
    End Sub

    Private Sub FlashTimer_Tick(sender As Object, e As EventArgs)
        ' Toggle visibility on/off
        Me.Visible = Not Me.Visible
        flashCount += 1
        If flashCount >= flashMax Then
            flashTimer.Stop()
            Me.Visible = False
        End If
    End Sub
End Class