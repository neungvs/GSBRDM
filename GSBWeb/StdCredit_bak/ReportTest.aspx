<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Mainpage.Master" CodeBehind="ReportTest.aspx.vb" Inherits="STDcredit.ReportTest" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h1>Test Run Report</h1> </br>
        <table>
            <tr>
                <td>วันที่ (Time)</td>
                <td>
                    <asp:DropDownList ID="DrlTime" runat="server"  DataTextField="timeid" DataValueField="timeid">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td>สถานการณ์ (Scenario)</td>
                <td class="auto-style1">
                    <asp:DropDownList ID="DrlScenario" runat="server" DataTextField="SCENARIOID" DataValueField="SCENARIOID">
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
                <td>
                    &nbsp;</td>
                <td colspan="2" align="right">
                    <asp:Button ID="btnBack" runat="server" Text="<<" />
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" />
                    <asp:Button ID="btnOk" runat="server" Text="ตกลง" />
                </td>
            </tr>
        </table><br>
   <div style="height:100vh;">
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">           
        </rsweb:ReportViewer> 
    </div>
</asp:Content>
