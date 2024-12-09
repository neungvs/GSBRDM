Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class RDM_Report
    Inherits System.Web.UI.Page
    Dim gui As New Guid
    Dim permisBiz As New PermissionReportBiz

    Public Sub New()
        '
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'LoadData()
            'SetControl()
        End If
    End Sub

    Private Sub LoadData()
        Try
            Dim userLogon = Convert.ToString(Session("UserName"))
            GetRePortData(userLogon)
        Catch ex As Exception
            UtilsBiz.CreateMessageAlert(Page, ex.Message, gui)
        End Try
    End Sub

    'Get Count LN70  Reject Data
    Private Sub GetRePortData(userLogon As String)
        gvReport.DataSource = permisBiz.GetDataPermissionReport(userLogon)
        gvReport.DataBind()
    End Sub

    Private Sub SetControl()
        Dim ligl As HtmlGenericControl = Page.Master.FindControl("ligl")
        ligl.Attributes.Add("class", "active")
    End Sub

End Class
