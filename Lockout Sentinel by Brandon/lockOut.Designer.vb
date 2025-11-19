<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class lockOut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(lockOut))
        Timer1 = New Timer(components)
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        ' 
        ' lockOut
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.LimeGreen
        ClientSize = New Size(800, 450)
        ForeColor = SystemColors.ControlLightLight
        FormBorderStyle = FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "lockOut"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "LockOut"
        TransparencyKey = Color.LimeGreen
        WindowState = FormWindowState.Maximized
        ResumeLayout(False)
    End Sub

    Friend WithEvents Timer1 As Timer
End Class
