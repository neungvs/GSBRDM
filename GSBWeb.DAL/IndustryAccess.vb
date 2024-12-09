Imports System.Data.SqlClient
Imports System.Configuration
Imports Arsoft.Utility
Imports System.Globalization

Public Class IndustryAccess

    Dim _dbaccess As SQLServerDBAccess
    Dim _dbCon As New DBclass("ConnectionString_Report")

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

#Region "Method"


#Region "ISICCODE"

    Public Function GetDataIndustyLimit() As DataTable
        Dim _sql As String
        Dim _result As DataTable
        Try
            _sql = "select ROW_NUMBER()  over  (order by EffectiveDate) RowNumber "
            _sql += ",FORMAT(DATEADD(year,543, EffectiveDate),'dd/MM/yyyy') EffectiveDate "
            _sql += "From RDM_Report..Ref_GSB_Sector_Limit G "
            _sql += "where IsActive = 1 "
            _sql += "group by EffectiveDate "
            _sql += "order  by RowNumber desc"

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetDataIndustyLimit()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetSectorLimit(ByVal _effectiveDate As String) As List(Of IndustyDetailEntity)
        Dim _sql As String
        Dim _result As New List(Of IndustyDetailEntity)
        Dim _data As IndustyDetailEntity
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _param(1) As SQLServerDBParameter


        Try
            If String.IsNullOrEmpty(_effectiveDate) Then
                _frDate = _effectiveDate
            Else
                _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
                _efDate = _efDate.AddYears(-543)
            End If

            '_frDate = _efDate.ToString("yyyy-MM-dd")
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)
            'String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "Select ID,Level1,Level2,Level3 "
            _sql += ",IndustryDesc,LoantypeDesc "
            _sql += ",IndustryLimitPercentage "
            _sql += ",IndustryLimitAmount,EffectiveDate "
            _sql += ",Type "
            _sql += "FROM RDM_Report..[Ref_GSB_Sector_Limit_Detail] "
            _sql += "where EffectiveDate = @EffectiveDate "
            _sql += "order by Level1,Level2,Level3 "

            _param(0) = New SQLServerDBParameter("@EffectiveDate", _frDate)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read

                _data = New IndustyDetailEntity

                With _data
                    If _dbaccess.GetItem("Type") = 1 Then
                        _data.ISICCODE = _dbaccess.GetItem("Level1")
                        _data.ISICCODESUBLEVEL = _dbaccess.GetItem("Level2")
                        _data.Industry = _dbaccess.GetItem("IndustryDesc")
                    Else
                        _data.LN_TYPE_CODE = _dbaccess.GetItem("Level1")
                        _data.LN_SUB_TYPE = _dbaccess.GetItem("Level2")
                        _data.LN_MKT_CODE = _dbaccess.GetItem("Level3")
                        _data.LoanType = _dbaccess.GetItem("LoantypeDesc")
                    End If
                    _data.ID = _dbaccess.GetItem("ID")
                    _data.Type = _dbaccess.GetItem("Type")
                    _data.IndustryLimitPercentage = String.Format("{0:N2}", _dbaccess.GetItem("IndustryLimitPercentage"))
                    _data.IndustryLimitAmount = String.Format("{0:N2}", _dbaccess.GetItem("IndustryLimitAmount"))
                    _data.EffectiveDate = _dbaccess.GetItem("EffectiveDate")
                End With
                _result.Add(_data)
            Loop

            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetSectorLimit()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function DeleteByEffective(ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _param(2) As SQLServerDBParameter


        Try
            _dbaccess.BeginTransaction()
            _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "delete from Ref_GSB_Sector_Limit  where EffectiveDate = @EffectiveDate"

            _param(0) = New SQLServerDBParameter("@EffectiveDate", _frDate)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()

            DeleteDetailByEffectiveDate(_frDate)

            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryBiz", "DeleteByEffective()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Private Sub DeleteDetailByEffectiveDate(_frDate As String)
        Dim _sql As String = String.Empty
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()
            _sql = "delete from Ref_GSB_Sector_Limit_Detail  where EffectiveDate = @EffectiveDate"

            _param(0) = New SQLServerDBParameter("@EffectiveDate", _frDate)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryBiz", "DeleteDetailByEffectiveDate()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Public Function GetSectorLimitCriteria(ByVal _effectiveDate As String, ByVal _isicCode As String, ByVal _isicSubCode As String) As List(Of IndustyDetailEntity)
        Dim _sql As String
        Dim _result As New List(Of IndustyDetailEntity)
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _data As IndustyDetailEntity
        Dim _param(3) As SQLServerDBParameter

        Try
            _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "Select ID,Level1,Level2,Level3 "
            _sql += ",IndustryDesc,LoantypeDesc "
            _sql += ",IndustryLimitPercentage "
            _sql += ",IndustryLimitAmount,EffectiveDate "
            _sql += ",Type "
            _sql += "FROM RDM_Report..[Ref_GSB_Sector_Limit_Detail] "
            _sql += "where EffectiveDate = @EffectiveDate "


            If Not String.IsNullOrEmpty(_isicCode) Then
                _sql += "and (Level1 like '" + _isicCode + "%' "
                _sql += "or Level1 = @ISICCODE) "
            End If

            If Not String.IsNullOrEmpty(_isicSubCode) Then
                _sql += "and (Level2 like '" + _isicSubCode + "%' "
                _sql += "or Level2 = @ISICSUB) "
            End If

            _sql += "order by Level1,Level2,Level3 "

            _param(0) = New SQLServerDBParameter("@EffectiveDate", _frDate)

            If Not String.IsNullOrEmpty(_isicCode) And Not String.IsNullOrEmpty(_isicSubCode) Then

                _param(1) = New SQLServerDBParameter("@ISICCODE", _isicCode)
                _param(2) = New SQLServerDBParameter("@ISICSUB", _isicSubCode)
            Else
                If Not String.IsNullOrEmpty(_isicCode) Then
                    _param(1) = New SQLServerDBParameter("@ISICCODE", _isicCode)
                Else
                    _param(1) = New SQLServerDBParameter("@ISICSUB", _isicSubCode)
                End If
            End If

            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read

                _data = New IndustyDetailEntity

                With _data
                    If _dbaccess.GetItem("Type") = 1 Then
                        _data.ISICCODE = _dbaccess.GetItem("Level1")
                        _data.ISICCODESUBLEVEL = _dbaccess.GetItem("Level2")
                        _data.Industry = _dbaccess.GetItem("IndustryDesc")
                    Else
                        _data.LN_TYPE_CODE = _dbaccess.GetItem("Level1")
                        _data.LN_SUB_TYPE = _dbaccess.GetItem("Level2")
                        _data.LN_MKT_CODE = _dbaccess.GetItem("Level3")
                        _data.LoanType = _dbaccess.GetItem("LoantypeDesc")
                    End If
                    _data.ID = _dbaccess.GetItem("ID")
                    _data.Type = _dbaccess.GetItem("Type")
                    _data.IndustryLimitPercentage = _dbaccess.GetItem("IndustryLimitPercentage")
                    _data.IndustryLimitAmount = _dbaccess.GetItem("IndustryLimitAmount")
                    _data.EffectiveDate = _dbaccess.GetItem("EffectiveDate")
                End With

                _result.Add(_data)
            Loop

            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryBiz", "GetSectorLimitCriteria()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetHeadder(ByVal _effectiveDate As String) As List(Of IndustyHeaderEntity)
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _dt As DataTable
        Dim _efDate As DateTime
        Dim _frDate As String = ""
        Dim _result As New List(Of IndustyHeaderEntity)

        Try
            _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "SELECT top 1 IsAgree_1,IsAgree_2,IsAgree_3 "
            _sql += ",ApproveID_1, ApproveID_2, ApproveID_3 "
            _sql += ",IsApprove_1,IsApprove_2,IsApprove_3 "
            _sql += ",FORMAT(DATEADD(YEAR,543,ApproveDate_1),'dd/MM/yyyy') ApproveDate_1,FORMAT(DATEADD(YEAR,543,ApproveDate_2),'dd/MM/yyyy') ApproveDate_2,FORMAT(DATEADD(YEAR,543,ApproveDate_3),'dd/MM/yyyy') ApproveDate_3 "
            _sql += "FROM [RDM_Report]..[Ref_GSB_Sector_Limit] "
            _sql += "where EffectiveDate = '" + _frDate + "' "
            _sql += "and IsActive = 1 "

            _dt = _dbCon.ExecuteReader(_sql)

            For Each rs As DataRow In _dt.Rows
                Dim ent As New IndustyHeaderEntity
                'Dim approveDate1 As DateTime
                'Dim approveDate2 As DateTime
                'Dim approveDate3 As DateTime
                ent.IsAgree_1 = Convert.ToString(rs("IsAgree_1"))
                ent.IsAgree_2 = Convert.ToString(rs("IsAgree_2"))
                ent.IsAgree_3 = Convert.ToString(rs("IsAgree_3"))
                ent.IsApprove_1 = Convert.ToString(rs("IsApprove_1"))
                ent.IsApprove_2 = Convert.ToString(rs("IsApprove_2"))
                ent.IsApprove_3 = Convert.ToString(rs("IsApprove_3"))

                If Not String.IsNullOrEmpty(Convert.ToString(rs("ApproveDate_1"))) Then
                    'approveDate1 = DateTime.Parse(rs("ApproveDate_1"))
                    'approveDate1 = approveDate1.AddYears(543)
                    'ent.ApproveDate_1 = String.Format("{0:dd/MM/yyyy}", rs("ApproveDate_1"))
                    ent.ApproveDate_1 = Convert.ToString(rs("ApproveDate_1"))
                End If

                If Not String.IsNullOrEmpty(Convert.ToString(rs("ApproveDate_2"))) Then
                    'approveDate2 = DateTime.Parse(rs("ApproveDate_2"))
                    'approveDate2 = approveDate2.AddYears(543)
                    'ent.ApproveDate_2 = String.Format("{0:dd/MM/yyyy}", rs("ApproveDate_2"))
                    ent.ApproveDate_2 = Convert.ToString(rs("ApproveDate_2"))
                End If

                If Not String.IsNullOrEmpty(Convert.ToString(rs("ApproveDate_3"))) Then
                    'approveDate3 = DateTime.Parse(rs("ApproveDate_3"))
                    'approveDate3 = approveDate3.AddYears(543)
                    'ent.ApproveDate_3 = String.Format("{0:dd/MM/yyyy}", rs("ApproveDate_1"))
                    ent.ApproveDate_3 = Convert.ToString(rs("ApproveDate_3"))
                End If

                ent.ApproveID_1 = Convert.ToString(rs("ApproveID_1"))
                ent.ApproveID_2 = Convert.ToString(rs("ApproveID_2"))
                ent.ApproveID_3 = Convert.ToString(rs("ApproveID_3"))
                _result.Add(ent)
            Next

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryBiz", "GetHeadder()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function UpdateHeadder(ByVal _effectiveDate As String, ByVal _entHeader As IndustyHeaderEntity, ByVal _newEffectiveDate As String)
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _frDate As String

        _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
        _efDate = _efDate.AddYears(-543)
        _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)
        Dim _frnewEffectiveDate As String

        Try
            _dbaccess.BeginTransaction()
            If String.IsNullOrEmpty(_entHeader.ApproveDate_1) Then
                _entHeader.ApproveDate_1 = "null"
            Else
                _efDate = DateTime.ParseExact(_entHeader.ApproveDate_1, "dd/MM/yyyy", Nothing)
                _efDate = _efDate.AddYears(-543)
                _entHeader.ApproveDate_1 = String.Format("{0:yyyy-MM-dd}", _efDate)

            End If

            If String.IsNullOrEmpty(_entHeader.ApproveDate_2) Then
                _entHeader.ApproveDate_2 = "null"
            Else
                _efDate = DateTime.ParseExact(_entHeader.ApproveDate_2, "dd/MM/yyyy", Nothing)
                _efDate = _efDate.AddYears(-543)
                _entHeader.ApproveDate_2 = String.Format("{0:yyyy-MM-dd}", _efDate)
            End If


            If String.IsNullOrEmpty(_entHeader.ApproveDate_3) Then
                _entHeader.ApproveDate_3 = "null"
            Else
                _efDate = DateTime.ParseExact(_entHeader.ApproveDate_3, "dd/MM/yyyy", Nothing)
                _efDate = _efDate.AddYears(-543)
                _entHeader.ApproveDate_3 = String.Format("{0:yyyy-MM-dd}", _efDate)
            End If

            _sql = "update RDM_Report..Ref_GSB_Sector_Limit "
            _sql += "Set IsAgree_1 = " + _entHeader.IsAgree_1
            _sql += ",IsAgree_2 = " + _entHeader.IsAgree_2
            _sql += ",IsAgree_3 = " + _entHeader.IsAgree_3

            If String.IsNullOrEmpty(_entHeader.ApproveID_1) Then
                _sql += ",ApproveID_1 = null "
            Else
                _sql += ",ApproveID_1 = '" + _entHeader.ApproveID_1 + "' "
            End If

            If String.IsNullOrEmpty(_entHeader.ApproveID_2) Then
                _sql += ",ApproveID_2 = null "
            Else
                _sql += ",ApproveID_2 = '" + _entHeader.ApproveID_2 + "' "
            End If

            If String.IsNullOrEmpty(_entHeader.ApproveID_3) Then
                _sql += ",ApproveID_3 = null "
            Else
                _sql += ",ApproveID_3 = '" + _entHeader.ApproveID_3 + "' "
            End If

            _sql += ",IsApprove_1 = " + _entHeader.IsApprove_1
            _sql += ",IsApprove_2 = " + _entHeader.IsApprove_2
            _sql += ",IsApprove_3 = " + _entHeader.IsApprove_3

            If _entHeader.ApproveDate_1 = "null" Then
                _sql += ",ApproveDate_1 = " + _entHeader.ApproveDate_1
            Else
                _sql += ",ApproveDate_1 = '" + _entHeader.ApproveDate_1 + "' "
            End If

            If _entHeader.ApproveDate_2 = "null" Then
                _sql += ",ApproveDate_2 = " + _entHeader.ApproveDate_2
            Else
                _sql += ",ApproveDate_2 = '" + _entHeader.ApproveDate_2 + "' "
            End If

            If _entHeader.ApproveDate_3 = "null" Then
                _sql += ",ApproveDate_3 = " + _entHeader.ApproveDate_3
            Else
                _sql += ",ApproveDate_3 = '" + _entHeader.ApproveDate_3 + "' "
            End If

            If Not String.IsNullOrEmpty(_newEffectiveDate) Then
                _efDate = DateTime.ParseExact(_newEffectiveDate, "dd/MM/yyyy", Nothing)
                _efDate = _efDate.AddYears(-543)
                _frnewEffectiveDate = String.Format("{0:yyyy-MM-dd}", _efDate)
                _sql += ",EffectiveDate = '" + _frnewEffectiveDate + "' "
            End If

            _sql += " where EffectiveDate = '" + _frDate + "' "
            _sql += "and IsActive = 1 "

            _dbaccess.ExecuteNonQuery(_sql)
            _dbaccess.CommitTransaction()

            UpdateEffectivDateDetail(_effectiveDate, _newEffectiveDate)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "UpdateHeadder()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function

    Private Sub UpdateEffectivDateDetail(_effectiveDate As String, _newEffectiveDate As String)
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _frnewEffectiveDate As String

        _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
        _efDate = _efDate.AddYears(-543)
        _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

        If Not String.IsNullOrEmpty(_newEffectiveDate) Then
            _dbaccess.BeginTransaction()
            _sql = "update RDM_Report..Ref_GSB_Sector_Limit_Detail "
            _efDate = DateTime.ParseExact(_newEffectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frnewEffectiveDate = String.Format("{0:yyyy-MM-dd}", _efDate)
            _sql += "Set EffectiveDate = '" + _frnewEffectiveDate + "' "
            _sql += " where EffectiveDate = '" + _frDate + "' "
            _dbaccess.ExecuteNonQuery(_sql)
            _dbaccess.CommitTransaction()
        End If
    End Sub


    Public Function DeleteDetail(ByVal _secID As String)
        Dim _sql As String = String.Empty
        Dim _param(1) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()

            _sql = "Delete RDM_Report..Ref_GSB_Sector_Limit_Detail "
            _sql += "where [ID] =  @ID"

            _param(0) = New SQLServerDBParameter("@ID", _secID)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "DeleteDetail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function

    Public Function GetEditData(ByVal _secID As String) As IndustyDetailEntity
        Dim _sql As String = String.Empty
        Dim _result As New IndustyDetailEntity
        Dim _param(1) As SQLServerDBParameter


        Try
            _sql = "select ID,Level1,Level2,Level3,IndustryDesc,LoantypeDesc,Type,Cast([IndustryLimitPercentage] as decimal(28,2)) IndustryLimitPercentage"
            _sql += ",Cast([IndustryLimitAmount] as decimal(28,2)) IndustryLimitAmount,EffectiveDate "
            _sql += "from RDM_Report..Ref_GSB_Sector_Limit_Detail "
            _sql += "WHERE ID = @ID "

            _param(0) = New SQLServerDBParameter("@ID", _secID)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read
                With _result
                    If _dbaccess.GetItem("Type") = 1 Then
                        _result.ISICCODE = _dbaccess.GetItem("Level1")
                        _result.ISICCODESUBLEVEL = _dbaccess.GetItem("Level2")
                        _result.Industry = _dbaccess.GetItem("IndustryDesc")
                    Else
                        _result.LN_TYPE_CODE = _dbaccess.GetItem("Level1")
                        _result.LN_SUB_TYPE = _dbaccess.GetItem("Level2")
                        _result.LN_MKT_CODE = _dbaccess.GetItem("Level3")
                        _result.LoanType = _dbaccess.GetItem("LoantypeDesc")
                    End If
                    _result.ID = _dbaccess.GetItem("ID")
                    _result.Type = _dbaccess.GetItem("Type")
                    _result.IndustryLimitPercentage = _dbaccess.GetItem("IndustryLimitPercentage")
                    _result.IndustryLimitAmount = _dbaccess.GetItem("IndustryLimitAmount")
                    _result.EffectiveDate = _dbaccess.GetItem("EffectiveDate")
                End With
            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetEditData()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function UpdateDetail(ByVal _secID As String, ByVal _inPercent As Double?, ByVal _inAmount As Double?)
        Dim _sql As String = String.Empty
        Dim _param(3) As SQLServerDBParameter


        Try
            _dbaccess.BeginTransaction()

            _sql = "update RDM_Report..Ref_GSB_Sector_Limit_Detail "
            _sql += "Set IndustryLimitPercentage = @IndustryLimitPercentage"
            _sql += ",IndustryLimitAmount = @IndustryLimitAmount"
            _sql += ",CreateDate = GetDate() "
            _sql += "where ID = @ID"

            If _inPercent Is Nothing Then
                _param(0) = New SQLServerDBParameter("@IndustryLimitPercentage", DBNull.Value)
            Else
                _param(0) = New SQLServerDBParameter("@IndustryLimitPercentage", Convert.ToString(_inPercent))
            End If

            If _inAmount Is Nothing Then
                _param(1) = New SQLServerDBParameter("@IndustryLimitAmount", DBNull.Value)
            Else
                _param(1) = New SQLServerDBParameter("@IndustryLimitAmount", Convert.ToString(_inAmount))
            End If

            _param(2) = New SQLServerDBParameter("@ID", _secID)

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "UpdateDetail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function

    Public Function InsertDetail(ByVal _entDetail As IndustyLimitDetailEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = String.Empty
        Dim _param(8) As SQLServerDBParameter
        Dim _efDate As DateTime
        Dim _frDate As String

        Try
            _efDate = DateTime.ParseExact(_entDetail.EffectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)


            _dbaccess.BeginTransaction()

            _sql = "INSERT INTO [RDM_Report]..[Ref_GSB_Sector_Limit_Detail] "
            _sql += "(Level1,Level2,Level3,IndustryDesc,LoantypeDesc,Type,IndustryLimitPercentage,IndustryLimitAmount,EffectiveDate,CreateDate) "
            _sql += "VALUES (@Level1,@Level2,@Level3,@IndustryDesc,@LoantypeDesc,@Type,@IndustryLimitPercentage,@IndustryLimitAmount,@EffectiveDate,GETDATE()) "


            _param(0) = New SQLServerDBParameter("@Level1", _entDetail.Level1)
            _param(1) = New SQLServerDBParameter("@Level2", _entDetail.Level2)
            _param(2) = New SQLServerDBParameter("@Level3", _entDetail.Level3)
            _param(3) = New SQLServerDBParameter("@IndustryDesc", _entDetail.IndustryDesc)
            _param(4) = New SQLServerDBParameter("@LoantypeDesc", _entDetail.LoantypeDesc)
            _param(5) = New SQLServerDBParameter("@Type", Convert.ToInt16(_entDetail.Type))

            If _entDetail.IndustryLimitPercentage = "" Or _entDetail.IndustryLimitPercentage = Nothing Or _entDetail.IndustryLimitPercentage = "-" Then
                _param(6) = New SQLServerDBParameter("@IndustryLimitPercentage", DBNull.Value)
            Else
                _entDetail.IndustryLimitPercentage = _entDetail.IndustryLimitPercentage.Replace(",", "")
                _param(6) = New SQLServerDBParameter("@IndustryLimitPercentage", _entDetail.IndustryLimitPercentage)
            End If

            If _entDetail.IndustryLimitAmount = "" Or _entDetail.IndustryLimitAmount = Nothing Or _entDetail.IndustryLimitAmount = "-" Then
                _param(7) = New SQLServerDBParameter("@IndustryLimitAmount", DBNull.Value)
            Else
                _entDetail.IndustryLimitAmount = _entDetail.IndustryLimitAmount.Replace(",", "")
                _param(7) = New SQLServerDBParameter("@IndustryLimitAmount", _entDetail.IndustryLimitAmount)
            End If
            _param(8) = New SQLServerDBParameter("@EffectiveDate", _frDate)

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "InsertDetail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function AddNewIndustryLimit(ByVal _entHeader As IndustyHeaderEntity, ByVal _entDetail As List(Of IndustyDetailEntity))
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _dtConv As DateTime
        Dim _frDate As String
        _sql = ""

        ''Formet Date พศ to คศ
        _efDate = DateTime.ParseExact(_entHeader.EffectiveDate, "dd/MM/yyyy", Nothing)
        _efDate = _efDate.AddYears(-543)
        _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

        Try

            If String.IsNullOrEmpty(_entHeader.ApproveDate_1) Then
                _entHeader.ApproveDate_1 = "null"
            Else
                _dtConv = DateTime.ParseExact(_entHeader.ApproveDate_1, "dd/MM/yyyy", Nothing)
                _dtConv = _dtConv.AddYears(-543)
                _entHeader.ApproveDate_1 = String.Format("{0:yyyy-MM-dd}", _dtConv)

            End If

            If String.IsNullOrEmpty(_entHeader.ApproveDate_2) Then
                _entHeader.ApproveDate_2 = "null"
            Else
                _dtConv = DateTime.ParseExact(_entHeader.ApproveDate_2, "dd/MM/yyyy", Nothing)
                _dtConv = _dtConv.AddYears(-543)
                _entHeader.ApproveDate_2 = String.Format("{0:yyyy-MM-dd}", _dtConv)
            End If

            If String.IsNullOrEmpty(_entHeader.ApproveDate_3) Then
                _entHeader.ApproveDate_3 = "null"
            Else
                _dtConv = DateTime.ParseExact(_entHeader.ApproveDate_3, "dd/MM/yyyy", Nothing)
                _dtConv = _dtConv.AddYears(-543)
                _entHeader.ApproveDate_3 = String.Format("{0:yyyy-MM-dd}", _dtConv)
            End If


            For Each _ent As IndustyDetailEntity In _entDetail
                If _ent.IndustryLimitPercentage = "" Or _ent.IndustryLimitPercentage = Nothing Or _ent.IndustryLimitPercentage = "-" Then
                    _ent.IndustryLimitPercentage = ",null"
                Else
                    _ent.IndustryLimitPercentage = ",'" + _ent.IndustryLimitPercentage.Replace(",", "") + "'"
                End If

                If _ent.IndustryLimitAmount = "" Or _ent.IndustryLimitAmount = Nothing Or _ent.IndustryLimitAmount = "-" Then
                    _ent.IndustryLimitAmount = ",null"
                Else
                    _ent.IndustryLimitAmount = ",'" + _ent.IndustryLimitAmount.Replace(",", "") + "'"
                End If

                If _ent.LN_MKT_CODE = "" Or _ent.LN_MKT_CODE = Nothing Or _ent.LN_MKT_CODE = "-" Then
                    _ent.LN_MKT_CODE = ",''"
                Else
                    _ent.LN_MKT_CODE = ",'" + _ent.LN_MKT_CODE + "'"
                End If

                ''Get Header 
                _sql += "INSERT "
                _sql += "INTO RDM_Report..Ref_GSB_Sector_Limit(ISICCODE, ISICCODESUBLEVEL, Industry, IndustryLimitPercentage"
                _sql += ",IndustryLimitAmount "
                _sql += ",IsAgree_1,IsAgree_2,IsAgree_3 "
                _sql += ",ApproveID_1, ApproveID_2, ApproveID_3 "
                _sql += ",IsApprove_1,IsApprove_2,IsApprove_3 "
                _sql += ",ApproveDate_1,ApproveDate_2,ApproveDate_3 "
                _sql += ", CreateDate,EffectiveDate"
                _sql += ", IsActive,LN_MKT_CODE) "
                _sql += "VALUES('" + _ent.ISICCODE + "','" + _ent.ISICCODESUBLEVEL + "','" + _ent.Industry + "'" + _ent.IndustryLimitPercentage + "" + _ent.IndustryLimitAmount
                _sql += ",'" + _entHeader.IsAgree_1 + "','" + _entHeader.IsAgree_2 + "','" + _entHeader.IsAgree_3 + "' "

                If String.IsNullOrEmpty(_entHeader.ApproveID_1) Then
                    _sql += ",null"
                Else
                    _sql += ",'" + _entHeader.ApproveID_1 + "' "
                End If

                If String.IsNullOrEmpty(_entHeader.ApproveID_2) Then
                    _sql += ",null"
                Else
                    _sql += ",'" + _entHeader.ApproveID_2 + "' "
                End If

                If String.IsNullOrEmpty(_entHeader.ApproveID_3) Then
                    _sql += ",null"
                Else
                    _sql += ",'" + _entHeader.ApproveID_3 + "' "
                End If

                _sql += ",'" + _entHeader.IsApprove_1 + "', '" + _entHeader.IsApprove_2 + "','" + _entHeader.IsApprove_3 + "' "

                If _entHeader.ApproveDate_1 = "null" Then
                    _sql += "," + _entHeader.ApproveDate_1
                Else
                    _sql += ",'" + _entHeader.ApproveDate_1 + "' "
                End If
                If _entHeader.ApproveDate_2 = "null" Then
                    _sql += "," + _entHeader.ApproveDate_2
                Else
                    _sql += ",'" + _entHeader.ApproveDate_2 + "' "
                End If

                If _entHeader.ApproveDate_3 = "null" Then
                    _sql += "," + _entHeader.ApproveDate_3
                Else
                    _sql += ",'" + _entHeader.ApproveDate_3 + "' "
                End If

                _sql += ",Getdate() " + ",'" + _frDate + "'"
                _sql += ",1 "
                _sql += _ent.LN_MKT_CODE
                _sql += ") "
            Next

            _dbCon.ExecuteNonQuery(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "AddNewIndustryLimit()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function


    Public Function AddNewHeaderIndustryLimit(ByVal _entHeader As IndustyHeaderEntity) As Boolean
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _dtConv As DateTime
        Dim _frDate As String
        Dim _result As Boolean = False
        Dim _param(12) As SQLServerDBParameter


        Try
            _dbaccess.BeginTransaction()
            ''Formet Date พศ to คศ
            _efDate = DateTime.ParseExact(_entHeader.EffectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "INSERT INTO [dbo].[Ref_GSB_Sector_Limit] "
            _sql += "(IsAgree_1 "
            _sql += ",IsAgree_2 "
            _sql += ",IsAgree_3 "
            _sql += ",IsApprove_1 "
            _sql += ",IsApprove_2 "
            _sql += ",IsApprove_3 "
            _sql += ",ApproveID_1 "
            _sql += ",ApproveID_2 "
            _sql += ",ApproveID_3 "
            _sql += ",ApproveDate_1 "
            _sql += ",ApproveDate_2 "
            _sql += ",ApproveDate_3 "
            _sql += ",EffectiveDate "
            _sql += ",IsActive "
            _sql += ",CreateDate) "
            _sql += "VALUES (@IsAgree_1,@IsAgree_2,@IsAgree_3,@IsApprove_1"
            _sql += ",@IsApprove_2,@IsApprove_3,@ApproveID_1,@ApproveID_2,@ApproveID_3,@ApproveDate_1,@ApproveDate_2,@ApproveDate_3"
            _sql += ",@EffectiveDate,1,GETDATE()) "

            With _entHeader
                _param(0) = New SQLServerDBParameter("@IsAgree_1", .IsAgree_1)
                _param(1) = New SQLServerDBParameter("@IsAgree_2", .IsAgree_2)
                _param(2) = New SQLServerDBParameter("@IsAgree_3", .IsAgree_3)
                _param(3) = New SQLServerDBParameter("@IsApprove_1", .IsApprove_1)
                _param(4) = New SQLServerDBParameter("@IsApprove_2", .IsApprove_2)
                _param(5) = New SQLServerDBParameter("@IsApprove_3", .IsApprove_3)
                _param(6) = New SQLServerDBParameter("@ApproveID_1", IIf(String.IsNullOrEmpty(.ApproveID_1), DBNull.Value, .ApproveID_1))
                _param(7) = New SQLServerDBParameter("@ApproveID_2", IIf(String.IsNullOrEmpty(.ApproveID_2), DBNull.Value, .ApproveID_2))
                _param(8) = New SQLServerDBParameter("@ApproveID_3", IIf(String.IsNullOrEmpty(.ApproveID_3), DBNull.Value, .ApproveID_3))

                If String.IsNullOrEmpty(_entHeader.ApproveDate_1) Then
                    _param(9) = New SQLServerDBParameter("@ApproveDate_1", DBNull.Value)
                Else
                    _dtConv = DateTime.ParseExact(_entHeader.ApproveDate_1, "dd/MM/yyyy", Nothing)
                    _dtConv = _dtConv.AddYears(-543)
                    _entHeader.ApproveDate_1 = String.Format("{0:yyyy-MM-dd}", _dtConv)
                    _param(9) = New SQLServerDBParameter("@ApproveDate_1", _entHeader.ApproveDate_1)
                End If

                If String.IsNullOrEmpty(_entHeader.ApproveDate_2) Then
                    _param(10) = New SQLServerDBParameter("@ApproveDate_2", DBNull.Value)
                Else
                    _dtConv = DateTime.ParseExact(_entHeader.ApproveDate_2, "dd/MM/yyyy", Nothing)
                    _dtConv = _dtConv.AddYears(-543)
                    _entHeader.ApproveDate_2 = String.Format("{0:yyyy-MM-dd}", _dtConv)
                    _param(10) = New SQLServerDBParameter("@ApproveDate_2", _entHeader.ApproveDate_2)
                End If

                If String.IsNullOrEmpty(_entHeader.ApproveDate_3) Then
                    _param(11) = New SQLServerDBParameter("@ApproveDate_3", DBNull.Value)
                Else
                    _dtConv = DateTime.ParseExact(_entHeader.ApproveDate_3, "dd/MM/yyyy", Nothing)
                    _dtConv = _dtConv.AddYears(-543)
                    _entHeader.ApproveDate_3 = String.Format("{0:yyyy-MM-dd}", _dtConv)
                    _param(11) = New SQLServerDBParameter("@ApproveDate_3", _entHeader.ApproveDate_3)
                End If
                _param(12) = New SQLServerDBParameter("@EffectiveDate", _frDate)
            End With

            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryBiz", "AddNewHeaderIndustryLimit()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function AddDetailIndustryLimit(ByVal _entDetail As List(Of IndustyLimitDetailEntity)) As Boolean
        Dim _sql As String = String.Empty
        Dim _result As Boolean = False
        Dim _dtConv As DateTime
        Dim _param(8) As SQLServerDBParameter


        Try
            _dbaccess.BeginTransaction()

            _sql = "INSERT INTO [RDM_Report]..[Ref_GSB_Sector_Limit_Detail] "
            _sql += "(Level1,Level2,Level3,IndustryDesc,LoantypeDesc,Type,IndustryLimitPercentage,IndustryLimitAmount,EffectiveDate,CreateDate) "
            _sql += "VALUES (@Level1,@Level2,@Level3,@IndustryDesc,@LoantypeDesc,@Type,@IndustryLimitPercentage,@IndustryLimitAmount,@EffectiveDate,GETDATE()) "

            For Each _data In _entDetail
                With _data
                    _param(0) = New SQLServerDBParameter("@Level1", .Level1)
                    _param(1) = New SQLServerDBParameter("@Level2", .Level2)
                    _param(2) = New SQLServerDBParameter("@Level3", .Level3)
                    _param(3) = New SQLServerDBParameter("@IndustryDesc", .IndustryDesc)
                    _param(4) = New SQLServerDBParameter("@LoantypeDesc", .LoantypeDesc)
                    _param(5) = New SQLServerDBParameter("@Type", Convert.ToInt16(.Type))
                    If .IndustryLimitPercentage = "" Or .IndustryLimitPercentage = Nothing Or .IndustryLimitPercentage = "-" Then
                        _param(6) = New SQLServerDBParameter("@IndustryLimitPercentage", DBNull.Value)
                    Else
                        .IndustryLimitPercentage = .IndustryLimitPercentage.Replace(",", "")
                        _param(6) = New SQLServerDBParameter("@IndustryLimitPercentage", .IndustryLimitPercentage)
                    End If

                    If .IndustryLimitAmount = "" Or .IndustryLimitAmount = Nothing Or .IndustryLimitAmount = "-" Then
                        _param(7) = New SQLServerDBParameter("@IndustryLimitAmount", DBNull.Value)
                    Else
                        .IndustryLimitAmount = .IndustryLimitAmount.Replace(",", "")
                        _param(7) = New SQLServerDBParameter("@IndustryLimitAmount", .IndustryLimitAmount)
                    End If
                    _param(8) = New SQLServerDBParameter("@EffectiveDate", .EffectiveDate)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryBiz", "AddDetailIndustryLimit()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function LoadDDL_ISICCODE() As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable
        Try
            _sql = "select ISICCODE,ROW_NUMBER() OVER(ORDER BY ISICCODE) as Row "
            _sql += "from DIM_ISIC BUSINESS_TYPE "
            _sql += "left Join ( "
            _sql += "select case when ISNUMERIC(ISIC_CODE)<>1 then SUBSTRING(ISIC_CODE,1,1)  ELSE LEFT(ISIC_CODE,1) END  as ISICCODE "
            _sql += "from DIM_ISIC "
            _sql += "group by case when ISNUMERIC(ISIC_CODE)<>1 then SUBSTRING(ISIC_CODE,1,1)  ELSE LEFT(ISIC_CODE,1) END "
            _sql += ") as Ref_CUST_BUSINESS_TYPE "
            _sql += "on  SUBSTRING(BUSINESS_TYPE.ISIC_CODE,1,1)=  SUBSTRING(Ref_CUST_BUSINESS_TYPE.ISICCODE,1,1) "
            _sql += "where Ref_CUST_BUSINESS_TYPE.ISICCODE is not null "
            _sql += "group by ISICCODE "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "LoadDDL_ISICCODE()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function LoadDDL_ISICCODESUBLEVEL(ByVal _ISICCODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable
        Try
            _sql = "select  ROW_NUMBER() OVER(ORDER BY ISIC_CODE) AS Row , ISIC_CODE  as ISICCODESUBLEVEL ,ISIC_DESC_TH As CBT_DESC_LEV1 "
            _sql += "from  DIM_ISIC  where SUBSTRING(ISIC_CODE,1,1)  = '" + _ISICCODE + "' "

            _result = _dbCon.ExecuteReader(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "LoadDDL_ISICCODESUBLEVEL()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function Check_HeightLevel(ByVal _CODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable
        Try

            _sql = "select ISIC_CODE "
            _sql += "from DIM_ISIC  WHERE SUBSTRING(ISIC_CODE,2, LEN(ISIC_CODE) - 1) = 0 "
            _sql += "AND SUBSTRING(ISIC_CODE,1,1) = '" + _CODE + "' "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "Check_HeightLevel()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function Load_Industry(ByVal _CODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable
        Try
            _sql = "select ISIC_DESC_TH As CBT_DESC_LEV1,ISIC_CODE As CBT_CUST_BUSINESS_TYPE_ID  "
            _sql += "from DIM_ISIC "
            _sql += "where ISIC_CODE   = '" + _CODE + "' "

            _result = _dbCon.ExecuteReader(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "Load_Industry()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function CheckISIC(ByVal _ISICODE As String, ByVal _ISICSUB As String, ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean = True
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _dt As DataTable

        Try
            'Formet Date พศ to คศ
            _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "select * "
            _sql += "from Ref_GSB_Sector_Limit "
            _sql += "where EffectiveDate = '" + _frDate + "' "
            _sql += "and   ISICCODE = '" + _ISICODE + "' "
            _sql += "and   ISICCODESUBLEVEL = '" + _ISICSUB + "' "
            _sql += "and IsActive = 1 "

            _dt = _dbCon.ExecuteReader(_sql)

            If _dt.Rows.Count = 0 Then
                _result = True
            Else
                _result = False
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "CheckISIC()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result

    End Function

    Public Function CheckLnCode(ByVal _LNCODE As String, ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean = True
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _dt As DataTable

        Try
            'Formet Date พศ to คศ
            _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "select * "
            _sql += "from Ref_GSB_Sector_Limit "
            _sql += "where EffectiveDate = '" + _frDate + "' "
            _sql += "and   [LN_MKT_CODE] = '" + _LNCODE + "' "
            _sql += "and IsActive = 1 "

            _dt = _dbCon.ExecuteReader(_sql)

            If _dt.Rows.Count = 0 Then
                _result = True
            Else
                _result = False
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "CheckLnCode()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function CheckEffectiveDate(ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean = True
        Dim _sql As String = String.Empty
        Dim _efDate As DateTime
        Dim _frDate As String
        Dim _dt As DataTable

        Try
            'Formet Date พศ to คศ
            _efDate = DateTime.ParseExact(_effectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _sql = "select * "
            _sql += "from Ref_GSB_Sector_Limit "
            _sql += "where EffectiveDate = '" + _frDate + "' "
            _sql += "and IsActive = 1"

            _dt = _dbCon.ExecuteReader(_sql)

            If _dt.Rows.Count = 0 Then
                _result = True
            Else
                _result = False
            End If

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "CheckEffectiveDate()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetLnmktcode(ByVal _prefix As String) As String()
        Dim _result As New List(Of String)()
        Try
            Using _conn As New SqlConnection()
                _conn.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString_Report").ConnectionString
                Using cmd As New SqlCommand()
                    cmd.CommandText = "SELECT MKT_NAME,LN_MKT_CODE FROM ("
                    cmd.CommandText = cmd.CommandText + "SELECT LN_MKT_CODE + ' ' + MIN(MKT_CODE) MKT_NAME,LN_MKT_CODE FROM RDM_Report..DIM_PRODUCT"
                    cmd.CommandText = cmd.CommandText + " WHERE [SOURCE] NOT IN  ('INVESTMENT','MANUAL_FIS') and LN_MKT_CODE is not null  GROUP BY LN_MKT_CODE  HAVING LN_MKT_CODE + ' ' + MIN(MKT_CODE) like '%' + @SearchText + '%'"
                    cmd.CommandText = cmd.CommandText + ") A INNER JOIN (SELECT level3 FROM ISIC_Condition WHERE level3 is not null GROUP BY level3) C ON A.LN_MKT_CODE = C.level3 "
                    cmd.Parameters.AddWithValue("@SearchText", _prefix)
                    cmd.Connection = _conn
                    _conn.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        While sdr.Read()
                            _result.Add(String.Format("{0}-{1}", sdr("MKT_NAME"), sdr("LN_MKT_CODE")))
                        End While
                    End Using
                    _conn.Close()
                End Using
            End Using

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetLnmktcode()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result.ToArray()
    End Function

    Public Function GetLnmktcodeBySearch(ByVal _txtSearch As String) As String
        Dim _lnCode As New List(Of String)()
        Dim _result As String
        Try
            Using conn As New SqlConnection()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString_Report").ConnectionString
                Using cmd As New SqlCommand()
                    cmd.CommandText = "SELECT top 1 MKT_CODE FROM RDM_Report..DIM_PRODUCT WHERE  LN_MKT_CODE IN (SELECT level3 FROM RDM_Report..ISIC_Condition where level3 is not null GROUP BY level3)  and LN_MKT_CODE = '" + _txtSearch + "' "
                    cmd.Connection = conn
                    conn.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        While sdr.Read()
                            _lnCode.Add(String.Format("{0}", sdr("MKT_CODE")))
                        End While
                    End Using
                    conn.Close()
                End Using
            End Using

            If _lnCode.Count = 0 Then
                _result = "ไม่พบข้อมูล"
            Else
                _result = _lnCode(0)
            End If

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetLnmktcodeBySearch()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

#End Region

#Region "LOANTYPE"

    Public Function Load_Lntypecode(ByVal _LNCODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql = "select LN_TYPE_CODE,Min(TYPE_DESC) TYPE_DESC "
            _sql += "from ( "
            _sql += "Select LN_TYPE_CODE ,Min(TYPE_DESC)  TYPE_DESC "
            _sql += "from RDM_Report..DIM_PRODUCT p  "
            _sql += "inner join RDM_Report..ISIC_Condition i  "
            _sql += "ON p.LN_TYPE_CODE = i.level1  "
            _sql += "and p.LNSUBTYPEID = i.level2   "
            _sql += "and p.LN_MKT_CODE = i.level3    "
            _sql += "GROUP BY LN_TYPE_CODE "
            _sql += "UNION ALL "
            _sql += "Select LN_TYPE_CODE ,Min(TYPE_DESC)  TYPE_DESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1  "
            _sql += "where SOURCE <> 'ISLAMIC'  "
            _sql += "GROUP BY LN_TYPE_CODE  "
            _sql += ") A  "
            _sql += "where LN_TYPE_CODE = '" + _LNCODE + "' "
            _sql += "GROUP BY LN_TYPE_CODE  "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "Load_Lntypecode()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function LoadDDL_LNTYPECODE() As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql += "select LN_TYPE_CODE ,Min(TYPE_DESC) TYPE_DESC"
            _sql += " from ( "
            _sql += "Select LN_TYPE_CODE,Min(TYPE_DESC)  TYPE_DESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "GROUP BY LN_TYPE_CODE "
            _sql += "UNION ALL "
            _sql += "Select LN_TYPE_CODE,Min(TYPE_DESC)  TYPE_DESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "where SOURCE <> 'ISLAMIC' "
            _sql += "GROUP BY LN_TYPE_CODE "
            _sql += ") A "
            _sql += "GROUP BY LN_TYPE_CODE "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "LoadDDL_LNTYPECODE()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function LoadDDL_LNSUBTYPE() As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable
        Try
            _sql += "select LNSUBTYPEID ,Min(SUBDESC) + '_' + LNSUBTYPEID SUBDESC"
            _sql += " from ( "
            _sql += "Select LNSUBTYPEID,Min(SUBDESC)  SUBDESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "GROUP BY LNSUBTYPEID "
            _sql += "UNION ALL "
            _sql += "Select LNSUBTYPEID,Min(SUBDESC)  SUBDESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2  "
            _sql += "where SOURCE <> 'ISLAMIC' "
            _sql += "and LNSUBTYPEID is not null "
            _sql += "GROUP BY LNSUBTYPEID "
            _sql += ") A "
            _sql += "GROUP BY LNSUBTYPEID "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "LoadDDL_LNSUBTYPE()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetLnsubtypeByLntypecode(ByVal _LNTYPECODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql += "select LNSUBTYPEID ,Min(SUBDESC) + '_' + LNSUBTYPEID SUBDESC"
            _sql += " from ( "
            _sql += "Select LNSUBTYPEID,Min(SUBDESC)  SUBDESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "where LN_TYPE_CODE = '" + _LNTYPECODE + "' "
            _sql += "GROUP BY LNSUBTYPEID "
            _sql += "UNION ALL "
            _sql += "Select LNSUBTYPEID,Min(SUBDESC)  SUBDESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2  "
            _sql += "where SOURCE <> 'ISLAMIC' "
            _sql += "and LNSUBTYPEID is not null "
            _sql += "and LN_TYPE_CODE = '" + _LNTYPECODE + "' "
            _sql += "GROUP BY LNSUBTYPEID "
            _sql += ") A "
            _sql += "GROUP BY LNSUBTYPEID "

            _result = _dbCon.ExecuteReader(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetLnsubtypeByLntypecode()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function Load_Lnsubtype(ByVal _LNSUB As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql = "select LNSUBTYPEID,Min(SUBDESC) SUBDESC "
            _sql += "from ( "
            _sql += "Select LNSUBTYPEID ,Min(SUBDESC)  SUBDESC "
            _sql += "from RDM_Report..DIM_PRODUCT p  "
            _sql += "inner join RDM_Report..ISIC_Condition i  "
            _sql += "ON p.LN_TYPE_CODE = i.level1  "
            _sql += "and p.LNSUBTYPEID = i.level2   "
            _sql += "and p.LN_MKT_CODE = i.level3    "
            _sql += "GROUP BY LNSUBTYPEID "
            _sql += "UNION ALL "
            _sql += "Select LNSUBTYPEID ,Min(SUBDESC)  SUBDESC "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1  "
            _sql += "and p.LNSUBTYPEID = i.level2  "
            _sql += "where SOURCE <> 'ISLAMIC'  "
            _sql += "and LNSUBTYPEID is not null "
            _sql += "GROUP BY LNSUBTYPEID  "
            _sql += ") A  "
            _sql += "where LNSUBTYPEID = '" + _LNSUB + "' "
            _sql += "GROUP BY LNSUBTYPEID  "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "Load_Lnsubtype()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function LoadDDL_LNMKTCODE() As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql += "Select LN_MKT_CODE,Min(MKT_CODE) + '_' + LN_MKT_CODE  MKT_CODE "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "GROUP BY LN_MKT_CODE "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "LoadDDL_LNMKTCODE()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function Load_Lnmktcode(ByVal _MKCODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try

            _sql += "Select LN_MKT_CODE,Min(MKT_CODE)  MKT_CODE "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "where LN_MKT_CODE = '" + _MKCODE + "' "
            _sql += "GROUP BY LN_MKT_CODE "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "Load_Lnmktcode()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetLnmktcodeByLnsubtype(ByVal _LNSUBTYPE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql += "Select LN_MKT_CODE,Min(MKT_CODE)+ '_' + LN_MKT_CODE  MKT_CODE "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "where LNSUBTYPEID = '" + _LNSUBTYPE + "' "
            _sql += "GROUP BY LN_MKT_CODE "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetLnmktcodeByLnsubtype()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetLnmktcodeByLntypecode(ByVal _LNTYPECODE As String) As DataTable
        Dim _sql As String = String.Empty
        Dim _result As DataTable

        Try
            _sql += "Select LN_MKT_CODE,Min(MKT_CODE)  MKT_CODE "
            _sql += "from RDM_Report..DIM_PRODUCT p "
            _sql += "inner join RDM_Report..ISIC_Condition i "
            _sql += "ON p.LN_TYPE_CODE = i.level1 "
            _sql += "and p.LNSUBTYPEID = i.level2 "
            _sql += "and p.LN_MKT_CODE = i.level3 "
            _sql += "where LN_TYPE_CODE = '" + _LNTYPECODE + "' "
            _sql += "GROUP BY LN_MKT_CODE "

            _result = _dbCon.ExecuteReader(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("IndustryAccess", "GetLnmktcodeByLntypecode()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function


#End Region

#End Region



 
End Class
