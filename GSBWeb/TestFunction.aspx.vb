Public Class TestFunction
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Errorpoit_Click(sender As Object, e As EventArgs) Handles Errorpoit.Click
        Response.Write("")
        Page.ClientScript.RegisterStartupScript(Page.GetType, "alertscript", "alertify.error(" & "SampleError" & ");", True)
    End Sub
End Class