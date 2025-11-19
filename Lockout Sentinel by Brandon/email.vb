Public Class email
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not settings.ValidateName(TextBox4.Text, "Username") Then
            Exit Sub
        End If
        If Not ValidateEmail(TextBox1.Text) Then
            Exit Sub
        End If
        If Not ValidateEmail(TextBox6.Text) Then
            Exit Sub
        End If
        If Not ValidateSmtpHost(TextBox2.Text) Then
            Exit Sub
        End If
        If Not ValidatePort(TextBox3.Text) Then
            Exit Sub
        End If
        My.Settings.to_email = TextBox1.Text
        My.Settings.smtp_host = TextBox2.Text
        My.Settings.smtp_port = TextBox3.Text
        My.Settings.smtp_email = TextBox6.Text
        My.Settings.smtp_username = Form1.EncryptPassword(TextBox4.Text)
        My.Settings.smtp_password = Form1.EncryptPassword(TextBox5.Text)
        My.Settings.tls = CheckBox2.Checked

        If CheckBox1.Checked = True Then
            If TextBox5.Text <> "passwordisset" Then
                My.Settings.pass = Form1.EncryptPassword(TextBox5.Text)
            End If
        End If

        My.Settings.send_email = CheckBox1.Checked
        'My.Settings.Save()
        settings.Show()
        Me.Close()
    End Sub
    Private Function ValidatePort(portText As String) As Boolean
        Dim port As Integer

        If Not Integer.TryParse(portText.Trim(), port) Then
            MessageBox.Show("Please enter a valid number for the port.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If port < 1 OrElse port > 65535 Then
            MessageBox.Show("Port number must be between 1 and 65535.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateEmail(email As String) As Boolean
        email = email.Trim()

        If String.IsNullOrWhiteSpace(email) Then
            MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Simple regex for basic email validation
        Dim emailRegex As New Text.RegularExpressions.Regex(
        "^[^\s@]+@[^\s@]+\.[^\s@]+$"
    )

        If Not emailRegex.IsMatch(email) Then
            MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function
    Private Function ValidateSmtpHost(host As String) As Boolean
        host = host.Trim()

        If String.IsNullOrWhiteSpace(host) Then
            MessageBox.Show("SMTP host cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Simple regex for hostname validation: letters, digits, hyphens, dots
        Dim hostnameRegex As New Text.RegularExpressions.Regex("^[a-zA-Z0-9\-\.]+$")

        If Not hostnameRegex.IsMatch(host) Then
            MessageBox.Show("SMTP host contains invalid characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Additional checks:
        ' - Cannot start or end with a hyphen or dot
        If host.StartsWith("-") OrElse host.EndsWith("-") OrElse
       host.StartsWith(".") OrElse host.EndsWith(".") Then
            MessageBox.Show("SMTP host cannot start or end with a hyphen or dot.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function
    Private Sub email_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.to_email
        TextBox2.Text = My.Settings.smtp_host
        TextBox3.Text = My.Settings.smtp_port
        TextBox4.Text = My.Settings.smtp_username
        TextBox5.Text = My.Settings.smtp_password
        TextBox6.Text = My.Settings.smtp_email
        CheckBox1.Checked = My.Settings.send_email
        CheckBox2.Checked = My.Settings.tls
        If My.Settings.send_email = False Then
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
        Else
            TextBox5.Text = "passwordisset"
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
        Else
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
        End If
    End Sub

    Private Sub email_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        settings.Show()
        settings.BringToFront()
    End Sub
End Class