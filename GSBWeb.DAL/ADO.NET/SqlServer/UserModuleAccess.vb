Public Class UserModuleAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_userid As String) As UserModuleAccessEntity
        Dim _result As New UserModuleAccessEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_UserModuleAccess WHERE userid=" & _userid
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .MenuID = _dbaccess.GetItem("menuid")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As UserModuleAccessLists
        Dim _result As New UserModuleAccessLists
        Dim _module As UserModuleAccessEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_UserModuleAccess ORDER BY menuid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _module = New UserModuleAccessEntity
                With _module
                    .UserID = _dbaccess.GetItem("userid")
                    .MenuID = _dbaccess.GetItem("menuid")
                End With
                _result.Add(_module)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists(_userid As String) As UserModuleAccessLists
        Dim _result As New UserModuleAccessLists
        Dim _module As UserModuleAccessEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            If IsNothing(_userid) Then
                _userid = 0
            End If

            _sql = "SELECT * FROM GSBBRC_UserModuleAccess WHERE userid=" & _userid & " ORDER BY menuid "
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _module = New UserModuleAccessEntity
                With _module
                    .UserID = _dbaccess.GetItem("userid")
                    .MenuID = _dbaccess.GetItem("menuid")
                End With
                _result.Add(_module)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("UserModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As UserModuleAccessEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_UserModuleAccess(userid,menuid) "
            _sql = _sql & "VALUES(@userid,@menuid)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@userid", .UserID)
                _param(1) = New SQLServerDBParameter("@menuid", .MenuID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserModuleAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(_groupid As Integer, _userid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

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
            UtilLogfile.writeToLog("UserModuleAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(_datalist As UserModuleAccessLists, _userid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "DELETE FROM GSBBRC_UserModuleAccess WHERE userid=" & _userid
            _dbaccess.ExecuteNonQuery(_sql)

            _sql = "INSERT INTO GSBBRC_UserModuleAccess(userid,menuid) "
            _sql = _sql & "VALUES(@userid,@menuid)"

            For Each _data In _datalist
                With _data
                    _param(0) = New SQLServerDBParameter("@userid", .UserID)
                    _param(1) = New SQLServerDBParameter("@menuid", .MenuID)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("UserModuleAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _userid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_UserModuleAccess WHERE userid=" & _userid
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("UserModuleAccess", "Delete()", ex.Message)
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
