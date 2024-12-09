<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CPM_Report.aspx.vb" Inherits="GSBWeb.CPM_Report" %>
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
                        <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt;
                            background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px;">
                            รายงานสำหรับการบริหารพอร์ตสินเชื่อ (Credit Portfolio Management: CPM)
                        </div>
                        <div class="panel-body">
                             <asp:Panel ID="pnlReport" runat="server" />
                        </div>
<%--                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: large;
                                background: #FF99FF; padding-right: 5;">
                                Header 1
                            </div>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10; text-decoration: none;"><i class="fa fa-caret-right"></i>First item</a>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;"><i class="fa fa-caret-right"></i>Second item</a>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;"><i class="fa fa-caret-right"></i>Third item</a                                     
                        </div>--%>
<%--                        <div class="panel-footer" style="text-align: left; font-weight: bold; font-size: medium;
                            background: #CCCCCC; padding-right: 5; color: #000000; ">
                            หมายเหตุ: ในกรณีที่ต้องการ Zoom ขยายข้อมูลรายงานตารางหรือกราฟ สามารถดำเนินการได้ผ่านการ Export รายงานในรูปแบบ Excel, PDF, TIFF, Word
                        </div>--%>
                    </div>



                </div>

            </div>




<%--            <table width="70%" align="center">

                <tr style="width: 100%">
                    <td colspan="2" class="auto-style23">
                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF;">
                            1 รายงานที่ 1.1 รายงานยอดคงค้างสินเชื่อ เงินลงทุน และภาระผูกพันของธนาคาร
                        </div>
                    </td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.1.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.1.1 รายงานยอดคงค้างสินเชื่อ เงินลงทุน และภาระผูกพันของธนาคาร </span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.1.1_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.1.1 แสดงยอดคงค้างสินเชื่อและเงินลงทุน ของธนาคาร</span>
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
                            2 รายงานที่ 1.2 รายงานผลการดำเนินงานสินเชื่อ(รายเดือน)
                        </div>
                    </td>
                </tr>

                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.1 รายงานผลการดำเนินงานสินเชื่อ</span>
                        </a>
                    </td>
                </tr>


                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.2 รายงานสินเชื่อคงเหลือ</span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.2_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.2 กราฟสรุปรายงานสินเชื่อคงเหลือ</span>
                        </a>
                    </td>
                </tr>

                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.3 รายงานลูกหนี้ที่ไม่ก่อให้เกิดรายได้</span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.3_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.3 กราฟสรุปลูกหนี้ที่ไม่ก่อให้เกิดรายได้ (NPLs)</span>
                        </a>
                    </td>
                </tr>

                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.2.4 รายงานลูกหนี้จัดชั้นกล่าวถึงเป็นพิเศษ (SM)</span>
                        </a>
                    </td>
                </tr>

                     <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.4.1_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.4.1 กราฟสรุปลูกหนี้จัดชั้นกล่าวถึงเป็นพิเศษ (SM)</span>
                        </a>
                    </td>
                </tr>
 
                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.1_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.1 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน (สินเชี่อคงเหลือ)</span>
                        </a>
                    </td>
                </tr>

                
                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.2_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.2 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน (ลูกหนี้ไม่ก่อให้เกิดรายได้)</span>
                        </a>
                    </td>
                </tr>
                 
                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.3_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.3 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน  (ลูกหนี้จัดชั้นกล่าวถึงเป็นพิเศษ (SM))</span>
                        </a>
                    </td>
                </tr>
            
                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.4_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.4 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน แบ่งตามประเภทสินเชื่อ</span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.5_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.5 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน แบ่งตามประเภทสินเชื่อ (SM)</span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.6_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.6 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน แบ่งตามประเภทสินเชื่อ (NPLs)</span>
                        </a>
                    </td>
                </tr>

                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.7_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.7 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน แบ่งตามประเภทสินเชื่อ (อัตรา SM %) </span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.2.5.8_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.2.5.8 กราฟแสดงแนวโน้มย้อนหลัง 12 เดือน แบ่งตามประเภทสินเชื่อ (อัตรา NPL %) </span>
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
                            3 รายงานที่ 1.3 รายงานการจัดชั้นหนี้ และรายงานสำรองค่าเผื่อหนี้สงสัยจะสูญ</div>
                    </td>
                </tr>


                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.3.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.3.1 ตารางแสดงข้อมูลจัดชั้นหนี้ หนี้ที่ไม่ก่อให้เกิดรายได้ (NPLs) และค่าเผื่อหนี้สงสัยจะสูญ </span>
                        </a>
                    </td>
                </tr>


                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.3.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.3.2  ตารางแสดงรายงานสำรองค่าเผื่อหนี้สงสัยจะสูญแยกตามการจัดชั้นของประเภทสินเชื่อ </span>
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
                            4 รายงานที่ 1.4 รายงานการเคลื่อนย้ายชั้นหนี้ (รายเดือน)</div>
                    </td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.4.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.4.1 ตารางการเคลื่อนย้ายชั้นหนี้ (สินเชื่อคงเหลือ) </span>
                        </a>
                    </td>
                </tr>


                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.4.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.4.2 ตารางการเคลื่อนย้ายชั้นหนี้ (% สินเชื่อคงเหลือ) </span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.4.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.4.3 ตารางการเคลื่อนย้ายชั้นหนี้ (จำนวนบัญชี) </span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.4.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.4.4 ตารางการเคลื่อนย้ายชั้นหนี้ (% จำนวนบัญชี) </span>
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
                            5 รายงานที่ 1.5 รายงานสินเชื่อ TDR และ Re-Entry NPL (รายเดือน)</div>
                    </td>
                </tr>

                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.5.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.5.1 แสดงลูกหนี้ปรับปรุงโครงสร้างหนี้ (TDR) และ Re-Entry NPLs </span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.5.1_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>กราฟที่ 1.5.1 แสดงลูกหนี้ปรับปรุงโครงสร้างหนี้ (TDR) และ Re-Entry NPLs</span>
                        </a>
                    </td>
                </tr>

                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.5.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.5.2 แสดงลูกหนี้ปรับปรุงโครงสร้างหนี้ (TDR)</span>
                        </a>
                    </td>
                </tr>

                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.5.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.5.3 แสดง Re-Entry NPLs ของลูกหนี้ TDR</span>
                        </a>
                    </td>
                </tr>

                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.5.2_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'> กราฟที่ 1.5.2 แสดงแนวโน้มลูกหนี้ปรับปรุงโครงสร้างหนี้ (TDR) และ Re-Entry NPLs 
                        ย้อนหลัง 12 เดือน</span>
                        </a>
                    </td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.5.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'> ตารางที่ 1.5.4 รายงานสรุปข้อมูลลูกหนี้ปรับปรุงโครงสร้างหนี้ (TDR) 
                        แบ่งตามจำนวนครั้งที่ทำ TDR ในแต่ละบัญชี</span>
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
                            6 รายงานที่ 1.6 รายงานลูกหนี้กู้สินเชื่อมากกว่า 1 ประเภท
                        </div>
                    </td>
                </tr>

                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.6.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'> ตารางที่ 1.6.1 แสดงลูกหนี้กู้สินเชื่อมากกว่า 1 ประเภท หรือ 1 บัญชี</span>
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
                            7 รายงานที่ 1.7 รายงานสินเชื่อเพื่อพัฒนากลุ่มอาชีพ – กลุ่ม ช.พ.ค. ช.พ.ส. 
                            และเกื้อกูลฯ (SM)
                        </div>
                    </td>
                </tr>




                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>ตารางที่ 1.7.1 รายงานการจ่ายเงินสนับสนุนพิเศษ และหักหนี้ค้างชำระ สินเชื่อเพื่อพัฒนากลุ่มอาชีพโครงการ 
                        ช.พ.ค. ช.พ.ส. และเกื้อกูล</span>
                        </a>
                    </td>
                </tr>



                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.1.1_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        กราฟที่ 1.7.1.1 แสดงการจ่ายเงินสนับสนุนพิเศษ และหักหนี้ค้างชำระ สินเชื่อเพื่อพัฒนากลุ่มอาชีพโครงการ 
                        ช.พ.ค ช.พ.ส และเกื้อกูลฯ</span></a></td>
                </tr>


                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.1.2_Gr"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        กราฟที่ 1.7.1.2 แสดงการจ่ายเงินสนับสนุนพิเศษ และหักหนี้ค้างชำระ สินเชื่อเพื่อพัฒนากลุ่มอาชีพโครงการ 
                        ช.พ.ค ช.พ.ส และเกื้อกูลฯ (สัดส่วนจำนวนเงินงวดค้างต่อจำนวนเงินสนับสนุน)</span></a></td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
               

                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.2" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.7.2 รายงานสินเชื่อเพื่อพัฒนากลุ่มอาชีพโครงการ ช.พ.ค. ช.พ.ส. 
                        และเกื้อกูล แบ่งตามประวัติการหักชำระหนี้แทน</span></a></td>
                </tr>


                  <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                      <td class="auto-style28" 
                          style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                      </td>
                      <td class="auto-style25" 
                          style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                          <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.3" 
                              target="_self">
                          <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                          ตารางที่ 1.7.3 รายงานแสดงสินเชื่อพัฒนากลุ่มอาชีพโครงการ ช.พ.ค. ช.พ.ส. 
                          และเกื้อกูลฯ แบ่งตามจำนวนครั้งที่เคยนำเงินสนับสนุนฯหักชำระหนี้แทน (จำนวนบัญชี)</span></a></td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.4" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.7.4 รายงานสินเชื่อพัฒนากลุ่มอาชีพโครงการ ช.พ.ค ช.พ.ส และเกื้อกูลฯ 
                        แบ่งตามจำนวนครั้งที่เคยนำเงินสนับสนุนฯหักชำระหนี้แทน (สินเชื่อคงเหลือ)</span></a></td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.7.2_Gr" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        กราฟที่ 1.7.2 แสดงรายงานสินเชื่อพัฒนากลุ่มอาชีพโครงการ ช.พ.ค ช.พ.ส และเกื้อกูลฯ 
                        แบ่งตามจำนวนครั้งที่เคยนำเงินสนับสนุนฯ หักชำระหนี้แทน</span></a></td>
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
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" 
                            style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            8 รายงานที่ 1.8 รายงานสรุปสินเชื่อรายใหม่ (รายเดือน)</div>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.8.1" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.8.1 
                        รายงานเงินให้สินเชื่อลูกหนี้รายใหม่ที่อนุมัติแบ่งตามประเภทสินเชื่อ</span></a></td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.8.2" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.8.2 รายงานสรุปสินเชื่อรายใหม่ 20 อันดับแรก (Ad-hoc)</span> </a>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.8.3" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.8.3 รายงานเงินให้สินเชื่อลูกหนี้รายใหม่ที่อนุมัติในปี</span></a></td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.8.4" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.8.4 รายงานเงินให้สินเชื่อลูกหนี้รายใหม่ แบ่งตามอับดับ Rating</span></a></td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.8.5" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.8.5 รายงานเงินให้สินเชื่อลูกหนี้รายใหม่ แบ่งตามอับดับ Scoring</span>
                        </a>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.8.6" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.8.6 รายงานเงินให้สินเชื่อลูกหนี้รายใหม่ แบ่งตาม Industry</span></a></td>
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
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" 
                            style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            9 รายงานที่ 1.9 รายงานลูกหนี้กู้สินเชื่อ 20 รายแรก (Ad-hoc)</div>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.9.1" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.9.1 รายงานลูกหนี้กู้สินเชื่อ 20 รายแรก ตามประเภทสินเชื่อ</span></a></td>
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
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" 
                            style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            10 รายงานที่ 1.10 รายงาน Product Portfolio (รายเดือน) (Ad-hoc)</div>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.10.1" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.10.1 รายงาน Product Portfolio</span></a></td>
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
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" 
                            style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            11 รายงานที่ 1.11 รายงาน Bucket Portfolio (รายเดือน) (Ad-hoc)</div>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.11.1" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.11.1 รายงาน Bucket Portfolio</span></a></td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.11.1_Gr" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        กราฟที่ 1.11.1 กราฟ Bucket Portfolio</span></a></td>
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
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" 
                            style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            12 รายงานที่ 1.12 รายงาน Vintage Analysls (รายเดือน) (Ad-hoc)</div>
                    </td>
                </tr>
                <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                    <td class="auto-style28" 
                        style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                    </td>
                    <td class="auto-style25" 
                        style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_1.12.1" 
                            target="_self">
                        <span style="color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;">
                        ตารางที่ 1.12.1 รายงาน Vintage Analysls</span></a></td>
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
                <tr style="width: 100%">
                    <td colspan="2">
                        <div class="panel-heading" 
                            style="text-align: left; font-weight: bold; font-size: medium; background-color: #F279AF; padding-right: 5; color: #FFFFFF">
                            13 รายงานการกระจุกตัวสินเชื่อ</div>
                    </td>
                </tr>
                </tr>


                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_2.1"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        ตารางที่ 2.1 แสดงเพดานเงินให้สินเชื่อในแต่ละภาคธุรกิจ</span></a></td>
                </tr>


                 <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_2.2"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        ตารางที่ 2.2 แสดงอัตราส่วนการกำกับลูกหนี้รายใหญ่ตามกลุ่มลูกหนี้</span></a></td>
                </tr>

      
                   
                  <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_2.3"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        ตารางที่ 2.3 แสดงการกระจุกตัวสินเชื่อรายภาคธุรกิจ (Industry Limit) 
                        เปรียบเทียบเพดานเงินให้สินเชื่อ</span></a></td>
                </tr>


                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_2.4"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        ตารางที่ 2.4 ข้อมูลสินเชื่อแบ่งตามประเภทธุรกิจ</span></a></td>
                </tr>

                <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_2.5"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        ตารางที่ 2.5 แสดงอัตราส่วนการกำกับลูกหนี้รายใหญ่</span></a></td>
                </tr>


                   <tr height="29" style='mso-height-source: userset; height: 21.95pt'>
                    <td style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; " class="auto-style28"></td>
                    <td style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0"
                        class="auto-style25">
                        <a href="../ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_2.6"
                            target="_self"><span style='color: #595959; font-size: 10.0pt; text-decoration: none; font-size: medium;'>
                        ตารางที่ 2.6 แสดงรายละเอียดสินเชื่อ เงินลงทุน หรือการก่อภาระผูกพัน</span></a></td>
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
                      <tr height="29" style="mso-height-source: userset; height: 21.95pt">
                        <td class="auto-style28" 
                            style="vertical-align: bottom; text-align: right; font-size: medium; margin: 0; padding: 0; ">
                        </td>
                        <td class="auto-style25" 
                            style="vertical-align: bottom; text-align: left; font-size: larger; margin: 0; padding: 0">
                            หมายเหตุ: ในกรณีที่ต้องการ Zoom ขยายข้อมูลรายงานตารางหรือกราฟ 
                            สามารถดำเนินการได้ผ่านการ Export รายงานในรูปแบบ Excel, PDF, TIFF, Word<br />
                        </td>
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
