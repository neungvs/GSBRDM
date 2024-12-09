Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Web
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices.Interfaces

Public Class clsboldb
    Private objConn As SqlConnection
    Private objCmd As SqlCommand
    Private Trans As SqlTransaction
    Private strConnString As String
    Public Sub New()
        strConnString = ConfigurationManager.ConnectionStrings("ConnectionString_Report").ConnectionString
    End Sub

    Public Function QueryDataReader(ByVal strSQL As String) As SqlDataReader
        Dim dtReader As SqlDataReader
        objConn = New SqlConnection
        With objConn
            .ConnectionString = strConnString
            .Open()
        End With
        objCmd = New SqlCommand(strSQL, objConn)
        dtReader = objCmd.ExecuteReader()
        Return dtReader '*** Return DataReader ***'
    End Function

    Public Function QueryDataSet(ByVal strSQL As String) As DataSet
        Dim ds As New DataSet
        Dim dtAdapter As New SqlDataAdapter
        objConn = New SqlConnection
        With objConn
            .ConnectionString = strConnString
            .Open()
        End With
        objCmd = New SqlCommand
        With objCmd
            .Connection = objConn
            .CommandText = strSQL
            .CommandType = CommandType.Text
            .CommandTimeout = 300
        End With
        dtAdapter.SelectCommand = objCmd
        dtAdapter.Fill(ds)
        Return ds '*** Return DataSet ***'
    End Function

    Public Function QueryDataTable(ByVal strSQL As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        objConn = New SqlConnection
        With objConn
            .ConnectionString = strConnString
            .Open()
        End With
        dtAdapter = New SqlDataAdapter(strSQL, objConn)
        dtAdapter.Fill(dt)
        Return dt '*** Return DataTable ***'
    End Function

    Public Function QueryExecuteNonQuery(ByVal strSQL As String) As Boolean
        objConn = New SqlConnection
        With objConn
            .ConnectionString = strConnString
            .Open()
        End With
        Try
            objCmd = New SqlCommand
            With objCmd
                .Connection = objConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            objCmd.ExecuteNonQuery()
            Return True '*** Return True ***'
        Catch ex As Exception
            Return False '*** Return False ***'
        End Try
    End Function

    Public Function QueryExecuteScalar(ByVal strSQL As String) As Object
        Dim obj As Object
        objConn = New SqlConnection
        With objConn
            .ConnectionString = strConnString
            .Open()
        End With
        Try
            objCmd = New SqlCommand
            With objCmd
                .Connection = objConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            obj = objCmd.ExecuteScalar() '*** Return Scalar ***'
            Return obj
        Catch ex As Exception
            Return Nothing '*** Return Nothing ***'
        End Try
    End Function

    Public Function TransStart()
        objConn = New SqlConnection
        With objConn
            .ConnectionString = strConnString
            .Open()
        End With
        Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted)
    End Function

    Public Function TransExecute(ByVal strSQL As String) As Boolean
        objCmd = New SqlCommand
        With objCmd
            .Connection = objConn
            .Transaction = Trans
            .CommandType = CommandType.Text
            .CommandText = strSQL
        End With
        objCmd.ExecuteNonQuery()
    End Function

    Public Function TransRollBack()
        Trans.Rollback()
    End Function

    Public Function TransCommit()
        Trans.Commit()
    End Function

    Public Function CallReport(ByVal rePoerserver As ReportViewer, ByVal ReportName As String)
        Dim strReportServer As String = ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Dim strUserName As String = ConfigurationManager.AppSettings("UserAuthen").ToString()
        Dim strPassword As String = ConfigurationManager.AppSettings("PassWordAuthen").ToString()
        rePoerserver.ProcessingMode = ProcessingMode.Remote
        Dim RC = New ReportViewerCredentials(strUserName, strPassword, "")
        rePoerserver.ServerReport.ReportServerCredentials = RC
        rePoerserver.ServerReport.ReportServerUrl = New Uri(strReportServer)
        rePoerserver.ServerReport.ReportPath = ConfigurationManager.AppSettings("RootSSRSPath").ToString() + ReportName + ""
        rePoerserver.ServerReport.Refresh()
    End Function

    Public Function CallReportPara(ByVal rePoerserver As ReportViewer, ByVal ReportName As String, ByVal Parameter1 As ReportParameter, ByVal Parameter2 As ReportParameter, ByVal Parameter3 As ReportParameter, ByVal Parameter4 As ReportParameter)

        Dim strReportServer As String = ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Dim strUserName As String = ConfigurationManager.AppSettings("UserAuthen").ToString()
        Dim strPassword As String = ConfigurationManager.AppSettings("PassWordAuthen").ToString()
        rePoerserver.ProcessingMode = ProcessingMode.Remote
        Dim RC = New ReportViewerCredentials(strUserName, strPassword, "")


        rePoerserver.ServerReport.ReportServerCredentials = RC
        rePoerserver.ServerReport.ReportServerUrl = New Uri(strReportServer)
        rePoerserver.ServerReport.ReportPath = ConfigurationManager.AppSettings("RootSSRSPath").ToString() + ReportName + ""
        Dim Parameters() As ReportParameter = {Parameter1, Parameter2, Parameter3, Parameter4}
        rePoerserver.ServerReport.SetParameters(Parameters)
        rePoerserver.ServerReport.Refresh()

    End Function

    Public Function CallReportPara(ByVal rePoerserver As ReportViewer, ByVal ReportName As String, ByVal Parameter1 As ReportParameter, ByVal Parameter2 As ReportParameter, ByVal Parameter3 As ReportParameter, ByVal Parameter4 As ReportParameter, ByVal Parameter5 As ReportParameter, ByVal Parameter6 As ReportParameter, ByVal Parameter7 As ReportParameter, ByVal Parameter8 As ReportParameter)
        Dim strReportServer As String = ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Dim strUserName As String = ConfigurationManager.AppSettings("UserAuthen").ToString()
        Dim strPassword As String = ConfigurationManager.AppSettings("PassWordAuthen").ToString()
        rePoerserver.ProcessingMode = ProcessingMode.Remote
        Dim RC = New ReportViewerCredentials(strUserName, strPassword, "")


        rePoerserver.ServerReport.ReportServerCredentials = RC
        rePoerserver.ServerReport.ReportServerUrl = New Uri(strReportServer)
        rePoerserver.ServerReport.ReportPath = ConfigurationManager.AppSettings("RootSSRSPath").ToString() + ReportName + ""
        Dim Parameters() As ReportParameter = {Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, Parameter7, Parameter8}
        rePoerserver.ServerReport.SetParameters(Parameters)
        rePoerserver.ServerReport.Refresh()
    End Function

    Public Function CallReportPara(ByVal rePoerserver As ReportViewer, ByVal ReportName As String, ByVal Parameter1 As ReportParameter, ByVal Parameter2 As ReportParameter, ByVal Parameter3 As ReportParameter, ByVal Parameter4 As ReportParameter, ByVal Parameter5 As ReportParameter)
        Dim strReportServer As String = ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Dim strUserName As String = ConfigurationManager.AppSettings("UserAuthen").ToString()
        Dim strPassword As String = ConfigurationManager.AppSettings("PassWordAuthen").ToString()
        rePoerserver.ProcessingMode = ProcessingMode.Remote
        Dim RC = New ReportViewerCredentials(strUserName, strPassword, "")


        rePoerserver.ServerReport.ReportServerCredentials = RC
        rePoerserver.ServerReport.ReportServerUrl = New Uri(strReportServer)
        rePoerserver.ServerReport.ReportPath = ConfigurationManager.AppSettings("RootSSRSPath").ToString() + ReportName + ""
        Dim Parameters() As ReportParameter = {Parameter1, Parameter2, Parameter3, Parameter4, Parameter5}
        rePoerserver.ServerReport.SetParameters(Parameters)
        rePoerserver.ServerReport.Refresh()
    End Function

    Public Function CallReportPara(ByVal rePoerserver As ReportViewer, ByVal ReportName As String, ByVal Parameter1 As ReportParameter, ByVal Parameter2 As ReportParameter, ByVal Parameter3 As ReportParameter)
        Dim strReportServer As String = ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Dim strUserName As String = ConfigurationManager.AppSettings("UserAuthen").ToString()
        Dim strPassword As String = ConfigurationManager.AppSettings("PassWordAuthen").ToString()
        rePoerserver.ProcessingMode = ProcessingMode.Remote
        Dim RC = New ReportViewerCredentials(strUserName, strPassword, "")


        rePoerserver.ServerReport.ReportServerCredentials = RC
        rePoerserver.ServerReport.ReportServerUrl = New Uri(strReportServer)
        rePoerserver.ServerReport.ReportPath = ConfigurationManager.AppSettings("RootSSRSPath").ToString() + ReportName + ""
        Dim Parameters() As ReportParameter = {Parameter1, Parameter2, Parameter3}
        rePoerserver.ServerReport.SetParameters(Parameters)
        rePoerserver.ServerReport.Refresh()
    End Function

    Public Function CallReportPara(ByVal rePoerserver As ReportViewer, ByVal ReportName As String, ByVal Parameter1 As ReportParameter, ByVal Parameter2 As ReportParameter)
        Dim strReportServer As String = ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Dim strUserName As String = ConfigurationManager.AppSettings("UserAuthen").ToString()
        Dim strPassword As String = ConfigurationManager.AppSettings("PassWordAuthen").ToString()
        rePoerserver.ProcessingMode = ProcessingMode.Remote
        Dim RC = New ReportViewerCredentials(strUserName, strPassword, "")


        rePoerserver.ServerReport.ReportServerCredentials = RC
        rePoerserver.ServerReport.ReportServerUrl = New Uri(strReportServer)
        rePoerserver.ServerReport.ReportPath = ConfigurationManager.AppSettings("RootSSRSPath").ToString() + ReportName + ""
        Dim Parameters() As ReportParameter = {Parameter1, Parameter2}
        rePoerserver.ServerReport.SetParameters(Parameters)
        rePoerserver.ServerReport.Refresh()
    End Function



    Public Sub Close()
        objConn.Close()
        objConn = Nothing
    End Sub
End Class

