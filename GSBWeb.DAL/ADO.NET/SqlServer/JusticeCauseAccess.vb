Public Class JusticeCauseAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_datacode As String) As JusticeCauseEntity
        Dim _result As New JusticeCauseEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_JusticeCause WHERE causeid=" & _datacode
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .CauseID = _dbaccess.GetItem("causeid")
                    .CauseDesc = _dbaccess.GetItem("causedesc")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeCauseAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoByDesc(ByVal _datacode As String) As JusticeCauseEntity
        Dim _result As New JusticeCauseEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_JusticeCause WHERE causedesc='" & _datacode & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .CauseID = _dbaccess.GetItem("causeid")
                    .CauseDesc = _dbaccess.GetItem("causedesc")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeCauseAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As JusticeCauseLists
        Dim _result As New JusticeCauseLists
        Dim _status As JusticeCauseEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_JusticeCause ORDER BY causeid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New JusticeCauseEntity
                With _status
                    .CauseID = _dbaccess.GetItem("causeid")
                    .CauseDesc = _dbaccess.GetItem("causedesc")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeCauseAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As JusticeCauseEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess


        Try
            _sql = "INSERT INTO GSBBRC_JusticeCause(causeid,causedesc) "
            _sql = _sql & "VALUES(@causeid,@causedesc)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@causeid", .CauseID)
                _param(1) = New SQLServerDBParameter("@causedesc", .CauseDesc)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("JusticeCauseAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As JusticeCauseEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_JusticeCause SET "
            _sql = _sql & "causedesc=@causedesc "
            _sql = _sql & "WHERE causeid=@causeid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@causedesc", .CauseDesc)
                _param(1) = New SQLServerDBParameter("@causeid", .CauseID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("JusticeCauseAccess", "Update()", ex.Message)
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
            _sql = "DELETE FROM GSBBRC_JusticeCause WHERE causeid=" & _datacode
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeCauseAccess", "Delete()", ex.Message)
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
