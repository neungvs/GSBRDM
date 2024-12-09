Imports GSBWeb.DAL
Imports System.Data.SqlClient
Imports System.Configuration




Public Class PermissionReportBiz

    Dim dbCon As New DBclass("ConnectionString_Report")



    Public Sub New()


    End Sub

    Public Function GetDataPermissionReport(ByVal userLogon As String) As List(Of PermissionReportEnntity)

        Dim sql As String
        Dim dt As DataTable
        Dim Permis As New List(Of PermissionReportEnntity)


        sql = " select Repo_Name,Repo_Decription,Repo_Directory from Webpermission join WebAccount on perm_AccountId = ACCO_Id "
        sql += " Left join WebReports on perm_ReportId = Repo_Id "
        sql += " where ACCO_Name ='" + userLogon + "' and Repo_active = 1 and ACCO_Active = 1"


        dt = dbCon.ExecuteReader(sql)

        For Each rs As DataRow In dt.Rows

            Dim ent As New PermissionReportEnntity

            ent.RepoName = rs("Repo_Name").ToString()
            ent.RepoDecription = rs("Repo_Decription").ToString()
            ent.Directory = "~/" + rs("Repo_Directory").ToString()


            If String.IsNullOrEmpty(ent.RepoName) Then
                ent.RepoName = "-"
                ent.Directory = "-"
                ent.RepoDecription = "-"
            End If

            'Get ColumnConrect
            sql = String.Empty

            Permis.Add(ent)

        Next


        Return Permis

    End Function

    Public Function GetDataCheckUser(ByVal userLogon As String) As List(Of PermissionReportEnntity)
        Dim sql As String
        Dim dt As DataTable
        Dim checkUser As New List(Of PermissionReportEnntity)


        sql = " SELECT *,IIF(TimeFail is NULL, 0, DATEDIFF(SECOND,timefail,GETDATE())) as chkTime FROM WebAccount WHERE ACCO_Name='{0}'"
        sql = String.Format(sql, userLogon)

        'sql += " where ACCO_Name ='" + userLogon + "' and Repo_active = 1 and ACCO_Active = 1"

        dt = dbCon.ExecuteReader(sql)
        For Each rs As DataRow In dt.Rows

            Dim ent As New PermissionReportEnntity

            ent.accountName = rs("ACCO_Name").ToString()
            ent.CountFail = CInt(rs("CountFail"))
            ent.chkTime = CInt(rs("chkTime"))
            'Get ColumnConrect
            sql = String.Empty
            checkUser.Add(ent)

        Next
        Return checkUser

    End Function

    Public Function UpdateUser(ByVal userLogon As String, Optional isUpdate As Boolean = True) As Boolean
        Dim _result As Boolean = False
        Dim sql As String
        Try
            If isUpdate Then
                sql = "UPDATE WebAccount set CountFail=0,TimeFail=NULL  WHERE ACCO_Name = '{0}'"
                sql = String.Format(sql, userLogon)
            Else
                sql = "UPDATE WebAccount SET CountFail=CountFail+1,TimeFail=IIF(TimeFail IS NULL,GETDATE(),TimeFail) WHERE ACCO_Name ='{0}'"
                sql = String.Format(sql, userLogon)
            End If
            dbCon.ExecuteNonQuery(sql)
            _result = True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return _result
    End Function

End Class
