<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CIR_Report.aspx.vb" Inherits="GSBWeb.CIR_Report" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style11 {
            height: 22pt;
            width: 2181px;
        }
        .auto-style23 {
            height: 23px;
        }
        .auto-style25 {
            width: 2344px;
        }
        .auto-style26 {
            width: 42px;
        }
        .auto-style27 {
            width: 42px;
            height: 22pt;
        }
        .auto-style28 {
            width: 66px;
        }
        .auto-style29 {
            width: 66px;
            height: 22pt;
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
                    <div class="panel panel-default">
                        <div class=" NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt;
                            background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px; ">
                            รายงานเพื่อการบริหารภายใน : รายงานสินทรัพย์เสี่ยงและเงินกองทุน
                        </div>
                        <div class="panel-body">
                             <asp:Panel ID="pnlReport" runat="server" />
<!--
                             <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: large;
                                background: #F279AF; padding-right: 5; color: #000000; ">
                                Header 2
                            </div>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;">First item</a>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;">&nbsp;&nbsp;Second item</a>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;">&nbsp;&nbsp;Third item</a>
-->                                     
                        </div>
                    </div>
                </div>
            </div>

<%--            <table width="70%" align="center">
                <tr style="width: 100%">
                    <td colspan="2" class="auto-style23">
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF;">
                            1 รายงานการตรวจสอบและติดตามหลักประกัน
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">1.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานการตรวจสอบและติดตามหลักประกันตามประเภทผลิตภัณฑ์ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">1.2 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานการตรวจสอบและติดตามหลักประกันตามประเภทสินเชื่อ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">1.3 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานการตรวจสอบและติดตามหลักประกันแบ่งตามกลุ่มธุรกิจ </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">1.4 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.1.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานการตรวจสอบและติดตามหลักประกันแบ่งตามภาค </span>
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
                            2 รายงานสินเชื่อเพื่อที่อยู่อาศัย จำแนกตาม
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">2.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.2.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินเชื่อเพื่อที่อยู่อาศัย จำแนกตาม LTV Ratio </span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">2.2 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.2.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินเชื่อเพื่อที่อยู่อาศัยตามแนวราบ จำแนกตาม LTV Ratio
                            </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">2.3 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.2.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินเชื่อเพื่อที่อยู่อาศัยตามแนวสูง จำแนกตาม LTV Ratio
                            </span></a>
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
                            3 รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามการปรับลดความเสี่ยง
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">3.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามการปรับลดความเสี่ยงด้วยวิธี Simple และวิธี Comprehensive
                            </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">3.2 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.3.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานยอดคงค้างสินเชื่อ เงินลงทุน และภาระผูกพันของธนาคาร
                            </span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            4 รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามประเภทผลิตภัณฑ์
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">4.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามประเภทผลิตภัณฑ์ (รายเดือน)
                            </span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            5 รายงานเงินกองทุนและสินทรัพย์เสี่ยงด้านเครดิต ตามสายงานกิจการสาขา
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">5.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.5"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานเงินกองทุนและสินทรัพย์เสี่ยงด้านเครดิต ตามสายงานกิจการสาขา
                            </span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            6 รายงานสินเชื่อคงเหลือ และสินทรัพย์เสี่ยงด้านเครดิต แบ่งตามการจัดชั้นหนี้และหนี้ที่ไม่ก่อให้เกิดรายได้ (NPL)
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">6.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.6"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินเชื่อคงเหลือ และสินทรัพย์เสี่ยงด้านเครดิต แบ่งตามการจัดชั้นหนี้และหนี้ที่ไม่ก่อให้เกิดรายได้
                                (NPL) </span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            7 รายงานสินทรัพย์เสี่ยงด้านเครดิตแบ่งตามประเภทสินทรัพย์เปรียบเทียบเดือนปัจจุบัน และเดือนก่อนหน้า
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">7.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิตแบ่งตามประเภทสินทรัพย์ เปรียบเทียบเดือนปัจจุบัน และเดือนก่อนหน้า ตามวิธี SA </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">7.2 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิตสำหรับลูกหนี้ธุรกิจเอกชน แยกตามรายลูกค้า
                                (CIF) 20 รายแรก </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">7.3 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิตสำหรับลูกหนี้รายย่อย แยกตามประเภทสินเชื่อย่อย
                                (Market Code) 20 รายการแรก </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">7.4 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.7.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิตสำหรับสินทรัพย์อื่น แยกตามรายการทางบัญชี
                                (GL) 20 รายการแรก</span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            8 รายงานสินทรัพย์เสี่ยงด้านเครดิตตามกลุ่มลูกหนี้
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">8.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.8"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิตตามกลุ่มลูกหนี้ </span>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            9 รายงานการเปลี่ยนแปลงสินทรัพย์เสี่ยงด้านเครดิต
                        </div>
                    </td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">9.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.9"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานการเปลี่ยนแปลงสินทรัพย์เสี่ยงด้านเครดิต (เดือนปัจจุบัน
                                เทียบกับ เดือนก่อนหน้า) </span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            10 รายงานยอดสินเชื่อคงเหลือและสินทรัพย์เสี่ยงด้านเครดิต
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">10.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.10"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานยอดสินเชื่อคงเหลือและสินทรัพย์เสี่ยงด้านเครดิต (รายเดือน)</span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            11 รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามศูนย์ EVM
                        </div>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28">11.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.11"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานสินทรัพย์เสี่ยงด้านเครดิต ตามศูนย์ EVM (รายเดือน)
                            </span></a>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            12 รายงานเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
                        </div>
                    </td>
                </tr>
                <tr style='mso-height-source: userset;'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">12.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style11">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
                                (รายเดือน) </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.1_Gr" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">กราฟแสดงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาดย้อนหลัง 13 เดือน (รายเดือน) </span></a></td>
                </tr>
                <tr style='mso-height-source: userset;'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style11">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.1.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางแสดงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาดแยกตามวิธีการคำนวณ</span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">12.2&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.2" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">รายงานเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด (รายวัน)</span></a></td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.12.2.1" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางแสดงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาดแยกตามวิธีการคำนวณ </span></a></td>
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
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            13 รายงานเปรียบเทียบธุรกรรมที่ใช้ในการคำนวณเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด
                        </div>
                    </td>
                </tr>
                <tr style='mso-height-source: userset;'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">13.1 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style11">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานเปรียบเทียบธุรกรรมที่ใช้ในการคำนวณเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด (รายเดือน) </span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.2" target="_self">
                            <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางแสดงข้อมูลหลักทรัพย์ที่ต้องดำรงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดสูงสุด 10 อันดับแรก ตามประเภทความเสี่ยง  </span></a></td>
                </tr>
                <tr style='mso-height-source: userset;'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">13.2 &nbsp; </td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style11">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>รายงานเปรียบเทียบธุรกรรมที่ใช้ในการคำนวณเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดและสินทรัพย์เสี่ยงด้านตลาด (รายวัน)</span></a>
                    </td>
                </tr>
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td class="auto-style29" style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0;">&nbsp; </td>
                    <td colspan="2" style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0;">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CIR_5.13.4" 
                            target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">ตารางแสดงข้อมูลหลักทรัพย์ที่ต้องดำรงเงินกองทุนเพื่อรองรับความเสี่ยงด้านตลาดสูงสุด 10 อันดับแรก ตามประเภทความเสี่ยง </span></a></td>
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
                            target="_self"><span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">รายงานรายละเอียดเงินกองทุน</span></a></td>
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
                            <br>
                        </td>
                        <td style="width: 23pt" width="30"></td>
                    </tr>
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
