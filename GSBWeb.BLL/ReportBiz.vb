Imports GSBWeb.DAL

Public Class ReportBiz
    Dim _reportAcc As New ReportAccess

    Public Function GetReport() As DataTable
        Return _reportAcc.GetReport()
    End Function
End Class
