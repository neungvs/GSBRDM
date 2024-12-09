Imports GSBWeb.DAL
Imports GSBWeb.BLL.UtilsBiz


Public Class TF_NMBiz

    Dim dbCon As New DBclass


    Public Sub New()

    End Sub



    Public Function Count_TF_NM() As Int64

        Dim sql As String
        Dim dt As DataTable
        Dim cntRow As Int64

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "Select count(*) ROW_COUNT "
        sql += "FROM ( select   ROW_NUMBER() over  (partition by PositionID order by PositionID) id "
        sql += "from RDM_StagingDB..TMP_TF_NM NM "
        sql += "left join RDM_StagingDB..TMP_CU70 cu70 "
        sql += "on  Convert(varchar,CU70.CIF_NUMBER) = NM.CIF_Number "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName like'%TF_NM' "
        sql += "and rej.DataRecordID = NM.TMP_TF_NMID "
        sql += "where RejectFlag not in ('2','3','4') "
        sql += "and rej.ColumnName in ('GL_ACCOUNT','StartDate','EndDate','Currency_Original','ASSET_CLASS_TYPE') "
        sql += "and (NM.PrincipalOutstanding<> '' "
        sql += "and  NM.PrincipalOutstanding is not null) "
        sql += "and  (case  when ASSET_CLASS_TYPE in (3,4,5) "
        sql += "then  ( case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0)   as decimal(24,0) )   end) "
        sql += "else   ( case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0)   as decimal(24,0) )   end) "
        sql += "+ case when ISNUMERIC (AccruedInterest)= 1 "
        sql += "then  CAST( ISNULL(AccruedInterest,0)   as decimal(24,0) ) else 0   end  "
        sql += "end) > " + amountString + " "
        sql += ") t1 where t1.id = 1"


        dt = dbCon.ExecuteReader(sql)



        cntRow = Convert.ToInt64(dt(0)("ROW_COUNT"))

        Return cntRow

    End Function


    Public Function GetDataTF_NM() As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "Select  tf.TMP_TF_NMID,tf.PositionID,tf.CIF_Number "
        sql += ",tf.CustName ,tf.Status,tf.StartDate "
        sql += ",tf.Currency_Original ,tf.PrincipalOutstanding "
        sql += ",tf.AccruedInterest  ,tf.Coupon_Rate  "
        sql += ",tf.ASSET_CLASS_TYPE,AS_OF_DATE "
        sql += "FROM(select NM.TMP_TF_NMID,NM.CIF_NUMBER "
        sql += ",NM.PositionID ,ISNULL(cu70.CUST_NAME,'') "
        sql += "+' '+ ISNULL(cu70.CUST_SURNAME,'')  as CustName "
        sql += ",Status ,NM.StartDate ,NM.PrincipalOutstanding  "
        sql += ",NM.AccruedInterest,Currency_Original "
        sql += ",Coupon_Rate ,ASSET_CLASS_TYPE,AS_OF_DATE "
        sql += ",ROW_NUMBER()  over  (partition by rej.DataRecordID   "
        sql += "order by rej.DataRecordID) id "
        sql += "from RDM_StagingDB..TMP_TF_NM NM "
        sql += "left join RDM_StagingDB..TMP_CU70 cu70 "
        sql += "ON Convert(varchar,CU70.CIF_NUMBER) = NM.CIF_NUMBER "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName LIKE '%TF_NM' "
        sql += "and rej.DataRecordID = NM.TMP_TF_NMID "
        sql += "where RejectFlag not in (2,3,4)  "
        sql += "and rej.ColumnName in ('GL_ACCOUNT','StartDate','EndDate','Currency_Original','ASSET_CLASS_TYPE') "
        sql += "and (case  when ASSET_CLASS_TYPE  "
        sql += "in (3,4,5) then (case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0) "
        sql += "as decimal(24,0) )   end)  "
        sql += "else   ( case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0)   as decimal(24,0) )   end) "
        sql += "+ case when ISNUMERIC (AccruedInterest)= 1 "
        sql += "then  CAST( ISNULL(AccruedInterest,0)   as decimal(24,0) ) else 0   end "
        sql += "end) > " + amountString + " "
        sql += ") tf where tf.id = 1 order by tf.CIF_Number "



        dt = dbCon.ExecuteReader(sql)


        Return dt

    End Function


    Public Function GetTF_NM_ByCriteria(ByVal cifNum As String, ByVal accNo As String, ByVal cifName As String, ByVal cifSur As String) As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "Select  tf.TMP_TF_NMID,tf.PositionID,tf.CIF_Number "
        sql += ",tf.CustName ,tf.Status,tf.StartDate "
        sql += ",tf.Currency_Original ,tf.PrincipalOutstanding "
        sql += ",tf.AccruedInterest  ,tf.Coupon_Rate  "
        sql += ",tf.ASSET_CLASS_TYPE,AS_OF_DATE "
        sql += ",tf.CUST_NAME,tf.CUST_SURNAME "
        sql += "FROM(select NM.TMP_TF_NMID,NM.CIF_NUMBER "
        sql += ",NM.PositionID ,ISNULL(cu70.CUST_NAME,'') "
        sql += "+' '+ ISNULL(cu70.CUST_SURNAME,'')  as CustName "
        sql += ",Status ,NM.StartDate ,NM.PrincipalOutstanding  "
        sql += ",NM.AccruedInterest,Currency_Original "
        sql += ",Coupon_Rate ,ASSET_CLASS_TYPE,AS_OF_DATE "
        sql += ",ISNULL(CUST_NAME,'') CUST_NAME,ISNULL(CUST_SURNAME,'') CUST_SURNAME "
        sql += ",ROW_NUMBER()  over  (partition by rej.DataRecordID   "
        sql += "order by rej.DataRecordID) id "
        sql += "from RDM_StagingDB..TMP_TF_NM NM "
        sql += "left join RDM_StagingDB..TMP_CU70 cu70 "
        sql += "ON Convert(varchar,CU70.CIF_NUMBER) = NM.CIF_NUMBER "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej "
        sql += "on rej.DataSourceTbName LIKE '%TF_NM' "
        sql += "and rej.DataRecordID = NM.TMP_TF_NMID "
        sql += "where RejectFlag not in (2,3,4)  "
        sql += "and rej.ColumnName in ('GL_ACCOUNT','StartDate','EndDate','Currency_Original','ASSET_CLASS_TYPE') "
        sql += "and (case  when ASSET_CLASS_TYPE  "
        sql += "in (3,4,5) then (case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0) "
        sql += "as decimal(24,0) )   end)  "
        sql += "else   ( case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0)   as decimal(24,0) )   end) "
        sql += "+ case when ISNUMERIC (AccruedInterest)= 1 "
        sql += "then  CAST( ISNULL(AccruedInterest,0)   as decimal(24,0) ) else 0   end "
        sql += "end) > " + amountString + " "
        sql += ") tf where tf.id = 1 "


        If Not String.IsNullOrEmpty(cifNum) Then
            sql += "and (CIF_NUMBER like '" + cifNum + "%' "
            sql += "or CIF_NUMBER = '" + cifNum + "') "
        End If

        If Not String.IsNullOrEmpty(accNo) Then
            sql += "and (PositionID like '" + accNo + "%' "
            sql += "or PositionID = '" + accNo + "') "
        End If

        If Not String.IsNullOrEmpty(cifName) Then
            sql += "and (ISNULL(CUST_NAME,'') like '" + cifName + "%' "
            sql += "or ISNULL(CUST_NAME,'') = '" + cifName + "') "
        End If


        If Not String.IsNullOrEmpty(cifSur) Then
            sql += "and (ISNULL(CUST_SURNAME,'') like '" + cifSur + "%' "
            sql += "or ISNULL(CUST_SURNAME,'') = '" + cifSur + "') "
        End If


        sql += "order by tf.CIF_Number  "


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
        sql += "FROM [RDM_VLDDB].[dbo].[REJ_RejectLog] "
        sql += "WHERE [DataSourceTbName] = 'TMP_TF_NM' "
        sql += "and RejectFlag = 1 "
        sql += "and DataRecordID = '" + rejId + "' "
        sql += "and ColumnName in ('GL_ACCOUNT','StartDate','EndDate','Currency_Original','ASSET_CLASS_TYPE') "


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
            sql += "FROM  RDM_StagingDB..TMP_TF_NM "
            sql += "WHERE TMP_TF_NMID = '" + rejId + "' "


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
            sql += "and Source = 'TMP_TF_NM' "

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
        sql += ",(select CIF_Number from [RDM_StagingDB]..TMP_TF_NM where TMP_TF_NMID = rej.DataRecordID) CifNumber "
        sql += ",(select PositionID  from [RDM_StagingDB]..TMP_TF_NM where TMP_TF_NMID = rej.DataRecordID) AccNo "
        sql += "FROM [RDM_VLDDB]..[REJ_RejectLog] rej "
        sql += "inner join RDM_StagingDB..TMP_TF_NM nm "
        sql += "on rej.DataSourceTbName LIKE '%TF_NM' "
        sql += "and rej.DataRecordID = nm.TMP_TF_NMID "
        sql += "WHERE RejectFlag = '1' "
        sql += "and rej.ColumnName in ('GL_ACCOUNT','StartDate','EndDate','Currency_Original','ASSET_CLASS_TYPE') "
        sql += "and (case  when ASSET_CLASS_TYPE in (3,4,5) "
        sql += "then ( case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0)   as decimal(24,0) ) "
        sql += "end) else   ( case when ISNUMERIC (PrincipalOutstanding)= 1 "
        sql += "then  CAST( ISNULL(PrincipalOutstanding,0)   as decimal(24,0) ) "
        sql += "end) + case when ISNUMERIC (AccruedInterest)= 1 "
        sql += "then  CAST( ISNULL(AccruedInterest,0)   as decimal(24,0) ) else 0   end "
        sql += "end) > " + amountString + " "
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
            sql += "FROM  RDM_StagingDB..TMP_TF_NM "
            sql += "WHERE TMP_TF_NMID = '" + rs("DataRecordID").ToString() + "' "


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
            sql += "and Source = 'TMP_TF_NM' "

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






    Public Sub Save_edit(ByVal TMP_TF_NMID As String, ByVal columnName As String, ByVal columnValues As String)

        Try

            Dim sql As String = String.Empty

            sql = "UPDATE RDM_StagingDB..TMP_TF_NM "
            sql += "SET " + columnName + " = " + "'" + columnValues + "' "
            sql += "where TMP_TF_NMID  = " + TMP_TF_NMID

            dbCon.ExecuteNonQuery(sql)

            Insert_HistoryTF_NM(TMP_TF_NMID, columnName)

        Catch ex As Exception

        End Try

    End Sub


    Private Sub Insert_HistoryTF_NM(TMP_TF_NMID As Object, columnName As String)

        Try


            Dim sql As String = String.Empty

            sql = "INSERT INTO RDM_StagingDB..AdjustLog(Source, RecordID, UpdateBy, UpdateDate,ColumnName) "
            sql += "VALUES( " + " 'TMP_TF_NM'," + TMP_TF_NMID + ", 1  ,  getdate() ,'" + columnName + "') "

            dbCon.ExecuteNonQuery(sql)


        Catch ex As Exception

        End Try


    End Sub


End Class
