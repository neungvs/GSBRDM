Public Class ModuleAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_moduleid As String) As ModuleEntity
        Dim _result As New ModuleEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Module WHERE moduleid='" & _moduleid & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleName = _dbaccess.GetItem("modulename")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .PageCode = _dbaccess.GetItem("pagecode")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As ModuleLists
        Dim _result As New ModuleLists
        Dim _module As ModuleEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Module ORDER BY moduleid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _module = New ModuleEntity
                With _module
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleName = _dbaccess.GetItem("modulename")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .PageCode = _dbaccess.GetItem("pagecode")
                End With
                _result.Add(_module)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As ModuleEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_Module(moduleid,modulename,parentid,pagecode) "
            _sql = _sql & "VALUES(@moduleid,@modulename,@parentid,@pagecode)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@moduleid", .ModuleID)
                _param(1) = New SQLServerDBParameter("@modulename", .ModuleName)
                _param(2) = New SQLServerDBParameter("@parentid", .ParentID)
                _param(3) = New SQLServerDBParameter("@pagecode", .PageCode)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("ModuleAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As ModuleEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_Module SET "
            _sql = _sql & "modulename=@modulename,parentid=@parentid,pagecode=@pagecode "
            _sql = _sql & "WHERE moduleid=@moduleid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@modulename", .ModuleName)
                _param(1) = New SQLServerDBParameter("@parentid", .ParentID)
                _param(2) = New SQLServerDBParameter("@pagecode", .PageCode)
                _param(3) = New SQLServerDBParameter("@moduleid", .ModuleID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("ModuleAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _moduleid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_Module WHERE moduleid='" & _moduleid & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "Delete()", ex.Message)
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
