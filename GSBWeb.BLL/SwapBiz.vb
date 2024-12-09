Imports GSBWeb.DAL
Imports GSBWeb.BLL.UtilsBiz



Public Class SwapBiz

    Dim dbCon As New DBclass

    Public Sub New()


    End Sub



    Public Function Count_SWAP() As Int64

        Dim sql As String
        Dim dt As DataTable
        Dim cntRow As Int64


        Dim amountString As String = CStr(GetAmountTotal())

        sql = "select count(*) ROW_COUNT "
        sql += "FROM (Select  ROW_NUMBER() "
        sql += "over (partition by rej.DataRecordID order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_Swap Swap "
        sql += "left join RDM_StagingDB..TMP_CU cu "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif(Swap.CounterParty_CIF,'') "
        sql += "and cu.CIF_NUMBER is not null  "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej  "
        sql += "on rej.DataSourceTbName = 'TMP_Swap' "
        sql += "and rej.DataRecordID = Swap.TMP_SwapID "
        sql += "where   RejectFlag not in (2,3,4) "
        sql += "and rej.ColumnName in ('ASSET_GL_CODE','AS_OF_DATE','STARTDATE','ENDDATE') "
        sql += "and ISNULL(CAST(CASE WHEN ISNUMERIC(RecPrincipalAmount) = 1 "
        sql += "THEN RecPrincipalAmount ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += " > " + amountString + " "
        sql += ") t1 where t1.id = 1 "



        dt = dbCon.ExecuteReader(sql)

        cntRow = Convert.ToInt64(dt(0)("ROW_COUNT"))

        Return cntRow

    End Function



    Public Function GetDataSWAP() As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())


        sql = "select Swap.TMP_SwapID,Swap.CIF_NUMBER "
        sql += ",REPLACE(Swap.PositionID,' ','') PositionID  "
        sql += ",Swap.CustName ,Swap.StartDate ,Swap.EndDate "
        sql += ",FORMAT(ISNULL(CAST(CASE WHEN ISNUMERIC(RecPrincipalAmount) = 1 THEN RecPrincipalAmount ELSE null END AS decimal(38, 2)),0) * 0.000001,'##.00') as RecPrincipalAmount "
        sql += "FROM (Select  TMP_SwapID ,Swap.CounterParty_CIF as CIF_NUMBER "
        sql += ",ISNULL(CU.CUST_NAME,'') +' ' + ISNULL(CU.CUST_SURNAME,'') as CustName "
        sql += ",Swap.PositionID  as PositionID  ,Swap.StartDate ,Swap.EndDate "
        sql += ",Swap.RecPrincipalAmount  ,ROW_NUMBER() "
        sql += "over (partition by rej.DataRecordID  order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_Swap Swap  "
        sql += "left join RDM_StagingDB..TMP_CU cu  "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( Swap.CounterParty_CIF,'') "
        sql += "and cu.CIF_NUMBER is not null  "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej  "
        sql += "on rej.DataSourceTbName like '%TMP_Swap' "
        sql += "and rej.DataRecordID = Swap.TMP_SwapID "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "and rej.ColumnName in ('ASSET_GL_CODE','AS_OF_DATE','STARTDATE','ENDDATE') "
        sql += "and ISNULL(CAST(CASE WHEN ISNUMERIC(RecPrincipalAmount) = 1 "
        sql += "THEN RecPrincipalAmount ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += " > " + amountString + " "
        sql += ") Swap   where Swap.id = 1  order by  Swap.CIF_NUMBER "


        dt = dbCon.ExecuteReader(sql)

        Return dt

    End Function





    Function GetFW53ByCriteria(ByVal cifNum As String, ByVal accNo As String, ByVal cifName As String, ByVal cifSur As String) As DataTable

        Dim sql As String
        Dim dt As DataTable

        Dim amountString As String = CStr(GetAmountTotal())



        sql = "select Swap.TMP_SwapID,Swap.CIF_NUMBER "
        sql += ",REPLACE(Swap.PositionID,' ','') PositionID  "
        sql += ",Swap.CustName ,Swap.StartDate ,Swap.EndDate "
        sql += ",FORMAT(ISNULL(CAST(CASE WHEN ISNUMERIC(RecPrincipalAmount) = 1 THEN RecPrincipalAmount ELSE null END AS decimal(38, 2)),0) * 0.000001,'##.00') as RecPrincipalAmount "
        sql += ",Swap.CUST_NAME,Swap.CUST_SURNAME "
        sql += "FROM (Select  TMP_SwapID ,Swap.CounterParty_CIF as CIF_NUMBER "
        sql += ",ISNULL(CU.CUST_NAME,'') +' ' + ISNULL(CU.CUST_SURNAME,'') as CustName "
        sql += ",Swap.PositionID  as PositionID  ,Swap.StartDate ,Swap.EndDate "
        sql += ",Swap.RecPrincipalAmount   "
        sql += ",ISNULL(CUST_NAME,'') CUST_NAME,ISNULL(CUST_SURNAME,'') CUST_SURNAME "
        sql += ",ROW_NUMBER() over (partition by rej.DataRecordID  order by rej.DataRecordID) id "
        sql += "From RDM_StagingDB..TMP_Swap Swap  "
        sql += "left join RDM_StagingDB..TMP_CU cu  "
        sql += "On nullif(cu.CIF_NUMBER,'') = nullif( Swap.CounterParty_CIF,'') "
        sql += "and cu.CIF_NUMBER is not null  "
        sql += "left join RDM_VLDDB..REJ_RejectLog rej  "
        sql += "on rej.DataSourceTbName like '%TMP_Swap' "
        sql += "and rej.DataRecordID = Swap.TMP_SwapID "
        sql += "where RejectFlag not in (2,3,4) "
        sql += "and rej.ColumnName in ('ASSET_GL_CODE','AS_OF_DATE','STARTDATE','ENDDATE') "
        sql += "and ISNULL(CAST(CASE WHEN ISNUMERIC(RecPrincipalAmount) = 1 "
        sql += "THEN RecPrincipalAmount ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += " > " + amountString + " "
        sql += ") Swap   where Swap.id = 1 "




        If Not String.IsNullOrEmpty(cifNum) Then
            sql += "and (CIF_NUMBER like '" + cifNum + "%' "
            sql += "or CIF_NUMBER = '" + cifNum + "') "
        End If

        If Not String.IsNullOrEmpty(accNo) Then
            sql += "and (Swap.PositionID like '" + accNo + "%' "
            sql += "or Swap.PositionID = '" + accNo + "') "
        End If

        If Not String.IsNullOrEmpty(cifName) Then
            sql += "and (Replace(Replace(Replace(Isnull( cust_name, '' ),'/',''),'[',''),']','') like '%" + cifName.Replace("/", "").Replace("[", "").Replace("]", "") + "%' "
            sql += "or Replace(Replace(Replace(Isnull( cust_name, '' ),'/',''),'[',''),']','') = '" + cifName.Replace("/", "").Replace("[", "").Replace("]", "") + "') "
        End If


        If Not String.IsNullOrEmpty(cifSur) Then
            sql += "and (ISNULL(CUST_SURNAME,'') like '" + cifSur + "%' "
            sql += "or ISNULL(CUST_SURNAME,'') = '" + cifSur + "') "
        End If


        sql += "order by  Swap.CIF_NUMBER "

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
        sql += "WHERE [DataSourceTbName] = 'TMP_Swap' "
        sql += "and RejectFlag = 1 "
        sql += "and DataRecordID = '" + rejId + "' "
        sql += "and ColumnName in ('ASSET_GL_CODE','AS_OF_DATE','STARTDATE','ENDDATE') "


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
            sql += "FROM  RDM_StagingDB..TMP_Swap "
            sql += "WHERE TMP_SwapID = '" + rejId + "' "


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
            sql += "and Source = 'TMP_Swap' "

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
        sql += ",(select CounterParty_CIF from [RDM_StagingDB]..TMP_Swap where TMP_SwapID = rej.DataRecordID) CifNumber "
        sql += ",(select PositionID from [RDM_StagingDB]..TMP_Swap where TMP_SwapID = rej.DataRecordID) AccNo "
        sql += "FROM [RDM_VLDDB]..[REJ_RejectLog] rej  "
        sql += "inner join RDM_StagingDB..TMP_Swap Swap "
        sql += "on rej.DataSourceTbName like '%TMP_Swap' "
        sql += "and rej.DataRecordID = Swap.TMP_SwapID "
        sql += "WHERE RejectFlag = '1' "
        sql += "and rej.ColumnName in ('ASSET_GL_CODE','AS_OF_DATE','STARTDATE','ENDDATE') "
        sql += "and ISNULL(CAST(CASE WHEN ISNUMERIC(RecPrincipalAmount) = 1 "
        sql += "THEN RecPrincipalAmount ELSE null END AS decimal(38, 2)),0) * 0.000001 "
        sql += " > " + amountString + " "
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
            sql += "FROM  RDM_StagingDB..TMP_Swap "
            sql += "WHERE TMP_SwapID = '" + rs("DataRecordID").ToString() + "' "


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
            sql += "and Source = 'TMP_Swap' "

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







    Public Sub Save_edit(ByVal TMP_SwapID As String, ByVal columnName As String, ByVal columnValues As String)

        Try

            Dim sql As String = String.Empty

            sql = "UPDATE RDM_StagingDB..TMP_Swap "
            sql += "SET " + columnName + " = " + "'" + columnValues + "' "
            sql += "where TMP_SwapID  = " + TMP_SwapID


            dbCon.ExecuteNonQuery(sql)


            Insert_HistorySwapID(TMP_SwapID, columnName)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Insert_HistorySwapID(TMP_SwapID As String, columnName As String)

        Try

            Dim sql As String = String.Empty


            sql = "INSERT INTO RDM_StagingDB..AdjustLog(Source, RecordID, UpdateBy, UpdateDate,ColumnName) "
            sql += "VALUES( " + " 'TMP_Swap'," + TMP_SwapID + ", 1  ,  getdate() ,'" + columnName + "') "

            dbCon.ExecuteNonQuery(sql)

        Catch ex As Exception

        End Try

    End Sub




End Class
