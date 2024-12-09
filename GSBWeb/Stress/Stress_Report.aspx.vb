Imports GSBWeb.DAL

Public Class Stress_Report
    Inherits System.Web.UI.Page

    Dim _moduleid = "11000"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            SetReport()

        End If

    End Sub


    Private Sub SetControl()

        'Dim ligl As HtmlGenericControl = Page.Master.FindControl("ligl")
        'ligl.Attributes.Add("class", "active")

    End Sub

    Private Sub SetReport()
        Dim _userid = Session("UserID")
        Dim _moduleacc As New ModuleAccess
        Dim _lsmenu As List(Of ModuleEntity)
        Dim _html As New StringBuilder
        Dim li As New LiteralControl

        _lsmenu = _moduleacc.GetUserModules(_userid, _moduleid, 1, 0)
        For Each _menu As ModuleEntity In _lsmenu
            If _menu.LV = 1 Then
                _html.Append(String.Format("<div class=""NormalSubItems"" style="""">{0}</div>", _menu.ModuleNameTH))
            ElseIf _menu.LV = 2 Then
                If Left(_menu.ModuleNameTH, 11) = "ตารางที่ 2." Then
                    _html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left;  padding-right: 10;""><i class=""fa fa-caret-right"" style = ""padding-left: 15px;""></i> {1}</a>", _menu.LinkPage, _menu.ModuleNameTH))
                Else
                    _html.Append(String.Format("<div class=""NormalSubItems"" style="""">&nbsp;&nbsp;{0}</div>", _menu.ModuleNameTH))
                End If
            ElseIf Convert.ToInt16(_menu.LevelID) = 2 Then
                _html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left;  padding-right: 10;""><i class=""fa fa-caret-right"" style = ""padding-left: 30px;""></i> {1}</a>", _menu.LinkPage, _menu.ModuleNameTH))
            ElseIf Convert.ToInt16(_menu.LevelID) > 2 Then
                _html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left;  padding-right: 10;""><i class=""fa fa-caret-right"" style = ""padding-left: 30px;""></i> {1}</a>", _menu.LinkPage, _menu.ModuleNameTH))
            End If
        Next

        li.Text = _html.ToString()
        pnlReport.Controls.Add(li)

    End Sub




End Class