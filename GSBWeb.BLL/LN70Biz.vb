
Imports GSBWeb.DAL
Imports System.Web.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Web.HttpApplicationState
Imports System.Web.HttpServerUtility
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports GSBWeb.BLL.UtilsBiz




Public Class LN70Biz

    Dim dbCon As New DBclass



    Public Sub New()

    End Sub



    Public Function Count_ListLN70() As Int64

        Dim sql As String
        Dim dt As DataTable
        Dim cntRow As Int64


        Dim amountString As String = CStr(GetAmountTotal())


        sql = "  select count(*) ROW_COUNT "
        sql += " FROM (select   ROW_NUMBER() over (partition by ln.SUB_ACCNO order by SUB_ACCNO) id "
        sql += " from RDM_StagingDB..TMP_LN70 ln "
        sql += " inner join RDM_StagingDB..TMP_CU70 cu on cu.CIF_NUMBER = ln.DEBTER_CID "
        sql += " left join RDM_VLDDB..REJ_RejectLog rej on rej.DataSourceTbName = 'TMP_LN70' "
        sql += " and rej.DataRecordID = ln.TMP_LN70ID "
        sql += " where RejectFlag not in (2,3,4)"
        sql += "and rej.ColumnName in ('LNTYPECODE','LNSUBTYPEID','LNMKTCODE','ACC_MATUREDATE','NORMALRATE','GL_ACCOUNT_ID','DRAWDOWNDATE') "
        sql += "and ( case when   INT_REONGNITE_STATUS = '1'  then  CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) ) + "
        sql += "cast( CAST(ISNULL(ACRINTLASTMLY,0) as numeric(20,4)) as decimal(24,0))   when   INT_REONGNITE_STATUS =  '2' "
        sql += "then CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) )  end) > " + amountString + " "
        sql += ") t1 "
        sql += " where t1.id = 1 "


        dt = dbCon.ExecuteReader(sql)

       
        cntRow = Convert.ToInt64(dt(0)("ROW_COUNT"))
       
        Return cntRow

    End Function


    Public Function GetDataLN70() As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())



        sql = "select   ln.TMP_LN70ID,ln.CIF_NUMBER,ln.SUB_ACCNO "
        sql += ", ln.CustName,ln.OUTSTBALANCE,ln.ACRINTLASTMLY, ln.AS_OF_DATE,ln.LNTYPECODE,ln.INT_REONGNITE_STATUS "
        sql += ",ln.LNSUBTYPEID,ln.LNMKTCODE,ln.ACC_MATUREDATE "
        sql += ",ln.NORMALRATE,ln.SIGNDATE, ln.DEPTCODE_COSTCENTER "
        sql += ",ln.BOTOBJCODE,ln.OBJID,ln.DRAWDOWNDATE,ln.STATUSCODE "
        sql += ",ln.ISO_CURRENCY_CD,ln.BAD_DEBT_FLG "
        sql += "from (select   ln.TMP_LN70ID,ln.DEBTER_CID  as CIF_NUMBER,ln.SUB_ACCNO SUB_ACCNO "
        sql += ",ISNULL(CUST_NAME,'') +' ' + ISNULL(CUST_SURNAME,'') as CustName,ln.OUTSTBALANCE,ln.ACRINTLASTMLY,ln.INT_REONGNITE_STATUS "
        sql += ",ln.AS_OF_DATE ,LNTYPECODE,LNSUBTYPEID ,LNMKTCODE "
        sql += ",ACC_MATUREDATE ,NORMALRATE,SIGNDATE,DEPTCODE_COSTCENTER,BOTOBJCODE,GL_ACCOUNT_ID "
        sql += ",OBJID,DRAWDOWNDATE,STATUSCODE,ISO_CURRENCY_CD,ln.bad_debt_flg "
        sql += ",ROW_NUMBER() over (partition by  rej.DataRecordID  order by rej.DataRecordID) id "
        sql += "from RDM_StagingDB..TMP_LN70 ln inner join RDM_StagingDB..TMP_CU70 cu "
        sql += "on cu.CIF_NUMBER = ln.DEBTER_CID "
        sql += "left join RDM_VLDDB.dbo.REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName = 'TMP_LN70' "
        sql += "and rej.DataRecordID = ln.TMP_LN70ID "
        sql += "where RejectFlag not in (2,3,4)"
        sql += "and rej.ColumnName in ('LNTYPECODE','LNSUBTYPEID','LNMKTCODE','ACC_MATUREDATE','NORMALRATE','GL_ACCOUNT_ID','DRAWDOWNDATE') "
        sql += "and ( case when   INT_REONGNITE_STATUS = '1'  then  CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) ) + "
        sql += "cast( CAST(ISNULL(ACRINTLASTMLY,0) as numeric(20,4)) as decimal(24,0))   when   INT_REONGNITE_STATUS =  '2' "
        sql += "then CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) )  end) > " + amountString + " "
        sql += " ) ln "
        sql += "where ln.id = 1  "
        sql += "order by  ln.CIF_NUMBER "


        dt = dbCon.ExecuteReader(sql)

        Return dt

    End Function

    Public Function GetNameLN70(ByVal cifNum As String) As DataTable

        Dim sql As String
        Dim dt As DataTable

        sql = "select  CUST_NAME,CUST_SURNAME "
        sql += "from RDM_StagingDB..TMP_CU70 "
        sql += "where  CIF_NUMBER = '" + cifNum + "' "

        dt = dbCon.ExecuteReader(sql)

        Return dt

    End Function


    Function GetLN70ByCriteria(ByVal cifNum As String, ByVal accNo As String, ByVal cifName As String, ByVal cifSur As String) As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())

        sql = "select   ln.TMP_LN70ID,ln.CIF_NUMBER,ln.SUB_ACCNO "
        sql += ", ln.CustName,ln.OUTSTBALANCE,ln.ACRINTLASTMLY, ln.AS_OF_DATE,ln.LNTYPECODE,ln.INT_REONGNITE_STATUS "
        sql += ",ln.LNSUBTYPEID,ln.LNMKTCODE,ln.ACC_MATUREDATE "
        sql += ",ln.NORMALRATE,ln.SIGNDATE, ln.DEPTCODE_COSTCENTER "
        sql += ",ln.BOTOBJCODE,ln.OBJID,ln.DRAWDOWNDATE,ln.STATUSCODE "
        sql += ",ln.ISO_CURRENCY_CD,ln.BAD_DEBT_FLG "
        sql += ",ln.CUST_NAME,ln.CUST_SURNAME "
        sql += "from (select   ln.TMP_LN70ID,ln.DEBTER_CID  as CIF_NUMBER,ln.SUB_ACCNO SUB_ACCNO "
        sql += ",ISNULL(CUST_NAME,'') +' ' + ISNULL(CUST_SURNAME,'') as CustName,ln.OUTSTBALANCE,ln.ACRINTLASTMLY,ln.INT_REONGNITE_STATUS "
        sql += ",ln.AS_OF_DATE ,LNTYPECODE,LNSUBTYPEID ,LNMKTCODE "
        sql += ",ACC_MATUREDATE ,NORMALRATE,SIGNDATE,DEPTCODE_COSTCENTER,BOTOBJCODE,GL_ACCOUNT_ID "
        sql += ",OBJID,DRAWDOWNDATE,STATUSCODE,ISO_CURRENCY_CD,ln.bad_debt_flg "
        sql += ",ISNULL(CUST_NAME,'') CUST_NAME,ISNULL(CUST_SURNAME,'') CUST_SURNAME "
        sql += ",ROW_NUMBER() over (partition by  rej.DataRecordID  order by rej.DataRecordID) id "
        sql += "from RDM_StagingDB..TMP_LN70 ln inner join RDM_StagingDB..TMP_CU70 cu "
        sql += "on cu.CIF_NUMBER = ln.DEBTER_CID "
        sql += "left join RDM_VLDDB.dbo.REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName = 'TMP_LN70' "
        sql += "and rej.DataRecordID = ln.TMP_LN70ID "
        sql += "where RejectFlag not in (2,3,4)"
        sql += "and rej.ColumnName in ('LNTYPECODE','LNSUBTYPEID','LNMKTCODE','ACC_MATUREDATE','NORMALRATE','GL_ACCOUNT_ID','DRAWDOWNDATE') "
        sql += "and ( case when   INT_REONGNITE_STATUS = '1'  then  CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) ) + "
        sql += "cast( CAST(ISNULL(ACRINTLASTMLY,0) as numeric(20,4)) as decimal(24,0))   when   INT_REONGNITE_STATUS =  '2' "
        sql += "then CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) )  end) > " + amountString + " "
        sql += " ) ln "
        sql += "where ln.id = 1  "


        If Not String.IsNullOrEmpty(cifNum) Then
            sql += "and (CIF_NUMBER like '" + cifNum + "%' "
            sql += "or CIF_NUMBER = '" + cifNum + "') "
        End If

        If Not String.IsNullOrEmpty(accNo) Then
            sql += "and (ln.SUB_ACCNO like '" + accNo + "%' "
            sql += "or ln.SUB_ACCNO = '" + accNo + "') "
        End If

        If Not String.IsNullOrEmpty(cifName) Then
            sql += "and (ISNULL(CUST_NAME,'') like '" + cifName + "%' "
            sql += "or ISNULL(CUST_NAME,'') = '" + cifName + "') "
        End If


        If Not String.IsNullOrEmpty(cifSur) Then
            sql += "and (ISNULL(CUST_SURNAME,'') like '" + cifSur + "%' "
            sql += "or ISNULL(CUST_SURNAME,'') = '" + cifSur + "') "
        End If


        sql += "order by  ln.CIF_NUMBER "

        dt = dbCon.ExecuteReader(sql)

        Return dt


    End Function


    Public Function GetRejData(ByVal rejId As String) As List(Of RejLeistEntity)

        Dim sql As String
        Dim dt As DataTable
        Dim dtTMP As DataTable

        Dim lsRej As New List(Of RejLeistEntity)



        sql = "SELECT  ColumnName,ColumnValue,RejectReason "
        sql += ",ColumnLength,RejectFlag,DataRecordID,RejectDate "
        sql += "FROM [RDM_VLDDB].[dbo].[REJ_RejectLog] "
        sql += "WHERE RejectFlag = '1' and [DataSourceTbName] = 'TMP_LN70' "
        sql += "and DataRecordID = '" + rejId + "' "
        sql += "and ColumnName in ('LNTYPECODE','LNSUBTYPEID','LNMKTCODE','ACC_MATUREDATE','NORMALRATE','GL_ACCOUNT_ID','DRAWDOWNDATE') "


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
            sql += "FROM  RDM_StagingDB..TMP_LN70 "
            sql += "WHERE TMP_LN70ID = '" + rejId + "' "


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
            sql += "and Source = 'TMP_LN70' "

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
        sql += ",(select DEBTER_CID from [RDM_StagingDB]..TMP_LN70 where TMP_LN70ID = rej.DataRecordID) CifNumber "
        sql += ",(select SUB_ACCNO  from [RDM_StagingDB]..TMP_LN70 where TMP_LN70ID = rej.DataRecordID) AccNo "
        sql += "FROM [RDM_VLDDB]..[REJ_RejectLog] rej "
        sql += "inner join RDM_StagingDB..TMP_LN70 ln "
        sql += "on rej.DataSourceTbName = 'TMP_LN70' "
        sql += "and rej.DataRecordID = ln.TMP_LN70ID "
        sql += "WHERE RejectFlag = '1' "
        sql += "and [DataSourceTbName] = 'TMP_LN70' "
        sql += "and ColumnName in('LNTYPECODE','LNSUBTYPEID','LNMKTCODE','ACC_MATUREDATE','NORMALRATE','GL_ACCOUNT_ID','DRAWDOWNDATE') "
        sql += "and ( case when   INT_REONGNITE_STATUS = '0' "
        sql += "then  CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) ) "
        sql += "+ cast( CAST(ISNULL(ACRINTLASTMLY,0) as numeric(20,4)) as decimal(24,0)) "
        sql += "when   INT_REONGNITE_STATUS =  '1' "
        sql += "then CAST( ISNULL(OUTSTBALANCE,0)   as decimal(24,0) ) "
        sql += "end) > " + amountString + " "
        sql += "order by CifNumber "


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
            sql += "FROM  RDM_StagingDB..TMP_LN70 "
            sql += "WHERE TMP_LN70ID = '" + rs("DataRecordID").ToString() + "' "


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
            sql += "and Source = 'TMP_LN70' "

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





    Public Sub Save_edit(ByVal TMP_LN70ID As String, ByVal columnName As String, ByVal columnValues As String)

        Try

            Dim sql As String = String.Empty

            sql = "UPDATE RDM_StagingDB..TMP_LN70 "
            sql += "SET " + columnName + " = " + "'" + columnValues + "' "
            sql += "where TMP_LN70ID  = " + TMP_LN70ID


            dbCon.ExecuteNonQuery(sql)


            Insert_HistoryLN70(TMP_LN70ID, columnName)


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Insert_HistoryLN70(TMP_LN70ID As String, columnName As String)
        Try

            Dim sql As String = String.Empty


            sql = "  INSERT INTO RDM_StagingDB..AdjustLog(Source, RecordID, UpdateBy, UpdateDate,ColumnName)"
            sql += " VALUES( " + " 'TMP_LN70'," + TMP_LN70ID + ", 1  ,  getdate() ,'" + columnName + "')"

            dbCon.ExecuteNonQuery(sql)

        Catch ex As Exception

        End Try

    End Sub


End Class


