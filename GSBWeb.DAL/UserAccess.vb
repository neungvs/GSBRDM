Imports Arsoft.Utility

Public Class UserAccess
    'Dim dbCon As New DBclass("ConnectionString_Report")
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString("ConnectionString_Report")
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetUserID(_username As String) As Integer
        Dim _userid As Integer
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        Try

            If IsNumeric(_username) Then
                _sql = "SELECT userid FROM rdmsys_users WHERE disabled=0 and userflag <> -1 AND employeeid=@employeeid"

                _param(0) = New SQLServerDBParameter("@employeeid", _username)
                _userid = _dbaccess.ExecuteScalar(_sql, _param)
            Else
                _sql = "SELECT userid FROM rdmsys_users WHERE disabled=0 and userflag <> -1 AND username=@username"

                _param(0) = New SQLServerDBParameter("@username", _username)
                _userid = _dbaccess.ExecuteScalar(_sql, _param)
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _userid

    End Function

    Public Function GetUserInfo(ByVal _userid As String) As UserEntity
        Dim _result As UserEntity = Nothing
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT *,IIF(timefail is NULL, 0, DATEDIFF(second,timefail,GETDATE())) as checktime FROM rdmsys_users WHERE disabled=0 AND userid=@userid"

            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read
                _result = New UserEntity
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .CountFail = _dbaccess.GetItem("countfail")
                    .TimeFail = _dbaccess.GetItem("timefail")
                    .CheckTime = _dbaccess.GetItem("checktime")
                End With
            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function SetUserInfo(ByVal _user As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(6) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO rdmsys_users (username,password,employeeid,firstnameen,firstnameth,companyid,ou,groupid,deptid,disabled,userflag,countfail) "
            _sql = _sql & "VALUES(@username,@password,@employeeid,@firstnameen,@firstnameth,2,1,@groupid,0,0,1,0)"

            _dbaccess.BeginTransaction()
            With _user
                _param(0) = New SQLServerDBParameter("@username", .UserName)
                _param(1) = New SQLServerDBParameter("@password", .Password)
                _param(2) = New SQLServerDBParameter("@employeeid", .EmployeeID)
                _param(3) = New SQLServerDBParameter("@firstnameen", .FirstNameEN)
                _param(4) = New SQLServerDBParameter("@firstnameth", .FirstNameTH)
                _param(5) = New SQLServerDBParameter("@groupid", .GroupID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function GetAllUserInfo() As List(Of UserEntity)
        Dim _result As New List(Of UserEntity)
        Dim _data As UserEntity
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT userid,username,password,firstnameen,lastnameen,firstnameth,lastnameth,groupid,userflag,employeeid	FROM rdmsys_users WHERE disabled=0 and groupid <> 1 and userflag <> -1"
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _data = New UserEntity
                With _data
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .EmployeeID = _dbaccess.GetItem("EmployeeID")
                End With

                _result.Add(_data)
            Loop

            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function Insert(_data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(14) As SQLServerDBParameter
        Dim _crypt As New CryptographyControl

        Try
            _sql = "INSERT INTO rdmsys_users(username,password,employeeid,firstnameen,lastnameen,firstnameth,lastnameth,positionid,companyid,ou,deptid,groupid,userflag,disabled,createddate,modifieddate) "
            _sql = _sql & "VALUES(@username,@password,@employeeid,@firstnameen,@lastnameen,@firstnameth,@lastnameth,@positionid,@companyid,@ou,@deptid,@groupid,@userflag,@disabled,getdate(),getdate())"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@username", .UserName)
                If .Password Is "NULL" Then
                    _param(1) = New SQLServerDBParameter("@password", .Password)
                Else
                    _param(1) = New SQLServerDBParameter("@password", IIf(String.IsNullOrEmpty(.Password), DBUtility.GetString(""), _crypt.EncryptedMD5(.Password)))
                End If
                _param(2) = New SQLServerDBParameter("@employeeid", .EmployeeID)
                _param(3) = New SQLServerDBParameter("@firstnameen", .FirstnameEn)
                _param(4) = New SQLServerDBParameter("@lastnameen", .LastnameEn)
                _param(5) = New SQLServerDBParameter("@firstnameth", .FirstNameTH)
                _param(6) = New SQLServerDBParameter("@lastnameth", .LastNameTH)
                _param(7) = New SQLServerDBParameter("@positionid", "NULL")
                _param(8) = New SQLServerDBParameter("@companyid", 2)
                _param(9) = New SQLServerDBParameter("@ou", 1)
                _param(10) = New SQLServerDBParameter("@deptid", 0)
                _param(11) = New SQLServerDBParameter("@groupid", .GroupID)
                _param(12) = New SQLServerDBParameter("@userflag", 1)
                _param(13) = New SQLServerDBParameter("@disabled", 0)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(14) As SQLServerDBParameter
        Dim _crypt As New CryptographyControl

        Try
            _sql = "UPDATE rdmsys_users SET "
            _sql = _sql & "username=@username,employeeid=@employeeid,firstnameen=@firstnameen,lastnameen=@lastnameen,"
            _sql = _sql & "firstnameth=@firstnameth,lastnameth=@lastnameth,modifieddate=getdate() "
            _sql = _sql & "WHERE userid=@userid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@username", .UserName)
                _param(2) = New SQLServerDBParameter("@employeeid", .EmployeeID)
                _param(3) = New SQLServerDBParameter("@firstnameen", .FirstnameEn)
                _param(4) = New SQLServerDBParameter("@lastnameen", .LastnameEn)
                _param(5) = New SQLServerDBParameter("@firstnameth", .FirstNameTH)
                _param(6) = New SQLServerDBParameter("@lastnameth", .LastNameTH)
                _param(7) = New SQLServerDBParameter("@userid", .UserID)
            End With

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter
        Dim _realuserid As Integer
        Try

            _realuserid = Convert.ToInt32(_userid)

            If _realuserid = 0 Then
                _result = False
            Else
                _dbaccess.BeginTransaction()
                '_sql = "DELETE FROM rdmsys_users WHERE userid=@userid"
                _sql = "UPDATE rdmsys_users SET userflag = @userflag WHERE userid = @userid"
                _param(0) = New SQLServerDBParameter("@userflag", -1)
                _param(1) = New SQLServerDBParameter("@userid", Convert.ToInt32(_userid))
                _dbaccess.ExecuteNonQuery(_sql, _param)
                _dbaccess.CommitTransaction()
                _result = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Delete()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function Disabled(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String

        Try
            _sql = "UPDATE rdmsys_users SET disabled=1 WHERE userid=" & _userid
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

    Public Function GetGroupUser(ByVal _group As String, ByVal type As String) As List(Of UserEntity)
        Dim _userGroup As UserEntity
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter

        _sql = "SELECT groupid,groupcode,groupname_en,groupname_th FROM rdmsys_groupuser WHERE groupid <> @groupid and isactive=@isactive"
        _param(0) = New SQLServerDBParameter("@groupid", _group)
        _param(1) = New SQLServerDBParameter("@isactive", 1)
        _dbaccess.ExecuteReader(_sql, _param)
        
        Dim Isuser As New List(Of UserEntity)
        _userGroup = New UserEntity
        'If type = "u" Then
        '    With _userGroup
        '        .GroupID = 0
        '        .GroupCode = 0
        '        .GroupName_EN = "SelectAll"
        '        .GroupName_TH = "ทั้งหมด"
        '    End With
        'End If
        'Isuser.Add(_userGroup)
        Try
            Do While _dbaccess.Read
                _userGroup = New UserEntity
                With _userGroup
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupCode = _dbaccess.GetItem("groupcode")
                    .GroupName_EN = _dbaccess.GetItem("groupname_en")
                    .GroupName_TH = _dbaccess.GetItem("groupname_th")
                End With
                Isuser.Add(_userGroup)
                _userGroup = New UserEntity
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return Isuser
    End Function
    Public Function SearchGroup(ByVal _groupcode As String) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter


        Try
            _sql = "SELECT groupid,groupcode,groupname_en,groupname_th FROM rdmsys_groupuser WHERE groupcode = @groupcod and isactive = @isactive"
            _param(0) = New SQLServerDBParameter("@groupcod", _groupcode)
            _param(1) = New SQLServerDBParameter("@isactive", 1)
            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read()
                _result = New UserEntity
                With _result
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupCode = _dbaccess.GetItem("groupcode")
                    .GroupName_EN = _dbaccess.GetItem("groupname_en")
                    .GroupName_TH = _dbaccess.GetItem("groupname_th")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function SearchGroupByGroupNameTH(ByVal _groupname As String) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter


        Try
            _sql = "SELECT groupid,groupcode,groupname_en,groupname_th FROM rdmsys_groupuser WHERE groupname_th = @groupname and isactive = @isactive"
            _param(0) = New SQLServerDBParameter("@groupname", _groupname)
            _param(1) = New SQLServerDBParameter("@isactive", 1)
            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read()
                _result = New UserEntity
                With _result
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupCode = _dbaccess.GetItem("groupcode")
                    .GroupName_EN = _dbaccess.GetItem("groupname_en")
                    .GroupName_TH = _dbaccess.GetItem("groupname_th")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function InsertGroupUser(ByVal data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _userentity As UserEntity
        Dim _listuser As New List(Of UserEntity)
        Dim _param(7) As SQLServerDBParameter
        Dim datetimenow As DateTime = DateTime.Now
        Try
            '_sql = "SELECT MAX(groupid) groupid FROM rdmsys_groupuser;"
            '_dbaccess.ExecuteReader(_sql)
            'Do While _dbaccess.Read
            '    _userentity = New UserEntity
            '    With _userentity
            '        .GroupID = _dbaccess.GetItem("groupid")
            '    End With
            '    _listuser.Add(_userentity)
            'Loop
            'Dim myindex = _listuser.Item(0).GroupID + 1
            _sql = "INSERT INTO rdmsys_groupuser(groupcode,groupname_en,groupname_th,parentid,createddate,modifieddate,isactive) "
            _sql &= "VALUES (@groupcode,@groupname_en,@groupname_th,@parentid,@createddate,@modifieddate,@isactive);"

            _dbaccess.BeginTransaction()

            With data
                '_param(0) = New SQLServerDBParameter("@groupid", myindex)
                _param(0) = New SQLServerDBParameter("@groupcode", .GroupCode)
                _param(1) = New SQLServerDBParameter("@groupname_en", .GroupName_EN)
                _param(2) = New SQLServerDBParameter("@groupname_th", .GroupName_TH)
                _param(3) = New SQLServerDBParameter("@parentid", 0)
                _param(4) = New SQLServerDBParameter("@createddate", datetimenow)
                _param(5) = New SQLServerDBParameter("@modifieddate", datetimenow)
                _param(6) = New SQLServerDBParameter("@isactive", 1)
            End With

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function UpdateGroupUser(ByVal data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim countparam As Integer = 0
        Dim modifidatetime As DateTime = DateTime.Now
        Dim _param(5) As SQLServerDBParameter
        Try
            _sql = "UPDATE rdmsys_groupuser SET groupcode = @groupcode ,groupname_en = @groupname_en ,groupname_th = @groupname_th ,modifieddate = @modifieddate , isactive=@isactive "
            _sql &= "WHERE groupid = @groupid;"

            _dbaccess.BeginTransaction()

            With data
                _param(0) = New SQLServerDBParameter("@groupcode", .GroupCode)
                _param(1) = New SQLServerDBParameter("@groupname_en", .GroupName_EN)
                _param(2) = New SQLServerDBParameter("@groupname_th", .GroupName_TH)
                _param(3) = New SQLServerDBParameter("@modifieddate", modifidatetime)
                _param(4) = New SQLServerDBParameter("@isactive", 1)
                _param(5) = New SQLServerDBParameter("@groupid", .GroupID)
            End With

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function DeleteGroupuser(ByVal _groucode As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _groupid As Integer
        Dim _userEntity As UserEntity
        Try

            _sql = "SELECT groupid FROM rdmsys_groupuser WHERE groupcode = @groupcode"
            _param(1) = New SQLServerDBParameter("@groupcode", _groucode)
            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read
                _userEntity = New UserEntity()
                With _userEntity
                    .GroupID = _dbaccess.GetItem("groupid")
                End With
                _groupid = _userEntity.GroupID
            Loop
            _dbaccess.CloseReader()

            _sql = "UPDATE rdmsys_groupuser SET isactive=@isactive where groupid = @groupid"
            _dbaccess.BeginTransaction()
            _param(0) = New SQLServerDBParameter("@isactive", 0)
            _param(1) = New SQLServerDBParameter("@groupid", _groupid)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetUserdataByUsername(ByVal _username As String) As List(Of UserEntity)
        Dim _Return As New List(Of UserEntity)
        Dim _user As UserEntity
        Dim _sql As String = ""
        Dim _param(3) As SQLServerDBParameter
        Try

            'If IsNumeric(_username) Then
            '    If _username.Length >= 4 Then

            'ElseIf _username.Length < 4 Then
            '    _sql = "SELECT userid,firstnameen,lastnameen,firstnameth FROM rdmsys_users WHERE userid = @username AND userflag = @userflag"
            '    End If
            'Else
            '_sql = "SELECT userid,firstnameen,lastnameen,firstnameth FROM rdmsys_users WHERE username = @username AND userflag = @userflag "
            'End If

            _sql = "SELECT * FROM rdmsys_users WHERE username = @username AND userflag = @userflag"
            _param(0) = New SQLServerDBParameter("@username", _username)
            _param(1) = New SQLServerDBParameter("@userflag", 1)
            _dbaccess.ExecuteReader(_sql, _param)
            _user = New UserEntity()
            Do While _dbaccess.Read()
                With _user
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                    .CountFail = _dbaccess.GetItem("countfail")
                    .TimeFail = _dbaccess.GetItem("timefail")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .EmployeeID = _dbaccess.GetItem("EmployeeID")
                    .GroupID = _dbaccess.GetItem("groupid")
                End With
                _Return.Add(_user)
            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Return
    End Function

    Public Function GetUserdataByEmployeeID(ByVal _employeeid As String) As List(Of UserEntity)
        Dim _Return As New List(Of UserEntity)
        Dim _user As UserEntity
        Dim _sql As String = ""
        Dim _param(3) As SQLServerDBParameter
        Try

            'If IsNumeric(_username) Then
            '    If _username.Length >= 4 Then

            'ElseIf _username.Length < 4 Then
            '    _sql = "SELECT userid,firstnameen,lastnameen,firstnameth FROM rdmsys_users WHERE userid = @username AND userflag = @userflag"
            '    End If
            'Else
            '_sql = "SELECT userid,firstnameen,lastnameen,firstnameth FROM rdmsys_users WHERE username = @username AND userflag = @userflag "
            'End If

            _sql = "SELECT userid,firstnameen,lastnameen,firstnameth FROM rdmsys_users WHERE employeeid = @employeeid AND userflag = @userflag"
            _param(0) = New SQLServerDBParameter("@employeeid", _employeeid)
            _param(1) = New SQLServerDBParameter("@userflag", 1)
            _dbaccess.ExecuteReader(_sql, _param)
            _user = New UserEntity()
            Do While _dbaccess.Read()
                With _user
                    .UserID = _dbaccess.GetItem("userid")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                End With
                _Return.Add(_user)
            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Return
    End Function

    Public Function GetUserdataByGroupID(ByVal _groupid As String) As List(Of UserEntity)
        Dim _Return As New List(Of UserEntity)
        Dim _user As UserEntity
        Dim _sql As String = ""
        Dim _param(3) As SQLServerDBParameter

        Try
            _sql = "Select a.userid,firstnameen,lastnameen,firstnameth,lastnameth,b.groupcode From rdmsys_users a inner join rdmsys_groupuser_privilege c on a.userid = c.userid inner Join rdmsys_groupuser b On c.groupid = b.groupid where b.groupcode=@groupid"
            _param(0) = New SQLServerDBParameter("@groupid", _groupid)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                _user = New UserEntity()
                With _user
                    .UserID = _dbaccess.GetItem("userid")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                End With
                _Return.Add(_user)

            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Return


        'Dim sql As String
        'Dim dt As DataTable
        'Dim _data As New List(Of UserEntity)
        'Dim ent As UserEntity
        'sql = "Select userid,firstnameen,lastnameen,firstnameth From rdmsys_users a inner Join rdmsys_groupuser b On a.groupid = b.groupid where b.groupcode=@groupid"
        '_param(0) = New SQLServerDBParameter("@groupid", _groupid)
        '_dbaccess.ExecuteReader(sql, _param)

        'For Each rs As DataRow In dt.Rows
        '    ent = New UserEntity
        '    ent.UserID = rs("userid").ToString()
        '    ent.FirstNameEN = rs("firstnameen").ToString()
        '    ent.LastNameEN = rs("lastnameen").ToString()
        '    ent.FirstNameTH = rs("firstnameth").ToString()
        '    _data.Add(ent)
        'Next

        'Return _data


    End Function

    Public Function GetUserdataInGroupByGroupID(ByVal _groupid As String) As List(Of UserEntity)
        Dim _Return As New List(Of UserEntity)
        Dim _user As UserEntity
        Dim _sql As String = ""
        Dim _param(3) As SQLServerDBParameter

        Try
            _sql = "Select a.userid,firstnameen,lastnameen,firstnameth,lastnameth,b.groupcode From rdmsys_users a inner join rdmsys_groupuser_privilege c on a.userid = c.userid inner Join rdmsys_groupuser b On c.groupid = b.groupid where b.groupcode=@groupid and a.userflag <> -1  "
            _param(0) = New SQLServerDBParameter("@groupid", _groupid)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                _user = New UserEntity()
                With _user
                    .UserID = _dbaccess.GetItem("userid")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                End With
                _Return.Add(_user)

            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Return


    End Function


    Public Function GetGroupCodeFromGroupID(ByVal _groupid As Integer) As String
        Dim _Results As String
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Try
            _sql = "SELECT GroupCode FROM rdmsys_groupuser where groupid = @groupid"
            _param(0) = New SQLServerDBParameter("@groupid", _groupid)
            _Results = _dbaccess.ExecuteScalar(_sql, _param)
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Results
    End Function

    Public Function InsertLogdata(ByVal _data As UserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(5) As SQLServerDBParameter
        Try
            _dbaccess.BeginTransaction()
            _sql = "insert into rdmsys_logdata(Userid,UserName_th,GroupID,GroupName_th,UserAction) " & _
                   "values (@userid,@username_th,@grouid,@groupname_th,@useractivity)"
            With _data
                _param(0) = New SQLServerDBParameter("@userid", .UserID)
                _param(1) = New SQLServerDBParameter("@username_th", .FirstNameTH)
                _param(2) = New SQLServerDBParameter("@grouid", .GroupID)
                _param(3) = New SQLServerDBParameter("@groupname_th", .GroupName_TH)
                _param(4) = New SQLServerDBParameter("@useractivity", .UserActivity)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function SelectLogData(ByVal _groupid As Integer, ByVal _startdate As String, ByVal _enddate As String) As List(Of UserEntity)
        Dim _result As New List(Of UserEntity)
        Dim _userentity As UserEntity
        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter
        Dim _runnumber As Integer = 1
        Try
            _sql = "SELECT tba.DTmStamp,tbb.employeeid,tba.Userid,tba.UserName_th,tba.GroupID,tba.GroupName_th,tba.UserAction FROM rdmsys_logdata tba " & _
                   "LEFT JOIN rdmsys_users tbb ON tba.Userid = tbb.userid WHERE tba.GroupID = @groupid AND DTmStamp BETWEEN @startdate AND @enddate"
            _param(0) = New SQLServerDBParameter("@groupid", _groupid)
            _param(1) = New SQLServerDBParameter("@startdate", _startdate)
            _param(2) = New SQLServerDBParameter("@enddate", _enddate)

            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                _userentity = New UserEntity
                With _userentity
                    .Number = _runnumber
                    .DTmStamp = _dbaccess.GetItem("DTmStamp")
                    .EmployeeID = _dbaccess.GetItem("employeeid")
                    .UserID = _dbaccess.GetItem("Userid")
                    .UserName_TH = _dbaccess.GetItem("UserName_th")
                    .GroupID = _dbaccess.GetItem("GroupID")
                    .GroupName_TH = _dbaccess.GetItem("GroupName_th")
                    .UserActivity = _dbaccess.GetItem("UserAction")
                End With
                _result.Add(_userentity)
                _runnumber += 1
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function GetGroupDataforCreateLogData(ByVal _userid As Integer) As String
        Dim _result As String = ""
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        Try
            _sql = "SELECT CONCAT( tbgroup.groupcode,' - ' + tbgroup.groupname_th ) UserActivity FROM rdmsys_users tbUser " & _
                   "LEFT JOIN rdmsys_groupuser_privilege tbgupri ON tbUser.userid = tbgupri.userid " & _
                   "LEFT JOIN rdmsys_groupuser tbgroup ON tbgupri.groupid = tbgroup.groupid " & _
                   "WHERE tbUser.userid = @userid"
            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _result = _dbaccess.ExecuteScalar(_sql, _param)
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

    Public Function GetGroupInfo(ByVal _groupcode As String) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Try
            _sql = "SELECT groupid,groupcode,groupname_en,groupname_th,parentid,createddate,modifieddate,isactive FROM rdmsys_groupuser WHERE groupcode=@groupcode"
            _param(0) = New SQLServerDBParameter("@groupcode", _groupcode)
            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read()
                With _result
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupCode = _dbaccess.GetItem("groupcode")
                    .GroupName_EN = _dbaccess.GetItem("groupname_en")
                    .GroupName_TH = _dbaccess.GetItem("groupname_th")

                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function GetLastGroupCode() As Integer
        Dim _result As Integer = 0
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Try
            '_sql = "SELECT MAX(CONVERT(INT,groupcode)) groupcode FROM rdmsys_groupuser WHERE isactive = @isactive"
            _sql = "SELECT MAX(CONVERT(INT,Right(groupcode,4))) groupcode FROM rdmsys_groupuser"
            _result = _dbaccess.ExecuteScalar(_sql, _param)
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function GetUserRDMData(ByVal empid As Integer) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Try
            _sql = "SELECT  userid,username ,firstnameen ,lastnameen, firstnameth,lastnameth ,groupid FROM rdmsys_users WHERE employeeid=@employeeid and userflag = 1"
            _param(0) = New SQLServerDBParameter("@employeeid", empid)
            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read()
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                    .GroupID = _dbaccess.GetItem("groupid")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function GetUserRDMDataByEmpIDStr(ByVal empid As String) As UserEntity
        Dim _result As New UserEntity
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Try
            _sql = "SELECT  userid,username ,firstnameen ,lastnameen, firstnameth,lastnameth ,groupid FROM rdmsys_users WHERE employeeid=@employeeid and userflag = 1"
            _param(0) = New SQLServerDBParameter("@employeeid", empid)
            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read()
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                    .GroupID = _dbaccess.GetItem("groupid")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserAccess", "Disabled()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function



    Public Function GenerateModuleID(ByVal _parrentid As Integer, ByVal _levelid As Integer) As Integer
        Dim _return As Integer
        Dim _output As Integer = 0
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""

        If _parrentid = 0 Then

            Try
                _sql = "" &
                       "select max(moduleid/10000) maxid from [dbo].[rdmsys_module] "

                _dbaccess.ExecuteReader(_sql)
                Do While _dbaccess.Read
                    _output = (_dbaccess.GetItem("maxid") + 1) * 10000
                Loop
                _dbaccess.CloseReader()
                Return _output

            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
            _dbaccess.CloseReader()
            Return _return

        Else

            If _levelid = 1 Then

                _sql = "" &
                       "Select (moduleid - max(moduleid / 10000) * 10000) / 100 maxid from [dbo].[rdmsys_module] where parentid = @parentid group by moduleid  "

                _param(0) = New SQLServerDBParameter("@parentid", _parrentid)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _output = _parrentid + (_dbaccess.GetItem("maxid") + 1) * 100
                Loop

                If _output = 0 Then
                    _output = _parrentid + 1 * 100
                End If


                _dbaccess.CloseReader()
                Return _output

            Else


                _sql = "" &
                       "Select  max(moduleid - cast(left(@parentid,3) as integer)*100) maxid from [dbo].[rdmsys_module] where  left(@parentid,3) = left(parentid,3)  "

                _param(0) = New SQLServerDBParameter("@parentid", _parrentid)
                _dbaccess.ExecuteReader(_sql, _param)

                Do While _dbaccess.Read
                    _output = Convert.ToInt16(Left(_parrentid.ToString, 3)) * 100 + (_dbaccess.GetItem("maxid") + 1)
                Loop


                _dbaccess.CloseReader()

                Return _output


            End If


        End If



    End Function

    Public Function GetModuleID(ByVal _parrentid As Integer, ByVal _levelid As Integer) As Integer
        Dim _return As Integer
        Dim _output As Integer
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""

        If _parrentid = 0 Then

            Try
                _sql = "" &
                       "select max(moduleid/10000) maxid from [dbo].[rdmsys_module] "

                _dbaccess.ExecuteReader(_sql)
                Do While _dbaccess.Read
                    _output = (_dbaccess.GetItem("maxid") + 1) * 10000
                Loop
                _dbaccess.CloseReader()
                Return _output

            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
            _dbaccess.CloseReader()
            Return _return

        Else

            If _levelid = 1 Then

                _sql = "" &
                       "Select (moduleid - max(moduleid / 10000) * 10000) / 100 maxid from [dbo].[rdmsys_module] where parentid = @parentid group by moduleid  "

                _param(0) = New SQLServerDBParameter("@parentid", _parrentid)
                _dbaccess.ExecuteReader(_sql, _param)

                Do While _dbaccess.Read
                    _output = _parrentid + (_dbaccess.GetItem("maxid") + 1) * 100
                Loop
                _dbaccess.CloseReader()
                Return _output

            Else


                _sql = "" &
                       "Select  max(moduleid - cast(left(@parentid,3) as integer)*100) maxid from [dbo].[rdmsys_module] where  left(@parentid,3) = left(parentid,3)  "

                _param(0) = New SQLServerDBParameter("@parentid", _parrentid)
                _dbaccess.ExecuteReader(_sql, _param)

                Return Convert.ToInt16(Left(_parrentid.ToString, 3) + (_dbaccess.GetItem("maxid") + 1).ToString("00"))


            End If


        End If



    End Function
End Class
