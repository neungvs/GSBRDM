Public Class TransDetailAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_datacode As String) As TransDetailEntity
        Dim _result As New TransDetailEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_TransDetail WHERE accessno='" & _datacode & "'"
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                With _result
                    .AccessNo = _dbaccess.GetItem("accessno")
                    .TransNo = _dbaccess.GetItem("transno")
                    .RefID = _dbaccess.GetItem("refid")
                    .AccountNo = _dbaccess.GetItem("accountno")
                    .TableName = _dbaccess.GetItem("tablename")
                    .FieldName = _dbaccess.GetItem("fieldname")
                    .OldValue = _dbaccess.GetItem("oldvalue")
                    .NewValue = _dbaccess.GetItem("newvalue")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("TransDetailAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function GetInfoLists() As TransDetailLists
        Dim _result As New TransDetailLists
        Dim _status As TransDetailEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_TransDetail ORDER BY _accessno"
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _status = New TransDetailEntity
                With _status
                    .AccessNo = _dbaccess.GetItem("accessno")
                    .TransNo = _dbaccess.GetItem("transno")
                    .RefID = _dbaccess.GetItem("refid")
                    .AccountNo = _dbaccess.GetItem("accountno")
                    .TableName = _dbaccess.GetItem("tablename")
                    .FieldName = _dbaccess.GetItem("fieldname")
                    .OldValue = _dbaccess.GetItem("oldvalue")
                    .NewValue = _dbaccess.GetItem("newvalue")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("TransDetailAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As TransDetailEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(7) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_TransDetail(accessno,transno,refid,accountno,tablename,fieldname,oldvalue,newvalue) "
            _sql = _sql & "VALUES(@accessno,@transno,@refid,@accountno,@tablename,@fieldname,@oldvalue,@newvalue)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@accessno", .AccessNo)
                _param(1) = New SQLServerDBParameter("@transno", .TransNo)
                _param(2) = New SQLServerDBParameter("@refid", .RefID)
                _param(3) = New SQLServerDBParameter("@accountno", .AccountNo)
                _param(4) = New SQLServerDBParameter("@tablename", .TableName)
                _param(5) = New SQLServerDBParameter("@fieldname", .FieldName)
                _param(6) = New SQLServerDBParameter("@oldvalue", .OldValue)
                _param(7) = New SQLServerDBParameter("@newvalue", .NewValue)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("TransDetailAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As TransDetailEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(7) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_TransDetail SET "
            _sql = _sql & "transno=@transno,refid=@refid,accountno=@accountno,tablename=@tablename,"
            _sql = _sql & "fieldname=@fieldname,oldvalue=@oldvalue,newvalue=@newvalue "
            _sql = _sql & "WHERE casestatuscode=@casestatuscode"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@transno", .TransNo)
                _param(1) = New SQLServerDBParameter("@refid", .RefID)
                _param(2) = New SQLServerDBParameter("@accountno", .AccountNo)
                _param(3) = New SQLServerDBParameter("@tablename", .TableName)
                _param(4) = New SQLServerDBParameter("@fieldname", .FieldName)
                _param(5) = New SQLServerDBParameter("@oldvalue", .OldValue)
                _param(6) = New SQLServerDBParameter("@newvalue", .NewValue)
                _param(7) = New SQLServerDBParameter("@accessno", .AccessNo)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("TransDetailAccess", "Update()", ex.Message)
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
            _sql = "DELETE FROM GSBBRC_TransDetail WHERE AccessNo='" & _datacode & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("TransDetailAccess", "Delete()", ex.Message)
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
