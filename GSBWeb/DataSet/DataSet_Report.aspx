<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="DataSet_Report.aspx.vb" Inherits="GSBWeb.DataSet_Report" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
<%--                <div class="col-md-8 col-md-offset-2">        
                    <a href = "#" class = "list-group-item active">
                       Free Domain Name Registration
                    </a>

                    <a href = "#" class = "list-group-item">24*7 support</a>
                    <a href = "#" class = "list-group-item">Free Window Space hosting</a>
                    <a href = "#" class = "list-group-item">Number of Images</a>
                    <a href = "#" class = "list-group-item">Renewal cost per year</a>
                </div>--%>

<%--                <div class="container">
                  <h2>Basic List Group</h2>
                  <ul class="list-group" style="text-align: left;">
                    <li class="list-group-item">First item</li>
                    <li class="list-group-item">Second item</li>
                    <li class="list-group-item">Third item</li>
                  </ul>
                </div>--%>

                <div class="col-md-8 col-md-offset-2">        
<%--                    <a href = "#" class = "list-group-item active">
                       Free Domain Name Registration
                    </a>--%>
                    <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt;
                        background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px; ">
                        Data Set
                    </div>
<%--                    <a href = "#" class = "list-group-item" style="text-align: left; padding-right: 10;">1. Capital Fund (DS_CAP)</a>
                    <a href = "#" class = "list-group-item" style="text-align: left">2. Operational Risk (DS_OPR)</a>
                    <a href = "#" class = "list-group-item" style="text-align: left">3. Credit Risk Standardised Approach (DS_CRS)</a>
                    <a href = "#" class = "list-group-item" style="text-align: left">4. Contingent Summary (DS_COS)</a>--%>
                    <asp:Panel ID="pnlReport" runat="server"/>
                </div>
                
            </div>

<%--
            <div class="panel panel-default">
                <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF">
                    DataSet
                </div>
            </div>
            <table width="70%" align="center">               
                
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">1&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Capital%20Fund" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">Data Set: Capital Fund (DS_CAP)</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">2&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Operation%20Risk" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">Data Set: Operational
                        Risk (DS_OPR)</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">3&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/CRS_SFI3.0" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">Data Set: Credit
                        Risk Standardised Approach (DS_CRS)</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">4&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/COS_SFI3.0" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">Data Set: Contingent
                        Summary (DS_COS)</span></a></td>
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
                            <br>
                            <br>
                            <br>
                            <br>
                            <br></br>
                            <br>
                            <br>
                            <br>
                            <br></br>
                            <br>
                            <br>
                            <br></br>
                            <br>
                            <br>
                            <br></br>
                            <br>
                            <br></br>
                            <br>
                            <br></br>
                            <br>
                            <br></br>
                            <br>
                            <br></br>
                            <br></br>
                            <br></br>
                            <br></br>
                            <br></br>
                            <br></br>
                            <br></br>
                            <br></br>
                            <br></br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
                            </br>
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
