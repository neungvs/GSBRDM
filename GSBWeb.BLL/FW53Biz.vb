Imports GSBWeb
Imports GSBWeb.BLL.UtilsBiz
Imports GSBWeb.DAL





Public Class FW53Biz

    Dim dbCon As New DBclass

    Public Sub New()

    End Sub

    Public Function Count_FW53() As Int64

        Dim sql As String
        Dim dt As DataTable
        Dim cntRow As Int64

        Dim amountString As String = CStr(GetAmountTotal())

        sql = "select count(*) ROW_COUNT "
        sql += "FROM (Select   ROW_NUMBER()  "
        sql += "over (partition by rej.DataRecordID order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_FW53 FW53 "
        sql += "left join RDM_StagingDB..TMP_CU cu "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif(FW53.COUNTERPARTY_CIF,'') "
        sql += "and cu.CIF_NUMBER is not null "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej  "
        sql += "on rej.DataSourceTbName = 'TMP_FW53' "
        sql += "and rej.DataRecordID = FW53.TMP_FW53ID "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "and rej.ColumnName in ('GL_ACCOUNT_ID','AS_OF_DATE','ORIGINATION_DATE','MATURITY_DATE')   "
        sql += "and GSB_BUY_CCY_AMT is not null and  GSB_BUY_CCY_AMT <> '' "
        sql += "and  (ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_BUY_CCY_AMT) = 1 THEN GSB_BUY_CCY_AMT ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += ") t1 where t1.id = 1"


        dt = dbCon.ExecuteReader(sql)

        cntRow = Convert.ToInt64(dt(0)("ROW_COUNT"))

        Return cntRow

    End Function



    Public Function GetDataFW53() As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "select FW53.TMP_FW53ID,FW53.CIF_NUMBER "
        sql += ",REPLACE(FW53.ID_NUMBER,' ','') + SETTLEMENT_DATE ACCNO "
        sql += ",FW53.CustName "
        sql += ",FORMAT(ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_BUY_CCY_AMT) = 1 THEN GSB_BUY_CCY_AMT ELSE null END AS decimal(38, 2)),0) * 0.000001,'##.00') as GSB_BUY_CCY_AMT "
        sql += ",FW53.ORIGINATION_DATE "
        sql += "FROM (Select  TMP_FW53ID,FW53.COUNTERPARTY_CIF as CIF_NUMBER  "
        sql += ",FW53.ID_NUMBER  as ID_NUMBER "
        sql += ",FW53.SETTLEMENT_DATE "
        sql += ",ISNULL(CU.CUST_NAME,'') +' ' + ISNULL(CU.CUST_SURNAME,'') as CustName  "
        sql += ",FW53.GSB_BUY_CCY_AMT "
        sql += ",FW53.ORIGINATION_DATE  ,ROW_NUMBER() "
        sql += "over (partition by rej.DataRecordID order by rej.DataRecordID) id  "
        sql += "From RDM_StagingDB..TMP_FW53 FW53  left join RDM_StagingDB..TMP_CU cu "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( FW53.COUNTERPARTY_CIF,'')  "
        sql += "and cu.CIF_NUMBER is not null  left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName like '%TMP_FW53' and rej.DataRecordID = FW53.TMP_FW53ID  "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "and rej.ColumnName in ('GL_ACCOUNT_ID','AS_OF_DATE','ORIGINATION_DATE','MATURITY_DATE')   "
        sql += "and GSB_BUY_CCY_AMT is not null and  GSB_BUY_CCY_AMT <> '' "
        sql += "and  (ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_BUY_CCY_AMT) = 1 THEN GSB_BUY_CCY_AMT ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += ") FW53   where FW53.id = 1  order by  FW53.CIF_NUMBER "

        dt = dbCon.ExecuteReader(sql)

        Return dt




    End Function


    Function GetFW53ByCriteria(ByVal cifNum As String, ByVal accNo As String, ByVal cifName As String, ByVal cifSur As String) As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())



        sql = "select FW53.TMP_FW53ID,FW53.CIF_NUMBER "
        sql += ",REPLACE(FW53.ID_NUMBER,' ','') + SETTLEMENT_DATE ACCNO "
        sql += ",FW53.CustName "
        sql += ",FORMAT(ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_BUY_CCY_AMT) = 1 THEN GSB_BUY_CCY_AMT ELSE null END AS decimal(38, 2)),0) * 0.000001,'##.00') as GSB_BUY_CCY_AMT "
        sql += ",FW53.ORIGINATION_DATE "
        sql += ",FW53.CUST_NAME,FW53.CUST_SURNAME "
        sql += "FROM (Select  TMP_FW53ID,FW53.COUNTERPARTY_CIF as CIF_NUMBER  "
        sql += ",FW53.ID_NUMBER  as ID_NUMBER "
        sql += ",FW53.SETTLEMENT_DATE "
        sql += ",ISNULL(CU.CUST_NAME,'') +' ' + ISNULL(CU.CUST_SURNAME,'') as CustName  "
        sql += ",FW53.GSB_BUY_CCY_AMT "
        sql += ",FW53.ORIGINATION_DATE   "
        sql += ",ISNULL(CUST_NAME,'') CUST_NAME,ISNULL(CUST_SURNAME,'') CUST_SURNAME "
        sql += ",ROW_NUMBER() over (partition by rej.DataRecordID order by rej.DataRecordID) id  "
        sql += "From RDM_StagingDB..TMP_FW53 FW53  left join RDM_StagingDB..TMP_CU cu "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( FW53.COUNTERPARTY_CIF,'')  "
        sql += "and cu.CIF_NUMBER is not null  left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName like '%TMP_FW53' and rej.DataRecordID = FW53.TMP_FW53ID  "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "and rej.ColumnName in ('GL_ACCOUNT_ID','AS_OF_DATE','ORIGINATION_DATE','MATURITY_DATE')   "
        sql += "and GSB_BUY_CCY_AMT is not null and  GSB_BUY_CCY_AMT <> '' "
        sql += "and  (ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_BUY_CCY_AMT) = 1 THEN GSB_BUY_CCY_AMT ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += ") FW53   where FW53.id = 1  "




        If Not String.IsNullOrEmpty(cifNum) Then
            sql += "and (CIF_NUMBER like '" + cifNum + "%' "
            sql += "or CIF_NUMBER = '" + cifNum + "') "
        End If

        If Not String.IsNullOrEmpty(accNo) Then
            sql += "and (REPLACE(FW53.ID_NUMBER,' ','') + SETTLEMENT_DATE like '" + accNo + "%' "
            sql += "or REPLACE(FW53.ID_NUMBER,' ','') + SETTLEMENT_DATE = '" + accNo + "') "
        End If

        If Not String.IsNullOrEmpty(cifName) Then
            sql += "and (Replace(Replace(Replace(Isnull( cust_name, '' ),'/',''),'[',''),']','') like '%" + cifName.Replace("/", "").Replace("[", "").Replace("]", "") + "%' "
            sql += "or Replace(Replace(Replace(Isnull( cust_name, '' ),'/',''),'[',''),']','') = '" + cifName.Replace("/", "").Replace("[", "").Replace("]", "") + "') "
        End If


        If Not String.IsNullOrEmpty(cifSur) Then
            sql += "and (ISNULL(CUST_SURNAME,'') like '" + cifSur + "%' "
            sql += "or ISNULL(CUST_SURNAME,'') = '" + cifSur + "') "
        End If


        sql += "order by  FW53.CIF_NUMBER "

        dt = dbCon.ExecuteReader(sql)

        Return dt


    End Function



    Public Function GetRejData(ByVal rejId As String) As List(Of RejLeistEntity)

        Dim sql As String
        Dim dt As DataTable
        Dim dtTMP As DataTable

        Dim lsRej As New List(Of RejLeistEntity)


        sql = "SELECT  ColumnName,ColumnValue,RejectReason "
        sql += ",ColumnLength,RejectFlag,RejectReason,DataRecordID,RejectDate "
        sql += "FROM [RDM_VLDDB]..[REJ_RejectLog] "
        sql += "WHERE [DataSourceTbName] = 'TMP_FW53' "
        sql += "and RejectFlag = 1 "
        sql += "and DataRecordID = '" + rejId + "' "
        sql += "and ColumnName in ('GL_ACCOUNT_ID','AS_OF_DATE','ORIGINATION_DATE','MATURITY_DATE')   "


        dt = dbCon.ExecuteReader(sql)

        For Each rs As DataRow In dt.Rows

            Dim ent As New RejLeistEntity

            ent.ColumnName = rs("ColumnName").ToString()
            ent.ColumnValue = rs("ColumnValue").ToString()
            ent.RejectReason = rs("RejectReason").ToString()
            ent.DataRecordID = rs("DataRecordID").ToString()

            If String.IsNullOrEmpty(ent.ColumnValue) Then
                ent.ColumnValue = "-"
            End If



            'Get ColumnConrect
            sql = String.Empty


            sql = "SELECT " + Trim(ent.ColumnName) + " "
            sql += " as TMP_Values "
            sql += "FROM  RDM_StagingDB..TMP_FW53 "
            sql += "WHERE TMP_FW53ID = '" + rejId + "' "


            dtTMP = dbCon.ExecuteReader(sql)


            If dtTMP.Rows.Count <> 0 Then

                Dim TMP_Values As String = dtTMP(0)("TMP_Values").ToString()

                If Trim(TMP_Values) = Trim(ent.ColumnValue) Then
                    ent.ColumnConrect = ""
                Else

                    ent.ColumnConrect = TMP_Values
                End If
            Else
                ent.ColumnConrect = ""
            End If


            'Get ModifyDate
            sql = String.Empty

            sql = "SELECT Max(UpdateDate) ModifyDate "
            sql += "FROM [RDM_StagingDB]..[AdjustLog] "
            sql += "WHERE RecordID = '" + rejId + "' "
            sql += "and ColumnName = '" + ent.ColumnName + "' "
            sql += "and Source = 'TMP_FW53' "

            dtTMP = dbCon.ExecuteReader(sql)


            If dtTMP.Rows.Count <> 0 Then

                If dtTMP(0)("ModifyDate").ToString <> Nothing Then

                    Dim modifyDate As DateTime = CDate(dtTMP(0)("ModifyDate"))
                    modifyDate = modifyDate.AddYears(543)
                    ent.ModifyDate = modifyDate

                Else

                    Dim modifyDate As DateTime = CDate(rs("RejectDate"))
                    modifyDate = modifyDate.AddYears(543)
                    ent.ModifyDate = modifyDate

                End If



            Else
                Dim modifyDate As DateTime = CDate(rs("RejectDate"))
                modifyDate = modifyDate.AddYears(543)
                ent.ModifyDate = modifyDate
            End If


            lsRej.Add(ent)

        Next


        Return lsRej



    End Function



    Public Function GetRejData_Report() As List(Of RejListReportEntity)

        Dim sql As String
        Dim dt As DataTable
        Dim dtTMP As DataTable

        Dim lsRej As New List(Of RejListReportEntity)

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "SELECT  rej.ColumnName,rej.ColumnValue,rej.RejectReason,rej.RejectDate "
        sql += ",rej.ColumnLength,rej.RejectFlag,rej.DataRecordID "
        sql += ",(select COUNTERPARTY_CIF from [RDM_StagingDB]..TMP_FW53 where TMP_FW53ID = rej.DataRecordID) CifNumber "
        sql += ",(select REPLACE(FW53.ID_NUMBER,' ','') + SETTLEMENT_DATE  from [RDM_StagingDB]..TMP_FW53 where TMP_FW53ID = rej.DataRecordID) AccNo "
        sql += "FROM [RDM_VLDDB]..[REJ_RejectLog] rej "
        sql += "inner join RDM_StagingDB..TMP_FW53 FW53 "
        sql += "on rej.DataSourceTbName like '%TMP_FW53' "
        sql += "and rej.DataRecordID = FW53.TMP_FW53ID "
        sql += "WHERE RejectFlag = '1'"
        sql += "and rej.ColumnName in ('GL_ACCOUNT_ID','AS_OF_DATE','ORIGINATION_DATE','MATURITY_DATE')   "
        sql += "and GSB_BUY_CCY_AMT is not null "
        sql += "and  GSB_BUY_CCY_AMT <> '' "
        sql += "and GSB_BUY_CCY_AMT is not null and  GSB_BUY_CCY_AMT <> '' "
        sql += "and  (ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_BUY_CCY_AMT) = 1 THEN GSB_BUY_CCY_AMT ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += "order by CifNumber"



        dt = dbCon.ExecuteReader(sql)


        For Each rs As DataRow In dt.Rows

            Dim ent As New RejListReportEntity

            ent.CifNumber = rs("CifNumber").ToString()
            ent.AccNo = rs("AccNo").ToString()
            ent.ColumnName = rs("ColumnName").ToString()
            ent.ColumnValue = rs("ColumnValue").ToString()
            ent.RejectReason = rs("RejectReason").ToString()
            ent.DataRecordID = rs("DataRecordID").ToString()


            If String.IsNullOrEmpty(ent.ColumnValue) Then
                ent.ColumnValue = "-"
            End If



            'Get ColumnConrect
            sql = String.Empty


            sql = "SELECT " + Trim(ent.ColumnName) + " "
            sql += " as TMP_Values "
            sql += "FROM  RDM_StagingDB..TMP_FW53 "
            sql += "WHERE TMP_FW53ID = '" + rs("DataRecordID").ToString() + "' "


            dtTMP = dbCon.ExecuteReader(sql)


            If dtTMP.Rows.Count <> 0 Then

                Dim TMP_Values As String = dtTMP(0)("TMP_Values").ToString()

                If Trim(TMP_Values) = Trim(ent.ColumnValue) Then
                    ent.ColumnConrect = ""
                Else

                    ent.ColumnConrect = TMP_Values
                End If
            Else
                ent.ColumnConrect = ""
            End If


            'Get ModifyDate
            sql = String.Empty

            sql = "SELECT Max(UpdateDate) ModifyDate "
            sql += "FROM [RDM_StagingDB]..[AdjustLog] "
            sql += "WHERE RecordID = '" + rs("DataRecordID").ToString() + "' "
            sql += "and ColumnName = '" + ent.ColumnName + "' "
            sql += "and Source = 'TMP_FW53' "

            dtTMP = dbCon.ExecuteReader(sql)


            If dtTMP.Rows.Count <> 0 Then

                If dtTMP(0)("ModifyDate").ToString <> Nothing Then

                    Dim modifyDate As DateTime = CDate(dtTMP(0)("ModifyDate"))
                    modifyDate = modifyDate.AddYears(543)
                    ent.ModifyDate = modifyDate

                Else

                    Dim modifyDate As DateTime = CDate(rs("RejectDate"))
                    modifyDate = modifyDate.AddYears(543)
                    ent.ModifyDate = modifyDate

                End If

            Else
                Dim modifyDate As DateTime = CDate(rs("RejectDate"))
                modifyDate = modifyDate.AddYears(543)
                ent.ModifyDate = modifyDate
            End If


            lsRej.Add(ent)

        Next


        Return lsRej

    End Function






    Public Sub Save_edit(ByVal TMP_FW53ID As String, ByVal columnName As String, ByVal columnValues As String)

        Try

            Dim sql As String = String.Empty

            sql = "UPDATE RDM_StagingDB..TMP_FW53 "
            sql += "SET " + columnName + " = " + "'" + columnValues + "' "
            sql += "where TMP_FW53ID  = " + TMP_FW53ID


            dbCon.ExecuteNonQuery(sql)


            Insert_HistoryFW53(TMP_FW53ID, columnName)


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Insert_HistoryFW53(TMP_FW53ID As String, columnName As String)
        Try

            Dim sql As String = String.Empty


            sql = "INSERT INTO RDM_StagingDB..AdjustLog(Source, RecordID, UpdateBy, UpdateDate,ColumnName) "
            sql += "VALUES( " + " 'TMP_FW53'," + TMP_FW53ID + ", 1  ,  getdate() ,'" + columnName + "') "

            dbCon.ExecuteNonQuery(sql)

        Catch ex As Exception

        End Try
    End Sub







End Class
