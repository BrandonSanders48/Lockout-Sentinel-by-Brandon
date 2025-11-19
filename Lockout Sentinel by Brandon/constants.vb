Public Module Constants
    Public Const SmtpSubject As String = "Lockout Sentinel by Brandon"
    Public Const SmtpBodyTemplate As String = "<h3 style='color:#FFA500'>New alert from Lockout Sentinel by Brandon</h3><div style='width:400px'><p>An application designed to quickly monitor and manage locked-out Active Directory and Network Policy Server (NPS) users, with real-time alerts and user-friendly controls.</p></div><br><br><div style='background:#FFCCCB;color:maroon;padding:20px;border-radius:10px;width:100%;text-align:center'>The following users: <b>[{0}]</b> have been locked out of {1}.</div><br><br><br><br><div style='float:right;text-align:right;'>"
    Public Const LdapPathTemplate As String = "LDAP://DC={0},DC=local"
    Public Const LdapOUPathTemplate As String = "LDAP://OU=SHEF Users,DC={0},DC=local"
    Public Const RegistryLockoutPath As String = "SYSTEM\CurrentControlSet\Services\RemoteAccess\Parameters\AccountLockout"
End Module
