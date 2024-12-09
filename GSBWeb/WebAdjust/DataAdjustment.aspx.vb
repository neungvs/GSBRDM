Imports GSBWeb.BLL
Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports System.IO
Imports System.Web.UI.Page


Public Class DataAdjustment
    Inherits System.Web.UI.Page
    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
            'SetControl()
        End If
    End Sub


    ''' <summary>
    ''' GetData count CBS,TradeFinance,Investment
    ''' Returns count Data.
    ''' </summary>
    ''' 
    Private Sub LoadData()
        Try
            'GetData CBS
            GetDataLN70()

            'GetData TradeFinance
            GetDataTF_FIXED()
            GetDataTF_FLOAT()
            GetDataTF_NM()

            'GetData Investment
            GetDataIN51()
            GetDataIN52()
            GetDataWF52()
            GetDataFW53()
            GetDataCO52()
            GetDataSwap()
        Catch ex As Exception
            UtilsBiz.CreateMessageAlert(Page, ex.Message, gui)
        End Try

    End Sub

    'Get Count LN70  Reject Data
    Private Sub GetDataLN70()

        Dim lnBiz As New LN70Biz
        Dim cntLn70 As Int64 = 0

        cntLn70 = lnBiz.Count_ListLN70()

        If cntLn70 = 0 Then
            hpLN_70.Text = "0"
            hpLN_70.Enabled = False
            hpLN_70.Font.Underline = False
            hpLN_70.ForeColor = Drawing.Color.Black
        Else
            hpLN_70.Text = cntLn70.ToString
            hpLN_70.Enabled = True
            hpLN_70.Font.Underline = True
            hpLN_70.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count TF_FIXED Reject Data
    Private Sub GetDataTF_FIXED()

        Dim tfBiz As New TF_FIXEDBiz
        Dim cnttffixed As Int64 = 0

        cnttffixed = tfBiz.Count_TF_FIXED()

        If cnttffixed = 0 Then
            hpTF_FIXED.Text = "0"
            hpTF_FIXED.Enabled = False
            hpTF_FIXED.Font.Underline = False
            hpTF_FIXED.ForeColor = Drawing.Color.Black
        Else
            hpTF_FIXED.Text = cnttffixed.ToString
            hpTF_FIXED.Enabled = True
            hpTF_FIXED.Font.Underline = True
            hpTF_FIXED.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count TF_FLOAT Reject Data
    Private Sub GetDataTF_FLOAT()

        Dim tfBiz As New TF_FLOATBiz
        Dim cnttffloat As Int64 = 0

        cnttffloat = tfBiz.Count_TF_FLOAT()

        If cnttffloat = 0 Then
            hpTF_FLOAT.Text = "0"
            hpTF_FLOAT.Enabled = False
            hpTF_FLOAT.Font.Underline = False
            hpTF_FLOAT.ForeColor = Drawing.Color.Black
        Else
            hpTF_FLOAT.Text = cnttffloat.ToString
            hpTF_FLOAT.Enabled = True
            hpTF_FLOAT.Font.Underline = True
            hpTF_FLOAT.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count TF_NM Reject Data
    Private Sub GetDataTF_NM()

        Dim tfBiz As New TF_NMBiz
        Dim cnttfNM As Int64 = 0

        cnttfNM = tfBiz.Count_TF_NM

        If cnttfNM = 0 Then
            hpTF_NM.Text = "0"
            hpTF_NM.Enabled = False
            hpTF_NM.Font.Underline = False
            hpTF_NM.ForeColor = Drawing.Color.Black
        Else
            hpTF_NM.Text = cnttfNM.ToString
            hpTF_NM.Enabled = True
            hpTF_NM.Font.Underline = True
            hpTF_NM.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count IN51  Reject Data
    Private Sub GetDataIN51()

        Dim in51Biz As New IN51Biz
        Dim cntIN51 As Int64 = 0

        cntIN51 = in51Biz.Count_IN51()

        If cntIN51 = 0 Then
            hpIN51.Text = "0"
            hpIN51.Enabled = False
            hpIN51.Font.Underline = False
            hpIN51.ForeColor = Drawing.Color.Black
        Else
            hpIN51.Text = cntIN51.ToString
            hpIN51.Enabled = True
            hpIN51.Font.Underline = True
            hpIN51.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count IN52  Reject Data
    Private Sub GetDataIN52()

        Dim in52Biz As New IN52Biz
        Dim cntIN52 As Int64 = 0

        cntIN52 = in52Biz.Count_IN52()

        If cntIN52 = 0 Then
            hpIN52.Text = "0"
            hpIN52.Enabled = False
            hpIN52.Font.Underline = False
            hpIN52.ForeColor = Drawing.Color.Black
        Else
            hpIN52.Text = cntIN52.ToString
            hpIN52.Enabled = True
            hpIN52.Font.Underline = True
            hpIN52.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count CO52  Reject Data
    Private Sub GetDataCO52()

        Dim co52Biz As New CO52Biz
        Dim cntCO52 As Int64 = 0

        cntCO52 = co52Biz.Count_CO52()

        If cntCO52 = 0 Then
            hpCO52.Text = "0"
            hpCO52.Enabled = False
            hpCO52.Font.Underline = False
            hpCO52.ForeColor = Drawing.Color.Black
        Else
            hpCO52.Text = cntCO52.ToString
            hpCO52.Enabled = True
            hpCO52.Font.Underline = True
            hpCO52.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count WF52 Reject Data
    Private Sub GetDataWF52()

        Dim wf52Biz As New WF52Biz
        Dim cntWF52 As Int64 = 0

        cntWF52 = wf52Biz.Count_WF52()

        If cntWF52 = 0 Then
            hpWF52.Text = "0"
            hpWF52.Enabled = False
            hpWF52.Font.Underline = False
            hpWF52.ForeColor = Drawing.Color.Black
        Else
            hpWF52.Text = cntWF52.ToString
            hpWF52.Enabled = True
            hpWF52.Font.Underline = True
            hpWF52.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count FW53 Reject Data
    Private Sub GetDataFW53()

        Dim fw53Biz As New FW53Biz
        Dim cntFW53 As Int64 = 0

        cntFW53 = fw53Biz.Count_FW53()

        If cntFW53 = 0 Then
            hpFW53.Text = "0"
            hpFW53.Enabled = False
            hpFW53.Font.Underline = False
            hpFW53.ForeColor = Drawing.Color.Black
        Else
            hpFW53.Text = cntFW53.ToString
            hpFW53.Enabled = True
            hpFW53.Font.Underline = True
            hpFW53.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Get Count Swap Reject Data
    Private Sub GetDataSwap()

        Dim swapBiz As New SwapBiz
        Dim cntSWAP As Int64 = 0

        cntSWAP = swapBiz.Count_SWAP()

        If cntSWAP = 0 Then
            hpSwap.Text = "0"
            hpSwap.Enabled = False
            hpSwap.Font.Underline = False
            hpSwap.ForeColor = Drawing.Color.Black
        Else
            hpSwap.Text = cntSWAP.ToString
            hpSwap.Enabled = True
            hpSwap.Font.Underline = True
            hpSwap.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    Private Sub SetControl()
        Dim liAj As HtmlGenericControl = Page.Master.FindControl("liAj")
        liAj.Attributes.Add("class", "active")

        Dim ddAj As HtmlGenericControl = Page.Master.FindControl("ddAj")
        ddAj.Attributes.Add("class", "active")
    End Sub

End Class
