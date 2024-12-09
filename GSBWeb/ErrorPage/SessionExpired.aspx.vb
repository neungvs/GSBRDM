Public Class zSessionExpired
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Application("user") = "Start"
    End Sub

End Class