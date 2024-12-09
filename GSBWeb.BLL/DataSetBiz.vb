Imports GSBWeb.DAL
Imports System.Data.SqlClient
Imports System.Configuration
'Imports GSBWeb.BLL.UtilsBiz

Public Class DataSetBiz
    Dim _dataset As New DataSetAccess

    Public Function GetYearReport() As List(Of String)
        Dim _data As List(Of String)
        _data = _dataset.GetYearReport()

        Return _data
    End Function

    Public Function GetDataSetMarketReport(ByVal _period As String) As List(Of DataSetMarketEntity)
        Dim _data As List(Of DataSetMarketEntity)
        _data = _dataset.GetDataSetMarketReport(_period)

        Return _data
    End Function

    Public Sub Dispost()
        _dataset.Dispost()
    End Sub
End Class

