Public Class CourtDetailAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(ByVal _redCaseNo As String, ByVal _refId As String) As CourtDetailEntity 'Created By ReX
        Dim _result As New CourtDetailEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtDetail WHERE redcaseno='" & _redCaseNo & "' AND refid='" & _refId & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .RecNo = _dbaccess.GetItem("recno")
                    .CaseStatusCode = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("casestatuscode"))
                    .EffectiveDate = UtilOfValidate.CheckString(_dbaccess.GetItem("effectivedate"))
                    .GazDate = UtilOfValidate.CheckString(_dbaccess.GetItem("gazdate"))
                    .GazBook = UtilOfValidate.CheckString(_dbaccess.GetItem("gazbook"))
                    .GazPart = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpart"))
                    .GazPage = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpage"))
                    .PaymentDueDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentduedate"))
                    .PaymentBackDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentbackdate"))
                    .PaymentCheckDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentcheckdate"))
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtDetailAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfo(ByVal _datacode As String) As CourtDetailEntity
        Dim _result As New CourtDetailEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtDetail WHERE redcaseno='" & _datacode & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .RecNo = _dbaccess.GetItem("recno")
                    .CaseStatusCode = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("casestatuscode"))
                    .EffectiveDate = UtilOfValidate.CheckString(_dbaccess.GetItem("effectivedate"))
                    .GazDate = UtilOfValidate.CheckString(_dbaccess.GetItem("gazdate"))
                    .GazBook = UtilOfValidate.CheckString(_dbaccess.GetItem("gazbook"))
                    .GazPart = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpart"))
                    .GazPage = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpage"))
                    .PaymentDueDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentduedate"))
                    .PaymentBackDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentbackdate"))
                    .PaymentCheckDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentcheckdate"))
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtDetailAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists(ByVal _redCaseNo As String, ByVal _refId As String) As CourtDetailLists
        Dim _result As New CourtDetailLists
        Dim _status As CourtDetailEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtDetail WHERE redcaseno='" & _redCaseNo & "' AND refid='" & _refId & "' ORDER BY redcaseno"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CourtDetailEntity
                With _status
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .RecNo = _dbaccess.GetItem("recno")
                    .CaseStatusCode = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("casestatuscode"))
                    .EffectiveDate = UtilOfValidate.CheckString(_dbaccess.GetItem("effectivedate"))
                    .GazDate = UtilOfValidate.CheckString(_dbaccess.GetItem("gazdate"))
                    .GazBook = UtilOfValidate.CheckString(_dbaccess.GetItem("gazbook"))
                    .GazPart = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpart"))
                    .GazPage = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpage"))
                    .PaymentDueDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentduedate"))
                    .PaymentBackDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentbackdate"))
                    .PaymentCheckDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentcheckdate"))
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtDetailAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As CourtDetailLists
        Dim _result As New CourtDetailLists
        Dim _status As CourtDetailEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtDetail ORDER BY redcaseno"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CourtDetailEntity
                With _status
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .RecNo = _dbaccess.GetItem("recno")
                    .CaseStatusCode = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("casestatuscode"))
                    .EffectiveDate = UtilOfValidate.CheckString(_dbaccess.GetItem("effectivedate"))
                    .GazDate = UtilOfValidate.CheckString(_dbaccess.GetItem("gazdate"))
                    .GazBook = UtilOfValidate.CheckString(_dbaccess.GetItem("gazbook"))
                    .GazPart = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpart"))
                    .GazPage = UtilOfValidate.CheckString(_dbaccess.GetItem("gazpage"))
                    .PaymentDueDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentduedate"))
                    .PaymentBackDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentbackdate"))
                    .PaymentCheckDate = UtilOfValidate.CheckString(_dbaccess.GetItem("paymentcheckdate"))
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtDetailAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(ByVal _data As CourtDetailEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(11) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess


        Try
            _sql = "INSERT INTO GSBBRC_CourtDetail("
            _sql = _sql & "redcaseno,refid,recno,casestatuscode,effectivedate,gazdate,"
            _sql = _sql & "gazbook,gazpart,gazpage,paymentduedate,paymentbackdate,paymentcheckdate"
            _sql = _sql & ") "
            _sql = _sql & "VALUES("
            _sql = _sql & "@redcaseno,@refid,@recno,@casestatuscode,@effectivedate,@gazdate,"
            _sql = _sql & "@gazbook,@gazpart,@gazpage,@paymentduedate,@paymentbackdate,@paymentcheckdate"
            _sql = _sql & ") "

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@redcaseno", .RedCaseNo)
                _param(1) = New SQLServerDBParameter("@refid", .RefID)
                _param(2) = New SQLServerDBParameter("@recno", .RecNo)
                _param(3) = New SQLServerDBParameter("@casestatuscode", .CaseStatusCode)
                _param(4) = New SQLServerDBParameter("@effectivedate", .EffectiveDate)
                _param(5) = New SQLServerDBParameter("@gazdate", .GazDate)
                _param(6) = New SQLServerDBParameter("@gazbook", .GazBook)
                _param(7) = New SQLServerDBParameter("@gazpart", .GazPart)
                _param(8) = New SQLServerDBParameter("@gazpage", .GazPage)
                _param(9) = New SQLServerDBParameter("@paymentduedate", .PaymentDueDate)
                _param(10) = New SQLServerDBParameter("@paymentbackdate", .PaymentBackDate)
                _param(11) = New SQLServerDBParameter("@paymentcheckdate", .PaymentCheckDate)

            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtDetailAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(ByVal _data As CourtDetailEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(11) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_CourtDetail SET "
            _sql = _sql & "redcaseno=@redcaseno,recno=@recno,casestatuscode=@casestatuscode,"
            _sql = _sql & "effectivedate=@effectivedate,gazdate=@gazdate,gazbook=@gazbook,gazpart=@gazpart,"
            _sql = _sql & "gazpage=@gazpage,paymentduedate=@paymentduedate,paymentbackdate=@paymentbackdate,paymentcheckdate=@paymentcheckdate"
            _sql = _sql & "WHERE refid=@refid"

            '_RedCaseNo,_RefID,_RecNo,_CaseStatusCode,_EffectiveDate,_GazDate,
            '_GazBook,_GazPart,_GazPage,_PaymentDueDate,_PaymentBackDate,_PaymentCheckDate

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@redcaseno", .RedCaseNo)
                _param(1) = New SQLServerDBParameter("@recno", .RecNo)
                _param(2) = New SQLServerDBParameter("@casestatuscode", .CaseStatusCode)
                _param(3) = New SQLServerDBParameter("@effectivedate", .EffectiveDate)
                _param(4) = New SQLServerDBParameter("@gazdate", .GazDate)
                _param(5) = New SQLServerDBParameter("@gazbook", .GazBook)
                _param(6) = New SQLServerDBParameter("@gazpart", .GazPart)
                _param(7) = New SQLServerDBParameter("@gazpage", .GazPage)
                _param(8) = New SQLServerDBParameter("@paymentduedate", .PaymentDueDate)
                _param(9) = New SQLServerDBParameter("@paymentbackdate", .PaymentBackDate)
                _param(10) = New SQLServerDBParameter("@paymentcheckdate", .PaymentCheckDate)
                _param(11) = New SQLServerDBParameter("@refid", .RefID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtDetailAccess", "Update()", ex.Message)
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
            _sql = "DELETE FROM GSBBRC_CourtDetail WHERE refid='" & _datacode & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtDetailAccess", "Delete()", ex.Message)
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
