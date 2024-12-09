Public Class CourtBankAccess
#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(ByVal _redCodeNo As String) As CourtBankEntity
        Dim _result As New CourtBankEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtBank WHERE redcaseno='" & _redCodeNo & "'"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .Title = UtilOfValidate.CheckString(_dbaccess.GetItem("title"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .AccountNo = UtilOfValidate.CheckString(_dbaccess.GetItem("accountno"))
                    .BirthDate = UtilOfValidate.CheckString(_dbaccess.GetItem("birthdate"))
                    .CourtFee = _dbaccess.GetItem("courtfee")
                    .SequestrateAction = _dbaccess.GetItem("sequestrateaction")
                    .CourtRetainer = _dbaccess.GetItem("courtretainer")
                    .ResultID = _dbaccess.GetItem("resultid")
                    .CauseID = _dbaccess.GetItem("causeid")
                    .DefaultDate = UtilOfValidate.CheckString(_dbaccess.GetItem("defaultdate"))
                    .SequestrateDate = UtilOfValidate.CheckString(_dbaccess.GetItem("sequestratedate"))
                    .SellAssetDate = UtilOfValidate.CheckString(_dbaccess.GetItem("sellassetdate"))
                    .RepayDate = UtilOfValidate.CheckString(_dbaccess.GetItem("repaydate"))
                    .LegalDate = UtilOfValidate.CheckString(_dbaccess.GetItem("legaldate"))
                    .SellAssetResult = _dbaccess.GetItem("sellassetresult")
                    .HaveAsset = _dbaccess.GetItem("haveasset")
                    .JusticeDate = UtilOfValidate.CheckString(_dbaccess.GetItem("justicedate"))
                    .ReceivePaymentDate = UtilOfValidate.CheckString(_dbaccess.GetItem("receivepaymentdate"))
                    .Collateral = _dbaccess.GetItem("collateral")
                    .ApplicationPayment = _dbaccess.GetItem("applicationpayment")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                    .Recorder = _dbaccess.GetItem("recorder")
                    .Approver = _dbaccess.GetItem("approver")
                    .ApprovedDate = UtilOfValidate.CheckString(_dbaccess.GetItem("approveddate"))
                    .IsApproved = _dbaccess.GetItem("isapproved")
                    .CourtFlag = _dbaccess.GetItem("courtflag")
                    .DebtorStatusID = _dbaccess.GetItem("debtorstatusid")
                    .Comment = _dbaccess.GetItem("Comment")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtBankAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoGroupRefIdByCourtFlagList(ByVal _courtFlag As Integer, ByVal _userid As Integer) As CourtBankLists
        Dim _result As New CourtBankLists
        Dim _status As CourtBankEntity
        Dim _sql As String = ""
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            If _courtFlag = 1 Then
                _sql = "SELECT DISTINCT redcaseno,refid,title,custname,custsurname,courtflag,isapproved,'' as casestatusdesc "
                _sql = _sql & "FROM GSBBRC_CourtBank "
                _sql = _sql & "WHERE courtflag=1"
                _sql = _sql & " AND (isapproved = 0 OR isapproved = -1 )"
                _sql = _sql & " AND recorder IN ("
                _sql = _sql & "  SELECT distinct a.recorder"
                _sql = _sql & "  FROM GSBBRC_CourtBank a,GSBBRC_User b"
                _sql = _sql & "  WHERE a.recorder=b.userid AND "
                _sql = _sql & "   b.deptid = (SELECT deptid FROM GSBBRC_User WHERE userid=" & _userid & ") "
                _sql = _sql & ") "
                _sql = _sql & "Group By RedCaseNo,RefID,Title,CustName,CustSurname,CourtFlag,IsApproved"
            End If

            If _courtFlag = 2 Then
                _sql = "SELECT DISTINCT b.*,c.casestatusdesc,d.title,d.custname,d.custsurname,d.courtflag "
                _sql = _sql & "FROM GSBBRC_CourtDetail a, "
                _sql = _sql & "("
                _sql = _sql & " SELECT a.redcaseno,a.refid,max(b.effectivedate) as effectivedate"
                _sql = _sql & " FROM GSBBRC_CourtBank a, GSBBRC_CourtDetail b"
                _sql = _sql & " WHERE a.redcaseno=b.redcaseno AND a.refid=b.refid AND a.courtflag=2"
                _sql = _sql & "   AND (a.isapproved = 0 OR a.isapproved = -1 )"
                _sql = _sql & " GROUP BY a.redcaseno,a.refid"
                _sql = _sql & ") b, "
                '_sql = _sql & "("
                '_sql = _sql & " SELECT a.redcaseno,a.refid,b.recno,max(b.effectivedate) as effectivedate"
                '_sql = _sql & " FROM GSBBRC_CourtBank a, GSBBRC_CourtDetail b"
                '_sql = _sql & " WHERE a.redcaseno=b.redcaseno AND a.refid=b.refid AND a.courtflag=2"
                '_sql = _sql & "   AND (a.isapproved = 0 OR a.isapproved = -1 )"
                '_sql = _sql & " GROUP BY a.redcaseno,a.refid,b.recno"
                '_sql = _sql & ") b, "
                _sql = _sql & "GSBBRC_CaseStatus c,"
                _sql = _sql & "GSBBRC_CourtBank d "
                _sql = _sql & "WHERE"
                _sql = _sql & " a.redcaseno=b.redcaseno AND a.refid=b.refid AND"
                _sql = _sql & " a.effectivedate=b.effectivedate AND"
                '_sql = _sql & " a.recno=b.recno AND a.effectivedate=b.effectivedate AND"
                _sql = _sql & " a.casestatuscode=c.casestatuscode AND"
                _sql = _sql & " b.redcaseno=d.redcaseno AND b.refid=d.refid AND"
                _sql = _sql & " d.Recorder IN"
                _sql = _sql & " ("
                _sql = _sql & "  SELECT distinct a.recorder"
                _sql = _sql & "  FROM GSBBRC_CourtBank a,GSBBRC_User b"
                _sql = _sql & "  WHERE a.recorder=b.userid AND "
                _sql = _sql & "   b.deptid = (SELECT deptid FROM GSBBRC_User WHERE userid=" & _userid & ") "
                _sql = _sql & " )"
                _sql = _sql & "ORDER BY c.casestatusdesc"
            End If

            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _status = New CourtBankEntity
                With _status
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .Title = UtilOfValidate.CheckString(_dbaccess.GetItem("title"))
                    .CustName = _dbaccess.GetItem("custname")
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .CourtFlag = _dbaccess.GetItem("courtflag")
                    .AccountNo = _dbaccess.GetItem("casestatusdesc") 'ให้ file account แทน casestatusdesc
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtBankAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists(ByVal _redCaseNo As String, ByVal _refId As String) As CourtBankLists
        Dim _result As New CourtBankLists
        Dim _status As CourtBankEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtBank WHERE redcaseno='" & _redCaseNo & "' AND refid = '" & _refId & "' ORDER BY redcaseno"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CourtBankEntity
                With _status
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .Title = UtilOfValidate.CheckString(_dbaccess.GetItem("title"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .AccountNo = UtilOfValidate.CheckString(_dbaccess.GetItem("accountno"))
                    .BirthDate = UtilOfValidate.CheckString(_dbaccess.GetItem("birthdate"))
                    .CourtFee = _dbaccess.GetItem("courtfee")
                    .SequestrateAction = _dbaccess.GetItem("sequestrateaction")
                    .CourtRetainer = _dbaccess.GetItem("courtretainer")
                    .ResultID = _dbaccess.GetItem("resultid")
                    .CauseID = _dbaccess.GetItem("causeid")
                    .DefaultDate = UtilOfValidate.CheckString(_dbaccess.GetItem("defaultdate"))
                    .SequestrateDate = UtilOfValidate.CheckString(_dbaccess.GetItem("sequestratedate"))
                    .SellAssetDate = UtilOfValidate.CheckString(_dbaccess.GetItem("sellassetdate"))
                    .RepayDate = UtilOfValidate.CheckString(_dbaccess.GetItem("repaydate"))
                    .LegalDate = UtilOfValidate.CheckString(_dbaccess.GetItem("legaldate"))
                    .SellAssetResult = _dbaccess.GetItem("sellassetresult")
                    .HaveAsset = _dbaccess.GetItem("haveasset")
                    .JusticeDate = UtilOfValidate.CheckString(_dbaccess.GetItem("justicedate"))
                    .ReceivePaymentDate = UtilOfValidate.CheckString(_dbaccess.GetItem("receivepaymentdate"))
                    .Collateral = _dbaccess.GetItem("collateral")
                    .ApplicationPayment = _dbaccess.GetItem("applicationpayment")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                    .Recorder = _dbaccess.GetItem("recorder")
                    .Approver = _dbaccess.GetItem("approver")
                    .ApprovedDate = UtilOfValidate.CheckString(_dbaccess.GetItem("approveddate"))
                    .IsApproved = _dbaccess.GetItem("isapproved")
                    .CourtFlag = _dbaccess.GetItem("courtflag")
                    .DebtorStatusID = _dbaccess.GetItem("debtorstatusid")
                    .Comment = UtilOfValidate.CheckString(_dbaccess.GetItem("comment"))
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtBankAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As CourtBankLists
        Dim _result As New CourtBankLists
        Dim _status As CourtBankEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_CourtBank ORDER BY redno"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _status = New CourtBankEntity
                With _status
                    .RedCaseNo = _dbaccess.GetItem("redcaseno")
                    .RefID = _dbaccess.GetItem("refid")
                    .Title = UtilOfValidate.CheckString(_dbaccess.GetItem("title"))
                    .CustName = UtilOfValidate.CheckString(_dbaccess.GetItem("custname"))
                    .CustSurname = UtilOfValidate.CheckString(_dbaccess.GetItem("custsurname"))
                    .AccountNo = UtilOfValidate.CheckString(_dbaccess.GetItem("accountno"))
                    .BirthDate = UtilOfValidate.CheckString(_dbaccess.GetItem("birthdate"))
                    .CourtFee = _dbaccess.GetItem("courtfee")
                    .SequestrateAction = UtilOfValidate.CheckString(_dbaccess.GetItem("sequestrateaction"))
                    .CourtRetainer = _dbaccess.GetItem("courtretainer")
                    .ResultID = _dbaccess.GetItem("resultid")
                    .CauseID = _dbaccess.GetItem("causeid")
                    .DefaultDate = UtilOfValidate.CheckString(_dbaccess.GetItem("defaultdate"))
                    .SequestrateDate = UtilOfValidate.CheckString(_dbaccess.GetItem("sequestratedate"))
                    .SellAssetDate = UtilOfValidate.CheckString(_dbaccess.GetItem("sellassetdate"))
                    .RepayDate = UtilOfValidate.CheckString(_dbaccess.GetItem("repaydate"))
                    .LegalDate = UtilOfValidate.CheckString(_dbaccess.GetItem("legaldate"))
                    .SellAssetResult = _dbaccess.GetItem("sellassetresult")
                    .HaveAsset = _dbaccess.GetItem("haveasset")
                    .JusticeDate = UtilOfValidate.CheckString(_dbaccess.GetItem("justicedate"))
                    .ReceivePaymentDate = UtilOfValidate.CheckString(_dbaccess.GetItem("receivepaymentdate"))
                    .Collateral = _dbaccess.GetItem("collateral")
                    .ApplicationPayment = _dbaccess.GetItem("applicationpayment")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                    .Recorder = _dbaccess.GetItem("recorder")
                    .Approver = _dbaccess.GetItem("approver")
                    .ApprovedDate = UtilOfValidate.CheckString(_dbaccess.GetItem("approveddate"))
                    .IsApproved = _dbaccess.GetItem("isapproved")
                    .CourtFlag = _dbaccess.GetItem("courtflag")
                    .DebtorStatusID = _dbaccess.GetItem("DebtorStatusID")
                    .Comment = _dbaccess.GetItem("Comment")
                End With
                _result.Add(_status)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtBankAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(ByVal _data As CourtBankEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(29) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_CourtBank("
            _sql = _sql & "redcaseno,refid,accountno,birthdate,courtfee,sequestrateaction,"
            _sql = _sql & "courtretainer,resultid,causeid,defaultdate,sequestratedate,sellassetdate,"
            _sql = _sql & "repaydate,legaldate,sellassetresult,haveasset,justicedate,receivepaymentdate,collateral,"
            _sql = _sql & "applicationpayment,createddate,modifieddate,recorder,approver,approveddate,isapproved,courtflag,custname,custsurname,title"
            _sql = _sql & ",debtorstatusid,comment"
            _sql = _sql & ") "
            _sql = _sql & "VALUES("
            _sql = _sql & "@redcaseno,@refid,@accountno,@birthdate,@courtfee,@sequestrateaction"
            _sql = _sql & "@courtretainer,@resultid,@causeid,@defaultdate,@sequestratedate,@sellassetdate"
            _sql = _sql & "@repaydate,@legaldate,@sellassetresult,@haveasset,@justicedate,@receivepaymentdate,@collateral"
            _sql = _sql & "@applicationpayment,getdate(),getdate(),@recorder,@approver,@approveddate,@isapproved,@courtflag,@custname,@custsurname,@title,@debtorstatusid,@comment"
            _sql = _sql & ")"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@redcaseno", DBUtility.GetString(.RedCaseNo))
                _param(1) = New SQLServerDBParameter("@refid", DBUtility.GetString(.RefID))
                _param(2) = New SQLServerDBParameter("@accountno", DBUtility.GetString(.AccountNo))
                _param(3) = New SQLServerDBParameter("@birthdate", DBUtility.GetString(.BirthDate))
                _param(4) = New SQLServerDBParameter("@courtfee", DBUtility.GetNumeric(.CourtFee))
                _param(5) = New SQLServerDBParameter("@sequestrateaction", DBUtility.GetBoolean(.SequestrateAction))
                _param(6) = New SQLServerDBParameter("@courtretainer", DBUtility.GetNumeric(.CourtRetainer))
                _param(7) = New SQLServerDBParameter("@resultid", DBUtility.GetNumeric(.ResultID))
                _param(8) = New SQLServerDBParameter("@causeid", DBUtility.GetNumeric(.CauseID))
                _param(9) = New SQLServerDBParameter("@defaultdate", DBUtility.GetString(.DefaultDate))
                _param(10) = New SQLServerDBParameter("@sequestratedate", DBUtility.GetString(.SequestrateDate))
                _param(11) = New SQLServerDBParameter("@sellassetdate", DBUtility.GetString(.SellAssetDate))
                _param(12) = New SQLServerDBParameter("@repaydate", DBUtility.GetString(.RepayDate))
                _param(13) = New SQLServerDBParameter("@legaldate", DBUtility.GetString(.LegalDate))
                _param(14) = New SQLServerDBParameter("@sellassetresult", DBUtility.GetBoolean(.SellAssetResult))
                _param(15) = New SQLServerDBParameter("@haveasset", DBUtility.GetBoolean(.HaveAsset))
                _param(16) = New SQLServerDBParameter("@justicedate", DBUtility.GetString(.JusticeDate))
                _param(17) = New SQLServerDBParameter("@receivepaymentdate", DBUtility.GetString(.ReceivePaymentDate))
                _param(18) = New SQLServerDBParameter("@collateral", DBUtility.GetBoolean(.Collateral))
                _param(19) = New SQLServerDBParameter("@applicationpayment", DBUtility.GetBoolean(.ApplicationPayment))
                '_param(20) = New SQLServerDBParameter("@createddate", .CreatedDate)
                '_param(21) = New SQLServerDBParameter("@modifieddate", .ModifiedDate)
                _param(20) = New SQLServerDBParameter("@recorder", DBUtility.GetNumeric(.Recorder))
                _param(21) = New SQLServerDBParameter("@approver", DBUtility.GetNumeric(.Approver))
                _param(22) = New SQLServerDBParameter("@approveddate", DBUtility.GetString(.ApprovedDate))
                _param(23) = New SQLServerDBParameter("@isapproved", DBUtility.GetBoolean(.IsApproved))
                _param(24) = New SQLServerDBParameter("@courtflag", DBUtility.GetNumeric(.CourtFlag))
                _param(25) = New SQLServerDBParameter("@custname", DBUtility.GetString(.CustName))
                _param(26) = New SQLServerDBParameter("@custsurname", DBUtility.GetString(.CustSurname))
                _param(27) = New SQLServerDBParameter("@title", DBUtility.GetString(.Title))
                _param(28) = New SQLServerDBParameter("@debtorstatusid", .DebtorStatusID)
                _param(29) = New SQLServerDBParameter("@comment", DBUtility.GetString(.Comment))
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtBankAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(ByVal _datalist As CourtBankLists) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(29) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_CourtBank("
            _sql = _sql & "redcaseno,refid,accountno,birthdate,courtfee,sequestrateaction,"
            _sql = _sql & "courtretainer,resultid,causeid,defaultdate,sequestratedate,sellassetdate,"
            _sql = _sql & "repaydate,legaldate,sellassetresult,haveasset,justicedate,receivepaymentdate,collateral,"
            _sql = _sql & "applicationpayment,createddate,modifieddate,recorder,approver,approveddate,isapproved,courtflag,custname,custsurname,title "
            _sql = _sql & ",debtorstatusid,comment"
            _sql = _sql & ") "
            _sql = _sql & "VALUES("
            _sql = _sql & "@redcaseno,@refid,@accountno,@birthdate,@courtfee,@sequestrateaction,"
            _sql = _sql & "@courtretainer,@resultid,@causeid,@defaultdate,@sequestratedate,@sellassetdate,"
            _sql = _sql & "@repaydate,@legaldate,@sellassetresult,@haveasset,@justicedate,@receivepaymentdate,@collateral,"
            _sql = _sql & "@applicationpayment,getdate(),getdate(),@recorder,@approver,@approveddate,@isapproved,@courtflag,@custname,@custsurname,@title,@debtorstatusid,@comment"
            _sql = _sql & ")"

            _dbaccess.BeginTransaction()

            For Each _data In _datalist
                With _data
                    _param(0) = New SQLServerDBParameter("@redcaseno", DBUtility.GetString(.RedCaseNo))
                    _param(1) = New SQLServerDBParameter("@refid", DBUtility.GetString(.RefID))
                    _param(2) = New SQLServerDBParameter("@accountno", DBUtility.GetString(.AccountNo))
                    _param(3) = New SQLServerDBParameter("@birthdate", DBUtility.GetString(.BirthDate))
                    _param(4) = New SQLServerDBParameter("@courtfee", DBUtility.GetNumeric(.CourtFee))
                    _param(5) = New SQLServerDBParameter("@sequestrateaction", DBUtility.GetBoolean(.SequestrateAction))
                    _param(6) = New SQLServerDBParameter("@courtretainer", DBUtility.GetNumeric(.CourtRetainer))
                    _param(7) = New SQLServerDBParameter("@resultid", DBUtility.GetNumeric(.ResultID))
                    _param(8) = New SQLServerDBParameter("@causeid", DBUtility.GetNumeric(.CauseID))
                    _param(9) = New SQLServerDBParameter("@defaultdate", DBUtility.GetString(.DefaultDate))
                    _param(10) = New SQLServerDBParameter("@sequestratedate", DBUtility.GetString(.SequestrateDate))
                    _param(11) = New SQLServerDBParameter("@sellassetdate", DBUtility.GetString(.SellAssetDate))
                    _param(12) = New SQLServerDBParameter("@repaydate", DBUtility.GetString(.RepayDate))
                    _param(13) = New SQLServerDBParameter("@legaldate", DBUtility.GetString(.LegalDate))
                    _param(14) = New SQLServerDBParameter("@sellassetresult", DBUtility.GetBoolean(.SellAssetResult))
                    _param(15) = New SQLServerDBParameter("@haveasset", DBUtility.GetBoolean(.HaveAsset))
                    _param(16) = New SQLServerDBParameter("@justicedate", DBUtility.GetString(.JusticeDate))
                    _param(17) = New SQLServerDBParameter("@receivepaymentdate", DBUtility.GetString(.ReceivePaymentDate))
                    _param(18) = New SQLServerDBParameter("@collateral", DBUtility.GetBoolean(.Collateral))
                    _param(19) = New SQLServerDBParameter("@applicationpayment", DBUtility.GetBoolean(.ApplicationPayment))
                    '_param(20) = New SQLServerDBParameter("@createddate", .CreatedDate)
                    '_param(21) = New SQLServerDBParameter("@modifieddate", .ModifiedDate)
                    _param(20) = New SQLServerDBParameter("@recorder", DBUtility.GetNumeric(.Recorder))
                    _param(21) = New SQLServerDBParameter("@approver", DBUtility.GetNumeric(.Approver))
                    _param(22) = New SQLServerDBParameter("@approveddate", DBUtility.GetString(.ApprovedDate))
                    _param(23) = New SQLServerDBParameter("@isapproved", DBUtility.GetBoolean(.IsApproved))
                    _param(24) = New SQLServerDBParameter("@courtflag", DBUtility.GetNumeric(.CourtFlag))
                    _param(25) = New SQLServerDBParameter("@custname", DBUtility.GetString(.CustName))
                    _param(26) = New SQLServerDBParameter("@custsurname", DBUtility.GetString(.CustSurname))
                    _param(27) = New SQLServerDBParameter("@title", DBUtility.GetString(.Title))
                    _param(28) = New SQLServerDBParameter("@debtorstatusid", .DebtorStatusID)
                    _param(29) = New SQLServerDBParameter("@comment", DBUtility.GetString(.Comment))

                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtBankAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(ByVal _datalist As CourtBankLists, ByVal _redCaseNo As String, ByVal _refId As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(29) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "DELETE FROM GSBBRC_CourtBank WHERE redcaseno='" & _redCaseNo & "' AND refid='" & _refId & "'"
            _dbaccess.ExecuteNonQuery(_sql)

            _sql = "INSERT INTO GSBBRC_CourtBank("
            _sql = _sql & "redcaseno,refid,accountno,birthdate,courtfee,sequestrateaction,"
            _sql = _sql & "courtretainer,resultid,causeid,defaultdate,sequestratedate,sellassetdate,"
            _sql = _sql & "repaydate,legaldate,sellassetresult,haveasset,justicedate,receivepaymentdate,collateral,"
            _sql = _sql & "applicationpayment,createddate,modifieddate,recorder,approver,approveddate,isapproved,courtflag,custname,custsurname,title "
            _sql = _sql & ",debtorstatusid,comment"
            _sql = _sql & ") "
            _sql = _sql & "VALUES("
            _sql = _sql & "@redcaseno,@refid,@accountno,@birthdate,@courtfee,@sequestrateaction,"
            _sql = _sql & "@courtretainer,@resultid,@causeid,@defaultdate,@sequestratedate,@sellassetdate,"
            _sql = _sql & "@repaydate,@legaldate,@sellassetresult,@haveasset,@justicedate,@receivepaymentdate,@collateral,"
            _sql = _sql & "@applicationpayment,getdate(),getdate(),@recorder,@approver,@approveddate,@isapproved,@courtflag,@custname,@custsurname,@title,@debtorstatusid,@comment"
            _sql = _sql & ")"

            For Each _data In _datalist
                With _data
                    _param(0) = New SQLServerDBParameter("@redcaseno", DBUtility.GetString(.RedCaseNo))
                    _param(1) = New SQLServerDBParameter("@refid", DBUtility.GetString(.RefID))
                    _param(2) = New SQLServerDBParameter("@accountno", DBUtility.GetString(.AccountNo))
                    _param(3) = New SQLServerDBParameter("@birthdate", DBUtility.GetString(.BirthDate))
                    _param(4) = New SQLServerDBParameter("@courtfee", DBUtility.GetNumeric(.CourtFee))
                    _param(5) = New SQLServerDBParameter("@sequestrateaction", DBUtility.GetBoolean(.SequestrateAction))
                    _param(6) = New SQLServerDBParameter("@courtretainer", DBUtility.GetNumeric(.CourtRetainer))
                    _param(7) = New SQLServerDBParameter("@resultid", DBUtility.GetNumeric(.ResultID))
                    _param(8) = New SQLServerDBParameter("@causeid", DBUtility.GetNumeric(.CauseID))
                    _param(9) = New SQLServerDBParameter("@defaultdate", DBUtility.GetString(.DefaultDate))
                    _param(10) = New SQLServerDBParameter("@sequestratedate", DBUtility.GetString(.SequestrateDate))
                    _param(11) = New SQLServerDBParameter("@sellassetdate", DBUtility.GetString(.SellAssetDate))
                    _param(12) = New SQLServerDBParameter("@repaydate", DBUtility.GetString(.RepayDate))
                    _param(13) = New SQLServerDBParameter("@legaldate", DBUtility.GetString(.LegalDate))
                    _param(14) = New SQLServerDBParameter("@sellassetresult", DBUtility.GetBoolean(.SellAssetResult))
                    _param(15) = New SQLServerDBParameter("@haveasset", DBUtility.GetBoolean(.HaveAsset))
                    _param(16) = New SQLServerDBParameter("@justicedate", DBUtility.GetString(.JusticeDate))
                    _param(17) = New SQLServerDBParameter("@receivepaymentdate", DBUtility.GetString(.ReceivePaymentDate))
                    _param(18) = New SQLServerDBParameter("@collateral", DBUtility.GetBoolean(.Collateral))
                    _param(19) = New SQLServerDBParameter("@applicationpayment", DBUtility.GetBoolean(.ApplicationPayment))
                    '_param(20) = New SQLServerDBParameter("@createddate", .CreatedDate)
                    '_param(21) = New SQLServerDBParameter("@modifieddate", .ModifiedDate)
                    _param(20) = New SQLServerDBParameter("@recorder", DBUtility.GetNumeric(.Recorder))
                    _param(21) = New SQLServerDBParameter("@approver", DBUtility.GetNumeric(.Approver))
                    _param(22) = New SQLServerDBParameter("@approveddate", DBUtility.GetString(.ApprovedDate))
                    _param(23) = New SQLServerDBParameter("@isapproved", DBUtility.GetBoolean(.IsApproved))
                    _param(24) = New SQLServerDBParameter("@courtflag", DBUtility.GetNumeric(.CourtFlag))
                    _param(25) = New SQLServerDBParameter("@custname", DBUtility.GetString(.CustName))
                    _param(26) = New SQLServerDBParameter("@custsurname", DBUtility.GetString(.CustSurname))
                    _param(27) = New SQLServerDBParameter("@title", DBUtility.GetString(.Title))
                    _param(28) = New SQLServerDBParameter("@debtorstatusid", .DebtorStatusID)
                    _param(29) = New SQLServerDBParameter("@comment", DBUtility.GetString(.Comment))

                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtBankAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(ByVal _data As CourtBankEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(29) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_CourtBank SET "
            _sql = _sql & "birthdate=@birthdate,courtfee=@courtfee,sequestrateaction=@sequestrateaction,"
            _sql = _sql & "courtretainer=@courtretainer,resultid=@resultid,causeid=@causeid,defaultdate=@defaultdate,sequestratedate=@sequestratedate,sellassetdate=@sellassetdate,"
            _sql = _sql & "repaydate=@repaydate,legaldate=@legaldate,sellassetresult=@sellassetresult,haveasset=@haveasset,justicedate=@justicedate,receivepaymentdate=@receivepaymentdate,collateral=@collateral,"
            _sql = _sql & "applicationpayment=@applicationpayment,modifieddate=getdate(),recorder=@recorder,approver=@approver,approveddate=@approveddate,isapproved=@isapproved,courtflag=@courtflag,custname=@custname,custsurname=@custsurname,title=@title "
            _sql = _sql & ",debtorstatusid=@debtorstatusid,comment=@comment"
            _sql = _sql & "WHERE redcaseno=@redcaseno AND refid=@refid"

            _dbaccess.BeginTransaction()

            With _data
                _param(0) = New SQLServerDBParameter("@accountno", .AccountNo)
                _param(1) = New SQLServerDBParameter("@birthdate", DBUtility.GetString(.BirthDate))
                _param(2) = New SQLServerDBParameter("@courtfee", .CourtFee)
                _param(3) = New SQLServerDBParameter("@sequestrateaction", DBUtility.GetBoolean(.SequestrateAction))
                _param(4) = New SQLServerDBParameter("@courtretainer", .CourtRetainer)
                _param(5) = New SQLServerDBParameter("@resultid", .ResultID)
                _param(6) = New SQLServerDBParameter("@causeid", .CauseID)
                _param(7) = New SQLServerDBParameter("@defaultdate", DBUtility.GetString(.DefaultDate))
                _param(8) = New SQLServerDBParameter("@sequestratedate", DBUtility.GetString(.SequestrateDate))
                _param(9) = New SQLServerDBParameter("@sellassetdate", DBUtility.GetString(.SellAssetDate))
                _param(10) = New SQLServerDBParameter("@repaydate", DBUtility.GetString(.RepayDate))
                _param(11) = New SQLServerDBParameter("@legaldate", DBUtility.GetString(.LegalDate))
                _param(12) = New SQLServerDBParameter("@sellassetresult", DBUtility.GetBoolean(.SellAssetResult))
                _param(13) = New SQLServerDBParameter("@haveasset", DBUtility.GetBoolean(.HaveAsset))
                _param(14) = New SQLServerDBParameter("@justicedate", DBUtility.GetString(.JusticeDate))
                _param(15) = New SQLServerDBParameter("@receivepaymentdate", DBUtility.GetString(.ReceivePaymentDate))
                _param(16) = New SQLServerDBParameter("@collateral", DBUtility.GetBoolean(.Collateral))
                _param(17) = New SQLServerDBParameter("@applicationpayment", DBUtility.GetBoolean(.ApplicationPayment))
                '_param(18) = New SQLServerDBParameter("@createddate", .CreatedDate)
                '_param(19) = New SQLServerDBParameter("@modifieddate", .ModifiedDate)
                _param(18) = New SQLServerDBParameter("@recorder", .Recorder)
                _param(19) = New SQLServerDBParameter("@approver", .Approver)
                _param(20) = New SQLServerDBParameter("@approveddate", DBUtility.GetString(.ApprovedDate))
                _param(21) = New SQLServerDBParameter("@isapproved", DBUtility.GetBoolean(.IsApproved))
                _param(22) = New SQLServerDBParameter("@courtflag", .CourtFlag)
                _param(23) = New SQLServerDBParameter("@custname", DBUtility.GetString(.CustName))
                _param(24) = New SQLServerDBParameter("@custsurname", DBUtility.GetString(.CustSurname))
                _param(25) = New SQLServerDBParameter("@title", DBUtility.GetString(.Title))
                _param(26) = New SQLServerDBParameter("@redcaseno", .RedCaseNo)
                _param(27) = New SQLServerDBParameter("@refid", .RefID)
                _param(28) = New SQLServerDBParameter("@debtorstatusid", .DebtorStatusID)
                _param(29) = New SQLServerDBParameter("@comment", DBUtility.GetString(.Comment))
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)


            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtBankAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function IsApproved(_redcaseno As String, _refid As String, _isapproved As Integer, _approver As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _approveddate As String = ""
        Dim _param(4) As SQLServerDBParameter
        Dim _uparam(1) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "UPDATE GSBBRC_CourtBank SET "
            _sql = _sql & "approver=@approver,approveddate=@approveddate,isapproved=@isapproved "
            _sql = _sql & "WHERE redcaseno=@redcaseno AND refid=@refid"

            _approveddate = UtilOfDate.ConvertDate(UtilOfDate.CurrentDate, UtilOfDate.DateFormat.StringYMD, UtilOfDate.YearFormat.Christian)
            _param(0) = New SQLServerDBParameter("@approver", _approver)
            _param(1) = New SQLServerDBParameter("@approveddate", _approveddate)
            _param(2) = New SQLServerDBParameter("@isapproved", _isapproved)
            _param(3) = New SQLServerDBParameter("@redcaseno", _redcaseno)
            _param(4) = New SQLServerDBParameter("@refid", _refid)
            _dbaccess.ExecuteNonQuery(_sql, _param)

            If _isapproved = 1 Then 'Approved=True
                _sql = "INSERT INTO GSBBRC_CourtBankHistory "
                _sql = _sql & "SELECT * FROM GSBBRC_CourtBank WHERE redcaseno=@redcaseno AND refid=@refid"

                _uparam(0) = New SQLServerDBParameter("@redcaseno", _redcaseno)
                _uparam(1) = New SQLServerDBParameter("@refid", _refid)
                _dbaccess.ExecuteNonQuery(_sql, _uparam)
            End If

            _result = True

            _dbaccess.CommitTransaction()
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("CourtBankAccess", "IsApproved()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _redCaseNo As String, ByVal _refId As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        '_redcaseno,_refid,_accountno,_birthdate,_courtfee,_sequestrateaction
        '_courtretainer,_resultid,_causeid,_defaultdate,_sequestratedate,_sellassetdate
        '_repaydate,_legaldate,_sellassetresult,_haveasset,_justicedate,_receivepaymentdate,_collateral
        '_applicationpayment,_createddate,_modifieddate,_recorder,_approver,_approveddate,_isapproved,_courtflag

        Try
            _sql = "DELETE FROM GSBBRC_CourtBank WHERE redcaseno='" & _redCaseNo & "' AND refid='" & _refId & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtBankAccess", "Delete()", ex.Message)
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

        '_redcaseno,_refid,_accountno,_birthdate,_courtfee,_sequestrateaction
        '_courtretainer,_resultid,_causeid,_defaultdate,_sequestratedate,_sellassetdate
        '_repaydate,_legaldate,_sellassetresult,_haveasset,_justicedate,_receivepaymentdate,_collateral
        '_applicationpayment,_createddate,_modifieddate,_recorder,_approver,_approveddate,_isapproved,_courtflag

        Try
            _sql = "DELETE FROM GSBBRC_CourtBank WHERE redcaseno='" & _datacode & "'"
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("CourtBankAccess", "Delete()", ex.Message)
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
