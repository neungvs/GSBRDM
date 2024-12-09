<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Capital_Calulation_Report.aspx.vb" Inherits="GSBWeb.Capital_Calulation_Report" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-8 col-md-offset-2"> 
                    <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 16pt;
                        background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px;">
                        รายงานการคำนวณเงินกองทุน
                    </div>
                    <asp:Panel ID="pnlReport" runat="server" />
                </div>
            </div>


<%--            <table width="70%" align="center">
  <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td width="30px"></td>
  <td style="vertical-align:bottom; text-align:left; font-size:medium;"> 1. &nbsp</td>
  <td style="vertical-align:bottom; text-align:left;font-size:larger;"><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Regulatory%20Report2"
  target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none;font-size:medium;'>รายงานการคำนวณเงินกองทุนตามหลักเกณฑ์ของธนาคารแห่งประเทศไทย Regulatory
  Capital (สรุป)</span></a></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td width="30px"></td>
  <td style="vertical-align:bottom; text-align:left; font-size:medium;">2. &nbsp</td>
  <td style="vertical-align:bottom; text-align:left;font-size:larger;"><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Regulatory%20Report"
  target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none;font-size:medium;'>รายงานการคำนวณเงินกองทุนตามหลักเกณฑ์ของธนาคารแห่งประเทศไทย Regulatory
  Capital (รายละเอียด)</span></a></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td width="30px"></td>
  <td style="vertical-align:bottom; text-align:left; font-size:medium;">3.&nbsp</td>
  <td style="vertical-align:bottom; text-align:left;font-size:larger;"><a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Internal%20Report2" target="_self">
  <span style='color: #595959; font-size: 10.0pt; text-decoration: none;font-size:medium;'>รายงานการคำนวณเงินกองทุนเพื่อการบริหาร (สรุป)</span></a></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td width="30px"></td>
  <td style="vertical-align:bottom; text-align:left; font-size:medium;">4.&nbsp</td>
  <td style="vertical-align:bottom; text-align:left;font-size:larger;"><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Internal%20Report"  target="_self">
  <span style='color: #595959; font-size: 10.0pt; text-decoration: none;font-size:medium;'>รายงานการคำนวณเงินกองทุนเพื่อการบริหาร (รายละเอียด)</span></a></td>
  <td></td>
 </tr>
 <tr height=0 style='display:none'>
  <td width=86 style='width:65pt'></td>
  <td width=30 style='width:23pt'></td>
  <td width=46 style='width:35pt'></td>
  <td width=1287 style='width:965pt'></td>
  <td width=30 style='width:23pt'></td>
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
