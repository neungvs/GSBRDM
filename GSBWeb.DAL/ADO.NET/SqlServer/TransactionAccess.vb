Public Class TransactionAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_accessno As String) As TransactionEntity
        Dim _result As New TransactionEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Transaction WHERE accessno='" & _accessno & "'"
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                With _result
                    .AccessNo = _dbaccess.GetItem("accessno")
                    .TransNo = _dbaccess.GetItem("transno")
                    .MenuID = _dbaccess.GetItem("menuid")
                    .RequestTime = _dbaccess.GetItem("requesttime")
                    .ResponseTime = _dbaccess.GetItem("responsetime")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("TransactionAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As TransactionLists
        Dim _result As New TransactionLists
        Dim _status As TransactionEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Transaction ORDER BY transno"

            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New TransactionEntity
                With _status
                    .AccessNo = _dbaccess.GetItem("accessno")
                    .TransNo = _dbaccess.GetItem("transno")
                    .MenuID = _dbaccess.GetItem("menuid")
                    .RequestTime = _dbaccess.GetItem("requesttime")
                    .ResponseTime = _dbaccess.GetItem("responsetime")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("TransactionAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As TransactionEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(4) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_Transaction(accessno,transno,menuid,requesttime,responsetime) "
            _sql = _sql & "VALUES(@accessno,@transno,@menuid,@requesttime,@responsetime)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@accessno", .AccessNo)
                _param(1) = New SQLServerDBParameter("@transno", .TransNo)
                _param(2) = New SQLServerDBParameter("@menuid", .MenuID)
                _param(3) = New SQLServerDBParameter("@requesttime", .RequestTime)
                _param(4) = New SQLServerDBParameter("@responsetime", .ResponseTime)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("TransactionAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As TransactionEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_Transaction SET "
            _sql = _sql & "accessno=@accessno,menuid=@menuid,requesttime=@requesttime,responsetime=@responsetime "
            _sql = _sql & "WHERE transno=@transno"

            '_accessno,_transno,_menuid,_requesttime,_responsetime

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@accessno", .AccessNo)
                _param(1) = New SQLServerDBParameter("@menuid", .MenuID)
                _param(2) = New SQLServerDBParameter("@requesttime", .RequestTime)
                _param(3) = New SQLServerDBParameter("@responsetime", .ResponseTime)
                _param(4) = New SQLServerDBParameter("@transno", .TransNo)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("TransactionAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _accessno As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_Transaction WHERE accessno='" & _accessno & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("TransactionAccess", "Delete()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function AddTransaction(_menuid As String, _accessno As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _param(0) = New SQLServerDBParameter("@accessno", _accessno)
            _param(1) = New SQLServerDBParameter("@menuid", _menuid)

            _sql = "EXEC AddTransaction @accessno,@menuid"
            _dbaccess.ExecuteNonQuery(_sql, _param)

            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("SessionAccess", "AddTransaction()", ex.Message)
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
