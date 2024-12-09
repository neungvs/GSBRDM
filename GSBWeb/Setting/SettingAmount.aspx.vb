Imports GSBWeb.BLL
Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports System.IO
Imports System.Web.UI.Page
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Xml

Public Class SettingAmount
    Inherits System.Web.UI.Page

    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
            'SetControl()
        End If
    End Sub

    Private Sub LoadData()
        ReadXml(Server.MapPath("~/Setting/AmountSetting.xml"))
    End Sub

    Public Sub CreateMessageAlert(ByVal pg As Page, ByVal strMessage As String, ByVal guidKey As Guid)
        Dim strScript As String = "alert('" & strMessage & "');"
        System.Web.UI.ScriptManager.RegisterStartupScript(pg, pg.GetType(), guidKey.ToString(), strScript, True)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrEmpty(txtAmount.Text) Then
            Me.CreateMessageAlert(Page, "กรุณากรอกข้อมูลจำนวนเงิน", gui)
        Else
            If CDbl(txtAmount.Text) < 0 Then
                Me.CreateMessageAlert(Page, "จำนวนเงินติดลบไม่ได้", gui)
            Else
                Dim MyXML As New XmlDocument()
                MyXML.Load(Server.MapPath("~/Setting/AmountSetting.xml"))
                Dim MyXMLNode As XmlNode = MyXML.SelectSingleNode("/setting/AmountSetting")
                If MyXMLNode IsNot Nothing Then
                    MyXMLNode.ChildNodes(0).InnerText = CDbl(txtAmount.Text)
                End If ' Save the Xml.

                MyXML.Save(Server.MapPath("~/Setting/AmountSetting.xml"))
                ReadXml(Server.MapPath("~/Setting/AmountSetting.xml"))

                txtAmount.Text = ""
            End If
        End If
    End Sub

    Private Sub SetControl()
        Dim liAj As HtmlGenericControl = Page.Master.FindControl("liSt")
        liAj.Attributes.Add("class", "active")

        Dim ddAj As HtmlGenericControl = Page.Master.FindControl("ddAj")
        ddAj.Attributes.Add("class", "active")

    End Sub

    Private Sub ReadXml(FileName As String)
        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(FileName)
        Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/setting")
        Dim amountString As String = ""

        For Each node As XmlNode In nodes
            amountString = node.SelectSingleNode("AmountSetting").InnerText
        Next
        lblAmount.Text = "จำนวนวงเงินที่ตั้งค่า " + String.Format("{0:#,##0.00}", CDbl(amountString)) + " บาท"
    End Sub

End Class