Public Class Report
    Inherits System.Web.UI.Page

    Public ReportServiceURL As String = System.Configuration.ConfigurationManager.AppSettings.Get("ReportServiceURL")
    Public Username As String = System.Configuration.ConfigurationManager.AppSettings.Get("Username")
    Public Password As String = System.Configuration.ConfigurationManager.AppSettings.Get("Password")
    Public DomainName As String = System.Configuration.ConfigurationManager.AppSettings.Get("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                ReportViewer1.ShowCredentialPrompts = False
                'ReportViewer1.KeepSessionAlive = False
                'ReportViewer1.AsyncRendering = False

                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
                ReportViewer1.ServerReport.ReportServerUrl = New System.Uri(ReportServiceURL)

                Dim nc = New ReportViewerCredentials(Username, Password, DomainName)
                ReportViewer1.ServerReport.ReportServerCredentials = nc

                ReportViewer1.ServerReport.ReportPath = Request.QueryString("ReportURL")
                ReportViewer1.ServerReport.Refresh()

            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End If
    End Sub

End Class