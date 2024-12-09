<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="18LossDist.aspx.vb" Inherits="GSBWeb._18LossDist" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
       function GetSelectedTextValue()
       {
            var DrlScenario = document.getElementById("<%=DrlScenario.ClientID%>");
            if (DrlScenario.value == 0)
            {
                alert("กรุณาเลือก สถานการณ์ (Scenario)");
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
  <h1>Loss Distribution</h1> </br>
        <table>
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
                <td>การกระจายตัวทางสถิติ</td>
                <td>
                    <asp:DropDownList ID="DrlPdfCdf" runat="server">
                        <asp:ListItem>PDF</asp:ListItem>
                        <asp:ListItem>CDF</asp:ListItem>
                    </asp:DropDownList>
                 </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
           
            <tr>
                <td>&nbsp;</td>
                <td></td>
                <td>
                    &nbsp;</td>
                <td colspan="2" align="right">
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" />
                    <asp:Button ID="btnOk" runat="server" OnClientClick="return GetSelectedTextValue()" Text="ตกลง" />
                </td>
            </tr>
        </table><br>
   <div style="height:100vh;">
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">           
        </rsweb:ReportViewer> 
    </div>
</asp:Content>
