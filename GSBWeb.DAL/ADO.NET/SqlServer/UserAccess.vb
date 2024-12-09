Public Class UserAccess

#Region "Attributes"
    Dim _userdata As UserEntity
#End Region

#Region "Methods"

    Public Function IsPrivilage(ByVal _employeeid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _user As Integer = 0
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "select count(*) FROM GSBBRC_User WHERE employeeid='" & _employeeid & "'"
            _user = _dbaccess.ExecuteScalar(_sql)
            If _user > 0 Then
                _result = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "IsPrivilage()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Login(_username As String, _password As String, Optional _userflag As String = "") As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _crypt As New CryptographyControl

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_User "
            _sql = _sql & "WHERE disabled=0 AND username=@username AND password=@password"

            _param(0) = New SQLServerDBParameter("@username", _username)
            _param(1) = New SQLServerDBParameter("@password", _crypt.EncryptedMD5(_password))

            If _userflag <> "" Then
                _sql = _sql & " AND userflag=@userflag"
                _param(2) = New SQLServerDBParameter("@userflag", _userflag)
            End If

            _dbaccess.ExecuteReader(_sql, _param)

            If _dbaccess.Read Then
                _userdata = New UserEntity
                With _userdata
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .EmployeeID = _dbaccess.GetItem("employeeid")
                    .FirstnameEn = _dbaccess.GetItem("firstnameen")
                    .LastnameEn = _dbaccess.GetItem("lastnameen")
                    .FirstnameTh = _dbaccess.GetItem("firstnameth")
                    '.LastnameTh = _dbaccess.GetItem("lastnameth")
                    .PositionID = _dbaccess.GetItem("positionid")
                    .CompanyID = _dbaccess.GetItem("companyid")
                    .OU = _dbaccess.GetItem("ou")
                    .DeptID = _dbaccess.GetItem("deptid")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .Disabled = _dbaccess.GetItem("disabled")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
                _result = True
            End If
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Login()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public ReadOnly Property GetUserLogin As UserEntity
        Get
            Return _userdata
        End Get
    End Property

    Public Function GetInfo(_userid As String) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_User WHERE userid=" & _userid
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .EmployeeID = _dbaccess.GetItem("employeeid")
                    .FirstnameEn = _dbaccess.GetItem("firstnameen")
                    .LastnameEn = _dbaccess.GetItem("lastnameen")
                    .FirstnameTh = _dbaccess.GetItem("firstnameth")
                    '.LastnameTh = _dbaccess.GetItem("lastnameth")
                    .PositionID = _dbaccess.GetItem("positionid")
                    .CompanyID = _dbaccess.GetItem("companyid")
                    .OU = _dbaccess.GetItem("ou")
                    .DeptID = _dbaccess.GetItem("deptid")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .Disabled = _dbaccess.GetItem("disabled")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfo(_username As String, _employeeid As String) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_User WHERE "
            If _username.Trim.Length > 0 Then
                _sql = _sql & " UserName = '" & _username & "'"
                If _employeeid.Trim.Length > 0 Then
                    _sql = _sql & " OR EmployeeID = '" & _employeeid & "'"
                End If
            ElseIf _employeeid.Trim.Length > 0 Then
                _sql = _sql & " EmployeeID = '" & _employeeid & "'"
            End If

            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .EmployeeID = _dbaccess.GetItem("employeeid")
                    .FirstnameEn = _dbaccess.GetItem("firstnameen")
                    .LastnameEn = _dbaccess.GetItem("lastnameen")
                    .FirstnameTh = _dbaccess.GetItem("firstnameth")
                    '.LastnameTh = _dbaccess.GetItem("lastnameth")
                    .PositionID = _dbaccess.GetItem("positionid")
                    .CompanyID = _dbaccess.GetItem("companyid")
                    .OU = _dbaccess.GetItem("ou")
                    .DeptID = _dbaccess.GetItem("deptid")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .Disabled = _dbaccess.GetItem("disabled")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As UserLists
        Dim _result As New UserLists
        Dim _status As UserEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_User ORDER BY userid"
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _status = New UserEntity
                With _status
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .EmployeeID = _dbaccess.GetItem("employeeid")
                    .FirstnameEn = _dbaccess.GetItem("firstnameen")
                    .LastnameEn = _dbaccess.GetItem("lastnameen")
                    .FirstnameTh = _dbaccess.GetItem("firstnameth")
                    '.LastnameTh = _dbaccess.GetItem("lastnameth")
                    .PositionID = _dbaccess.GetItem("positionid")
                    .CompanyID = _dbaccess.GetItem("companyid")
                    .OU = _dbaccess.GetItem("ou")
                    .DeptID = _dbaccess.GetItem("deptid")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .Disabled = _dbaccess.GetItem("disabled")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(12) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _crypt As New CryptographyControl

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess
        'userid,username,password,employeeid,firstnameen,lastnameen,firstnameth,lastnameth,groupid,disabled,createddate,modifieddate

        Try
            _sql = "INSERT INTO GSBBRC_User(username,password,employeeid,firstnameen,lastnameen,firstnameth,positionid,companyid,ou,deptid,groupid,userflag,disabled,createddate,modifieddate) "
            _sql = _sql & "VALUES(@username,@password,@employeeid,@firstnameen,@lastnameen,@firstnameth,@positionid,@companyid,@ou,@deptid,@groupid,@userflag,@disabled,getdate(),getdate())"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@username", .UserName)
                _param(1) = New SQLServerDBParameter("@password", _crypt.EncryptedMD5(.Password))
                _param(2) = New SQLServerDBParameter("@employeeid", .EmployeeID)
                _param(3) = New SQLServerDBParameter("@firstnameen", .FirstnameEn)
                _param(4) = New SQLServerDBParameter("@lastnameen", .LastnameEn)
                _param(5) = New SQLServerDBParameter("@firstnameth", .FirstnameTh)
                _param(6) = New SQLServerDBParameter("@positionid", .PositionID)
                _param(7) = New SQLServerDBParameter("@companyid", .CompanyID)
                _param(8) = New SQLServerDBParameter("@ou", .OU)
                _param(9) = New SQLServerDBParameter("@deptid", .DeptID)
                _param(10) = New SQLServerDBParameter("@groupid", .GroupID)
                _param(11) = New SQLServerDBParameter("@userflag", .UserFlag)
                _param(12) = New SQLServerDBParameter("@disabled", .Disabled)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(13) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _crypt As New CryptographyControl

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_User SET "
            _sql = _sql & "username=@username,password=@password,employeeid=@employeeid,firstnameen=@firstnameen,lastnameen=@lastnameen,"
            _sql = _sql & "firstnameth=@firstnameth,positionid=@positionid,companyid=@companyid,ou=@ou,deptid=@deptid,groupid=@groupid,userflag=@userflag,disabled=@disabled,modifieddate=getdate() "
            _sql = _sql & "WHERE userid=@userid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@username", .UserName)
                _param(1) = New SQLServerDBParameter("@password", _crypt.EncryptedMD5(.Password))
                _param(2) = New SQLServerDBParameter("@employeeid", .EmployeeID)
                _param(3) = New SQLServerDBParameter("@firstnameen", .FirstnameEn)
                _param(4) = New SQLServerDBParameter("@lastnameen", .LastnameEn)
                _param(5) = New SQLServerDBParameter("@firstnameth", .FirstnameTh)
                _param(6) = New SQLServerDBParameter("@positionid", .PositionID)
                _param(7) = New SQLServerDBParameter("@companyid", .CompanyID)
                _param(8) = New SQLServerDBParameter("@ou", .OU)
                _param(9) = New SQLServerDBParameter("@deptid", .DeptID)
                _param(10) = New SQLServerDBParameter("@groupid", .GroupID)
                _param(11) = New SQLServerDBParameter("@userflag", .UserFlag)
                _param(12) = New SQLServerDBParameter("@disabled", .Disabled)
                _param(13) = New SQLServerDBParameter("@userid", .UserID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_User WHERE userid=" & _userid
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Delete()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Disabled(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_User SET disabled=1 WHERE userid=" & _userid
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function ChangPassword(_userid As String, _password As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _crypt As New CryptographyControl

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_User SET password=@password WHERE userid=@userid"
            _param(0) = New SQLServerDBParameter("@password", _crypt.EncryptedMD5(_password))
            _param(1) = New SQLServerDBParameter("@userid", _userid)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function IsNewUser(ByVal _employeeid As String) As Boolean
        Dim _result As Boolean = True
        Dim _sql As String
        Dim _user As Integer = 0
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "select count(*) FROM GSBBRC_User WHERE employeeid='" & _employeeid & "'"
            _user = _dbaccess.ExecuteScalar(_sql)
            If _user > 0 Then
                _result = False
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "IsNewUser()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function SetModulePrivilegeDefault(_employeeid As String, _groupid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _userid As Integer
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "select userid FROM GSBBRC_User WHERE employeeid='" & _employeeid & "'"
            _userid = _dbaccess.ExecuteScalar(_sql)

            _sql = "DELETE FROM GSBBRC_UserModuleAccess WHERE userid=" & _userid
            _dbaccess.ExecuteNonQuery(_sql)

            _sql = "INSERT INTO GSBBRC_UserModuleAccess(menuid,userid) "
            _sql = _sql & "SELECT menuid,@userid from GSBBRC_GroupModuleAccess where GroupID=@groupid"

            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _param(1) = New SQLServerDBParameter("@groupid", _groupid)

            _dbaccess.ExecuteNonQuery(_sql, _param)

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserAccess", "SetModulePrivilegeDefault()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function IsModulePrivilege(_userid As Integer, _moduleid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _data As Integer = 0

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            _sql = "SELECT count(*) from GSBBRC_UserModuleAccess where userid=@userid AND menuid=@menuid"
            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _param(1) = New SQLServerDBParameter("@menuid", _moduleid)
            _data = _dbaccess.ExecuteScalar(_sql, _param)
            If _data > 0 Then
                _result = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "IsModulePrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function GetUserID(ByVal _employeeid As String) As Integer
        Dim _result As Integer
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "select userid FROM GSBBRC_User WHERE employeeid='" & _employeeid & "'"
            _result = _dbaccess.ExecuteScalar(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "GetUserID()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function IsDisabled(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _user As Integer = 0
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "select count(*) FROM GSBBRC_User WHERE disabled=1 AND userid=" & _userid
            _user = _dbaccess.ExecuteScalar(_sql)
            If _user > 0 Then
                _result = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "IsDisabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

#End Region

End Class
