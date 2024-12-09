Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports Arsoft.Utility

Public Class DataSet_Market_Report
    Inherits System.Web.UI.Page

    Private _biz As New DataSetBiz
    Private _ftp As New FTPUtils

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadYear()
        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim _period As String
        _period = ddlYear.SelectedValue & ddlMonth.SelectedValue
        Download(_period)
        GetReport(_period)
    End Sub

    Private Sub LoadYear()
        ddlYear.DataSource = _biz.GetYearReport
        ddlYear.DataBind()
    End Sub

    Private Sub GetReport(_period As String)
        gvReport.DataSource = _biz.GetDataSetMarketReport(_period)
        gvReport.DataBind()
    End Sub

    Private Sub Download(_period As String)
        Try
            Dim _ftpinfo As New FTPInfo
            With _ftpinfo
                .ExecutablePath = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_WinSCP")) '"D:\SVN\RDM_Web_V1\WinSCP-5.11.2\WinSCP.exe"
                .HostName = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_HostName")) '"127.0.0.1"
                .Protocol = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_Protocol")) '"21"
                .UserName = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_Username")) '"FTPAdmin"
                .Password = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_Password")) '"password"
                .SSHKey = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_SSHKey"))
            End With

            Dim _ftppath As String
            Dim _localpath As String
            _ftppath = CStr(System.Configuration.ConfigurationManager.AppSettings("FTP_DataSetMarket"))
            _localpath = CStr(System.Configuration.ConfigurationManager.AppSettings("Path_DataSetMarket"))

            Dim _ftp As New FTPUtils
            _ftp.DatasetMarketDownload(_ftpinfo, _period, _ftppath, _localpath)

        Catch ex As Exception
            UtilLogfile.writeToLog("DataSet_Market_Report", "Download", ex.Message)
        End Try

    End Sub
End Class