Public Class JusticeResultAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_datacode As String) As JusticeResultEntity
        Dim _result As New JusticeResultEntity
        Dim _sql As String
        Dim _dbaccess As New SQLServerDBAccess
        Try
            _sql = "SELECT * FROM GSBBRC_JusticeResult WHERE ResultID=" & _datacode
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .ResultID = _dbaccess.GetItem("resultid")
                    .ResultDesc = _dbaccess.GetItem("resultdesc")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeResultAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoByDeac(ByVal _datacode As String) As JusticeResultEntity
        Dim _result As New JusticeResultEntity
        Dim _sql As String
        Dim _dbaccess As New SQLServerDBAccess
        Try
            _sql = "SELECT * FROM GSBBRC_JusticeResult WHERE resultdesc='" & _datacode & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .ResultID = _dbaccess.GetItem("resultid")
                    .ResultDesc = _dbaccess.GetItem("resultdesc")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeResultAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As JusticeResultLists
        Dim _result As New JusticeResultLists
        Dim _status As JusticeResultEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_JusticeResult ORDER BY resultid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New JusticeResultEntity
                With _status
                    .ResultID = _dbaccess.GetItem("resultid")
                    .ResultDesc = _dbaccess.GetItem("resultdesc")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeResultAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As JusticeResultEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_JusticeResult(resultid,resultdesc) "
            _sql = _sql & "VALUES(@resultid,@resultdesc)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@resultid", .ResultID)
                _param(1) = New SQLServerDBParameter("@resultdesc", .ResultDesc)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("JusticeResultAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As JusticeResultEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_JusticeResult SET "
            _sql = _sql & "resultdesc=@resultdesc "
            _sql = _sql & "WHERE resultid=@resultid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@resultdesc", .ResultDesc)
                _param(1) = New SQLServerDBParameter("@resultid", .ResultID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("JusticeResultAccess", "Update()", ex.Message)
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
            _sql = "DELETE FROM GSBBRC_JusticeResult WHERE resultid=" & _datacode
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("JusticeResultAccess", "Delete()", ex.Message)
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
