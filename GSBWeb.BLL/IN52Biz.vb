Imports GSBWeb.DAL
Imports GSBWeb.BLL.UtilsBiz




Public Class IN52Biz

    Dim dbCon As New DBclass


    Public Sub New()


    End Sub



    Public Function Count_IN52() As Int64

        Dim sql As String
        Dim dt As DataTable
        Dim cntRow As Int64

        Dim amountString As String = CStr(GetAmountTotal())

        sql = "select count(*) ROW_COUNT "
        sql += "FROM(Select  ROW_NUMBER() over (partition by rej.DataRecordID order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_IN52 IN52  "
        sql += "left join RDM_StagingDB..TMP_CU cu "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( in52.Issuer_CIF,'')  "
        sql += "and cu.CIF_NUMBER is not null left join RDM_VLDDB..REJ_RejectLog rej  "
        sql += "on rej.DataSourceTbName = 'TMP_IN52' "
        sql += "and rej.DataRecordID = IN52.TMP_IN52ID  "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "AND rej.ColumnName in ('GL_ACCOUNT_ID','CUR_GROSS_RATE','SETTLEMENT_DATE','ORIGINATION_DATE') "
        sql += "and CUR_BOOK_BAL is not null and  CUR_BOOK_BAL<> '' "
        sql += "and GSB_INT_ACCRUAL is not null and  GSB_INT_ACCRUAL <> '' "
        sql += "And (ISNULL(CAST(CASE WHEN ISNUMERIC(CUR_BOOK_BAL) = 1 "
        sql += "THEN CUR_BOOK_BAL ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += "+ ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_INT_ACCRUAL) = 1 THEN GSB_INT_ACCRUAL ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += ") t1 where t1.id = 1"



        dt = dbCon.ExecuteReader(sql)

        cntRow = Convert.ToInt64(dt(0)("ROW_COUNT"))

        Return cntRow

    End Function




    Public Function GetDataIN52() As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "select IN52.TMP_IN52ID,IN52.CIF_NUMBER "
        sql += ",REPLACE(IN52.ID_NUMBER,' ','') + SETTLEMENT_DATE + POSITION_ID ACCNO  "
        sql += ",IN52.CustName "
        sql += ",FORMAT(ISNULL(CAST(CASE WHEN ISNUMERIC(CUR_BOOK_BAL) = 1 THEN CUR_BOOK_BAL ELSE null END AS decimal(38, 2)),0) * 0.000001,'##.00') as CUR_BOOK_BAL"
        sql += ",IN52.GSB_INT_ACCRUAL ,IN52.ORIGINATION_DATE "
        sql += "FROM (Select  TMP_IN52ID,IN52.Issuer_CIF as CIF_NUMBER "
        sql += ",IN52.ID_NUMBER  as ID_NUMBER  "
        sql += ",IN52.SETTLEMENT_DATE,IN52.POSITION_ID "
        sql += ",ISNULL(CU.CUST_NAME,'') +' ' + ISNULL(CU.CUST_SURNAME,'') as CustName  "
        sql += ",IN52.CUR_BOOK_BAL,IN52.GSB_INT_ACCRUAL,IN52.ORIGINATION_DATE  "
        sql += ",ROW_NUMBER() over (partition by rej.DataRecordID order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_IN52 IN52  "
        sql += "left join RDM_StagingDB..TMP_CU cu  "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( IN52.Issuer_CIF,'') "
        sql += "and cu.CIF_NUMBER is not null  "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName like '%TMP_IN52' "
        sql += "and rej.DataRecordID = IN52.TMP_IN52ID "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "AND rej.ColumnName in ('GL_ACCOUNT_ID','CUR_GROSS_RATE','SETTLEMENT_DATE','ORIGINATION_DATE') "
        sql += "and CUR_BOOK_BAL is not null  "
        sql += "and  CUR_BOOK_BAL<> ''  "
        sql += "and GSB_INT_ACCRUAL is not null and  GSB_INT_ACCRUAL <> '' "
        sql += "And (ISNULL(CAST(CASE WHEN ISNUMERIC(CUR_BOOK_BAL) = 1 THEN CUR_BOOK_BAL ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += "+ ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_INT_ACCRUAL) = 1 THEN GSB_INT_ACCRUAL ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += ") IN52   where IN52.id = 1  order by  IN52.CIF_NUMBER "


        dt = dbCon.ExecuteReader(sql)

        Return dt


    End Function


    Function GetIN52ByCriteria(ByVal cifNum As String, ByVal accNo As String, ByVal cifName As String, ByVal cifSur As String) As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())



        sql = "select IN52.TMP_IN52ID,IN52.CIF_NUMBER "
        sql += ",REPLACE(IN52.ID_NUMBER,' ','') + SETTLEMENT_DATE + POSITION_ID ACCNO  "
        sql += ",IN52.CustName "
        sql += ",FORMAT(ISNULL(CAST(CASE WHEN ISNUMERIC(CUR_BOOK_BAL) = 1 THEN CUR_BOOK_BAL ELSE null END AS decimal(38, 2)),0) * 0.000001,'##.00') as CUR_BOOK_BAL"
        sql += ",IN52.GSB_INT_ACCRUAL ,IN52.ORIGINATION_DATE "
        sql += ",IN52.CUST_NAME,IN52.CUST_SURNAME "
        sql += "FROM (Select  TMP_IN52ID,IN52.Issuer_CIF as CIF_NUMBER "
        sql += ",IN52.ID_NUMBER  as ID_NUMBER  "
        sql += ",IN52.SETTLEMENT_DATE,IN52.POSITION_ID "
        sql += ",ISNULL(CU.CUST_NAME,'') +' ' + ISNULL(CU.CUST_SURNAME,'') as CustName  "
        sql += ",IN52.CUR_BOOK_BAL,IN52.GSB_INT_ACCRUAL,IN52.ORIGINATION_DATE  "
        sql += ",ISNULL(CUST_NAME,'') CUST_NAME,ISNULL(CUST_SURNAME,'') CUST_SURNAME "
        sql += ",ROW_NUMBER() over (partition by rej.DataRecordID order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_IN52 IN52  "
        sql += "left join RDM_StagingDB..TMP_CU cu  "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( IN52.Issuer_CIF,'') "
        sql += "and cu.CIF_NUMBER is not null  "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName like '%TMP_IN52' "
        sql += "and rej.DataRecordID = IN52.TMP_IN52ID "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "AND rej.ColumnName in ('GL_ACCOUNT_ID','CUR_GROSS_RATE','SETTLEMENT_DATE','ORIGINATION_DATE') "
        sql += "and CUR_BOOK_BAL is not null  "
        sql += "and  CUR_BOOK_BAL<> ''  "
        sql += "and GSB_INT_ACCRUAL is not null and  GSB_INT_ACCRUAL <> '' "
        sql += "And (ISNULL(CAST(CASE WHEN ISNUMERIC(CUR_BOOK_BAL) = 1 THEN CUR_BOOK_BAL ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += "+ ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_INT_ACCRUAL) = 1 THEN GSB_INT_ACCRUAL ELSE null END AS decimal(38, 2)),0) * 0.000001) "
        sql += " > " + amountString + " "
        sql += ") IN52   where IN52.id = 1 "




        If Not String.IsNullOrEmpty(cifNum) Then
            sql += "and (CIF_NUMBER like '" + cifNum + "%' "
            sql += "or CIF_NUMBER = '" + cifNum + "') "
        End If

        If Not String.IsNullOrEmpty(accNo) Then
            sql += "and (REPLACE(IN52.ID_NUMBER,' ','') + SETTLEMENT_DATE + POSITION_ID like '" + accNo + "%' "
            sql += "or REPLACE(IN52.ID_NUMBER,' ','') + SETTLEMENT_DATE + POSITION_ID = '" + accNo + "') "
        End If

        If Not String.IsNullOrEmpty(cifName) Then
            sql += "and (Replace(Replace(Replace(Isnull( cust_name, '' ),'/',''),'[',''),']','') like '%" + cifName.Replace("/", "").Replace("[", "").Replace("]", "") + "%' "
            sql += "or Replace(Replace(Replace(Isnull( cust_name, '' ),'/',''),'[',''),']','') = '" + cifName.Replace("/", "").Replace("[", "").Replace("]", "") + "') "
        End If


        If Not String.IsNullOrEmpty(cifSur) Then
            sql += "and (ISNULL(CUST_SURNAME,'') like '" + cifSur + "%' "
            sql += "or ISNULL(CUST_SURNAME,'') = '" + cifSur + "') "
        End If


        sql += "order by  IN52.CIF_NUMBER "

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
        sql += "WHERE [DataSourceTbName] = 'TMP_IN52' "
        sql += "and RejectFlag = 1 "
        sql += "and DataRecordID = '" + rejId + "' "
        sql += "AND ColumnName in ('GL_ACCOUNT_ID','CUR_GROSS_RATE','SETTLEMENT_DATE','ORIGINATION_DATE') "


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
            sql += "FROM  RDM_StagingDB..TMP_IN52 "
            sql += "WHERE TMP_IN52ID = '" + rejId + "' "


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
            sql += "and Source = 'TMP_IN52' "

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
        sql += ",(select Issuer_CIF from [RDM_StagingDB]..TMP_IN52 where TMP_IN52ID = rej.DataRecordID) CifNumber "
        sql += ",(select ID_NUMBER + SETTLEMENT_DATE+POSITION_ID  from [RDM_StagingDB]..TMP_IN52 where TMP_IN52ID = rej.DataRecordID) AccNo "
        sql += "FROM [RDM_VLDDB]..[REJ_RejectLog] rej "
        sql += "inner join RDM_StagingDB..TMP_IN52 IN52 "
        sql += "on rej.DataSourceTbName like '%TMP_IN52' "
        sql += "and rej.DataRecordID = IN52.TMP_IN52ID "
        sql += "WHERE RejectFlag = '1'"
        sql += "and CUR_BOOK_BAL is not null  "
        sql += "and  CUR_BOOK_BAL<> '' "
        sql += "and GSB_INT_ACCRUAL is not null "
        sql += "and  GSB_INT_ACCRUAL <> '' "
        sql += "AND rej.ColumnName in ('GL_ACCOUNT_ID','CUR_GROSS_RATE','SETTLEMENT_DATE','ORIGINATION_DATE') "
        sql += "And (ISNULL(CAST(CASE WHEN ISNUMERIC(CUR_BOOK_BAL) = 1 "
        sql += "THEN CUR_BOOK_BAL ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += "+ ISNULL(CAST(CASE WHEN ISNUMERIC(GSB_INT_ACCRUAL) = 1 "
        sql += "THEN GSB_INT_ACCRUAL ELSE null END AS decimal(38, 2)),0) * 0.000001) "
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
            sql += "FROM  RDM_StagingDB..TMP_IN52 "
            sql += "WHERE TMP_IN52ID = '" + rs("DataRecordID").ToString() + "' "


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
            sql += "and Source = 'TMP_IN52' "

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




    Public Sub Save_edit(ByVal TMP_IN52ID As String, ByVal columnName As String, ByVal columnValues As String)

        Try

            Dim sql As String = String.Empty

            sql = "UPDATE RDM_StagingDB..TMP_IN52 "
            sql += "SET " + columnName + " = " + "'" + columnValues + "' "
            sql += "where TMP_IN52ID  = " + TMP_IN52ID


            dbCon.ExecuteNonQuery(sql)


            Insert_HistoryIN52(TMP_IN52ID, columnName)


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Insert_HistoryIN52(TMP_IN52ID As String, columnName As String)
        Try

            Dim sql As String = String.Empty


            sql = "INSERT INTO RDM_StagingDB..AdjustLog(Source, RecordID, UpdateBy, UpdateDate,ColumnName) "
            sql += "VALUES( " + " 'TMP_IN52'," + TMP_IN52ID + ", 1  ,  getdate() ,'" + columnName + "') "

            dbCon.ExecuteNonQuery(sql)

        Catch ex As Exception

        End Try
    End Sub


End Class
