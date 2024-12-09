<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alert_and_confirm.ascx.vb" Inherits="GSBWeb.alert_and_confirm" %>

<!-- Bootstrap CSS -->
<link href="<%= Page.ResolveClientUrl("~/Styles/bootstrap.css")%>" rel="stylesheet" type="text/css" />
<!-- Font awesome -->
<link href="<%= Page.ResolveClientUrl("~/fonts/font-awesome-4.7.0/css/font-awesome.min.css")%>" rel="stylesheet" type="text/css" />
 <style type="text/css">
        @font-face {
            font-family: Tahoma;
            /*font-family: 'DBHelvethaicaXMedCondv3_2';
            src: url('~/fonts/DBHelvethaicaXMedCondv3_2.woff') format('woff');*/
        }
</style>
<div style="background-color:#e6e6e6;opacity:0.4;filter:alpha(opacity=40);">
    <div id="MessageBox" runat="server" style="opacity:1;filter:alpha(opacity=100);">
        <center>
            <table width = "400px" style="height:400px;" border="0">
                <tr>
                    <td align="center" style="height:300px;">
                        <asp:Image ID="ImageData" runat="server" Width="400px" Height="400" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="HMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                       <asp:Button ID="OK" runat="server" Text=""  />&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="Cancel" runat="server" Text="" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
</div>
