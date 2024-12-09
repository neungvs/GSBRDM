Public Class CaseStatusAccess

#Region "Attributes"
    Private _startfieldduedate As Integer
    Private _endfielddduedate As Integer
    Private _startfieldbackduedate As Integer
#End Region

#Region "Methods"


    Public ReadOnly Property StartFieldDuedate As Integer
        Get
            Return _startfieldduedate + 1
        End Get
    End Property

    Public ReadOnly Property EndFieldDuedate As Integer
        Get
            Return _endfielddduedate - 1
        End Get

    End Property

    Public Sub GetRecordFieldDuedate()
        Dim _result As New CaseStatusEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT MIN(endfield) as minfield, MAX(startfield) as maxfield  FROM GSBBRC_CaseStatus WHERE casestatuscode in (3,4)"
            _dbaccess.ExecuteReader(_sql)
            If _dbaccess.Read Then
                _startfieldduedate = _dbaccess.GetItem("minfield")
                _endfielddduedate = _dbaccess.GetItem("maxfield")
            End If
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CaseStatusAccess", "GetFieldRecord()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
    End Sub

    Public Function GetStartFieldBackDuedate() As Integer
        Dim _result As Integer
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT endfield FROM GSBBRC_CaseStatus WHERE casestatuscode=9"
            _result = _dbaccess.ExecuteScalar(_sql)
            _result = _result + 1
        Catch ex As Exception
            UtilLogfile.writeToLog("CaseStatusAccess", "GetFieldRecord()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetEndRecordFieldCourt() As Integer
        Dim _result As Integer
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT endfield FROM GSBBRC_CaseStatus WHERE casestatuscode=1"
            _result = _dbaccess.ExecuteScalar(_sql)
            _result = _result - 1
        Catch ex As Exception
            UtilLogfile.writeToLog("CaseStatusAccess", "GetFieldRecord()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfo(_casestatuscode As String) As CaseStatusEntity
        Dim _result As New CaseStatusEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CaseStatus WHERE casestatuscode=" & _casestatuscode
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .CaseStatusCode = _dbaccess.GetItem("casestatuscode")
                    .CaseStatusDesc = _dbaccess.GetItem("casestatusdesc")
                    .StartField = _dbaccess.GetItem("startfield")
                    .EndField = _dbaccess.GetItem("endfield")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CaseStatusAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As CaseStatusLists
        Dim _result As New CaseStatusLists
        Dim _status As CaseStatusEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CaseStatus ORDER BY casestatuscode"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CaseStatusEntity
                With _status
                    .CaseStatusCode = _dbaccess.GetItem("casestatuscode")
                    .CaseStatusDesc = _dbaccess.GetItem("casestatusdesc")
                    .StartField = _dbaccess.GetItem("startfield")
                    .EndField = _dbaccess.GetItem("endfield")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CaseStatusAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As CaseStatusEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess


        Try
            _sql = "INSERT INTO GSBBRC_CaseStatus(casestatuscode,casestatusdesc,startfield,endfield) "
            _sql = _sql & "VALUES(@casestatusdesc,@casestatusdesc,@startfield,@endfield)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@casestatuscode", .CaseStatusCode)
                _param(1) = New SQLServerDBParameter("@casestatusdesc", .CaseStatusDesc)
                _param(2) = New SQLServerDBParameter("@startfield", .StartField)
                _param(3) = New SQLServerDBParameter("@endfield", .EndField)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CaseStatusAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As CaseStatusEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE gsbbrc_casestatus SET "
            _sql = _sql & "casestatusdesc=@casestatusdesc,startfield=@startfield,endfield=@endfield "
            _sql = _sql & "WHERE casestatuscode=@casestatuscode"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@casestatusdesc", .CaseStatusDesc)
                _param(1) = New SQLServerDBParameter("@startfield", .StartField)
                _param(2) = New SQLServerDBParameter("@endfield", .EndField)
                _param(3) = New SQLServerDBParameter("@casestatuscode", .CaseStatusCode)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CaseStatusAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _casestatuscode As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_CaseStatus WHERE casestatusdesc=" & _casestatuscode
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("CaseStatusAccess", "Delete()", ex.Message)
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
