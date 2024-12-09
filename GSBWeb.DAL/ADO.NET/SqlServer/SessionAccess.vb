Public Class SessionAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetSessionID() As String
        Dim _result As String = ""
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "EXEC GetSessionID 1"
            _result = _dbaccess.ExecuteScalar(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "GetSessionID()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfo(_datacode As String) As SessionEntity
        Dim _result As New SessionEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Session WHERE casestatuscode='" & _datacode & "'"
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                With _result
                    .AccessNo = _dbaccess.GetItem("accessno")
                    .UserID = _dbaccess.GetItem("userid")
                    .Logintime = _dbaccess.GetItem("logintime")
                    .Logouttime = _dbaccess.GetItem("logouttime")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As SessionLists
        Dim _result As New SessionLists
        Dim _status As SessionEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Session ORDER BY accessno"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New SessionEntity
                With _status
                    .AccessNo = _dbaccess.GetItem("accessno")
                    .UserID = _dbaccess.GetItem("userid")
                    .Logintime = _dbaccess.GetItem("logintime")
                    .Logouttime = _dbaccess.GetItem("logouttime")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As SessionEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_Session(accessno,userid,logintime,logouttime) "
            _sql = _sql & "VALUES(@accessno,@userid,@logintime,@logouttime)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@accessno", .AccessNo)
                _param(1) = New SQLServerDBParameter("@userid", .UserID)
                _param(2) = New SQLServerDBParameter("@logintime", .Logintime)
                _param(3) = New SQLServerDBParameter("@logouttime", .Logouttime)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("SessionAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As SessionEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_Session SET "
            _sql = _sql & "userid=@userid,logintime=@logintime,logouttime=@logouttime "
            _sql = _sql & "WHERE accessno=@accessno"

            '_accessno,_userid,_logintime,_logouttime

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@userid", .UserID)
                _param(1) = New SQLServerDBParameter("@logintime", .Logintime)
                _param(2) = New SQLServerDBParameter("@logouttime", .Logouttime)
                _param(3) = New SQLServerDBParameter("@accessno", .AccessNo)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("SessionAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _datacode As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_Session WHERE accessno='" & _datacode & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "Delete()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function BeginSession(_userid As Integer, _sessionid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _ckdata As Integer = 0
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _param(0) = New SQLServerDBParameter("@accessno", _sessionid)
            _param(1) = New SQLServerDBParameter("@userid", _userid)

            _sql = "SELECT count(*) FROM GSBBRC_Session WHERE accessno=@accessno AND userid=@userid   "
            _ckdata = _dbaccess.ExecuteScalar(_sql, _param)

            If _ckdata = 0 Then
                _sql = "INSERT INTO GSBBRC_Session(accessno,userid,logintime) "
                _sql = _sql & "VALUES(@accessno,@userid,getdate())"
                _dbaccess.ExecuteNonQuery(_sql, _param)
            End If

            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "BeginSession()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function EndSession(_sessionid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            _param(0) = New SQLServerDBParameter("@accessno", _sessionid)

            _sql = "UPDATE GSBBRC_Transaction SET "
            _sql = _sql & "responsetime=getdate() "
            _sql = _sql & "WHERE accessno=@accessno AND responsetime IS NULL"
            _dbaccess.ExecuteNonQuery(_sql, _param)

            _sql = "UPDATE GSBBRC_Session SET "
            _sql = _sql & "logouttime=getdate() "
            _sql = _sql & "WHERE accessno=@accessno"
            _dbaccess.ExecuteNonQuery(_sql, _param)

            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "EndSession()", ex.Message)
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
