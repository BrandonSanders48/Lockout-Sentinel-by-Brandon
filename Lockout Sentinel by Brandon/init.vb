Imports System.Drawing.Drawing2D

Public Class init


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Form1.Show()
        Me.Hide()
        Timer1.Stop()
    End Sub

    Private Sub RoundPanelCorners(pnl As Panel, radius As Integer)
        Dim path As New GraphicsPath()
        Dim rect As Rectangle = pnl.ClientRectangle

        ' Add rounded rectangle path
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        ' Apply to panel
        pnl.Region = New Region(path)
    End Sub
    Private Sub init_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RoundPanelCorners(Panel1, 20) ' Panel1 with 20px radius
        'application.DoEvents() ' Force UI to repaint Panel6
        '  Await Task.Delay(200)  ' Optional: Small delay to help show animation/spinner
        ' Form1.Show()
        'Form1.Hide()


        Dim version As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
        Label2.Text = "Version: " & version & " beta"

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
    End Sub
End Class