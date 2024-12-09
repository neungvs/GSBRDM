Imports System.DirectoryServices
Imports System.Configuration
Imports GSBWeb.DAL
Imports Arsoft.Utility

Public Class ActiveDirectoryManagement

#Region "Attributes"
    Private _aduser As ActiveDirectoryEntity
#End Region

#Region "Methods"

    Public Function CheckAuthen(_username As String, _password As String) As Boolean
        Dim _bl As Boolean = False
        Try
            Dim _domainname As String = ConfigurationManager.AppSettings("LDAPDirectory")
            Dim _entry As New DirectoryEntry(_domainname, _username, _password)
            'Dim _objAdSearcher As New DirectorySearcher(_entry)
            'Dim _obj As Object = _entry.NativeObject
            Dim _search As New DirectorySearcher(_entry)
            Dim _result As SearchResult
            'Dim _ou() As String

            With _search
                .Filter = "(SAMAccountName=" & _username & ")"
                .PropertiesToLoad.Add("cn")
                _result = .FindOne
            End With

            If Not IsNothing(_result) Then
                _bl = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen()", ex.Message)
            'Return "Error authenticating user. " & ex.Message
        End Try
        Return _bl


    End Function
    Public Function IsAuthen(_username As String, _password As String) As String
        Try
            Dim _domainname As String = ConfigurationManager.AppSettings("LDAPDirectory")
            Dim _entry As New DirectoryEntry(_domainname, _username, _password)
            'Dim _objAdSearcher As New DirectorySearcher(_entry)
            'Dim _obj As Object = _entry.NativeObject
            Dim _search As New DirectorySearcher(_entry)
            Dim _result As SearchResult
            Dim _ou() As String

            With _search
                .Filter = "(SAMAccountName=" & _username & ")"
                .PropertiesToLoad.Add("cn")
                _result = .FindOne
            End With

            If IsNothing(_result) Then
                Return "false"
            Else
                _aduser = New ActiveDirectoryEntity
                With _aduser
                    .EmployeeID = _result.GetDirectoryEntry.Properties("employeeID").Item(0).ToString
                    .NameEn = _result.GetDirectoryEntry.Properties("givenname").Item(0).ToString
                    .SurnameEn = _result.GetDirectoryEntry.Properties("sn").Item(0).ToString
                    .NameLo = _result.GetDirectoryEntry.Properties("description").Item(0).ToString
                    .Title = _result.GetDirectoryEntry.Properties("title").Item(0).ToString
                    .Company = _result.GetDirectoryEntry.Properties("company").Item(0).ToString
                    .Pager = _result.GetDirectoryEntry.Properties("pager").Item(0).ToString
                    .DeptCode = Left(Right(.Company, 5), 4)
                    '.DeptCode = _result.GetDirectoryEntry.Properties("postalCode").Item(0).ToString

                    _ou = _result.GetDirectoryEntry.Properties("distinguishedName").Item(0).ToString.Split(",")
                    If _ou.Length > 3 Then
                        .OrganizationUnit = _ou(2).ToString.Replace("OU=", "")
                        .MemberOf = _ou(1).ToString.Replace("OU=", "")
                    End If

                    '.SAMAccountName = _result.GetDirectoryEntry.Properties("SAMAccountName").Item(0).ToString
                    '.Mail = _result.GetDirectoryEntry.Properties("mail").Item(0).ToString
                    '.UserPrincipalName = _result.GetDirectoryEntry.Properties("userPrincipalName").Item(0).ToString

                    '/############################/
                    'String allPath = metaData.getAttributes().toString();
                    'String str1 = allPath.substring(allPath.indexOf("distinguishedName"), (allPath.indexOf("distinguishedName") + 60));
                    'int point1 = str1.lastIndexOf("OU"); 
                    'String str2 = str1.substring(point1,point1+5);
                    'String ou = str2.substring(3,5);
                    'ADData.setOu(ou);

                    'For Each p In _result.GetDirectoryEntry.Properties.PropertyNames
                    '    UtilLogfile.writeToLog("ActiveDirectoryManagement", p.ToString & ":", _result.GetDirectoryEntry.Properties(p.ToString).Item(0).ToString)
                    'Next

                End With

            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen()", ex.Message)
            Return "Error authenticating user. " & ex.Message
        End Try
        Return "true"


    End Function

    Public Function IsAuthen(_username As String, _password As String, _ldapdirectory As String) As String

        Try
            Dim _domainname As String = _ldapdirectory
            Dim _entry As New DirectoryEntry(_domainname, _username, _password)
            'Dim _objAdSearcher As New DirectorySearcher(_entry)
            'Dim _obj As Object = _entry.NativeObject
            Dim _search As New DirectorySearcher(_entry)
            Dim _result As SearchResult
            Dim _ou() As String

            Try
                With _search
                    .Filter = "(SAMAccountName=" & _username & ")"
                    .PropertiesToLoad.Add("cn")
                    _result = .FindOne
                End With

                If IsNothing(_result) Then
                    Return "false"
                Else
                    _aduser = New ActiveDirectoryEntity
                    With _aduser
                        .EmployeeID = _result.GetDirectoryEntry.Properties("employeeID").Item(0).ToString
                        .NameEn = _result.GetDirectoryEntry.Properties("givenname").Item(0).ToString
                        .SurnameEn = _result.GetDirectoryEntry.Properties("sn").Item(0).ToString
                        .NameLo = _result.GetDirectoryEntry.Properties("description").Item(0).ToString
                        .Title = _result.GetDirectoryEntry.Properties("title").Item(0).ToString
                        .Company = _result.GetDirectoryEntry.Properties("company").Item(0).ToString
                        .Pager = _result.GetDirectoryEntry.Properties("pager").Item(0).ToString
                        .DeptCode = Left(Right(.Company, 5), 4)
                        '.DeptCode = _result.GetDirectoryEntry.Properties("postalCode").Item(0).ToString

                        _ou = _result.GetDirectoryEntry.Properties("distinguishedName").Item(0).ToString.Split(",")
                        If _ou.Length > 3 Then
                            .OrganizationUnit = _ou(2).ToString.Replace("OU=", "")
                            .MemberOf = _ou(1).ToString.Replace("OU=", "")
                        End If

                        '.SAMAccountName = _result.GetDirectoryEntry.Properties("SAMAccountName").Item(0).ToString
                        '.Mail = _result.GetDirectoryEntry.Properties("mail").Item(0).ToString
                        '.UserPrincipalName = _result.GetDirectoryEntry.Properties("userPrincipalName").Item(0).ToString

                        '/############################/
                        'String allPath = metaData.getAttributes().toString();
                        'String str1 = allPath.substring(allPath.indexOf("distinguishedName"), (allPath.indexOf("distinguishedName") + 60));
                        'int point1 = str1.lastIndexOf("OU"); 
                        'String str2 = str1.substring(point1,point1+5);
                        'String ou = str2.substring(3,5);
                        'ADData.setOu(ou);

                        'For Each p In _result.GetDirectoryEntry.Properties.PropertyNames
                        '    UtilLogfile.writeToLog("ActiveDirectoryManagement", p.ToString & ":", _result.GetDirectoryEntry.Properties(p.ToString).Item(0).ToString)
                        'Next

                    End With
                End If

            Catch ex As Exception
                UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(1)", ex.Message)
                Return "Error authenticating user. " & ex.Message
            End Try
        Catch ex As Exception
            UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(2)", ex.Message)
            Return "Error authenticating user. " & ex.Message
        End Try
        Return "true"
    End Function

    Public Function CheckGroup(_username As String, _password As String, _group As String) As Boolean
        Dim _domainname As String = ConfigurationManager.AppSettings("LDAPDirectory")
        Dim _memberof As String
        Dim _groupList() As String
        Dim _proCount As Integer
        Dim _objRootEntry As New DirectoryEntry(_domainname, _username, _password)
        Dim _objADSearcher As New DirectorySearcher(_objRootEntry)
        _objADSearcher.Filter = "(&(objectClass=user)(SAMAccountName=" & _username & "))"
        Dim _oResult As SearchResult
        _oResult = _objADSearcher.FindOne
        _proCount = _oResult.GetDirectoryEntry.Properties("memberof").Count
        For i = 0 To _proCount
            _memberof = _oResult.GetDirectoryEntry.Properties("memberof")(i).ToString
            _groupList = _memberof.Split(",")
            If _groupList(0).Substring(3).Equals(_group) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function CheckGroup(_username As String, _password As String, _group As String, _ldapdirectory As String) As Boolean
        Dim _domainname As String = _ldapdirectory
        Dim _memberof As String
        Dim _groupList() As String
        Dim _proCount As Integer
        Dim _objRootEntry As New DirectoryEntry(_domainname, _username, _password)
        Dim _objADSearcher As New DirectorySearcher(_objRootEntry)
        _objADSearcher.Filter = "(&(objectClass=user)(SAMAccountName=" & _username & "))"
        Dim _oResult As SearchResult
        _oResult = _objADSearcher.FindOne
        _proCount = _oResult.GetDirectoryEntry.Properties("memberof").Count
        For i = 0 To _proCount
            _memberof = _oResult.GetDirectoryEntry.Properties("memberof")(i).ToString
            _groupList = _memberof.Split(",")
            If _groupList(0).Substring(3).Equals(_group) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function GetUserId(_username As String, _password As String, _ldapdirectory As String) As String

        Try
            Dim _domainname As String = _ldapdirectory
            Dim _entry As New DirectoryEntry(_domainname, _username, _password)
            'Dim _objAdSearcher As New DirectorySearcher(_entry)
            'Dim _obj As Object = _entry.NativeObject
            Dim _search As New DirectorySearcher(_entry)
            Dim _result As SearchResult
            Dim _ou() As String

            Try
                With _search
                    .Filter = "(SAMAccountName=" & _username & ")"
                    .PropertiesToLoad.Add("cn")
                    _result = .FindOne
                End With

                If IsNothing(_result) Then
                    Return "false"
                Else
                    _aduser = New ActiveDirectoryEntity
                    With _aduser
                        .EmployeeID = "" '_result.GetDirectoryEntry.Properties("employeeID").Item(0).ToString
                        Try
                            .EmployeeID = _result.GetDirectoryEntry.Properties("employeeID").Item(0).ToString
                        Catch

                        End Try
                        .NameEn = _result.GetDirectoryEntry.Properties("givenname").Item(0).ToString
                        '.DeptCode = _result.GetDirectoryEntry.Properties("postalCode").Item(0).ToString
                        _ou = _result.GetDirectoryEntry.Properties("distinguishedName").Item(0).ToString.Split(",")
                        If _ou.Length > 3 Then
                            .OrganizationUnit = _ou(2).ToString.Replace("OU=", "")
                            .MemberOf = _ou(1).ToString.Replace("OU=", "")
                        End If

                        '.SAMAccountName = _result.GetDirectoryEntry.Properties("SAMAccountName").Item(0).ToString
                        '.Mail = _result.GetDirectoryEntry.Properties("mail").Item(0).ToString
                        '.UserPrincipalName = _result.GetDirectoryEntry.Properties("userPrincipalName").Item(0).ToString

                        '/############################/
                        'String allPath = metaData.getAttributes().toString();
                        'String str1 = allPath.substring(allPath.indexOf("distinguishedName"), (allPath.indexOf("distinguishedName") + 60));
                        'int point1 = str1.lastIndexOf("OU"); 
                        'String str2 = str1.substring(point1,point1+5);
                        'String ou = str2.substring(3,5);
                        'ADData.setOu(ou);

                        'For Each p In _result.GetDirectoryEntry.Properties.PropertyNames
                        '    UtilLogfile.writeToLog("ActiveDirectoryManagement", p.ToString & ":", _result.GetDirectoryEntry.Properties(p.ToString).Item(0).ToString)
                        'Next

                    End With
                End If
            Catch ex As Exception
                UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(1)", ex.Message)
                Return "Error authenticating user. " & ex.Message
            End Try
        Catch ex As Exception
            UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(2)", ex.Message)
            Return "Error authenticating user. " & ex.Message
        End Try
        Return "true"
    End Function


    Public Function GetUserADByEmployeeID(_employeeid As String, _username As String, _password As String, _ldapdirectory As String) As ActiveDirectoryEntity

        Try
            Dim _domainname As String = _ldapdirectory
            Dim _entry As New DirectoryEntry(_domainname, _username, _password)
            'Dim _objAdSearcher As New DirectorySearcher(_entry)
            'Dim _obj As Object = _entry.NativeObject
            Dim _search As New DirectorySearcher(_entry)
            Dim _result As SearchResult
            Dim _results As SearchResultCollection
            Dim _ou() As String

            Try
                With _search
                    .Filter = "(employeeID=" & _employeeid & ")"
                    .PropertiesToLoad.Add("cn")
                    _result = .FindOne
                End With

                If IsNothing(_result) Then
                    _aduser = New ActiveDirectoryEntity
                    _aduser.Msg = "false"
                    Return _aduser
                Else
                    _aduser = New ActiveDirectoryEntity
                    With _aduser
                        .EmployeeID = "" '_result.GetDirectoryEntry.Properties("employeeID").Item(0).ToString
                        Try
                            .EmployeeID = _result.GetDirectoryEntry.Properties("employeeID").Item(0).ToString
                            .NameEn = _result.GetDirectoryEntry.Properties("givenname").Item(0).ToString
                            .SurnameEn = _result.GetDirectoryEntry.Properties("sn").Item(0).ToString
                            Dim namesprit() As String
                            namesprit = _result.GetDirectoryEntry.Properties("description").Item(0).ToString.Split(" ")
                            .NameLo = namesprit(0)
                            .SurnameLo = namesprit(1)
                            .DisplayName = _result.GetDirectoryEntry.Properties("cn").Item(0).ToString
                        Catch

                        End Try
                        '.NameEn = _result.GetDirectoryEntry.Properties("givenname").Item(0).ToString
                        '.DeptCode = _result.GetDirectoryEntry.Properties("postalCode").Item(0).ToString
                        _ou = _result.GetDirectoryEntry.Properties("distinguishedName").Item(0).ToString.Split(",")
                        If _ou.Length > 3 Then
                            .OrganizationUnit = _ou(2).ToString.Replace("OU=", "")
                            .MemberOf = _ou(1).ToString.Replace("OU=", "")
                        End If

                        '.SAMAccountName = _result.GetDirectoryEntry.Properties("SAMAccountName").Item(0).ToString
                        '.Mail = _result.GetDirectoryEntry.Properties("mail").Item(0).ToString
                        '.UserPrincipalName = _result.GetDirectoryEntry.Properties("userPrincipalName").Item(0).ToString

                        '/############################/
                        'String allPath = metaData.getAttributes().toString();
                        'String str1 = allPath.substring(allPath.indexOf("distinguishedName"), (allPath.indexOf("distinguishedName") + 60));
                        'int point1 = str1.lastIndexOf("OU"); 
                        'String str2 = str1.substring(point1,point1+5);
                        'String ou = str2.substring(3,5);
                        'ADData.setOu(ou);

                        'For Each p In _result.GetDirectoryEntry.Properties.PropertyNames
                        '    UtilLogfile.writeToLog("ActiveDirectoryManagement", p.ToString & ":", _result.GetDirectoryEntry.Properties(p.ToString).Item(0).ToString)
                        'Next

                    End With
                End If
            Catch ex As Exception
                UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(1)", ex.Message)
                _aduser.Msg = "Error authenticating user. " & ex.Message
                Return _aduser
            End Try
        Catch ex As Exception
                UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(2)", ex.Message)
                _aduser.Msg = "Error authenticating user. " & ex.Message
            Return _aduser
        End Try
        _aduser.Msg = "true"
                Return _aduser
    End Function

    Public Function GetUserFromAD(ByVal _username As String, ByVal _password As String, ByVal _ldapdirectory As String, ByVal _employeeid As Integer) As UserEntity
        Dim _return As New UserEntity
        Try
            Dim _domainname As String = _ldapdirectory
            Dim _entry As New DirectoryEntry(_domainname, _username, _password)
            Dim _search As New DirectorySearcher(_entry)
            Dim _result As SearchResult
            Dim _ou() As String
            Try

            Catch ex As Exception
                UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(1)", ex.Message)
                Return New UserEntity
            End Try
        Catch ex As Exception
            UtilLogfile.writeToLog("ActiveDirectoryManagement", "IsAuthen(2)", ex.Message)
            Return New UserEntity
        End Try
        Return _return
    End Function
    Public ReadOnly Property UserLoginInfo As ActiveDirectoryEntity
        Get
            Return _aduser
        End Get
    End Property
#End Region

End Class
