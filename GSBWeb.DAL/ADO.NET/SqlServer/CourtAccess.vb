Public Class CourtAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(ByVal _redCaseNo As String, ByVal _refId As String) As CourtEntity 'Created By ReX
        Dim _result As New CourtEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Court WHERE redcaseno='" & _redCaseNo & "' AND refid='" & _refId & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .RedCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseno"))
                    .RefID = UtilOfValidate.CheckString(_dbaccess.GetItem("refid"))
                    .RecNo = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("recno"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .CourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("courtname"))
                    .BlackCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseno"))
                    .BlackCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseyear"))
                    .RedCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseyear"))
                    .Plaintiff = UtilOfValidate.CheckString(_dbaccess.GetItem("plaintiff"))
                    .Defendant = UtilOfValidate.CheckString(_dbaccess.GetItem("defendant"))
                    .CustNo = UtilOfValidate.CheckString(_dbaccess.GetItem("custno"))
                    .PbID = UtilOfValidate.CheckString(_dbaccess.GetItem("pbid"))
                    .DeptID = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("deptid"))
                    .Feeddate = UtilOfValidate.CheckString(_dbaccess.GetItem("feeddate"))
                    .BCourtRefID = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtrefid"))
                    .BCourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtname"))
                    .BCourtSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtsurname"))
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfo_ByRefId(ByVal _refID As String) As CourtEntity
        Dim _result As New CourtEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Court WHERE redcaseno='" & _refID & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .RedCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseno"))
                    .RefID = UtilOfValidate.CheckString(_dbaccess.GetItem("refid"))
                    .RecNo = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("recno"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .CourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("courtname"))
                    .BlackCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseno"))
                    .BlackCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseyear"))
                    .RedCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseyear"))
                    .Plaintiff = UtilOfValidate.CheckString(_dbaccess.GetItem("plaintiff"))
                    .Defendant = UtilOfValidate.CheckString(_dbaccess.GetItem("defendant"))
                    .CustNo = UtilOfValidate.CheckString(_dbaccess.GetItem("custno"))
                    .PbID = UtilOfValidate.CheckString(_dbaccess.GetItem("pbid"))
                    .DeptID = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("deptid"))
                    .Feeddate = UtilOfValidate.CheckString(_dbaccess.GetItem("feeddate"))
                    .BCourtRefID = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtrefid"))
                    .BCourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtname"))
                    .BCourtSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtsurname"))
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfo(ByVal _redcaseno As String) As CourtEntity
        Dim _result As New CourtEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Court WHERE redcaseno='" & _redcaseno & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .RedCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseno"))
                    .RefID = UtilOfValidate.CheckString(_dbaccess.GetItem("refid"))
                    .RecNo = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("recno"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .CourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("courtname"))
                    .BlackCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseno"))
                    .BlackCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseyear"))
                    .RedCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseyear"))
                    .Plaintiff = UtilOfValidate.CheckString(_dbaccess.GetItem("plaintiff"))
                    .Defendant = UtilOfValidate.CheckString(_dbaccess.GetItem("defendant"))
                    .CustNo = UtilOfValidate.CheckString(_dbaccess.GetItem("custno"))
                    .PbID = UtilOfValidate.CheckString(_dbaccess.GetItem("pbid"))
                    .DeptID = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("deptid"))
                    .Feeddate = UtilOfValidate.CheckString(_dbaccess.GetItem("feeddate"))
                    .BCourtRefID = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtrefid"))
                    .BCourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtname"))
                    .BCourtSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtsurname"))
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists_ByRefId(ByVal _refid As String) As CourtLists
        Dim _result As New CourtLists
        Dim _status As CourtEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Court WHERE refid='" & _refid & "' ORDER BY recno "
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CourtEntity
                With _status
                    .RedCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseno"))
                    .RefID = UtilOfValidate.CheckString(_dbaccess.GetItem("refid"))
                    .RecNo = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("recno"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .CourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("courtname"))
                    .BlackCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseno"))
                    .BlackCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseyear"))
                    .RedCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseyear"))
                    .Plaintiff = UtilOfValidate.CheckString(_dbaccess.GetItem("plaintiff"))
                    .Defendant = UtilOfValidate.CheckString(_dbaccess.GetItem("defendant"))
                    .CustNo = UtilOfValidate.CheckString(_dbaccess.GetItem("custno"))
                    .PbID = UtilOfValidate.CheckString(_dbaccess.GetItem("pbid"))
                    .DeptID = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("deptid"))
                    .Feeddate = UtilOfValidate.CheckString(_dbaccess.GetItem("feeddate"))
                    .BCourtRefID = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtrefid"))
                    .BCourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtname"))
                    .BCourtSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtsurname"))
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As CourtLists
        Dim _result As New CourtLists
        Dim _status As CourtEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_Court ORDER BY recno"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CourtEntity
                With _status
                    .RedCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseno"))
                    .RefID = UtilOfValidate.CheckString(_dbaccess.GetItem("refid"))
                    .RecNo = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("recno"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .CourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("courtname"))
                    .BlackCaseNo = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseno"))
                    .BlackCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("blackcaseyear"))
                    .RedCaseYear = UtilOfValidate.CheckString(_dbaccess.GetItem("redcaseyear"))
                    .Plaintiff = UtilOfValidate.CheckString(_dbaccess.GetItem("plaintiff"))
                    .Defendant = UtilOfValidate.CheckString(_dbaccess.GetItem("defendant"))
                    .CustNo = UtilOfValidate.CheckString(_dbaccess.GetItem("custno"))
                    .PbID = UtilOfValidate.CheckString(_dbaccess.GetItem("pbid"))
                    .DeptID = UtilOfValidate.CheckNumeric(_dbaccess.GetItem("deptid"))
                    .Feeddate = UtilOfValidate.CheckString(_dbaccess.GetItem("feeddate"))
                    .BCourtRefID = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtrefid"))
                    .BCourtName = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtname"))
                    .BCourtSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("bcourtsurname"))
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As CourtEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(16) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_Court("
            _sql = _sql & "redcaseno,refid,recno,custname,custsurname,courtname,blackcaseno,"
            _sql = _sql & "blackcaseyear,redcaseyear,plaintiff,defendant,pbid,deptid,feeddate,bcourtrefid,bcourtname,bcourtsurname"
            _sql = _sql & ") "
            _sql = _sql & "VALUES("
            _sql = _sql & "@redcaseno,@refid,@recno,@custname,@custsurname,@courtname,@blackcaseno,"
            _sql = _sql & "@blackcaseyear,@redcaseyear,@plaintiff,@defendant,@pbid,@deptid,@feeddate,@bcourtrefid,@bcourtname,@bcourtsurname"
            _sql = _sql & ")"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@redcaseno", DBUtility.GetString(.RedCaseNo))
                _param(1) = New SQLServerDBParameter("@refid", DBUtility.GetString(.RefID))
                _param(2) = New SQLServerDBParameter("@recno", DBUtility.GetNumeric(.RecNo))
                _param(3) = New SQLServerDBParameter("@custname", DBUtility.GetString(.CustName))
                _param(4) = New SQLServerDBParameter("@custsurname", DBUtility.GetString(.CustSurname))
                _param(5) = New SQLServerDBParameter("@courtname", DBUtility.GetString(.CourtName))
                _param(6) = New SQLServerDBParameter("@blackcaseno", DBUtility.GetString(.BlackCaseNo))
                _param(7) = New SQLServerDBParameter("@blackcaseyear", DBUtility.GetString(.BlackCaseYear))
                _param(8) = New SQLServerDBParameter("@redcaseyear", DBUtility.GetString(.RedCaseYear))
                _param(9) = New SQLServerDBParameter("@plaintiff", DBUtility.GetString(.Plaintiff))
                _param(10) = New SQLServerDBParameter("@defendant", DBUtility.GetString(.Defendant))
                _param(11) = New SQLServerDBParameter("@pbid", DBUtility.GetString(.PbID))
                _param(12) = New SQLServerDBParameter("@deptid", DBUtility.GetNumeric(.DeptID))
                _param(13) = New SQLServerDBParameter("@feeddate", DBUtility.GetString(.Feeddate))
                _param(14) = New SQLServerDBParameter("@bcourtrefid", DBUtility.GetString(.BCourtRefID))
                _param(15) = New SQLServerDBParameter("@bcourtname", DBUtility.GetString(.BCourtName))
                _param(16) = New SQLServerDBParameter("@bcourtsurname", DBUtility.GetString(.BCourtSurname))
            End With

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(_datafiles As DataFileLists, _courtlists As CourtLists, _courtdetaillists As CourtDetailLists) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _sqldatafile As String = ""
        Dim _sqlcourt As String = ""
        Dim _sqlcourtdetail As String = ""
        Dim _paramdatafile(9) As SQLServerDBParameter
        Dim _paramcourt(16) As SQLServerDBParameter
        Dim _paramcourtdetail(11) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _feeddate As String

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            _sql = "SELECT TOP 1 feeddate FROM GSBBRC_Court"
            _feeddate = _dbaccess.ExecuteScalar(_sql)
            If IsNothing(_feeddate) Then
                _feeddate = ""
            End If
            _dbaccess.BeginTransaction()

            '### Data Files ###
            _sqldatafile = "INSERT INTO GSBBRC_DataFile("
            _sqldatafile = _sqldatafile & "filename,feeddate,totalrecord,acceptrecord,rejectrecord,"
            _sqldatafile = _sqldatafile & "datestart,datefinish,filestatus,createddate,createdby"
            _sqldatafile = _sqldatafile & ") "
            _sqldatafile = _sqldatafile & "VALUES("
            _sqldatafile = _sqldatafile & "@filename,@feeddate,@totalrecord,@acceptrecord,@rejectrecord,"
            _sqldatafile = _sqldatafile & "@datestart,@datefinish,@filestatus,@createddate,@createdby"
            _sqldatafile = _sqldatafile & ")"

            For Each _data In _datafiles
                With _data
                    _paramdatafile(0) = New SQLServerDBParameter("@filename", DBUtility.GetString(.FileName))
                    _paramdatafile(1) = New SQLServerDBParameter("@feeddate", DBUtility.GetString(.FeedDate))
                    _paramdatafile(2) = New SQLServerDBParameter("@totalrecord", DBUtility.GetNumeric(.TotalRecord))
                    _paramdatafile(3) = New SQLServerDBParameter("@acceptrecord", DBUtility.GetNumeric(.AcceptRecord))
                    _paramdatafile(4) = New SQLServerDBParameter("@rejectrecord", DBUtility.GetNumeric(.RejectRecord))
                    _paramdatafile(5) = New SQLServerDBParameter("@datestart", .DateStart)
                    _paramdatafile(6) = New SQLServerDBParameter("@datefinish", .DateFinish)
                    _paramdatafile(7) = New SQLServerDBParameter("@filestatus", DBUtility.GetNumeric(.FileStatus))
                    _paramdatafile(8) = New SQLServerDBParameter("@createddate", .CreatedDate)
                    _paramdatafile(9) = New SQLServerDBParameter("@createdby", DBUtility.GetString(.CreatedBy))
                End With
                _dbaccess.ExecuteNonQuery(_sqldatafile, _paramdatafile)
            Next

            '### Court Master ###
            _sql = "INSERT INTO GSBBRC_CourtHistory "
            _sql = _sql & "SELECT * FROM GSBBRC_Court"
            _dbaccess.ExecuteScalar(_sql)

            _sql = "DELETE FROM GSBBRC_Court"
            _dbaccess.ExecuteScalar(_sql)

            _sqlcourt = "INSERT INTO GSBBRC_Court("
            _sqlcourt = _sqlcourt & "redcaseno,refid,recno,custname,custsurname,courtname,blackcaseno,"
            _sqlcourt = _sqlcourt & "blackcaseyear,redcaseyear,plaintiff,defendant,pbid,deptid,feeddate,bcourtrefid,bcourtname,bcourtsurname"
            _sqlcourt = _sqlcourt & ") "
            _sqlcourt = _sqlcourt & "VALUES("
            _sqlcourt = _sqlcourt & "@redcaseno,@refid,@recno,@custname,@custsurname,@courtname,@blackcaseno,"
            _sqlcourt = _sqlcourt & "@blackcaseyear,@redcaseyear,@plaintiff,@defendant,@pbid,@deptid,@feeddate,@bcourtrefid,@bcourtname,@bcourtsurname"
            _sqlcourt = _sqlcourt & ")"

            For Each _data In _courtlists
                With _data
                    _paramcourt(0) = New SQLServerDBParameter("@redcaseno", DBUtility.GetString(.RedCaseNo))
                    _paramcourt(1) = New SQLServerDBParameter("@refid", DBUtility.GetString(.RefID))
                    _paramcourt(2) = New SQLServerDBParameter("@recno", DBUtility.GetNumeric(.RecNo))
                    _paramcourt(3) = New SQLServerDBParameter("@custname", DBUtility.GetString(.CustName))
                    _paramcourt(4) = New SQLServerDBParameter("@custsurname", DBUtility.GetString(.CustSurname))
                    _paramcourt(5) = New SQLServerDBParameter("@courtname", DBUtility.GetString(.CourtName))
                    _paramcourt(6) = New SQLServerDBParameter("@blackcaseno", DBUtility.GetString(.BlackCaseNo))
                    _paramcourt(7) = New SQLServerDBParameter("@blackcaseyear", DBUtility.GetString(.BlackCaseYear))
                    _paramcourt(8) = New SQLServerDBParameter("@redcaseyear", DBUtility.GetString(.RedCaseYear))
                    _paramcourt(9) = New SQLServerDBParameter("@plaintiff", DBUtility.GetString(.Plaintiff))
                    _paramcourt(10) = New SQLServerDBParameter("@defendant", DBUtility.GetString(.Defendant))
                    _paramcourt(11) = New SQLServerDBParameter("@pbid", DBUtility.GetString(.PbID))
                    _paramcourt(12) = New SQLServerDBParameter("@deptid", DBUtility.GetNumeric(.DeptID))
                    _paramcourt(13) = New SQLServerDBParameter("@feeddate", DBUtility.GetString(.Feeddate))
                    _paramcourt(14) = New SQLServerDBParameter("@bcourtrefid", DBUtility.GetString(.BCourtRefID))
                    _paramcourt(15) = New SQLServerDBParameter("@bcourtname", DBUtility.GetString(.BCourtName))
                    _paramcourt(16) = New SQLServerDBParameter("@bcourtsurname", DBUtility.GetString(.BCourtSurname))
                End With
                _dbaccess.ExecuteNonQuery(_sqlcourt, _paramcourt)
            Next


            '### Court Detail ###
            _sql = "INSERT INTO GSBBRC_CourtDetailHistory "
            _sql = _sql & "SELECT *,'" & _feeddate & "' FROM GSBBRC_CourtDetail"
            _dbaccess.ExecuteScalar(_sql)

            _sql = "DELETE FROM GSBBRC_CourtDetail"
            _dbaccess.ExecuteScalar(_sql)

            _sqlcourtdetail = "INSERT INTO GSBBRC_CourtDetail("
            _sqlcourtdetail = _sqlcourtdetail & "redcaseno,refid,recno,casestatuscode,effectivedate,gazdate,"
            _sqlcourtdetail = _sqlcourtdetail & "gazbook,gazpart,gazpage,paymentduedate,paymentbackdate,paymentcheckdate"
            _sqlcourtdetail = _sqlcourtdetail & ") "
            _sqlcourtdetail = _sqlcourtdetail & "VALUES("
            _sqlcourtdetail = _sqlcourtdetail & "@redcaseno,@refid,@recno,@casestatuscode,@effectivedate,@gazdate,"
            _sqlcourtdetail = _sqlcourtdetail & "@gazbook,@gazpart,@gazpage,@paymentduedate,@paymentbackdate,@paymentcheckdate"
            _sqlcourtdetail = _sqlcourtdetail & ") "

            For Each _data In _courtdetaillists
                With _data
                    _paramcourtdetail(0) = New SQLServerDBParameter("@redcaseno", DBUtility.GetString(.RedCaseNo))
                    _paramcourtdetail(1) = New SQLServerDBParameter("@refid", DBUtility.GetString(.RefID))
                    _paramcourtdetail(2) = New SQLServerDBParameter("@recno", DBUtility.GetNumeric(.RecNo))
                    _paramcourtdetail(3) = New SQLServerDBParameter("@casestatuscode", DBUtility.GetNumeric(.CaseStatusCode))
                    _paramcourtdetail(4) = New SQLServerDBParameter("@effectivedate", DBUtility.GetString(.EffectiveDate))
                    _paramcourtdetail(5) = New SQLServerDBParameter("@gazdate", DBUtility.GetString(.GazDate))
                    _paramcourtdetail(6) = New SQLServerDBParameter("@gazbook", DBUtility.GetString(.GazBook))
                    _paramcourtdetail(7) = New SQLServerDBParameter("@gazpart", DBUtility.GetString(.GazPart))
                    _paramcourtdetail(8) = New SQLServerDBParameter("@gazpage", DBUtility.GetString(.GazPage))
                    _paramcourtdetail(9) = New SQLServerDBParameter("@paymentduedate", DBUtility.GetString(.PaymentDueDate))
                    _paramcourtdetail(10) = New SQLServerDBParameter("@paymentbackdate", DBUtility.GetString(.PaymentBackDate))
                    _paramcourtdetail(11) = New SQLServerDBParameter("@paymentcheckdate", DBUtility.GetString(.PaymentCheckDate))
                End With
                _dbaccess.ExecuteNonQuery(_sqlcourtdetail, _paramcourtdetail)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function InsertPrepare(_datafiles As DataFileLists, _courtlists As CourtLists, _courtdetaillists As CourtDetailLists) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _sqldatafile As String = ""
        Dim _sqlcourt As String = ""
        Dim _sqlcourtdetail As String = ""
        Dim _paramdatafile(9) As SQLServerDBParameter
        Dim _paramcourt(16) As SQLServerDBParameter
        Dim _paramcourtdetail(11) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess
        Dim _feeddate As String

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            _sql = "SELECT TOP 1 feeddate FROM GSBBRC_Court"
            _feeddate = _dbaccess.ExecuteScalar(_sql)
            If IsNothing(_feeddate) Then
                _feeddate = ""
            End If
            _dbaccess.BeginTransaction()

            '### Data Files ###
            _sqldatafile = "INSERT INTO GSBBRC_DataFile("
            _sqldatafile = _sqldatafile & "filename,feeddate,totalrecord,acceptrecord,rejectrecord,"
            _sqldatafile = _sqldatafile & "datestart,datefinish,filestatus,createddate,createdby"
            _sqldatafile = _sqldatafile & ") "
            _sqldatafile = _sqldatafile & "VALUES("
            _sqldatafile = _sqldatafile & "@filename,@feeddate,@totalrecord,@acceptrecord,@rejectrecord,"
            _sqldatafile = _sqldatafile & "@datestart,@datefinish,@filestatus,@createddate,@createdby"
            _sqldatafile = _sqldatafile & ")"

            For Each _data In _datafiles
                With _data
                    _paramdatafile(0) = New SQLServerDBParameter("@filename", DBUtility.GetString(.FileName))
                    _paramdatafile(1) = New SQLServerDBParameter("@feeddate", DBUtility.GetString(.FeedDate))
                    _paramdatafile(2) = New SQLServerDBParameter("@totalrecord", DBUtility.GetNumeric(.TotalRecord))
                    _paramdatafile(3) = New SQLServerDBParameter("@acceptrecord", DBUtility.GetNumeric(.AcceptRecord))
                    _paramdatafile(4) = New SQLServerDBParameter("@rejectrecord", DBUtility.GetNumeric(.RejectRecord))
                    _paramdatafile(5) = New SQLServerDBParameter("@datestart", .DateStart)
                    _paramdatafile(6) = New SQLServerDBParameter("@datefinish", .DateFinish)
                    _paramdatafile(7) = New SQLServerDBParameter("@filestatus", DBUtility.GetNumeric(.FileStatus))
                    _paramdatafile(8) = New SQLServerDBParameter("@createddate", .CreatedDate)
                    _paramdatafile(9) = New SQLServerDBParameter("@createdby", DBUtility.GetString(.CreatedBy))
                End With
                _dbaccess.ExecuteNonQuery(_sqldatafile, _paramdatafile)
            Next

            '### Court Master ###
            _sql = "INSERT INTO GSBBRC_CourtHistory "
            _sql = _sql & "SELECT * FROM GSBBRC_Court"
            _dbaccess.ExecuteScalar(_sql)

            _sql = "DELETE FROM GSBBRC_Court"
            _dbaccess.ExecuteScalar(_sql)

            _sqlcourt = "INSERT INTO GSBBRC_Court("
            _sqlcourt = _sqlcourt & "redcaseno,refid,recno,custname,custsurname,courtname,blackcaseno,"
            _sqlcourt = _sqlcourt & "blackcaseyear,redcaseyear,plaintiff,defendant,pbid,deptid,feeddate,bcourtrefid,bcourtname,bcourtsurname"
            _sqlcourt = _sqlcourt & ") "
            _sqlcourt = _sqlcourt & "VALUES("
            _sqlcourt = _sqlcourt & "@redcaseno1,@refid1,@recno1,@custname,@custsurname,@courtname,@blackcaseno,"
            _sqlcourt = _sqlcourt & "@blackcaseyear,@redcaseyear,@plaintiff,@defendant,@pbid,@deptid,@feeddate,@bcourtrefid,@bcourtname,@bcourtsurname"
            _sqlcourt = _sqlcourt & ")"

            _dbaccess.BeginPrepare(_sqlcourt)
            _paramcourt(0) = New SQLServerDBParameter("@redcaseno1", SqlDbType.VarChar, 15)
            _paramcourt(1) = New SQLServerDBParameter("@refid1", SqlDbType.VarChar, 15)
            _paramcourt(2) = New SQLServerDBParameter("@recno1", SqlDbType.Int, 0)
            _paramcourt(3) = New SQLServerDBParameter("@custname", SqlDbType.VarChar, 100)
            _paramcourt(4) = New SQLServerDBParameter("@custsurname", SqlDbType.VarChar, 100)
            _paramcourt(5) = New SQLServerDBParameter("@courtname", SqlDbType.VarChar, 60)
            _paramcourt(6) = New SQLServerDBParameter("@blackcaseno", SqlDbType.VarChar, 15)
            _paramcourt(7) = New SQLServerDBParameter("@blackcaseyear", SqlDbType.VarChar, 4)
            _paramcourt(8) = New SQLServerDBParameter("@redcaseyear", SqlDbType.VarChar, 4)
            _paramcourt(9) = New SQLServerDBParameter("@plaintiff", SqlDbType.VarChar, 600)
            _paramcourt(10) = New SQLServerDBParameter("@defendant", SqlDbType.VarChar, 600)
            _paramcourt(11) = New SQLServerDBParameter("@pbid", SqlDbType.VarChar, 1)
            _paramcourt(12) = New SQLServerDBParameter("@deptid", SqlDbType.Int, 0)
            _paramcourt(13) = New SQLServerDBParameter("@feeddate", SqlDbType.VarChar, 8)
            _paramcourt(14) = New SQLServerDBParameter("@bcourtrefid", SqlDbType.VarChar, 15)
            _paramcourt(15) = New SQLServerDBParameter("@bcourtname", SqlDbType.VarChar, 100)
            _paramcourt(16) = New SQLServerDBParameter("@bcourtsurname", SqlDbType.VarChar, 100)
            For Each _data In _courtlists
                With _data
                    _paramcourt(0).Values = DBUtility.GetString(.RedCaseNo)
                    _paramcourt(1).Values = DBUtility.GetString(.RefID)
                    _paramcourt(2).Values = DBUtility.GetNumeric(.RecNo)
                    _paramcourt(3).Values = DBUtility.GetString(.CustName)
                    _paramcourt(4).Values = DBUtility.GetString(.CustSurname)
                    _paramcourt(5).Values = DBUtility.GetString(.CourtName)
                    _paramcourt(6).Values = DBUtility.GetString(.BlackCaseNo)
                    _paramcourt(7).Values = DBUtility.GetString(.BlackCaseYear)
                    _paramcourt(8).Values = DBUtility.GetString(.RedCaseYear)
                    _paramcourt(9).Values = DBUtility.GetString(.Plaintiff)
                    _paramcourt(10).Values = DBUtility.GetString(.Defendant)
                    _paramcourt(11).Values = DBUtility.GetString(.PbID)
                    _paramcourt(12).Values = DBUtility.GetNumeric(.DeptID)
                    _paramcourt(13).Values = DBUtility.GetString(.Feeddate)
                    _paramcourt(14).Values = DBUtility.GetString(.BCourtRefID)
                    _paramcourt(15).Values = DBUtility.GetString(.BCourtName)
                    _paramcourt(16).Values = DBUtility.GetString(.BCourtSurname)
                End With
                '_dbaccess.ExecuteNonQuery(_sqlcourt, _paramcourt)
                _dbaccess.ExecutePrepare(_paramcourt)
            Next


            '### Court Detail ###
            _sql = "INSERT INTO GSBBRC_CourtDetailHistory "
            _sql = _sql & "SELECT *,'" & _feeddate & "' FROM GSBBRC_CourtDetail"
            _dbaccess.ExecuteScalar(_sql)

            _sql = "DELETE FROM GSBBRC_CourtDetail"
            _dbaccess.ExecuteScalar(_sql)

            _sqlcourtdetail = "INSERT INTO GSBBRC_CourtDetail("
            _sqlcourtdetail = _sqlcourtdetail & "redcaseno,refid,recno,casestatuscode,effectivedate,gazdate,"
            _sqlcourtdetail = _sqlcourtdetail & "gazbook,gazpart,gazpage,paymentduedate,paymentbackdate,paymentcheckdate"
            _sqlcourtdetail = _sqlcourtdetail & ") "
            _sqlcourtdetail = _sqlcourtdetail & "VALUES("
            _sqlcourtdetail = _sqlcourtdetail & "@redcaseno2,@refid2,@recno2,@casestatuscode,@effectivedate,@gazdate,"
            _sqlcourtdetail = _sqlcourtdetail & "@gazbook,@gazpart,@gazpage,@paymentduedate,@paymentbackdate,@paymentcheckdate"
            _sqlcourtdetail = _sqlcourtdetail & ") "

            _dbaccess.BeginPrepare(_sqlcourtdetail)
            _paramcourtdetail(0) = New SQLServerDBParameter("@redcaseno2", SqlDbType.VarChar, 15)
            _paramcourtdetail(1) = New SQLServerDBParameter("@refid2", SqlDbType.VarChar, 15)
            _paramcourtdetail(2) = New SQLServerDBParameter("@recno2", SqlDbType.Int, 0)
            _paramcourtdetail(3) = New SQLServerDBParameter("@casestatuscode", SqlDbType.SmallInt, 0)
            _paramcourtdetail(4) = New SQLServerDBParameter("@effectivedate", SqlDbType.VarChar, 8)
            _paramcourtdetail(5) = New SQLServerDBParameter("@gazdate", SqlDbType.VarChar, 8)
            _paramcourtdetail(6) = New SQLServerDBParameter("@gazbook", SqlDbType.VarChar, 10)
            _paramcourtdetail(7) = New SQLServerDBParameter("@gazpart", SqlDbType.VarChar, 10)
            _paramcourtdetail(8) = New SQLServerDBParameter("@gazpage", SqlDbType.VarChar, 4)
            _paramcourtdetail(9) = New SQLServerDBParameter("@paymentduedate", SqlDbType.VarChar, 8)
            _paramcourtdetail(10) = New SQLServerDBParameter("@paymentbackdate", SqlDbType.VarChar, 8)
            _paramcourtdetail(11) = New SQLServerDBParameter("@paymentcheckdate", SqlDbType.VarChar, 8)

            For Each _data In _courtdetaillists
                With _data
                    _paramcourtdetail(0).Values = DBUtility.GetString(.RedCaseNo)
                    _paramcourtdetail(1).Values = DBUtility.GetString(.RefID)
                    _paramcourtdetail(2).Values = DBUtility.GetNumeric(.RecNo)
                    _paramcourtdetail(3).Values = DBUtility.GetNumeric(.CaseStatusCode)
                    _paramcourtdetail(4).Values = DBUtility.GetString(.EffectiveDate)
                    _paramcourtdetail(5).Values = DBUtility.GetString(.GazDate)
                    _paramcourtdetail(6).Values = DBUtility.GetString(.GazBook)
                    _paramcourtdetail(7).Values = DBUtility.GetString(.GazPart)
                    _paramcourtdetail(8).Values = DBUtility.GetString(.GazPage)
                    _paramcourtdetail(9).Values = DBUtility.GetString(.PaymentDueDate)
                    _paramcourtdetail(10).Values = DBUtility.GetString(.PaymentBackDate)
                    _paramcourtdetail(11).Values = DBUtility.GetString(.PaymentCheckDate)
                End With
                '_dbaccess.ExecuteNonQuery(_sqlcourtdetail, _paramcourtdetail)
                _dbaccess.ExecutePrepare(_paramcourtdetail)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As CourtEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(14) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            'redno,refid,recno,custname,custsurname,courtname,blackno,
            'blackyear,redyear,plaintiff,defendant,custno,pbid,deptid,feeddate

            _sql = "UPDATE GSBBRC_Court SET "
            _sql = _sql & "redcaseno=@redcaseno,recno=@recno,custname=@custname,"
            _sql = _sql & "custsurname=@custsurname,courtname=@courtname,blackcaseno=@blackcaseno,"
            _sql = _sql & "blackcaseyear=@blackcaseyear,redcaseyear=@redcaseyear,plaintiff=@plaintiff,defendant=@defendant,"
            _sql = _sql & "custno=@custno,pbid=@pbid,deptid=@deptid,feeddate=@feeddate "
            _sql = _sql & "WHERE refid=@refid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@redcaseno", .RedCaseNo)
                _param(1) = New SQLServerDBParameter("@recno", .RecNo)
                _param(2) = New SQLServerDBParameter("@custname", .CustName)
                _param(3) = New SQLServerDBParameter("@custsurname", .CustSurname)
                _param(4) = New SQLServerDBParameter("@courtname", .CourtName)
                _param(5) = New SQLServerDBParameter("@blackcaseno", .BlackCaseNo)
                _param(6) = New SQLServerDBParameter("@blackcaseyear", .BlackCaseYear)
                _param(7) = New SQLServerDBParameter("@redcaseyear", .RedCaseYear)
                _param(8) = New SQLServerDBParameter("@plaintiff", .Plaintiff)
                _param(9) = New SQLServerDBParameter("@defendant", .Defendant)
                _param(10) = New SQLServerDBParameter("@custno", .CustNo)
                _param(11) = New SQLServerDBParameter("@pbid", .PbID)
                _param(12) = New SQLServerDBParameter("@deptid", .DeptID)
                _param(13) = New SQLServerDBParameter("@feeddate", .Feeddate)
                _param(14) = New SQLServerDBParameter("@refid", .RefID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtAccess", "Update()", ex.Message)
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
            _sql = "DELETE FROM GSBBRC_Court WHERE refid='" & _datacode & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtAccess", "Delete()", ex.Message)
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
