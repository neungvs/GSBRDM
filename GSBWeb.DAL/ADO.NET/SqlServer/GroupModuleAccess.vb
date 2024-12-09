Public Class GroupModuleAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_groupid As String) As GroupModuleAccessEntity
        Dim _result As New GroupModuleAccessEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupModuleAccess WHERE groupid='" & _groupid & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .GroupID = _dbaccess.GetItem("groupid")
                    .MenuID = _dbaccess.GetItem("menuid")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As GroupModuleAccessLists
        Dim _result As New GroupModuleAccessLists
        Dim _module As GroupModuleAccessEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupModuleAccess"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _module = New GroupModuleAccessEntity
                With _module
                    .GroupID = _dbaccess.GetItem("groupid")
                    .MenuID = _dbaccess.GetItem("menuid")
                End With
                _result.Add(_module)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists(_groupID As String) As GroupModuleAccessLists
        Dim _result As New GroupModuleAccessLists
        Dim _module As GroupModuleAccessEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupModuleAccess WHERE groupid='" & _groupID & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _module = New GroupModuleAccessEntity
                With _module
                    .GroupID = _dbaccess.GetItem("groupid")
                    .MenuID = _dbaccess.GetItem("menuid")
                End With
                _result.Add(_module)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As GroupModuleAccessEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_GroupModuleAccess(groupid,menuid) "
            _sql = _sql & "VALUES(@groupid,@menuid)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@groupid", .GroupID)
                _param(1) = New SQLServerDBParameter("@menuid", .MenuID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("GroupModuleAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(_datalist As GroupModuleAccessLists, _groupid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "DELETE FROM GSBBRC_GroupModuleAccess WHERE groupid=" & _groupid
            _dbaccess.ExecuteNonQuery(_sql)

            _sql = "INSERT INTO GSBBRC_GroupModuleAccess(groupid,menuid) "
            _sql = _sql & "VALUES(@groupid,@menuid)"

            For Each _data In _datalist
                With _data
                    _param(0) = New SQLServerDBParameter("@groupid", .GroupID)
                    _param(1) = New SQLServerDBParameter("@menuid", .MenuID)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("GroupModuleAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _groupid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_GroupModuleAccess WHERE groupid=" & _groupid
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupModuleAccess", "Delete()", ex.Message)
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
