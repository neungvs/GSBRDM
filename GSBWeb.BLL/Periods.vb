Imports GSBWeb.DAL
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Period_data

    Dim dbCon As New DBclass("ConnectionString_Report")

    Public Sub New()
        '
    End Sub

    Public Function GetData() As DataTable
        Dim sql As String
        Dim dt As DataTable

        sql = "SELECT  DATEADD(yyyy,543,convert(date,Period))AS Period FROM rdm_system WHERE SystemId = 9"
        dt = dbCon.ExecuteReader(sql)

        Return dt
    End Function

    Public Function GetPeriod() As DataTable
        Dim sql As String
        Dim dt As DataTable

        sql = "SELECT Month_End FROM History_DB..REF_GSB_Last_WorkingDay"
        dt = dbCon.ExecuteReader(sql)

        Return dt
    End Function

    Public Sub UpdatePeriod(ByVal Period As String)

        Dim sql As String = String.Empty

        sql = "UPDATE RDM_Report..rdm_system "

        If Period Is Nothing Then
            sql += "SET Period = null WHERE SystemId = 9"
        Else
            sql += "SET Period = " + Convert.ToString(Period) + " WHERE SystemId = 9"
        End If

        dbCon.ExecuteNonQuery(sql)
    End Sub

End Class
