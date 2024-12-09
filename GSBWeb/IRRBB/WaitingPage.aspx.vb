Imports GSBWeb.DAL

Public Class WaitingPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Timer1.Enabled = True

    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim data As New IRRBBAccess
        Dim tblStatus As New DataTable
        Dim statusid As String
        Dim statusdesc As String

        tblStatus = data.SelectIRRBBStatus(Session("TIMEID"))

        If tblStatus.Rows.Count > 0 Then

            lblProcess.Text = tblStatus.Rows(0)("PROCESS_STATUS").ToString()
            statusid = tblStatus.Rows(0)("PROCESS_STATUS_ID").ToString()
            statusdesc = tblStatus.Rows(0)("PROCESS_STATUS").ToString()

            If statusid = "1" Then

                data.Update_Status(Session("UserID").ToString, Session("TIMEID"))

                Timer1.Enabled = False
                Response.Redirect("~/IRRBB/IRRBB.aspx")

            ElseIf statusid = "2" Then

                Session("Status") = "PROCESSERROR"
                Session("StatusDesc") = statusdesc

                data.Update_Status(Session("UserID").ToString, Session("TIMEID"))

                Timer1.Enabled = False
                Response.Redirect("~/IRRBB/IRRBB.aspx")

            End If

        End If


    End Sub
End Class