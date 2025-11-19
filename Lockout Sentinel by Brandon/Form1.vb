Imports System.DirectoryServices
Imports System.DirectoryServices.AccountManagement
Imports System.Drawing.Drawing2D
Imports System.Management
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Security
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Timers
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Microsoft.Win32
Imports AuthenticationLevel = System.Management.AuthenticationLevel

Public Class Form1
    ' Track known items for AD and NPS to detect changes
    Private knownItems1 As New HashSet(Of String)() ' For AD
    Private knownItems2 As New HashSet(Of String)() ' For NPS

    ' Flag to track first load to prevent false alerting
    Private firstLoadDone1 As Boolean = False
    Private firstLoadDone2 As Boolean = False
    ' API declarations
    <DllImport("user32.dll")>
    Public Shared Function ClipCursor(ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function ClipCursor(lpRect As IntPtr) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure
    Public Sub ReleaseMouse()
        ClipCursor(IntPtr.Zero)
        SetClickThrough(True)
    End Sub

    ' Center mouse cursor
    Public Sub CenterMouse()
        Dim centerX As Integer = Me.Left + (Me.Width \ 2)
        Dim centerY As Integer = Me.Top + (Me.Height \ 2)
        Cursor.Position = New Point(centerX, centerY)
    End Sub
    Public Sub LockMouseToForm()
        Dim rect As New RECT With {
        .Left = Me.Left,
        .Top = Me.Top,
        .Right = Me.Right,
        .Bottom = Me.Bottom
    }
        ClipCursor(rect)
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        settings.Show()
    End Sub
    Private Sub Form1_Move(sender As Object, e As EventArgs) Handles Me.Move
        If LinkLabel4.Visible = True Then
            LockMouseToForm()
        End If
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If LinkLabel4.Visible = True Then
            LockMouseToForm()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.NeedsUpgrade Then
            Try
                My.Settings.Upgrade()
                My.Settings.NeedsUpgrade = False
                My.Settings.Save()
            Catch ex As Exception
                MessageBox.Show("Failed to upgrade settings: " & ex.Message, "Cannot Restore Saved Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub CheckDNSResolution()
        Try
            Dim expectedIP As String = "9.9.9.9"
            Dim resolvedIPs() As IPAddress = Dns.GetHostAddresses("lockoutsentinel.brandonsanders.org")

            ' Check if any of the resolved IPs match expected
            Dim matchFound As Boolean = resolvedIPs.Any(Function(ip) ip.ToString() = expectedIP)

            If Not matchFound Then
                MessageBox.Show("DNS IP does not match expected value. Exiting...", "DNS Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If

        Catch ex As Exception
            MessageBox.Show("DNS resolution failed.", "DNS Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
    End Sub
    Private Async Function CheckLockedOutUsers(username As String, password As String) As Task
        Try
            ' Run the directory search in a background thread
            Dim allUsers As List(Of String) = Await Task.Run(Function()
                                                                 Dim users As New List(Of String)

                                                                 ' Prepare list of search roots (OUs or fallback to root)
                                                                 Dim searchRoots As New List(Of String)

                                                                 If My.Settings.SelectedOUs IsNot Nothing AndAlso My.Settings.SelectedOUs.Count > 0 Then
                                                                     searchRoots.AddRange(My.Settings.SelectedOUs.Cast(Of String)())
                                                                 Else
                                                                     ' Fallback to root domain
                                                                     searchRoots.Add("LDAP://DC=" & My.Settings.domain & ",DC=local")
                                                                 End If

                                                                 For Each ouPath As String In searchRoots
                                                                     Try
                                                                         Dim entry As DirectoryEntry

                                                                         If Not String.IsNullOrWhiteSpace(username) AndAlso Not String.IsNullOrWhiteSpace(password) AndAlso My.Settings.admin = True Then
                                                                             Dim domainAndUser As String = My.Settings.domain & "\" & username
                                                                             entry = New DirectoryEntry(ouPath, domainAndUser, password, AuthenticationTypes.Secure)
                                                                         Else
                                                                             entry = New DirectoryEntry(ouPath)
                                                                         End If

                                                                         Dim searcher As New DirectorySearcher(entry)
                                                                         searcher.Filter = "(&(objectCategory=person)(objectClass=user)(lockoutTime>=1))"
                                                                         searcher.PropertiesToLoad.Add("sAMAccountName")
                                                                         searcher.SearchScope = SearchScope.Subtree ' Important for full domain search

                                                                         Dim results As SearchResultCollection = searcher.FindAll()

                                                                         For Each result As SearchResult In results
                                                                             If result.Properties.Contains("sAMAccountName") Then
                                                                                 Dim username22 As String = result.Properties("sAMAccountName")(0).ToString().ToUpper()
                                                                                 If Not My.Settings.ignored_ad.Contains(username22) Then
                                                                                     If Not users.Contains(username22) Then
                                                                                         users.Add(username22)
                                                                                     End If
                                                                                 End If
                                                                             End If
                                                                         Next
                                                                     Catch comEx As System.Runtime.InteropServices.COMException
                                                                         users.Add("Error: " & ouPath & " - " & comEx.Message)
                                                                     Catch ex As Exception
                                                                         users.Add("Unhandled Error in " & ouPath & ": " & ex.Message)
                                                                     End Try
                                                                 Next

                                                                 Return users
                                                             End Function)

            ' Update UI
            ListBox1.Items.Clear()
            ListBox1.Items.AddRange(allUsers.ToArray())

            If Not allUsers.Any OrElse allUsers(0).StartsWith("Error") Then
                Label1.Text = "Failed to get locked out users"
            Else
                Label1.Text = allUsers.Count & " Locked Out AD User(s) Found:"
            End If

            ' These calls might also throw — handle separately or here
            Dim username2 As String = DecryptPassword(My.Settings.user)
            Dim password2 As String = DecryptPassword(My.Settings.pass)

            Dim userCount As Integer = Await CountObjectsInDomainAsync("person", username2, password2)
            Dim computerCount As Integer = Await CountObjectsInDomainAsync("computer", username2, password2)

            Label8.Text = userCount
            Label7.Text = computerCount

        Catch ex As Exception
            ListBox1.Items.Clear()
            ListBox1.Items.Add("Fatal Error: " & ex.Message)
            Label1.Text = "Unable to check locked users"
        End Try
    End Function



    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Await CheckLockedOutUsers(DecryptPassword(My.Settings.user), DecryptPassword(My.Settings.pass))

        If Label13.Text.Trim = "" Or Label13.Text = Nothing Then
            Panel7.Visible = False
        End If
        Dim allLockedOutUsers As New List(Of String)
        Dim dcList As String() = My.Settings.dc.Split(","c)
        Label13.Text = ""
        For Each dc In dcList
            Dim trimmedDC = dc.Trim
            Dim users As List(Of String) = Await GetLockedUsersViaWMI(trimmedDC)
            If users IsNot Nothing Then
                For Each user In users
                    Dim entry = $"{trimmedDC} - {user}"
                    If My.Settings.ignored_nps.Contains(entry) Then
                        'item exists
                        Continue For
                    Else
                        allLockedOutUsers.Add(entry)
                    End If
                Next
            End If
        Next

        Dim result = Await RefreshListBoxGeneralAsync(ListBox2, allLockedOutUsers, knownItems2, firstLoadDone2, "NPS")
        knownItems2 = result.UpdatedKnownItems
        firstLoadDone2 = result.UpdatedFirstLoadDone
        Label2.Text = ListBox2.Items.Count & " Locked Out NPS User(s) Found:"

        CheckAccessWindow()
        If Label13.Text.Trim = "" Or Label13.Text = Nothing Then
            Panel7.Visible = False
        End If
    End Sub
    Public Sub CheckAccessWindow()
        ' Parse times
        Dim startTime As TimeSpan = TimeSpan.Parse(My.Settings.start_time)
        Dim endTime As TimeSpan = TimeSpan.Parse(My.Settings.end_time)

        ' Get current time and day
        Dim now As DateTime = DateTime.Now
        Dim currentTime As TimeSpan = now.TimeOfDay
        Dim currentDayIndex As Integer = CInt(now.DayOfWeek) ' Sunday = 0

        ' Check if today is enabled
        Dim weekString As String = My.Settings.week.PadLeft(7, "0"c)
        Dim isDayEnabled As Boolean = weekString.Length > currentDayIndex AndAlso weekString(currentDayIndex) = "1"c

        ' Check if within time range
        Dim isInTimeWindow As Boolean = (currentTime >= startTime AndAlso currentTime <= endTime)

        ' Enable or disable the button
        Button1.Enabled = isDayEnabled AndAlso isInTimeWindow
        Button2.Enabled = isDayEnabled AndAlso isInTimeWindow
    End Sub
    Private Async Function CountObjectsInDomainAsync(objectClass As String, username As String, password As String) As Task(Of Integer)
        Return Await Task.Run(Function()
                                  Dim totalCount As Integer = 0

                                  ' Decide what to search
                                  Dim searchRoots As New List(Of String)

                                  If My.Settings.SelectedOUs IsNot Nothing AndAlso My.Settings.SelectedOUs.Count > 0 Then
                                      searchRoots.AddRange(My.Settings.SelectedOUs.Cast(Of String)())
                                  Else
                                      ' Fallback to full domain root
                                      searchRoots.Add("LDAP://DC=" & My.Settings.domain & ",DC=local")
                                  End If

                                  For Each ouPath As String In searchRoots
                                      Try
                                          Dim entry As DirectoryEntry

                                          If Not String.IsNullOrWhiteSpace(username) AndAlso Not String.IsNullOrWhiteSpace(password) AndAlso My.Settings.admin = True Then
                                              Dim domainAndUser As String = My.Settings.domain & "\" & username
                                              entry = New DirectoryEntry(ouPath, domainAndUser, password, AuthenticationTypes.Secure)
                                          Else
                                              entry = New DirectoryEntry(ouPath) ' Use current credentials
                                          End If

                                          Dim searcher As New DirectorySearcher(entry)
                                          searcher.Filter = $"(&(objectCategory={objectClass}))"
                                          searcher.SearchScope = SearchScope.Subtree
                                          searcher.SizeLimit = 0

                                          Dim results As SearchResultCollection = searcher.FindAll()
                                          totalCount += results.Count

                                      Catch ex As System.Runtime.InteropServices.COMException
                                          Debug.WriteLine("LDAP COMException (" & ouPath & "): " & ex.Message)
                                      Catch ex As Exception
                                          Debug.WriteLine("General LDAP error (" & ouPath & "): " & ex.Message)
                                      End Try
                                  Next

                                  Return totalCount
                              End Function)
    End Function

    Private Sub UnlockUserAccount(samAccountName As String, username As String, password As String)
        Try
            Dim ldapPath As String = "LDAP://DC=" & My.Settings.domain & ",DC=local"
            Dim entry As DirectoryEntry

            If Not String.IsNullOrWhiteSpace(username) AndAlso Not String.IsNullOrWhiteSpace(password) Then
                Dim domainAndUser As String = My.Settings.domain & "\" & username
                entry = New DirectoryEntry(ldapPath, domainAndUser, password, AuthenticationTypes.Secure)
            Else
                entry = New DirectoryEntry(ldapPath)
            End If

            Dim searcher As New DirectorySearcher(entry)
            searcher.Filter = $"(&(objectCategory=person)(objectClass=user)(sAMAccountName={samAccountName}))"
            Dim result As SearchResult = searcher.FindOne()

            If result IsNot Nothing Then
                Dim userEntry As DirectoryEntry = result.GetDirectoryEntry()
                userEntry.Properties("lockoutTime").Value = 0
                userEntry.CommitChanges()
            End If

        Catch ex As Exception
            allSucceeded = False
            MessageBox.Show($"Failed to unlock {samAccountName}: {ex.Message}", "Unlock Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetWmiConnectionOptions() As ConnectionOptions
        Dim adminUser As String = DecryptPassword(My.Settings.user)
        Dim adminPass As String = DecryptPassword(My.Settings.pass)
        Dim adminDomain As String = My.Settings.domain

        If Not String.IsNullOrWhiteSpace(adminUser) AndAlso
       Not String.IsNullOrWhiteSpace(adminPass) AndAlso
       Not String.IsNullOrWhiteSpace(adminDomain) Then

            Return New ConnectionOptions() With {
            .Username = $"{adminDomain}\{adminUser}",
            .Password = adminPass,
            .Impersonation = ImpersonationLevel.Impersonate,
            .Authentication = AuthenticationLevel.PacketPrivacy
        }
        End If

        ' Default to current user context
        Return New ConnectionOptions() With {
        .Impersonation = ImpersonationLevel.Impersonate,
        .Authentication = AuthenticationLevel.PacketPrivacy
    }
    End Function

    Private Const HKEY_LOCAL_MACHINE As UInteger = &H80000002UI
    Private Async Function GetLockedUsersViaWMI(serverName As String) As Task(Of List(Of String))
        Const HKEY_LOCAL_MACHINE As UInteger = &H80000002UI

        Try
            ' Run WMI logic in background
            Dim lockedUsers As List(Of String) = Await Task.Run(Function()
                                                                    Dim resultList As New List(Of String)

                                                                    Try
                                                                        ' WMI setup
                                                                        Dim options As ConnectionOptions = GetWmiConnectionOptions()
                                                                        Dim scope As New ManagementScope($"\\{serverName}\root\default", options)
                                                                        scope.Connect()

                                                                        Dim registry As New ManagementClass(scope, New ManagementPath("StdRegProv"), Nothing)
                                                                        Dim inParams As ManagementBaseObject = registry.GetMethodParameters("EnumKey")
                                                                        inParams("hDefKey") = HKEY_LOCAL_MACHINE
                                                                        inParams("sSubKeyName") = "SYSTEM\CurrentControlSet\Services\RemoteAccess\Parameters\AccountLockout"

                                                                        Dim outParams As ManagementBaseObject = registry.InvokeMethod("EnumKey", inParams, Nothing)

                                                                        If outParams IsNot Nothing AndAlso Convert.ToInt32(outParams("ReturnValue")) = 0 Then
                                                                            Dim subKeys As String() = TryCast(outParams("sNames"), String())
                                                                            If subKeys IsNot Nothing Then resultList.AddRange(subKeys)
                                                                        Else
                                                                            Throw New Exception($"Failed to enumerate keys. ReturnValue: {outParams?("ReturnValue")}")
                                                                        End If

                                                                    Catch ex As Exception
                                                                        ' Log error in result list to return it without crashing
                                                                        resultList.Clear()
                                                                        resultList.Add("WMI Error: " & ex.Message)
                                                                    End Try

                                                                    Return resultList
                                                                End Function)

            ' If it's a WMI error, show it in the UI
            If lockedUsers.Count = 1 AndAlso lockedUsers(0).StartsWith("WMI Error:") Then
                If Label13.InvokeRequired Then
                    Label13.Invoke(Sub()
                                       Label13.Text &= $"{serverName.ToUpper}: {lockedUsers(0)}. "
                                       Panel7.Visible = True
                                   End Sub)
                Else
                    Label13.Text &= $"{serverName.ToUpper}: {lockedUsers(0)}. "
                    Panel7.Visible = True
                End If

                Return New List(Of String)()
            End If

            Return lockedUsers

        Catch ex As Exception
            ' You're back on the UI thread here — it's safe to touch controls
            If Label13.InvokeRequired Then
                Label13.Invoke(Sub()
                                   Label13.Text &= $"{serverName.ToUpper}: {ex.Message.Trim}. "
                                   Panel7.Visible = True
                               End Sub)
            Else
                Label13.Text &= $"{serverName.ToUpper}: {ex.Message.Trim}. "
                Panel7.Visible = True
            End If

            Return New List(Of String)()
        End Try
    End Function


    Dim allSucceeded As Boolean = True
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select one or more users to unlock.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim username = DecryptPassword(My.Settings.user)
        Dim password = DecryptPassword(My.Settings.pass)

        For Each selectedUser As String In ListBox1.SelectedItems
            Try
                UnlockUserAccount(selectedUser, username, password)
            Catch ex As Exception
                allSucceeded = False
                MessageBox.Show($"Failed to unlock user '{selectedUser}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next

        If allSucceeded Then
            MessageBox.Show("Selected user(s) have been unlocked.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        allSucceeded = True
        Await CheckLockedOutUsers(username, password)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        For i = 0 To ListBox1.Items.Count - 1
            ListBox1.SetSelected(i, True)
        Next
        LinkLabel9.Show()
        LinkLabel2.Hide()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        For i = 0 To ListBox2.Items.Count - 1
            ListBox2.SetSelected(i, True)
        Next
        LinkLabel10.Show()
        LinkLabel3.Hide()
    End Sub
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function LockWorkStation() As Boolean
    End Function
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Stop()
        AllowSleep()
        ShowTaskbar()
        UninstallKeyHook()
        ClipCursor(IntPtr.Zero)
        If LinkLabel1.Visible = False Then
            LockWorkStation()
        End If
        ' Application.Exit()
    End Sub
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)

        If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.U Then
            ClipCursor(IntPtr.Zero)
            '  MessageBox.Show("Mouse unlocked.", "Developer Unlock", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox2.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select at least one locked user to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Copy selected items to a separate list to avoid modifying collection during iteration
        Dim itemsToDelete As New List(Of String)
        For Each item As String In ListBox2.SelectedItems
            itemsToDelete.Add(item)
        Next

        For Each item In itemsToDelete
            Dim parts = item.Split(New String() {" - "}, StringSplitOptions.None)
            If parts.Length = 2 Then
                Dim dcName = parts(0).Trim
                Dim userKey = parts(1).Trim

                Dim success = DeleteRegistryKeyViaWMI(dcName, userKey)
                If success Then
                    ListBox2.Items.Remove(item)
                End If
            Else
                MessageBox.Show($"Unexpected item format: {item}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Next
    End Sub
    Private Function DeleteRegistryKeyViaWMI(serverName As String, subKeyName As String) As Boolean
        Dim options As ConnectionOptions = GetWmiConnectionOptions()
        Dim scope As New ManagementScope($"\\{serverName}\root\default", options)
        scope.Connect()

        Try
            scope.Connect()

            Dim registry As New ManagementClass(scope, New ManagementPath("StdRegProv"), Nothing)

            Dim inParams As ManagementBaseObject = registry.GetMethodParameters("DeleteKey")
            inParams("hDefKey") = HKEY_LOCAL_MACHINE
            ' Full subkey path to delete
            inParams("sSubKeyName") = "SYSTEM\CurrentControlSet\Services\RemoteAccess\Parameters\AccountLockout\" & subKeyName

            Dim outParams As ManagementBaseObject = registry.InvokeMethod("DeleteKey", inParams, Nothing)

            If outParams IsNot Nothing AndAlso Convert.ToInt32(outParams("ReturnValue")) = 0 Then
                Return True
            Else
                MessageBox.Show($"Failed to delete key '{subKeyName}' on {serverName}. ReturnValue: {outParams("ReturnValue")}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show($"Error deleting key '{subKeyName}' on {serverName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Not settings.ValidateNumericInputLength(TextBox1.Text, 4, 8) Then
            TextBox1.Text = Nothing
            Exit Sub
        End If

        If TextBox1.Text = My.Settings.lockout_pass Then
            UninstallKeyHook()
            Timer3.Stop()
            AllowSleep()
            RemoveTopMost()
            LinkLabel1.Visible = True
            lockOut.Close()
            GroupBox2.Hide()
            GroupBox1.Show()
            ShowTaskbar()
            Activate() ' Bring to front
            ReleaseMouse()
            LinkLabel4.Visible = False
            Me.AcceptButton = Nothing
            LinkLabel1.Visible = True
            ControlBox = True
            MinimizeBox = True
            SetClickThrough(True)
        Else
            MessageBox.Show("Incorrect Password", "LockOut Active", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        TextBox1.Clear()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        'Timer1.Stop()
        GroupBox1.Hide()
        GroupBox2.Show()
        LinkLabel4.Visible = False
        Me.AcceptButton = Button5
        TextBox1.Focus()
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        GroupBox2.Hide()
        GroupBox1.Show()
        LinkLabel4.Visible = True

        Me.AcceptButton = Nothing
        TextBox1.Clear()
    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs)
        Dim newEntries As New List(Of String)

        ' Copy existing items from ListBox1
        For Each item As String In ListBox1.Items
            newEntries.Add(item.ToString)
        Next

        ' Create the new item
        Dim latestItem1 = "New Item " & Date.Now.Ticks.ToString

        ' Add it if it's not already present
        If Not newEntries.Contains(latestItem1) Then
            newEntries.Add(latestItem1)
        End If

        ' Update the listbox if anything new was added
        If newEntries.Count > ListBox1.Items.Count Then
            Dim result = Await RefreshListBoxGeneralAsync(ListBox1, newEntries, knownItems1, firstLoadDone1, "AD")
            knownItems1 = result.UpdatedKnownItems
            firstLoadDone1 = result.UpdatedFirstLoadDone
        End If
    End Sub

    Private redBorderForm As New border()
    Public Async Function RefreshListBoxGeneralAsync(
    listBox As ListBox,
    newItems As List(Of String),
    knownItems As HashSet(Of String),
    firstLoadDone As Boolean,
    itemType As String) As Task(Of (UpdatedKnownItems As HashSet(Of String), UpdatedFirstLoadDone As Boolean))

        Dim localKnownItems = knownItems
        Dim anyNew As Boolean = newItems.Any(Function(i) Not localKnownItems.Contains(i))

        listBox.BeginUpdate()
        listBox.Items.Clear()
        For Each item In newItems
            listBox.Items.Add(item)
        Next
        listBox.EndUpdate()

        Dim updatedKnownItems = New HashSet(Of String)(newItems)
        Dim updatedFirstLoadDone = True

        Dim commaSeparated = String.Join(", ", newItems)

        If anyNew AndAlso firstLoadDone Then
            If My.Settings.border Then
                redBorderForm.StartFlashing(My.Settings.flash_count)
            End If
            If My.Settings.send_email Then
                Await SendEmail(commaSeparated, itemType)
            End If
        End If

        ' If anyNew Then
        Label9.Text = DateTime.Now.ToString("h:mm tt")
        Label10.Text = DateTime.Now.ToString("MMM dd, yyyy")
        ' End If
        Return (updatedKnownItems, updatedFirstLoadDone)
    End Function

    Private Shared hookID As IntPtr = IntPtr.Zero

    Private Delegate Function LowLevelKeyboardProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowsHookEx(ByVal idHook As Integer, ByVal lpfn As LowLevelKeyboardProc,
                                             ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function UnhookWindowsHookEx(ByVal hhk As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer,
                                           ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Private Shared Function GetModuleHandle(lpModuleName As String) As IntPtr
    End Function
    Private Const VK_LWIN As Integer = &H5B
    Private Const VK_RWIN As Integer = &H5C
    Private Const VK_LCONTROL As Integer = &HA2
    Private Const VK_RCONTROL As Integer = &HA3
    Private Const VK_LMENU As Integer = &HA4
    Private Const VK_RMENU As Integer = &HA5
    Private Const WM_KEYDOWN As Integer = &H100
    Private Const WM_SYSKEYDOWN As Integer = &H104
    Private Const WH_KEYBOARD_LL As Integer = 13
    Private Delegate Function EnumWindowsProc(hwnd As IntPtr, lParam As IntPtr) As Boolean

    <DllImport("user32.dll")>
    Private Shared Function EnumWindows(lpEnumFunc As EnumWindowsProc, lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowThreadProcessId(hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function IsWindowVisible(hwnd As IntPtr) As Boolean
    End Function
    Public Sub MinimizeAllOtherWindows()
        Dim currentProcessId As Integer = Process.GetCurrentProcess().Id
        EnumWindows(AddressOf EnumWindowsCallback, CType(currentProcessId, IntPtr))
    End Sub

    Private Function EnumWindowsCallback(hwnd As IntPtr, lParam As IntPtr) As Boolean
        Dim windowProcessId As Integer = 0
        GetWindowThreadProcessId(hwnd, windowProcessId)

        ' If window belongs to another process and is visible
        If windowProcessId <> lParam.ToInt32() AndAlso IsWindowVisible(hwnd) Then
            ' Minimize the window
            ShowWindow(hwnd, SW_MINIMIZE)
        End If

        Return True ' Continue enumerating
    End Function
    Private Const SW_MINIMIZE As Integer = 6
    Private Const SW_RESTORE As Integer = 9
    Private Function HookCallback(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
        Const WM_KEYDOWN As Integer = &H100
        Const WM_SYSKEYDOWN As Integer = &H104
        Const VK_RETURN As Integer = &HD

        If nCode >= 0 AndAlso (wParam.ToInt32() = WM_KEYDOWN OrElse wParam.ToInt32() = WM_SYSKEYDOWN) Then
            Dim vkCode As Integer = Marshal.ReadInt32(lParam)

            ' Allow number keys and Enter
            Dim isTopRowDigit = vkCode >= Keys.D0 AndAlso vkCode <= Keys.D9
            Dim isNumpadDigit = vkCode >= Keys.NumPad0 AndAlso vkCode <= Keys.NumPad9

            If isTopRowDigit OrElse isNumpadDigit OrElse vkCode = VK_RETURN Then
                Return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam) ' Let these keys through
            End If

            ' Block all other keys
            Return CType(1, IntPtr)
        End If

        Return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam)
    End Function
    <DllImport("user32.dll")>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function FindWindowEx(hwndParent As IntPtr, hwndChildAfter As IntPtr, lpszClass As String, lpszWindow As String) As IntPtr
    End Function
    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOW As Integer = 5

    Public Sub HideTaskbar()
        ' Hide primary taskbar
        Dim taskbarWnd As IntPtr = FindWindow("Shell_TrayWnd", Nothing)
        If taskbarWnd <> IntPtr.Zero Then
            ShowWindow(taskbarWnd, SW_HIDE)
        End If

        ' Hide secondary taskbars (multi-monitor)
        Dim secondaryTaskbar As IntPtr = IntPtr.Zero
        Do
            secondaryTaskbar = FindWindowEx(IntPtr.Zero, secondaryTaskbar, "Shell_SecondaryTrayWnd", Nothing)
            If secondaryTaskbar <> IntPtr.Zero Then
                ShowWindow(secondaryTaskbar, SW_HIDE)
            End If
        Loop While secondaryTaskbar <> IntPtr.Zero
    End Sub

    Public Sub ShowTaskbar()
        ' Show primary taskbar
        Dim taskbarWnd As IntPtr = FindWindow("Shell_TrayWnd", Nothing)
        If taskbarWnd <> IntPtr.Zero Then
            ShowWindow(taskbarWnd, SW_SHOW)
        End If

        ' Show secondary taskbars (multi-monitor)
        Dim secondaryTaskbar As IntPtr = IntPtr.Zero
        Do
            secondaryTaskbar = FindWindowEx(IntPtr.Zero, secondaryTaskbar, "Shell_SecondaryTrayWnd", Nothing)
            If secondaryTaskbar <> IntPtr.Zero Then
                ShowWindow(secondaryTaskbar, SW_SHOW)
                Threading.Thread.Sleep(10) ' small delay to help UI refresh
            End If
        Loop While secondaryTaskbar <> IntPtr.Zero
    End Sub


    Private hookDelegate As LowLevelKeyboardProc = AddressOf HookCallback
    Public Sub InstallKeyHook()
        If hookID = IntPtr.Zero Then
            hookID = SetWindowsHookEx(WH_KEYBOARD_LL, hookDelegate, IntPtr.Zero, 0)
            If hookID = IntPtr.Zero Then
                Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
            End If
        End If
    End Sub

    Public Sub UninstallKeyHook()
        If hookID <> IntPtr.Zero Then
            Dim result As Boolean = UnhookWindowsHookEx(hookID)
            If Not result Then
                Dim err = Marshal.GetLastWin32Error()
                ' Handle error (optional logging)
            End If
            hookID = IntPtr.Zero
        End If
    End Sub

    Private Sub KeepTopMostWithoutFocus()
        Dim hwnd = Me.Handle
        SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOACTIVATE)
    End Sub
    Private Sub RemoveTopMost()
        Dim hwnd = Me.Handle
        SetWindowPos(hwnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOACTIVATE)
    End Sub

    <DllImport("user32.dll")>
    Private Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr,
                                         X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As Boolean
    End Function

    Private Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)
    Private Shared ReadOnly HWND_NOTOPMOST As New IntPtr(-2)
    Private Const SWP_NOMOVE As UInteger = &H2
    Private Const SWP_NOSIZE As UInteger = &H1
    Private Const SWP_NOACTIVATE As UInteger = &H10
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function

    Private Const GWL_EXSTYLE As Integer = -20
    Private Const WS_EX_TRANSPARENT As Integer = &H20
    Private Const WS_EX_LAYERED As Integer = &H80000

    Public Sub SetClickThrough(enabled As Boolean)
        Dim exStyle As Integer = GetWindowLong(Me.Handle, GWL_EXSTYLE)

        If enabled Then
            exStyle = exStyle Or WS_EX_TRANSPARENT
        Else
            exStyle = exStyle And Not WS_EX_TRANSPARENT
        End If

        SetWindowLong(Me.Handle, GWL_EXSTYLE, exStyle)
    End Sub
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function SetThreadExecutionState(esFlags As EXECUTION_STATE) As EXECUTION_STATE
    End Function

    <Flags>
    Private Enum EXECUTION_STATE As UInteger
        ES_CONTINUOUS = &H80000000UI
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
    End Enum
    Public Sub PreventSleep()
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or
                            EXECUTION_STATE.ES_SYSTEM_REQUIRED Or
                            EXECUTION_STATE.ES_DISPLAY_REQUIRED)
    End Sub
    Public Sub AllowSleep()
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
    End Sub
    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        If ListBox1.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select one or more users to unlock.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        For Each selectedItem As String In ListBox1.SelectedItems
            If Not My.Settings.ignored_ad.Contains(selectedItem) Then
                My.Settings.ignored_ad.Add(selectedItem)
                settings.ListBox1.Items.Add(selectedItem)
            End If
        Next

        ' Save changes
        My.Settings.Save()
        Dim itemsToRemove As New List(Of Object)

        For Each item In ListBox1.SelectedItems
            itemsToRemove.Add(item)
        Next

        ' Now remove each from the ListBox
        For Each item In itemsToRemove
            ListBox1.Items.Remove(item)
        Next

        MessageBox.Show("Selected items saved to hidden list.", "Item Hidden", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub LinkLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        If ListBox2.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select one or more users to unlock.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        For Each selectedItem As String In ListBox2.SelectedItems
            If Not My.Settings.ignored_nps.Contains(selectedItem) Then
                My.Settings.ignored_nps.Add(selectedItem)
                settings.ListBox2.Items.Add(selectedItem)
            End If
        Next

        ' Save changes
        My.Settings.Save()
        Dim itemsToRemove As New List(Of Object)

        For Each item In ListBox2.SelectedItems
            itemsToRemove.Add(item)
        Next

        ' Now remove each from the ListBox
        For Each item In itemsToRemove
            ListBox2.Items.Remove(item)
        Next
        MessageBox.Show("Selected items saved to hidden list.", "Item Hidden", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Async Function SendEmail(username As String, type As String) As Task
        Label13.Text = ""
        Try
            Using smtp As New SmtpClient(My.Settings.smtp_host, My.Settings.smtp_port) With {
            .EnableSsl = My.Settings.tls,
            .UseDefaultCredentials = False,
            .Credentials = New NetworkCredential(DecryptPassword(My.Settings.smtp_username), DecryptPassword(My.Settings.smtp_password))
        }
                Using mail As New MailMessage With {
                .From = New MailAddress(My.Settings.smtp_email),
                .Subject = Constants.SmtpSubject,
                .Body = String.Format(Constants.SmtpBodyTemplate, username, type.ToUpper()),
                .IsBodyHtml = True
            }
                    mail.To.Add(My.Settings.to_email)
                    Await smtp.SendMailAsync(mail)
                End Using
            End Using
        Catch ex As Exception
            Label13.Text = "Error sending email: " & ex.Message
            Panel7.Visible = True
        End Try
    End Function
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
    Private Sub LinkLabel9_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        For i = 0 To ListBox1.Items.Count - 1
            ListBox1.SetSelected(i, False)
        Next
        LinkLabel2.Show()
        LinkLabel9.Hide()
    End Sub

    Private Sub LinkLabel10_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        For i = 0 To ListBox2.Items.Count - 1
            ListBox2.SetSelected(i, False)
        Next
        LinkLabel3.Show()
        LinkLabel10.Hide()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If LinkLabel4.Visible = True Then
            LockMouseToForm()
            InstallKeyHook()
        End If
    End Sub

    Public Function EncryptPassword(plainText As String) As String
        Try
            Dim data As Byte() = Encoding.UTF8.GetBytes(plainText)
            Dim encrypted As Byte() = ProtectedData.Protect(data, Nothing, DataProtectionScope.CurrentUser)
            Return Convert.ToBase64String(encrypted)
        Catch ex As Exception
            MessageBox.Show("Error encrypting user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return ""
        End Try
    End Function

    Public Function DecryptPassword(encryptedBase64 As String) As String
        If String.IsNullOrWhiteSpace(encryptedBase64) Then Return ""
        Try
            Dim encrypted As Byte() = Convert.FromBase64String(encryptedBase64)
            Dim decrypted As Byte() = ProtectedData.Unprotect(encrypted, Nothing, DataProtectionScope.CurrentUser)
            Return Encoding.UTF8.GetString(decrypted)
        Catch ex As Exception
            MessageBox.Show("Error decrypting user data. Settings will be reset.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            My.Settings.Reset()
            My.Settings.Save()
            Return ""
        End Try
    End Function
    Private Sub LinkLabel11_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel11.LinkClicked
        Panel7.Hide()
        Label13.Text = ""
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Panel7.Hide()
        Label13.Text = ""
    End Sub

    Private Sub Panel7_Click(sender As Object, e As EventArgs) Handles Panel7.Click
        Panel7.Hide()
        Label13.Text = ""
    End Sub


    Private rnd As New Random()
    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        RoundPanelCorners(Panel1, 20) ' Panel1 with 20px radius
        RoundPanelCorners(Panel2, 20)
        RoundPanelCorners(Panel3, 20)
        RoundPanelCorners(Panel4, 20)
        RoundPanelCorners(Panel5, 20)

        quoteIndex = Rnd.Next(quotes.Length)
        Label3.Text = """" & quotes(quoteIndex) & """"

        CheckDNSResolution()

        Timer1.Interval = My.Settings.refresh * 1000

        If My.Settings.domain <> "" AndAlso My.Settings.dc <> "" Then
            ' No message box now — just run with or without credentials
            Await CheckLockedOutUsers(DecryptPassword(My.Settings.user), DecryptPassword(My.Settings.pass))

            Dim allLockedOutUsers As New List(Of String)
            Dim dcList As String() = My.Settings.dc.Split(","c)
            Label13.Text = ""
            For Each dc As String In dcList
                Dim trimmedDC = dc.Trim()
                Dim users As List(Of String) = Await GetLockedUsersViaWMI(trimmedDC)
                If users IsNot Nothing Then
                    For Each user In users
                        Dim entry = $"{trimmedDC} - {user}"

                        If My.Settings.ignored_nps.Contains(entry) Then
                            'item exists
                            Continue For
                        Else
                            allLockedOutUsers.Add(entry)
                        End If
                    Next
                End If
            Next

            Dim result = Await RefreshListBoxGeneralAsync(ListBox2, allLockedOutUsers, knownItems2, firstLoadDone2, "NPS")
            knownItems2 = result.UpdatedKnownItems
            firstLoadDone2 = result.UpdatedFirstLoadDone
            Label2.Text = ListBox2.Items.Count & " Locked Out NPS User(s) Found:"

            Timer1.Start()
            CheckAccessWindow()
        Else
            MessageBox.Show("Please complete the required fields within settings.", "Complete setup", MessageBoxButtons.OK, MessageBoxIcon.Information)
            settings.Show()
            settings.Button9.Visible = False
            My.Settings.advanced = False
            'settings.GroupBox3.BackColor = Color.MistyRose
            '  settings.GroupBox6.BackColor = Color.MistyRose
        End If
    End Sub
    Private quoteIndex As Integer = 0
    Private quotes As String() = {
    "Too many tries, you're locked.",
    "Locked out? Try remembering better.",
    "Failed logins have consequences.",
    "Passwords aren’t guesses.",
    "Locked accounts save networks.",
    "Authentication isn't a guessing game.",
    "Lockouts protect your perimeter.",
    "Access denied. For a reason.",
    "Brute force meets the wall.",
    "Security sleeps with locked doors.",
    "Your password, your responsibility.",
    "Mistyped again? Timeout time.",
    "Bad actors love weak logins.",
    "Guess less. Secure more.",
    "Too many fails, try tomorrow.",
    "Passwords are hard, apparently!"
}
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Label3.Text = """" & quotes(quoteIndex) & """"

        ' Move to the next quote, loop back to start if needed
        quoteIndex = (quoteIndex + 1) Mod quotes.Length
    End Sub

    Private Async Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Label12.Text = "..."
        Timer1.Stop()
        If My.Settings.domain <> "" AndAlso My.Settings.dc <> "" Then
            ' No message box now — just run with or without credentials
            Await CheckLockedOutUsers(DecryptPassword(My.Settings.user), DecryptPassword(My.Settings.pass))

            Dim allLockedOutUsers As New List(Of String)
            Dim dcList As String() = My.Settings.dc.Split(","c)
            Label13.Text = ""
            For Each dc As String In dcList
                Dim trimmedDC = dc.Trim()
                Dim users As List(Of String) = Await GetLockedUsersViaWMI(trimmedDC)
                If users IsNot Nothing Then
                    For Each user In users
                        Dim entry = $"{trimmedDC} - {user}"

                        If My.Settings.ignored_nps.Contains(entry) Then
                            'item exists
                            Continue For
                        Else
                            allLockedOutUsers.Add(entry)
                        End If
                    Next
                End If
            Next

            Dim result = Await RefreshListBoxGeneralAsync(ListBox2, allLockedOutUsers, knownItems2, firstLoadDone2, "NPS")
            knownItems2 = result.UpdatedKnownItems
            firstLoadDone2 = result.UpdatedFirstLoadDone
            Label2.Text = ListBox2.Items.Count & " Locked Out NPS User(s) Found:"

            Timer1.Start()
            CheckAccessWindow()
        Else
            MessageBox.Show("Please complete the required fields within settings.", "Complete setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
            settings.Show()
            settings.Button9.Visible = False
        End If
        Label12.Text = "⟳"
        Timer1.Start()
    End Sub



    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        TextBox1.SelectAll()
    End Sub
End Class
