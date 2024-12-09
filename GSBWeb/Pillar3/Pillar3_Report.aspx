<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Pillar3_Report.aspx.vb" Inherits="GSBWeb.Pillar3_Report" %>

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
                <div class="col-md-8 col-md-offset-2"> 
                    <div class="NormalHeader" style="height:90px; ">
                        รายงานมาตรฐานที่เป็นไปตามข้อกำหนดสำหรับการเปิดเผยข้อมูลเกี่ยวกับการดำรงเงินกองทุน (Pillar 3) ที่เกี่ยวกับความเพียงพอของเงินกองทุน และ/หรือ ความต้องการเงินกองทุนที่เกี่ยวข้องกับการประเมินความเสี่ยงด้านต่างๆของธนาคาร
                    </div>
                    <asp:Panel ID="pnlReport" runat="server" />
                </div>

            </div>


<%--            <div class="panel panel-default">
                <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF">
                    รายงานมาตรฐานที่เป็นไปตามข้อกำหนดสำหรับการเปิดเผยข้อมูลเกี่ยวกับการดำรงเงินกองทุน
  (Pillar 3) ที่เกี่ยวกับความเพียงพอของเงินกองทุน<br>
    และ/หรือ
  ความต้องการเงินกองทุนที่เกี่ยวข้องกับการประเมินความเสี่ยงด้านต่างๆของธนาคาร
                </div>
            </div>
            <table width="70%" align="center">               
                
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">1&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.1" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางเงินกองทุนของธนาคารออมสิน</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">2&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom;; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.2" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่าเงินกองทุนขั้นต่ำที่ต้องดำรงสำหรับความเสี่ยงด้านเครดิต
  แยกตามประเภทสินทรัพย์ โดยวิธี SA</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">3&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.3" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางเงินกองทุนขั้นต่ำที่ต้องดำรงสำหรับความเสี่ยงด้านตลาด
  (โดยวิธีมาตรฐาน/แบบจำลอง)</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">4&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.4" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางเงินกองทุนขั้นต่ำที่ต้องดำรงสำหรับความเสี่ยงด้านปฏิบัติการ
  (โดยวิธี BIA / SA / ASA)</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">5&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.5" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางอัตราส่วนเงินกองทุนทั้งสิ้นต่อสินทรัพย์เสี่ยง และอัตราส่วนเงินกองทุนชั้นที่ 
                        1 ต่อสินทรัพย์เสี่ยงของ</span></a><a 
                            href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.1" 
                            target="_self"><span 
                            style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ธนาคารออมสิน</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">6&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.6" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างของสินทรัพย์ในงบดุลและรายการนอกงบดุลที่สำคัญก่อนพิจารณาผลการปรับลดความเสี่ยงด้านเครดิต</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align:right; font-size: medium;  margin: 0; padding: 0;">7&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.7" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างของสินทรัพย์ในงบดุลและรายการนอกงบดุลที่สำคัญก่อนพิจารณาผลการปรับลดความเสี่ยงด้านเครดิต
  จำแนกตามประเทศหรือภูมิภาคของลูกหนี้ *</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">8&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.8" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างของสินทรัพย์ในงบดุลและรายการนอกงบดุลก่อนพิจารณาผลการปรับลดความเสี่ยงด้านเครดิต
  จำแนกตามอายุสัญญาที่เหลือ</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">9&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.9" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างเงินให้สินเชื่อรวมดอกเบี้ยค้างรับ
  และเงินลงทุนในตราสารหนี๊และตามเกณฑ์การจัดชั้นที่ธปท.กำหนดก่อนพิจารณาผลการปรับลดความเสี่ยงด้านเครดิตจำแนกตามประเทศหรือภูมิภาคของลูกหนี้*
  และตามเกณฑ์การจัดชั้นที่ธปท.กำหนด</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">10&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.10" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่าของเงินสำรองที่กันไว้ (General provision และ Specific
  provision) และหนี้สูญที่ตัดออกจากบัญชีระหว่างงวด
  สำหรับเงินให้สินเชื่อรวมดอกเบี้ยค้างรับและเงินลงทุนในตราสารหนี้
  จำแนกตามประเทศหรือภูมิภาค</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">11&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.11" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างเงินให้สินเชื่อรวมดอกเบี้ยค้างรับ*
  ก่อนพิจารณาผลการปรับลดความเสี่ยงด้านเครดิต
  จำแนกตามประเภทธุรกิจและเกณฑ์การจัดชั้นที่ธปท.กำหนด</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">12&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.12" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่าของเงินสำรองที่กันไว้ (General provision และ Specific
  provision) และมูลค่าของหนี้สูญที่ตัดออกจากบัญชีระหว่างวด
  สำหรับเงินให้สินเชื่อรวมดอกเบี้ยค้างรับ* จำแนกตามประเภทธุรกิจ</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">13&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.13" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตาราง Reconciliation ของการเปลี่ยนแปลงมูลค่าของเงินสำรองที่กันไว้
  (General provision และ Specific provision)
  สำหรับเงินให้สินเชื่อรวมดอกเบี้ยค้างรับ*</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">14&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.14" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างของสินทรัพย์ในงบดุลและมูลค่าเทียบเท่าสินทรัพย์ในงบดุลของรายการนอกงบดุล*
  แยกตามประเภทสินทรัพย์โดยวิธี SA</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">15&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.15" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างของสินทรัพย์ในงบดุลและรายการนอกงบดุลสุทธิ**
  หลังพิจารณามูลค่าการปรับลดความเสี่ยงด้านเครดิตในแต่ละประเภทสินทรัพย์จำแนกตามแต่ละน้ำหนักความเสี่ยงโดยวิธี
  SA</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">16&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.16" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่ายอดคงค้างในส่วนที่มีหลักประกัน** ของแต่ละประเภทสินทรัพย์
  โดยวิธี SA จำแนกตามประเภทของหลักประกัน</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">17&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.17" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่าเงินกองทุนขั้นต่ำสำหรับความเสี่ยงด้านตลาดในแต่ละประเภท
  โดยวิธีมาตรฐาน</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">18&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.18" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางมูลค่าฐานะที่เกี่ยวข้องกับตราสารทุนในบัญชีเพื่อการธนาคาร</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align:text-bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">19&nbsp; </td>
                    <td colspan="2" style="vertical-align:text-bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_Pillar3_3.19" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางผลการเปลี่ยนแปลงของอัตราดอกเบี้ย*ต่อรายได้สุทธิ (Earnings)</span></a></td>
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

