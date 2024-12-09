

Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Configuration
Imports System.Reflection
Imports System.Collections.Specialized
Imports System.Web.Configuration
Imports System.Xml
Imports System.Web



Public Class UtilsBiz


    Public Sub New()

    End Sub


    Public Shared Sub CreateMessageAlert(ByVal pg As Page, ByVal strMessage As String, ByVal guidKey As Guid)

        Dim strScript As String = "alert('" & strMessage & "');"
        ScriptManager.RegisterStartupScript(pg, pg.GetType(), guidKey.ToString(), strScript, True)


    End Sub


    Public Shared Function checkForSQLInjection(ByVal parameter As String) As Boolean


        Dim isSQLInjection As Boolean = False

        Dim blackList As String() = {"--", ";--", ";", "/*", "*/", "@@", _
        "@", "char", "nchar", "varchar", "nvarchar", "alter", _
        "begin", "cast", "create", "cursor", "declare", "delete", _
        "drop", "end", "exec", "execute", "fetch", "insert", _
        "kill", "open", "select", "sys", "sysobjects", "syscolumns", _
        "table", "update"}



        For i = 0 To blackList.Length - 1

            If parameter.ToLower.IndexOf(blackList(i), StringComparison.OrdinalIgnoreCase) >= 0 Then


                isSQLInjection = True

            End If

        Next


        Return isSQLInjection


    End Function



    Public Shared Function GetAmountTotal() As String

        Dim Server = HttpContext.Current.Server


        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(Server.MapPath("~/Setting/AmountSetting.xml"))
        Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/setting")
        Dim amountString As String = ""


        For Each node As XmlNode In nodes

            amountString = node.SelectSingleNode("AmountSetting").InnerText

        Next

        Return amountString


    End Function






End Class
