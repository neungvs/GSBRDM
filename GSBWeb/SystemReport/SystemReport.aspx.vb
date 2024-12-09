Imports GSBWeb.DAL

Public Class SystemReport
    Inherits System.Web.UI.Page

    Dim _moduleid = "10700"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'SetControl()
            SetReport()
        End If
    End Sub

    Private Sub SetControl()
        Dim ligl As HtmlGenericControl = Page.Master.FindControl("ligl")
        ligl.Attributes.Add("class", "active")
    End Sub

    Private Sub SetReport()
        Dim _userid = Session("UserID")
        Dim _moduleacc As New ModuleAccess
        Dim _lsmenu As List(Of ModuleEntity)
        Dim _html As New StringBuilder
        Dim li As New LiteralControl

        _lsmenu = _moduleacc.GetUserModules(_userid, _moduleid, 1, 0)
        For Each _menu As ModuleEntity In _lsmenu
            If _menu.LV > 0 Then
                '_html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">{1}. {2}</a>", _menu.LinkPage, _menu.HeaderSeq, _menu.ModuleNameTH))
                _html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;""><i class=""fa fa-caret-right"" style = ""padding-left: 15px;""></i>  {1}</a>", _menu.LinkPage, _menu.ModuleNameTH))
            End If
        Next


        '_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">1. Capital Fund (DS_CAP)</a>")
        '_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">2. Operational Risk (DS_OPR)</a>")
        '_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">3. Credit Risk Standardised Approach (DS_CRS)</a>")
        '_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">4. Contingent Summary (DS_COS)</a>")

        li.Text = _html.ToString()
        pnlReport.Controls.Add(li)

    End Sub

End Class