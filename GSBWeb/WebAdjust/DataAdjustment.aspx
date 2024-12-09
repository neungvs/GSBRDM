<%@ Page Title="WebAdjust" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="DataAdjustment.aspx.vb" Inherits="GSBWeb.DataAdjustment" %>

<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-1 col-md-1">
                    &nbsp;
                </div>
                <div class="col-xs-10 col-md-10">
                    <table align="center" style="border: thin solid #C0C0C0; margin-left: 0px; margin-top: 0px;
                        margin-right: 0px; padding-top: 0;" width="100%" cssclass="table table-hover table-striped"
                        cssclass="footable">
                        <tr height="35px">
                            <td colspan="2"  style="background-color: #CCC;/*FF388C*/  text-align: left;" class="auto-style11">
                                <asp:Label ID="Label3" class="NormalHeader" runat="server" Text="การปรับปรุงข้อมูล (Adjustment)" Style=" 
                                     /*color: White*/height: 30px;"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #DDD;/**/ border-color: Black;">
                            <td style="text-align: left; border: thin solid #CCC; color: Black;">
                                <span lang="TH" class="style8">ระบบงาน </span><span>CBS</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <span lang="TH" font-size: medium; color: Black">จำนวน Reject</span>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE;/*FFCEDB*/">
                            <td style="text-align: left; border: thin solid #CCC; ">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp  <span ><span >&nbsp;ข้อมูลสัญญากู้ของสินเชื่อที่จัดเก็บบน
                                    CBS (LN70)</span></span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpLN_70" Enabled="true" runat="server" ToolTip="ดูรายละเอียด"
                                    NavigateUrl="~/WebAdjust/LN70_RejList.aspx" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #DDD/*F279AF*/">
                            <td style="text-align: left; border: thin solid #CCC/*C0C0C0*/;" class="style10">
                                <span lang="TH" class="style8">ระบบ </span><span class="style8">Trade Finance</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <span lang="TH" font-size: medium">จำนวน Reject</span>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลบัญชีเงินกู้ในธุรกิจ Trade Finance ที่มีอัตราดอกเบี้ยคงที่
                                    (Fixed) (TF_FIXED)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpTF_FIXED" Enabled="true" NavigateUrl="~/WebAdjust/TF_FIXED_RejList.aspx"
                                    ToolTip="ดูรายละเอียด" runat="server" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp;&nbsp;&nbsp; <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style10">&nbsp;ข้อมูลบัญชีเงินกู้ในธุรกิจ Trade Finance ที่มีอัตราดอกเบี้ยลอยตัว
                                    (Floating) (TF_FLOAT)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpTF_FLOAT" Enabled="true" runat="server" NavigateUrl="~/WebAdjust/TF_FLOAT_RejList.aspx"
                                    ToolTip="ดูรายละเอียด" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp;&nbsp;&nbsp; <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลบัญชีเงินกู้ในธุรกิจ Trade Finance ที่เป็น NPL
                                    (TF_NM)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpTF_NM" Enabled="true" runat="server" NavigateUrl="~/WebAdjust/TF_NM_RejList.aspx"
                                    ToolTip="ดูรายละเอียด" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #DDD">
                            <td style="text-align: left; border: thin solid #CCC;" class="style11">
                                ระบบ Investment
                            </td>
                            <td style="text-align: center; border: thin solid #CCC; font-size: medium;">
                                จำนวน Reject
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลยอดคงเหลือหลักทรัพย์ของธุรกรรม DF,Interbank
                                    ขาปล่อยกู้</span> <span class="style8">(IN51)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpIN51" ToolTip="ดูรายละเอียด" Enabled="true" NavigateUrl="~/WebAdjust/IN51_RejList.aspx"
                                    runat="server" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลยอดคงเหลือหลักทรัพย์ของธุรกรรม FI,EQ,Repo ขาปล่อยกู้
                                    (IN52)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpIN52" ToolTip="ดูรายละเอียด" Enabled="true" NavigateUrl="~/WebAdjust/IN52_RejList.aspx"
                                    runat="server" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลยอดคงเหลือหลักทรัพย์ของ Repo ขากู้ (WF52)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpWF52" ToolTip="ดูรายละเอียด" Enabled="true" runat="server" NavigateUrl="~/WebAdjust/WF52_RejList.aspx"
                                    Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลยอดคงเหลือสิ้นวันของธุรกรรม FX (FW53)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpFW53" ToolTip="ดูรายละเอียด" Enabled="true" runat="server" NavigateUrl="~/WebAdjust/FW53_RejList.aspx"
                                    Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลยอดคงเหลือหลักประกันของธุรกรรม Repo,SBL และข้อมูลหลักทรัพย์ที่ยืมและให้ยืม
                                    (CO52)</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpCO52" ToolTip="ดูรายละเอียด" Enabled="true" NavigateUrl="~/WebAdjust/CO52_RejList.aspx"
                                    runat="server" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color: #EEE">
                            <td style="text-align: left; border: thin solid #CCC;">
                                &nbsp&nbsp&nbsp <span class="glyphicon glyphicon-triangle-right" aria-hidden="true">
                                </span>&nbsp&nbsp<span class="style8">&nbsp;ข้อมูลยอดคงเหลือสิ้นวันธุรกรรมต่างประเทศ</span>
                            </td>
                            <td style="text-align: center; border: thin solid #CCC;">
                                <asp:HyperLink ID="hpSwap" ToolTip="ดูรายละเอียด" Enabled="true" NavigateUrl="~/WebAdjust/Swap_RejList.aspx"
                                    runat="server" Text="0" ForeColor="Blue"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-xs-1 col-md-1">
                    &nbsp;
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-md-1">
                    &nbsp;
                </div>
                <div class="col-xs-10 col-md-10">
                    &nbsp;
                </div>
                <div class="col-xs-1 col-md-1">
                    &nbsp;
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="../Images/LoaderRed.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
