
Imports GSBWeb.DAL

Public Class Expired
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'CheckSession.chkSession = "Strat"
        Application("User") = "Strat"
        'Session.Clear()



    End Sub

End Class