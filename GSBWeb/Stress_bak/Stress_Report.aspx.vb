Public Class Stress_Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            SetControl()

        End If

    End Sub


    Private Sub SetControl()

        'Dim ligl As HtmlGenericControl = Page.Master.FindControl("ligl")
        'ligl.Attributes.Add("class", "active")

    End Sub



End Class