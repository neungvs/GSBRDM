Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports System.Globalization


Public Class RDM_Periods

    Inherits System.Web.UI.Page
    Dim Perioddata As New Period_data
    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        GetData()

    End Sub
    Private Sub GetData()

        'Dim dt As DataTable = Perioddata.GetData()

        'If dt.Rows.Count > 0 Then
        'txtPeriodbox.Text = CStr(dt.Rows(0)("Period"))

        'End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            'Dim Checked As String = 0
            'Dim Periodbox As String

            'Periodbox = txtApproveDate1.Text
            'Dim _datelist() As String
            '_datelist = Periodbox.Split("/")
            ''29/01/2561
            'Dim _period As String = 0
            'Dim _period_update As String = 0
            'Dim _period_Check As String = 0
            'If _datelist.Count = 3 Then


            '    If Int32.Parse(_datelist(2)) > 2500 Then
            '        _period = _datelist(2) - 543
            '    End If
            '    _period_Check = _datelist(0) + "/" + _datelist(1)
            '    _period_update = _datelist(0) + "/" + _datelist(1) + "/" + _period.ToString() 'dd/mm/yyy
            'End If

            'Dim datePeriod As DateTime = Convert.ToDateTime(_period_update)
            'Dim format As String = "yyyyMMdd"
            'Dim str As String = datePeriod.ToString(format)
            'Dim dt As DataTable = Perioddata.GetPeriod()
            'Dim row As DataRow

            'For Each row In dt.Rows
            '    Dim strDetail As String
            '    strDetail = row("Month_End")
            '    Dim _datelist1() As String
            '    _datelist1 = strDetail.Split("/")
            '    If _datelist1.Count = 3 Then


            '        strDetail = _datelist1(0) + "/" + _datelist1(1)  'dd/mm
            '    End If


            '    If (strDetail = _period_Check) Then

            '        Checked = 1
            '    End If

            'Next row

            'If (Checked = 0) Then

            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert",
            '   "alert('งวดการรัน ต้องเป็นวันสิ้นเดือนเท่านั้น!');window.location ='RDM_Periods.aspx';", True)

            '    Return

            'Else

            ' Perioddata.UpdatePeriod(str)

            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert",
            '"alert('บันทึกข้อมูลสำเร็จ');window.location ='RDM_Periods.aspx';", True)

            'End If

        Catch ex As Exception

            UtilsBiz.CreateMessageAlert(Page, ex.Message, gui)

        End Try


    End Sub

End Class
