<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Stress_Report.aspx.vb" Inherits="GSBWeb.Stress_Report" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
<uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF">
                    รายงานการทดสอบภาวะวิกฤติ (Stress Test) และการทดสอบแบบจำลอง (Back Test)
                </div>
            </div>
            <table width="75%" align="center">
                <tr style="width: 100%">
                    <td colspan="2" class="auto-style23">
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF;">
                            1 รายงาน Summary Stress Test
                        </div>
                    </td>
       
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> </td>
                 <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"> 
                   
                        <a style="text-decoration:none">
                           <span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size:medium;'> 1.1 รายงานการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านตลาดและด้านเครดิต </span>
               </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
              
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'> ตารางที่ 1.1.1 ตารางสรุปการทดสอบภาวะวิกฤติภาพรวม </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.2 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) : Net Exposure </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.3 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) : ค่าใช้จ่ายหนี้สูญและหนี้สงสัยจะสูญ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.4 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) : ค่าใช้จ่ายหนี้สูญ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.5"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.5 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) : หลักประกันที่นำมาหักจากลูกหนี้จัดชั้นได้ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.6"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.6 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) : Risk Weighted Asset (RWA) </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.7"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.7 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) : Reserve </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.8"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.8 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) (ประเภทสินทรัพย์ตามธนาคารแห่งประเทศไทย): Risk Weighted Asset (RWA) </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.9"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.9 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านเครดิต (รายปี) (ประเภทสินทรัพย์ตามธนาคารแห่งประเทศไทย): Reserve </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.1.10"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.10 ตารางสรุปการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านตลาด </span>
                        </a>
                    </td>
                </tr>
                
              
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <br />
                    </td>
                </tr>
               <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: larger; margin: 0; padding: 0; " class="auto-style28"> </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a style="text-decoration:none">
                           <span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size:medium;'> 1.2 รายงานการทดสอบภาวะวิกฤติ สำหรับความเสี่ยงด้านการกระจุกตัว </span>
               </a>
                    </td>
                </tr>
               <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.1 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.2 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) : Net Exposure </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.3 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) : ค่าใช้จ่ายหนี้สูญและหนี้สงสัยจะสูญ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.4 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) : ค่าใช้จ่ายหนี้สูญ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.5"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.5 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) : Risk Weighted Asset (RWA) </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.6"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.6 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) : Reserve </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.7"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.7 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) (ประเภทสินทรัพย์ตามธนาคารแห่งประเทศไทย) : Risk Weighted </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress1.2.8"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.8 ตารางสรุปการทดสอบภาวะวิกฤติภาพ สำหรับความเสี่ยงด้านกระจุกตัวของเครดิต (รายปี) (ประเภทสินทรัพย์ตามธนาคารแห่งประเทศไทย) : Reserve </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <br />
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF;">
                            2 รายงาน Back Testing VaR
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress2.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 2.1 ตาราง Market VaR Back Testing </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28" width="4%"> &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Stress2.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 2.2 ตาราง Credit VaR Back Testing </span>
                        </a>
                    </td>
                </tr>
               
       
                   <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style28" 
                            style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                        </td>
                        <td class="auto-style25" 
                            style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                          <br />
                        </td>
                    </tr>
            </table>
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
