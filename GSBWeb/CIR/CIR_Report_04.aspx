<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CIR_Report_04.aspx.vb" Inherits="GSBWeb.CIR_Report_04" %>

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
            <div class="panel panel-default">
                <div class="panel-heading" style="text-align: left; font-weight: normal;
                    background: #FF388C; padding-right: 5; color: #FFFFFF;">
                    รายงานเพื่อการบริหารภายใน : รายงานสินทรัพย์เสี่ยงและเงินกองทุน
                </div>
            </div>
            <table width="70%" align="center">
               <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                           14 รายงานอัตราความเพียงพอของเงินกองทุนขั้นต่ำ
                        </div>
                    </td>
                </tr
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                </tr>
                <tr>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">14.1&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.1" 
                            target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">รายงานอัตราความเพียงพอของเงินกองทุนขั้นต่ำ (รายเดือน)</span></a></td>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                        <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;"><a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.2" target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">กราฟแสดงระดับเงินกองทุน สินทรัพย์เสี่ยงทั้งสิ้น และสินทรัพย์เสี่ยงส่วนที่เพิ่มได้ </span></a></td>
                    </tr>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                        <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;"><a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.3" target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">กราฟแสดงการเปลี่ยนแปลงของอัตราส่วนเงินกองทุนย้อนหลัง 13 เดือนล่าสุด</span></a></td>
                    </tr>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                        <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;"><a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.4" target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">กราฟแสดงการเปลี่ยนแปลงของอัตราส่วนเงินกองทุน ระดับเงินกองทุนทั้งสิ้นและสินทรัพย์เสี่ยงทั้งสิ้น ย้อนหลัง 13 เดือน</span></a></td>
                    </tr>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style28" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; "></td>
                        <td class="auto-style25" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                            <br />
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td colspan="2">
                            <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                                15 รายงานรายละเอียดเงินกองทุน
                            </div>
                        </td>
                    </tr>
                </tr>
                <tr>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">15.1&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.16" 
                            target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">รายงานรายละเอียดเงินกองทุน (Basel II)</span></a></td>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style28" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; "></td>
                        <td class="auto-style25" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                            <br />
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td colspan="2">
                            <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                                16 รายงานการดำรงเงินกองทุน
                            </div>
                        </td>
                    </tr>
                </tr>
                <tr>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style29">16.1&nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0" colspan="2">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.17" target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">รายงานการดำรงเงินกองทุน</span></a></td>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style28" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; "></td>
                        <td class="auto-style25" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                            <br />
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td colspan="2">
                            <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                                17 รายงานสินทรัพย์เสี่ยงรวม
                            </div>
                        </td>
                    </tr>
                </tr>

                <tr>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">17.1&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.18" 
                            target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">รายงานสินทรัพย์เสี่ยงรวม</span></a></td>
                    <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style28" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; "></td>
                        <td class="auto-style25" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                            <br />
                        </td>
                    </tr>
                    <tr height="0" style="display: none">
                        <td class="auto-style28">
                            <br></br>
                        </td>
                        <td style="width: 23pt" width="30"></td>
                    </tr>
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
    <%-- <table cellpadding=0 cellspacing=0>
 <tr height=32 style='mso-height-source:userset;height:24.0pt'>
  <td height=32 style='height:24.0pt'>&nbsp;</td>
  <td></td>
  <td>&nbsp;</td>
  <td align=left><span
  style='mso-spacerun:yes'>&nbsp;</span>รายงานเพื่อการบริหารภายใน :
  รายงานสินทรัพย์เสี่ยงและเงินกองทุน</td>
  <td ></td>
 </tr>
 <tr height=21 style='mso-height-source:userset;height:15.95pt'>
  <td height=21  style='height:15.95pt'>&nbsp;</td>
  <td></td>
  <td></td>
  <td></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29 style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td>1</td>
  <td width=1287 style='width:965pt'>รายงานการตรวจสอบและติดตามหลักประกัน<span
  style='mso-spacerun:yes'>&nbsp;</span></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>1.1 รายงานการตรวจสอบและติดตามหลักประกันตามประเภทผลิตภัณฑ์</span></a></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29 style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td>&nbsp;</td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.2"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>1.2 รายงานการตรวจสอบและติดตามหลักประกันตามประเภทสินเชื่อ</span></a></td>
  <td></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29 style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.3"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>1.3 รายงานการตรวจสอบและติดตามหลักประกันแบ่งตามกลุ่มธุรกิจ</span></a></td>
  <td></td>
 </tr>
 <tr  height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29 style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.4"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>1.4 รายงานการตรวจสอบและติดตามหลักประกันแบ่งตามภาค</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td>2</td>
  <td>รายงานสินเชื่อเพื่อที่อยู่อาศัย จำแนกตาม LTV Ratio</td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td>&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.2.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>2.1 รายงานสินเชื่อเพื่อที่อยู่อาศัย จำแนกตาม LTV Ratio</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.2.2"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>2.2 รายงานสินเชื่อเพื่อที่อยู่อาศัยตามแนวราบ จำแนกตาม LTV Ratio</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.2.3"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>2.3 รายงานสินเชื่อเพื่อที่อยู่อาศัยตามแนวสูง จำแนกตาม LTV Ratio</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >3</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.3"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามการปรับลดความเสี่ยงด้วยวิธี Simple
  และวิธี Comprehensive</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.3.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>3.1 รายงานยอดคงค้างสินเชื่อ เงินลงทุน และภาระผูกพันของธนาคาร</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >4</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.4"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามประเภทผลิตภัณฑ์ (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29 style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >5</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.5"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานเงินกองทุนและสินทรัพย์เสี่ยงด้านเครดิต ตามสายงานกิจการสาขา
  (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >6</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.6"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินเชื่อคงเหลือ และสินทรัพย์เสี่ยงด้านเครดิต
  แบ่งตามการจัดชั้นหนี้และหนี้ที่ไม่ก่อให้เกิดรายได้ (NPL)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >7</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินทรัพย์เสี่ยงด้านเครดิตแบ่งตามประเภทสินทรัพย์
  เปรียบเทียบเดือนปัจจุบัน และเดือนก่อนหน้า ตามวิธี SA<span
  style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.2"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>7.1 รายงานสินทรัพย์เสี่ยงด้านเครดิตสำหรับลูกหนี้ธุรกิจเอกชน
  แยกตามรายลูกค้า (CIF) 20 รายแรก</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.3"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>7.2 รายงานสินทรัพย์เสี่ยงด้านเครดิตสำหรับลูกหนี้รายย่อย
  แยกตามประเภทสินเชื่อย่อย (Market Code) 20 รายการแรก<span
  style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.4"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>7.3 รายงานสินทรัพย์เสี่ยงด้านเครดิตสำหรับสินทรัพย์อื่น
  แยกตามรายการทางบัญชี (GL) 20 รายการแรก<span
  style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >8</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.8"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินทรัพย์เสี่ยงด้านเครดิตตามกลุ่มลูกหนี้<span
  style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >9</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.9"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานการเปลี่ยนแปลงสินทรัพย์เสี่ยงด้านเครดิต (เดือนปัจจุบัน เทียบกับ
  เดือนก่อนหน้า)<span style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >10</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.10"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานยอดสินเชื่อคงเหลือและสินทรัพย์เสี่ยงด้านเครดิต (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >11</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.11"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามศูนย์ EVM (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >12</td>
  <td width=1287 style='width:965pt'>รายงานเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด</td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>12.1
  รายงานเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
  (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.1_Gr"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>กราฟแสดงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาดย้อนหลัง
  13 เดือน (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.1.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>ตารางแสดงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาดแยกตามวิธีการคำนวณ<span
  style='mso-spacerun:yes'>&nbsp;&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.2"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>12.2
  รายงานเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
  (รายวัน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.2.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>ตารางแสดงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาดแยกตามวิธีการคำนวณ<span
  style='mso-spacerun:yes'>&nbsp;&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >13</td>
  <td width=1287 style='width:965pt'>รายงานเปรียบเทียบธุรกรรมที่ใช้ในการคำนวณเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด</td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>13.1
  รายงานเปรียบเทียบธุรกรรมที่ใช้ในการคำนวณเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
  (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.2"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>ตารางแสดงข้อมูลหลักทรัพย์ที่ต้องดำรงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดสูงสุด
  10 อันดับแรก ตามประเภทความเสี่ยง<span style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.3"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>13.2
  รายงานเปรียบเทียบธุรกรรมที่ใช้ในการคำนวณเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
  (รายวัน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.4"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>ตารางแสดงข้อมูลหลักทรัพย์ที่ต้องดำรงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดสูงสุด
  10 อันดับแรก ตามประเภทความเสี่ยง<span style='mso-spacerun:yes'>&nbsp;</span></span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29 style='height:21.95pt'>&nbsp;</td>
  <td ></td>
  <td >14</td>
  <td  align=left width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.1"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานอัตราความเพียงพอของเงินกองทุนขั้นต่ำ (รายเดือน)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.2"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>กราฟแสดงระดับเงินกองทุน สินทรัพย์เสี่ยงทั้งสิ้น
  และสินทรัพย์เสี่ยงส่วนที่เพิ่มได้</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td ></td>
  <td width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.3"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>กราฟแสดงการเปลี่ยนแปลงของอัตราส่วนเงินกองทุนย้อนหลัง 13 เดือนล่าสุด</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >&nbsp;</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.14.4"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>กราฟแสดงการเปลี่ยนแปลงของอัตราส่วนเงินกองทุน
  ระดับเงินกองทุนทั้งสิ้นและสินทรัพย์เสี่ยงทั้งสิ้น ย้อนหลัง 13 เดือน</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >15</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.16"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานรายละเอียดเงินกองทุน (Basel II)</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >16</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.17"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานการดำรงเงินกองทุน</span></a></td>
  <td ></td>
 </tr>
 <tr height=29 style='mso-height-source:userset;height:21.95pt'>
  <td height=29  style='height:21.95pt'>&nbsp;</td>
  <td></td>
  <td >17</td>
  <td  width=1287 style='width:965pt'><a
  href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.18"
  target="_parent"><span style='color:#595959;font-size:10.0pt;text-decoration:
  none'>รายงานสินทรัพย์เสี่ยงรวม</span></a></td>
  <td ></td>
 </tr>
 <tr height=0 style='display:none'>
  <td width=86 style='width:65pt'></td>
  <td width=30 style='width:23pt'></td>
  <td width=46 style='width:35pt'></td>
  <td width=1287 style='width:965pt'></td>
  <td width=30 style='width:23pt'></td>
 </tr>
</table>--%>
</asp:Content>
