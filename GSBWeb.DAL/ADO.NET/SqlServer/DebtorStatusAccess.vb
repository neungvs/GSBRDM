Public Class DebtorStatusAccess

#Region "Attributes"

#End Region

#Region "Methods"

    Public Function GetInfo(ByVal _debtorstatusid As Integer) As DebtorStatusEntity
        Dim _result As New DebtorStatusEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_DebtorStatus WHERE debtorstatusid=" & _debtorstatusid
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .DebtorStatusID = _dbaccess.GetItem("debtorstatusid")
                    .DebtorStatusDesc = _dbaccess.GetItem("debtorstatusdesc")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("DebtorStatusAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoByDesc(ByVal _debtorstatusid As String) As DebtorStatusEntity
        Dim _result As New DebtorStatusEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_DebtorStatus WHERE debtorstatusdesc='" & _debtorstatusid & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .DebtorStatusID = _dbaccess.GetItem("debtorstatusid")
                    .DebtorStatusDesc = _dbaccess.GetItem("debtorstatusdesc")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("DebtorStatusAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As DebtorStatusLists
        Dim _result As New DebtorStatusLists
        Dim _status As DebtorStatusEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_DebtorStatus ORDER BY debtorstatusid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New DebtorStatusEntity
                With _status
                    .DebtorStatusID = _dbaccess.GetItem("debtorstatusid")
                    .DebtorStatusDesc = _dbaccess.GetItem("debtorstatusdesc")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("DebtorStatusAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    'Public Function Insert(ByVal _data As CaseStatusEntity) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _param(3) As SQLServerDBParameter
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess


    '    Try
    '        _sql = "INSERT INTO GSBBRC_CaseStatus(casestatuscode,casestatusdesc,startfield,endfield) "
    '        _sql = _sql & "VALUES(@casestatusdesc,@casestatusdesc,@startfield,@endfield)"

    '        _dbaccess.BeginTransaction()
    '        With _data
    '            _param(0) = New SQLServerDBParameter("@casestatuscode", .CaseStatusCode)
    '            _param(1) = New SQLServerDBParameter("@casestatusdesc", .CaseStatusDesc)
    '            _param(2) = New SQLServerDBParameter("@startfield", .StartField)
    '            _param(3) = New SQLServerDBParameter("@endfield", .EndField)
    '        End With
    '        _dbaccess.ExecuteNonQuery(_sql, _param)
    '        _dbaccess.CommitTransaction()
    '        _result = True
    '    Catch ex As Exception
    '        _dbaccess.RollbackTransaction()
    '        UtilLogfile.writeToLog("DebtorStatusAccess", "Insert()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try

    '    Return _result
    'End Function

    'Public Function Update(ByVal _data As CaseStatusEntity) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _param(3) As SQLServerDBParameter
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try
    '        _sql = "UPDATE gsbbrc_casestatus SET "
    '        _sql = _sql & "casestatusdesc=@casestatusdesc,startfield=@startfield,endfield=@endfield "
    '        _sql = _sql & "WHERE casestatuscode=@casestatuscode"

    '        _dbaccess.BeginTransaction()
    '        With _data
    '            _param(0) = New SQLServerDBParameter("@casestatusdesc", .CaseStatusDesc)
    '            _param(1) = New SQLServerDBParameter("@startfield", .StartField)
    '            _param(2) = New SQLServerDBParameter("@endfield", .EndField)
    '            _param(3) = New SQLServerDBParameter("@casestatuscode", .CaseStatusCode)
    '        End With
    '        _dbaccess.ExecuteNonQuery(_sql, _param)
    '        _dbaccess.CommitTransaction()
    '        _result = True
    '    Catch ex As Exception
    '        _dbaccess.RollbackTransaction()
    '        UtilLogfile.writeToLog("DebtorStatusAccess", "Update()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try

    '    Return _result
    'End Function

    'Public Function Delete(ByVal _casestatuscode As String) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try
    '        _sql = "DELETE FROM GSBBRC_CaseStatus WHERE casestatusdesc=" & _casestatuscode
    '        _dbaccess.ExecuteNonQuery(_sql)
    '        _result = True
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("DebtorStatusAccess", "Delete()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try
    '    Return _result
    'End Function

#End Region

End Class
