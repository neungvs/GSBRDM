<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="9DimensionOtype.aspx.vb" Inherits="GSBWeb._9DimensionOtype" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="1200">
    </asp:ScriptManager>

    <script type="text/javascript">
        function GetSelectedTextValue() {
            var DrlScenario = document.getElementById("<%=DrlScenario.ClientID%>");
            var Drlsimcom = document.getElementById("<%=Drlsimcom.ClientID%>");
            var Drloem = document.getElementById("<%=Drloem.ClientID%>");
            if (DrlScenario.value == 0) {
                alert("กรุณาเลือก สถานการณ์ (Scenario)");
                return false;
            }
            if (Drlsimcom.value == 0) {
                alert("กรุณาเลือก วิธีการปรับลดความเสี่ยง");
                return false;
            }
            if (Drloem.value == 0) {
                alert("กรุณาเลือก วิธีการคำนวณสำหรับตราสารอนุพันธ์");
                return false;
            }
            return true;
        }
        $(document).ready(function () {
            $("a[title='MHTML (web archive)']").parent().hide();
            $("a[title='TIFF file']").parent().hide();
            $("a[title='XML file with report data']").parent().hide();
            $("a[title='CSV (comma delimited)']").parent().hide();
        });
    </script>
    <div class="contentTableAreaStyle">
        <div>
            <h1>Dimension Report - Breakdown By Obligor Type</h1>
            </br>
        <table class="tabletypereport1">
            <tr>
                <td>วันที่ (Time)</td>
                <td>
                    <asp:DropDownList ID="DrlTimeYear" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="DrlTimeMonth" runat="server"></asp:DropDownList>
                </td>
                <td></td>
                <td>สถานการณ์ (Scenario)</td>
                <td class="auto-style1">
                    <asp:DropDownList ID="DrlScenario" runat="server" DataTextField="SCENARIODESC" DataValueField="SCENARIOID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>วิธีการปรับลดความเสี่ยง</td>
                <td>
                    <asp:DropDownList ID="Drlsimcom" runat="server" DataTextField="SIMP_COMP" DataValueField="SIMP_COMP">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td>วิธีการคำนวณสำหรับตราสารอนุพันธ์</td>
                <td class="auto-style1">
                    <asp:DropDownList ID="Drloem" runat="server" DataTextField="OEM_CEM" DataValueField="OEM_CEM">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td></td>
                <td>&nbsp;</td>
                <td colspan="2" align="right">
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" />
                    <asp:Button ID="btnOk" runat="server" OnClientClick="return GetSelectedTextValue()" Text="ตกลง" />
                </td>
            </tr>
        </table>
            <br>
        </div>
        <div style="height: 100vh;">
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>



