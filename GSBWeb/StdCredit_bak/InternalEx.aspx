﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Mainpage.Master" CodeBehind="InternalEx.aspx.vb" Inherits="STDcredit.InternalEx" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
  <h1>Internal & External Report </h1> </br>
    <table>
            <tr>
                <td>วันที่ (Time)</td>
                <td>
                    <asp:DropDownList ID="DrlTimeYear" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="DrlTimeMonth" runat="server"></asp:DropDownList>
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
             <tr>
                <td>หน่วย</td>
                <td>
                    <asp:DropDownList ID="Drl_count" runat="server" Height="20px" Width="149px">
                        <asp:ListItem Value="1">บาท</asp:ListItem>
                        <asp:ListItem Value="1000">พันบาท</asp:ListItem>
                        <asp:ListItem Value="1000000">ล้านบาท</asp:ListItem>
                    </asp:DropDownList>
                 </td>
                <td></td>
                <td>&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
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
        <br>
   <div style="height:100vh;">
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">           
        </rsweb:ReportViewer> 
    </div>
</asp:Content>


