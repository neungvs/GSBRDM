Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports GSBWeb.DAL

Public Class UC_Menu
    Inherits System.Web.UI.UserControl

    Dim _userinfo As UserEntity

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim _html As New StringBuilder

        'Dim li As New LiteralControl
        '_html.Append("<ul class=""nav navbar-nav"">")
        '_html.Append("  <li style=""text-decoration: inherit""><a href=""#"" style=""text-decoration: inherit"">RDM REPORT</a></li>")
        '_html.Append("  <li style=""text-decoration: inherit""><a href=""#"" style=""text-decoration: inherit"">RDM REPORT</a></li>")
        '_html.Append("  <li style=""text-decoration: inherit""><a href=""#"" style=""text-decoration: inherit"">RDM REPORT</a></li>")
        '_html.Append("  <li class=""dropdown"">")
        '_html.Append("      <a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"" role=""button"" aria-expanded=""false"" style=""text-decoration: inherit"">Dropdown <span class=""caret""></span></a>")
        '_html.Append("      <ul class=""dropdown-menu"" role=""menu"">")
        '_html.Append("          <li class=""dropdown""><a href=""#"" style=""text-decoration: inherit"">Action</a></li>")
        '_html.Append("          <li class=""dropdown""><a href=""#"" style=""text-decoration: inherit"">Something else here</a></li>")
        '_html.Append("          <li class=""divider""></li>")
        '_html.Append("          <li class=""dropdown""><a href=""#"" style=""text-decoration: inherit"">Separated link</a></li>")
        '_html.Append("      </ul>")
        '_html.Append("  </li>")
        '_html.Append("</ul>")
        ''_html.Append("")

        'li.Text = _html.ToString()

        'pnlMenu.Controls.Add(li)

        If Not Session("UserInfo") Is Nothing Then
            _userinfo = Session("UserInfo")
            CreateMenu(_userinfo.UserID)
        End If


    End Sub

    Private Function UList(ByVal _id As String, ByVal _cssClass As String, ByVal _style As String) As HtmlGenericControl
        Dim ul As HtmlGenericControl = New HtmlGenericControl("ul")
        ul.ID = _id
        ul.Attributes.Add("class", _cssClass)

        If _style <> "" Then
            ul.Attributes.Add("style", _style)
        End If

        Return ul
    End Function

    Private Function LIList(ByVal innerHtml As String, ByVal _cssClass As String, ByVal url As String) As HtmlGenericControl
        Dim li As HtmlGenericControl = New HtmlGenericControl("li")
        li.Attributes.Add("class", _cssClass)
        li.InnerHtml = "<a href=" & String.Format("{0}", url) & ">" & innerHtml & "</a>"
        Return li
    End Function


    Private Sub CreateMenu(_userid As String)
        Dim _moduleacc As New ModuleAccess
        Dim _lsmenu As List(Of ModuleEntity)
        _lsmenu = _moduleacc.GetUserModules(_userid, "", 0, 1)

        Dim _html As New StringBuilder
        Dim li As New LiteralControl
        Dim _moduleid As String = ""
        Dim _submenucount As Integer = 0
        Dim _parentid As String = ""

        If _lsmenu.Count > 0 Then
            '_html.Append("<ul class=""nav navbar-nav"" style=""margin-top:-30px;"">") '
            _html.Append("<ul class=""nav navbar-nav"" style=""margin-top:0;"">")

            For Each _menu As ModuleEntity In _lsmenu
                _moduleid = _menu.ModuleID
                If _menu.LevelID = 0 Then
                    Dim _sumsubmenu = From ls In _lsmenu
                                      Where ls.ParentID = _moduleid

                    If _parentid <> "" And _submenucount > 0 And _parentid <> _moduleid Then
                        _html.Append("</ul></li>")
                    End If

                    Dim site = Page.GetType()

                    _parentid = _moduleid
                    
                    If _sumsubmenu.Count > 0 Then
                        _submenucount = _sumsubmenu.Count
                        Dim counting As Integer = 0
                        Dim _buffer = ""
                        For i As Integer = 0 To _lsmenu.Count - 1
                            If site.BaseType.Name <> "Defult" Then
                                If _lsmenu(i).LinkPage <> Nothing And _menu.ModuleID = _lsmenu(i).ParentID Then
                                    _buffer = site.Name

                                    Dim first = _buffer.Substring(0, _buffer.IndexOf("_")).Replace("_", "")
                                    _buffer = _buffer.Substring(_buffer.IndexOf("_") + 1).Replace("_", ".")
                                   
                                    If _lsmenu(i).LinkPage.ToLower().IndexOf(_buffer) > -1 Then
                                        _html.Append("<li class=""dropdown active"" >")
                                        counting = 0
                                        Exit For
                                    Else
                                        If _lsmenu(i).LinkPage.IndexOf(site.BaseType.Name) > -1 Then
                                            _html.Append("<li class=""dropdown active"" >")
                                            counting = 0
                                            Exit For
                                        Else
                                            counting = 1
                                        End If
                                    End If

                                End If
                            Else
                                counting = 1
                            End If
                        Next
                        If counting = 1 Then
                            _html.Append("<li class=""dropdown"" >")
                        End If

                        _html.Append("<a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"" role=""button"" aria-expanded=""false"" style=""text-decoration: inherit"">" & _menu.ModuleNameEN & "<span class=""caret""></span></a>")
                        _html.Append("<ul class=""dropdown-menu"" role=""menu"">")
                    Else
                        _submenucount = 0
                        'If _menu.LinkPage <> Nothing And _menu.LinkPage <> "NULL" Then
                        '    If _menu.LinkPage.IndexOf(site.BaseType.Name) > -1 Then
                        '        _html.Append("<li style=""text-decoration: inherit;"" class=""active""><a href=" & Page.ResolveClientUrl("~/" & _menu.LinkPage) & " style=""text-decoration: inherit"">" & _menu.ModuleNameEN & "</a></li>")
                        '    Else
                        '        _html.Append("<li style=""text-decoration: inherit;""><a href=" & Page.ResolveClientUrl("~/" & _menu.LinkPage) & " style=""text-decoration: inherit"">" & _menu.ModuleNameEN & "</a></li>")
                        '    End If
                        'End If
                        _html.Append("<li style=""text-decoration: inherit;""><a href=" & Page.ResolveClientUrl("~/" & _menu.LinkPage) & " style=""text-decoration: inherit"">" & _menu.ModuleNameEN & "</a></li>")
                    End If
                Else
                    If _menu.ParentID = _parentid Then
                        _html.Append("<li class=""dropdown""><a href=" & Page.ResolveClientUrl("~/" & _menu.LinkPage) & " style=""text-decoration: inherit"">" & _menu.ModuleNameEN & "</a></li>")
                    End If

                    '_str = String.Format()
                End If

            Next

            If _parentid <> "" And _parentid <> _moduleid Then
                _html.Append("</ul></li>")
            End If

            _html.Append("</ul>")
        End If

        _moduleacc.Dispose()

        '_html.Append("  <li style=""text-decoration: inherit""><a href=""#"" style=""text-decoration: inherit"">RDM REPORT</a></li>")
        '_html.Append("  <li style=""text-decoration: inherit""><a href=""#"" style=""text-decoration: inherit"">RDM REPORT</a></li>")
        '_html.Append("  <li style=""text-decoration: inherit""><a href=""#"" style=""text-decoration: inherit"">RDM REPORT</a></li>")
        '_html.Append("  <li class=""dropdown"">")
        '_html.Append("      <a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"" role=""button"" aria-expanded=""false"" style=""text-decoration: inherit"">Dropdown <span class=""caret""></span></a>")
        '_html.Append("      <ul class=""dropdown-menu"" role=""menu"">")
        '_html.Append("          <li class=""dropdown""><a href=""#"" style=""text-decoration: inherit"">Action</a></li>")
        '_html.Append("          <li class=""dropdown""><a href=""#"" style=""text-decoration: inherit"">Something else here</a></li>")
        '_html.Append("          <li class=""divider""></li>")
        '_html.Append("          <li class=""dropdown""><a href=""#"" style=""text-decoration: inherit"">Separated link</a></li>")
        '_html.Append("      </ul>")
        '_html.Append("  </li>")
        '_html.Append("</ul>")
        '_html.Append("")

        '_html.Append("<ul class=""nav navbar-nav navbar-right"" style=""margin-top:-30px;"">") ' 
        _html.Append("<ul class=""nav navbar-nav navbar-right"" style=""margin-top:0;"">") ' 
        _html.Append("<li class=""dropdown"">")
        _html.Append("<a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"" role=""button"" aria-expanded=""false"" style=""text-decoration: inherit"">&nbsp;User&nbsp;<span class=""glyphicon glyphicon-user small""></span></a>")
        _html.Append("<ul class=""dropdown-menu"">")
        _html.Append("<li><a href=""#"" style=""text-decoration: inherit"">&nbsp;&nbsp;" & _userinfo.FirstNameEN & "&nbsp;<span class=""glyphicon glyphicon-user small pull-left ""></span></a></li>")
        _html.Append("<li class=""divider navbar-login-session-bg"" style=""text-decoration: inherit""></li>")
        _html.Append("<li><a href=" & Page.ResolveClientUrl("~/Login.aspx?info=0") & " style=""text-decoration: inherit"">Sign Out <span class=""glyphicon glyphicon-log-out pull-right""></span></a></li>")
        _html.Append("</ul></li></ul>")

        li.Text = _html.ToString()
        'li.Text = li.Text.Trim()
        pnlMenu.Controls.Add(li)

    End Sub
End Class