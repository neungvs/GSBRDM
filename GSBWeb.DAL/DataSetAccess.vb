Imports System.Data.SqlClient
Imports System.Configuration

Public Class DataSetAccess
    Dim dbCon As DBclass
    'Private Con As SqlConnection

    Public Sub New()
        'Me.Con = New SqlConnection
        'Me.Con.ConnectionString = ConfigurationManager.ConnectionStrings.Item("ConnectionString_Report").ConnectionString
        dbCon = New DBclass("ConnectionString_Report")
    End Sub

    Public Function GetYearReport() As List(Of String)

        Dim sql As String
        Dim dt As DataTable
        Dim _data As New List(Of String)

        sql = String.Format("SELECT DISTINCT LEFT(TIMEID,4) YTIMEID  FROM Ref_MTK_Report ORDER BY LEFT(TIMEID,4) DESC")

        dt = dbCon.ExecuteReader(sql)

        For Each rs As DataRow In dt.Rows
            _data.Add(rs("YTIMEID").ToString())
        Next

        If _data.Count = 0 Then
            _data.Add(Year(Now))
        End If

        Return _data

    End Function

    Public Function GetDataSetMarketReport(ByVal _period As String) As List(Of DataSetMarketEntity)

        Dim sql As String
        Dim dt As DataTable
        Dim _data As New List(Of DataSetMarketEntity)
        Dim ent As DataSetMarketEntity

        sql = String.Format("SELECT ID,TIMEID,ReportName FROM Ref_MTK_Report WHERE TIMEID LIKE '{0}%' ORDER BY ID", _period)

        dt = dbCon.ExecuteReader(sql)

        For Each rs As DataRow In dt.Rows
            ent = New DataSetMarketEntity
            ent.ID = rs("ID").ToString()
            ent.TimeID = rs("TIMEID").ToString()
            ent.ReportName = rs("ReportName").ToString()
            _data.Add(ent)
        Next

        Return _data

    End Function

    Public Sub Dispost()
        If Not dbCon Is Nothing Then
            dbCon = Nothing
        End If
    End Sub

End Class

