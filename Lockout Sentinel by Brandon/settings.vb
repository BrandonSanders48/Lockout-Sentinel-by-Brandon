Imports System.Collections.Specialized
Imports System.Runtime.CompilerServices.RuntimeHelpers
Imports System.Runtime.InteropServices

Public Class settings
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        email.Show()
        Me.Hide()
    End Sub
    Public Function ValidateTimeRange(startTextBox As TextBox, endTextBox As TextBox) As Boolean
        Dim startTime As TimeSpan
        Dim endTime As TimeSpan

        ' Check if start time is valid
        If Not TimeSpan.TryParseExact(startTextBox.Text.Trim(), "hh\:mm", Nothing, startTime) Then
            MessageBox.Show("Invalid start time format for LockDown. Use HH:mm (e.g., 08:30).", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            startTextBox.Focus()
            Return False
        End If

        ' Check if end time is valid
        If Not TimeSpan.TryParseExact(endTextBox.Text.Trim(), "hh\:mm", Nothing, endTime) Then
            MessageBox.Show("Invalid end time format for LockDown. Use HH:mm (e.g., 17:00).", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            endTextBox.Focus()
            Return False
        End If

        ' Check if start time is before end time
        If startTime >= endTime Then
            MessageBox.Show("The LockDown start time must be earlier than end time.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            startTextBox.Focus()
            Return False
        End If

        ' Optional: Enforce specific allowed range (e.g., between 00:00 and 23:59)
        If startTime < TimeSpan.Zero OrElse endTime > TimeSpan.FromHours(24) Then
            MessageBox.Show("The LockDown time must be between 00:00 and 23:59.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        ' Optional: Enforce valid range (00:00 - 23:59)
        If startTime < TimeSpan.Zero OrElse endTime < TimeSpan.Zero OrElse
       startTime >= TimeSpan.FromHours(24) OrElse endTime >= TimeSpan.FromHours(24) Then
            MessageBox.Show("The LockDown times must be between 00:00 and 23:59.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Return True
    End Function
    Public Function ValidateName(domain As String, type As String) As Boolean
        domain = domain.Trim()

        If String.IsNullOrWhiteSpace(domain) Then
            MessageBox.Show(type & " cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If domain.Contains(".") Then
            MessageBox.Show(type & " must not include a dot (e.g., .com or .local).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim regex As New Text.RegularExpressions.Regex("^[a-zA-Z0-9-]+$")

        If Not regex.IsMatch(domain) Then
            MessageBox.Show(type & " can only contain letters, numbers, and hyphens.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function
    Public Function ValidateDCsInput(input As String) As Boolean
        If String.IsNullOrWhiteSpace(input) Then
            MessageBox.Show("Please enter at least one DC.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim dcList As String() = input.Split(","c)
        Dim regex As New Text.RegularExpressions.Regex("^[a-zA-Z0-9-]+$")

        For Each dc In dcList
            Dim trimmedDC = dc.Trim()

            If String.IsNullOrWhiteSpace(trimmedDC) Then
                MessageBox.Show("One of the DC entries is empty after trimming.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If trimmedDC.Contains(".") Then
                MessageBox.Show($"'{trimmedDC}' should not contain a dot (like .com or .local).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If Not regex.IsMatch(trimmedDC) Then
                MessageBox.Show($"'{trimmedDC}' contains invalid characters. Only letters, numbers, and hyphens are allowed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Next
        Return True
    End Function
    Public Function ValidateNumericInputLength(input As String, minChars As Integer, maxChars As Integer) As Boolean
        input = input.Trim()

        ' Check if input is numeric only
        If Not input.All(AddressOf Char.IsDigit) Then
            MessageBox.Show("Please enter numbers only (0–9).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Check minimum length
        If input.Length < minChars Then
            MessageBox.Show($"Input must be at least {minChars} digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Check maximum length
        If input.Length > maxChars Then
            MessageBox.Show($"Input must be no more than {maxChars} digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function
    Private Function ValidateSecondsInput(input As String) As Boolean
        Dim seconds As Integer

        If Not Integer.TryParse(input.Trim(), seconds) Then
            MessageBox.Show("Please enter a valid whole number for seconds.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If seconds < 0 Then
            MessageBox.Show("Seconds cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If seconds < 30 Then
            MessageBox.Show("Minimum allowed time is 30 seconds.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Const maxSeconds As Integer = 12 * 3600 ' 12 hours in seconds

        If seconds > maxSeconds Then
            MessageBox.Show($"Maximum allowed seconds is {maxSeconds} (12 hours).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        save()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        My.Settings.domain = TextBox3.Text
        'My.Settings.Save()

        If My.Settings.domain = Nothing Then
            MessageBox.Show("Domain is required. Please enter a domain in settings.")
        Else
            admin.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Form1.Timer1.Stop()
        ' Form1.ProgressBar1.Visible = False

        If My.Settings.ignored_ad IsNot Nothing Then
            ListBox1.Items.Clear()
            For Each item As String In My.Settings.ignored_ad
                ListBox1.Items.Add(item)
            Next
        End If

        If My.Settings.ignored_nps IsNot Nothing Then
            ListBox2.Items.Clear()
            For Each item As String In My.Settings.ignored_nps
                ListBox2.Items.Add(item)
            Next
        End If
        TextBox3.Text = My.Settings.domain
        TextBox4.Text = My.Settings.dc
        TextBox5.Text = My.Settings.lockout_pass
        TextBox6.Text = My.Settings.start_time
        TextBox7.Text = My.Settings.end_time
        CheckBox8.Checked = My.Settings.border
        TextBox9.Text = My.Settings.flash_count
        TextBox8.Text = My.Settings.refresh
        If My.Settings.border = True Then
            TextBox9.Enabled = True
        Else
            TextBox9.Enabled = False
        End If


        Dim states As String = My.Settings.week.PadRight(7, "0"c)

        CheckBox1.Checked = (states(0) = "1"c)
        CheckBox2.Checked = (states(1) = "1"c)
        CheckBox3.Checked = (states(2) = "1"c)
        CheckBox4.Checked = (states(3) = "1"c)
        CheckBox5.Checked = (states(4) = "1"c)
        CheckBox6.Checked = (states(5) = "1"c)
        CheckBox7.Checked = (states(6) = "1"c)

        If My.Settings.advanced = True Then
            GroupBox2.Visible = True
            Button9.Visible = False
            Button8.Visible = False
            Me.Size = New Size(606, 772)
            ' Dim screen As Screen = Screen.PrimaryScreen
            ' Dim workingArea As Rectangle = screen.WorkingArea

            ' Me.Left = workingArea.Left + (workingArea.Width - Me.Width) \ 2
            ' Me.Top = workingArea.Top + (workingArea.Height - Me.Height) \ 2
        Else
            GroupBox2.Visible = False
            Button9.Visible = True
            Button8.Visible = True
            Me.Size = New Size(606, 366)
            'Dim screen As Screen = Screen.PrimaryScreen
            'Dim workingArea As Rectangle = screen.WorkingArea

            'Me.Left = workingArea.Left + (workingArea.Width - Me.Width) \ 2
            '   Me.Top = workingArea.Top + (workingArea.Height - Me.Height) \ 2

        End If
    End Sub

    Private Sub settings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.Timer1.Start()
        Form1.ProgressBar1.Visible = True
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not ValidateName(TextBox1.Text, "Username") Then
            Exit Sub
        End If
        ListBox1.Items.Add(TextBox1.Text.ToUpper)
        TextBox1.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not ValidateName(TextBox2.Text, "Username") Then
            Exit Sub
        End If
        ListBox2.Items.Add(TextBox2.Text)
        TextBox2.Clear()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If ListBox1.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select one or more users to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim itemsToRemove As New List(Of Object)

        For Each selectedItem In ListBox1.SelectedItems
            itemsToRemove.Add(selectedItem)
        Next

        For Each item In itemsToRemove
            ListBox1.Items.Remove(item)
        Next
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If ListBox2.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select one or more users to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim itemsToRemove As New List(Of Object)

        For Each selectedItem In ListBox2.SelectedItems
            itemsToRemove.Add(selectedItem)
        Next

        For Each item In itemsToRemove
            ListBox2.Items.Remove(item)
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Settings.lockout_pass = TextBox5.Text
        My.Settings.Save()

        If Not ValidateNumericInputLength(My.Settings.lockout_pass, 4, 8) Then
            My.Settings.lockout_pass = Nothing
            My.Settings.Save()
            'MessageBox.Show("Please configure a lockout pin before using LockDown", "Pin Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            email.Close()
            admin.Close()
            Form1.HideTaskbar()
            Form1.MinimizeAllOtherWindows()
            Form1.InstallKeyHook()
            Form1.CenterMouse()
            Form1.ControlBox = False
            Form1.LockMouseToForm()
            lockOut.Show()
            Form1.PreventSleep()
            Form1.LinkLabel1.Visible = False
            Form1.LinkLabel4.Visible = True
            Form1.SetClickThrough(False)
            Form1.Timer3.Start()
            Me.Close()
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            TextBox9.Enabled = True
        Else
            TextBox9.Enabled = False
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Me.Close()
        init.Show()
        init.Label1.Hide()
        init.LinkLabel1.Show()
    End Sub
    Private Function save() As Boolean
        TopMost = False
        If Not ValidateSecondsInput(TextBox8.Text) Then
            Return False
        End If
        If Not ValidateDCsInput(TextBox4.Text) Then
            Return False
        End If
        If Not ValidateName(TextBox3.Text, "Domain") Then
            Return False
        End If
        If TextBox3.Text = "" Or TextBox4.Text = "" Then
            MessageBox.Show("Please complete the required fields before saving", "Complete required fields", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        If ValidateTimeRange(TextBox6, TextBox7) Then
        Else
            Return False
        End If
        If TextBox9.Text = Nothing Or TextBox9.Text < 1 Then
            MessageBox.Show("Border flash count must exceed 0", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        If TextBox9.Text > border.flashMax Then
            MessageBox.Show("Border flash count may not exceed " & border.flashMax.ToString, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        GroupBox3.BackColor = SystemColors.Control
        GroupBox6.BackColor = SystemColors.Control
        Dim savedList As New StringCollection

        For Each item As String In ListBox1.Items
            savedList.Add(item)
        Next

        My.Settings.ignored_ad = savedList

        Dim savedList2 As New StringCollection

        For Each item As String In ListBox2.Items
            savedList2.Add(item)
        Next

        My.Settings.ignored_nps = savedList2

        My.Settings.domain = TextBox3.Text
        My.Settings.dc = TextBox4.Text
        My.Settings.lockout_pass = TextBox5.Text
        My.Settings.start_time = TextBox6.Text
        My.Settings.end_time = TextBox7.Text
        My.Settings.refresh = TextBox8.Text
        My.Settings.border = CheckBox8.Checked
        My.Settings.flash_count = TextBox9.Text
        Dim states As New Text.StringBuilder
        states.Append(If(CheckBox1.Checked, "1", "0"))
        states.Append(If(CheckBox2.Checked, "1", "0"))
        states.Append(If(CheckBox3.Checked, "1", "0"))
        states.Append(If(CheckBox4.Checked, "1", "0"))
        states.Append(If(CheckBox5.Checked, "1", "0"))
        states.Append(If(CheckBox6.Checked, "1", "0"))
        states.Append(If(CheckBox7.Checked, "1", "0"))

        My.Settings.week = states.ToString
        My.Settings.Save()

        My.Settings.Reload()

        Form1.Timer1.Interval = TextBox8.Text * 1000
        Form1.Timer1.Start()
        Form1.ProgressBar1.Visible = True
        Form1.CheckAccessWindow()
        Form1.Show()
        Close()
    End Function
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        save()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        GroupBox2.Visible = True
        Button9.Visible = False
        Button8.Visible = False
        Me.Size = New Size(606, 772)
        My.Settings.advanced = True
        'My.Settings.Save()
        ' Dim screen As Screen = Screen.PrimaryScreen
        ' Dim workingArea As Rectangle = screen.WorkingArea
        ' Me.Left = workingArea.Left + (workingArea.Width - Me.Width) \ 2
        ' Me.Top = workingArea.Top + (workingArea.Height - Me.Height) \ 2
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        GroupBox2.Visible = False
        Button9.Visible = True
        Button8.Visible = True
        Me.Size = New Size(606, 366)
        My.Settings.advanced = False
        'My.Settings.Save()
        ' Dim screen As Screen = Screen.PrimaryScreen
        ' Dim workingArea As Rectangle = screen.WorkingArea

        ' Me.Left = workingArea.Left + (workingArea.Width - Me.Width) \ 2
        '  Me.Top = workingArea.Top + (workingArea.Height - Me.Height) \ 2
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        My.Settings.domain = TextBox3.Text
        'My.Settings.Save()

        If My.Settings.domain = Nothing Then
            MessageBox.Show("Domain is required. Please enter a domain in settings.")
        Else
            filter.Show()
            Me.Hide()
        End If
    End Sub


End Class