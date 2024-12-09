<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SystemReport.aspx.vb" Inherits="GSBWeb.SystemReport" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
        .auto-style1 {
            width: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />


    <asp:ScriptManager runat="server" >
    </asp:ScriptManager>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row">
            <div class="col-md-8 col-md-offset-2"> 
                <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt;
                    background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px; ">
                    System Report
                </div>
                <asp:Panel ID="pnlReport" runat="server" />
            </div>

        </div>
 <%--           <table width="70%" align="center">               
                
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">1&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Reconcile" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">Reconcile Report</span></a></td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">2&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Audit_Trial" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">Audit Trial Report</span></a></td>
                </tr>
               


                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <br />
                    </td>
                </tr>

                
                 <tr height="0" style="display: none">
                        <td class="auto-style28">
                            <br>
                            <br></br>
                            </br>
                        </td>
                        <td style="width: 23pt" width="30"></td>
                    </tr>
            </table>--%>

            <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
