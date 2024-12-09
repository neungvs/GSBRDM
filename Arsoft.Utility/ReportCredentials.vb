Public Class UtilReportCredentials : 

    'Implements Microsoft.Reporting.WebForms.IReportServerCredentials

    'Private _userName As String, _password As String, _domain As String

    'Public Sub New(userName As String, password As String, domain As String)
    '    _userName = userName
    '    _password = password
    '    _domain = domain
    'End Sub

    'Public ReadOnly Property ImpersonationUser() As System.Security.Principal.WindowsIdentity
    '    Get
    '        Return Nothing
    '    End Get
    'End Property

    'Public ReadOnly Property NetworkCredentials() As System.Net.ICredentials
    '    Get
    '        Return New System.Net.NetworkCredential(_userName, _password, _domain)
    '    End Get
    'End Property

    'Public Function GetFormsCredentials(ByRef authCoki As System.Net.Cookie, ByRef userName As String, ByRef password As String, ByRef authority As String) As Boolean
    '    userName = _userName
    '    password = _password
    '    authority = _domain
    '    authCoki = New System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain")
    '    Return True
    'End Function

End Class
