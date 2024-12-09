<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Industry_Limit_Add.aspx.vb" Inherits="GSBWeb.Industry_Limit_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>
    <style type="text/css">
        .style1
        {
            width: 204px;
        }
        .divoverflowIsiccode
        {
            overflow-y: auto;
            max-height: 80px;
            text-align: left;
            vertical-align: top;
            width: 230px;
        }
        .divoverflowLoanType
        {
            overflow-y: auto;
            max-height: 48px;
            text-align: left;
            vertical-align: top;
            width: 230px;
        }
        .style2
        {
            width: 10%;
            height: 23px;
        }
        .style3
        {
            width: 80%;
            height: 23px;
        }
    </style>
    <!-- Typeahead CSS -->
    <link href="<%= Page.ResolveClientUrl("~/Styles/Typeahead.css")%>" rel="stylesheet"
        type="text/css" />
    <!--Typehead JavaScript -->
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/bootstrap3-typeahead.min.js") %>"></script>
    <script type="text/javascript">

        function addCommas(clientID) {

            var nStr = document.getElementById(clientID.id).value;

            numcheck = /^[]?\d{1,18}(\.\d{1,4})?$/;

            if (numcheck.test(nStr) == false) {
                document.getElementById(clientID.id).value = "";
            }
            else {

                nStr += '';
                x = nStr.split('.');
                if (!x[0]) {
                    x[0] = "0";

                }
                x1 = x[0];
                if (!x[1]) {
                    x[1] = "00";
                }
                //check lenght
                else {
                    if (x[1].length == 1)
                        x[1] = x[1] + '0';
                }


                x2 = x.length > 1 ? '.' + x[1].substr(0, 2) : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + ',' + '$2');
                }

                document.getElementById(clientID.id).value = x1 + x2;

            }



            return true;
        }

        function removeCommas(clientID) {
            var nStr = document.getElementById(clientID.id).value;
            nStr = nStr.replace(/,/g, '');
            document.getElementById(clientID.id).value = nStr;

            $(clientID).select();

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    &nbsp;<uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="NormalHeader" style="/*text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF*/">
                    กำหนดข้อมูลเพดานเงินให้สินเชื่อ (Industry Limit)
                </div>
                <table align="left" width="100%">
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 80%" colspan="9">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="9%">
                            &nbsp;
                        </td>
                        <td width="40%" style="vertical-align: top">
                            <asp:Panel ID="pnlIssicType" runat="server" Width="100%">
                                <div class="panel panel-danger" width="100%" style="height: 260px">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%" class="NormalHeader">
                                                <asp:RadioButton ID="rdbIssicType" GroupName="TypeRadio" AutoPostBack="true" Text=""
                                                    runat="server" />
                                            </td>
                                            <td style="text-align: left" class="NormalHeader" class="style1" colspan="4">
                                                เพิ่มข้อมูลภาคธุรกิจ
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                                &nbsp;
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="Label6" runat="server" Text="" Style="font-size: medium"><span>ISICCODE : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="250px" style="text-align: left;">
                                                <asp:DropDownList ID="ddlISICCODE" Width="230px" class="form-control" AutoPostBack="true"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="Label7" runat="server" Text="" Style="font-size: medium" Width="200px">ISICCODESUBLEVEL : &nbsp;</asp:Label>
                                            </td>
                                            <td width="250px" style="text-align: left">
                                                <asp:DropDownList ID="ddlISICSUB" Width="230px" runat="server" AutoPostBack="true"
                                                    class="form-control">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr style="height: 80px">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="lblValue" runat="server" Text="" Style="vertical-align: top"><span>ภาคธุรกิจ : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="250px" colspan="1" style="vertical-align: top">
                                                <div class="divoverflowIsiccode">
                                                    <asp:Label ID="lblIndustry" AutoPostBack="true" runat="server" Style="font-size: larger"
                                                        Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                <asp:LinkButton ID="btnIsicTypeAdd" runat="server" CausesValidation="false" class="btn btn-danger"
                                                    Style="text-decoration: none; background: #FF388C;" ToolTip="เพิ่มข้อมูล" Width="100px"> เพิ่มข้อมูล <span 
                                                    aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                        <td width="2%">
                            &nbsp;
                        </td>
                        <td width="40%" style="vertical-align: top">
                            <asp:Panel ID="pnlLoanType" runat="server" Width="100%" Visible="true">
                                <div class="panel panel-danger" width="100%" style="height: 260px">
                                    <table width="100%">
                                        <tr>
                                            <td width="5%" class="NormalHeader">
                                                <asp:RadioButton ID="rdbLoanType" GroupName="TypeRadio" AutoPostBack="true" Text=""
                                                    runat="server" />
                                            </td>
                                            <td style="text-align: left" class="NormalHeader" class="style1" colspan="4">
                                                เพิ่มข้อมูลประเภทสินเชื่อ
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="Label1" runat="server" Text="" Style="font-size: medium"><span>LN_TYPE_CODE : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="200px" style="text-align: left;">
                                                <asp:DropDownList ID="ddlLNTYPECODE" Width="200px" class="form-control" AutoPostBack="true"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="Label3" runat="server" Text="" Style="font-size: medium" Width="200px">LN_SUB_TYPE : &nbsp;</asp:Label>
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                <asp:DropDownList ID="ddlLNSUBTYPE" Width="200px" runat="server" AutoPostBack="true"
                                                    class="form-control">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="Label4" runat="server" Text="" Style="font-size: medium" Width="200px">MKT_CODE : &nbsp;</asp:Label>
                                            </td>
                                            <td width="200px" colspan="1" style="text-align: left">
                                                <asp:DropDownList ID="ddlMKTCODE" Width="200px" runat="server" AutoPostBack="true"
                                                    class="form-control">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr style="height: 45px">
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="Label2" runat="server" Text="" Style="vertical-align: top"><span>ประเภทสินเชื่อ : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="300px" colspan="1" style="text-align: left; vertical-align: top">
                                                <div class="divoverflowLoanType">
                                                    <asp:Label ID="lblLoanType" AutoPostBack="true" runat="server" Style="font-size: larger"
                                                        Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                <asp:LinkButton ID="btnLoandTypeAdd" runat="server" CausesValidation="false" class="btn btn-danger"
                                                    Style="text-decoration: none; background: #FF388C;" ToolTip="เพิ่มข้อมูล" Width="100px"> เพิ่มข้อมูล <span 
                                                    aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                            </td>
                                            <td width="50px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                        <td width="9%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 80%" colspan="9">
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 80%" colspan="3">
                            <div class="panel panel-danger" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 80%" colspan="4">
                                            <div class="NormalHeader" style="border: thin solid #C0C0C0">
                                                เพดานเงินให้สินเชื่อ(Industry Limit)
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right; font-size: medium" class="style1">
                                            <asp:HiddenField ID="hfEffectiveDate" runat="server" />
                                        </td>
                                        <td width="150px" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td width="30%" style="text-align: left; font-size: medium">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trIsiccode">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right; vertical-align: top;" class="style1">
                                            ISICCODE : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; margin-right: 0; vertical-align: top;">
                                            <asp:Label ID="lblIsiccodeDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trIsicsub">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right; vertical-align: top;" class="style1">
                                            ISICCODESUBLEVEL : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; margin-right: 0; vertical-align: top;">
                                            <asp:Label ID="lblIsiccodesubDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trIndustry">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right; vertical-align: top;" class="style1">
                                            ภาคธุรกิจ : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; margin-right: 0; vertical-align: top;">
                                            <asp:Label ID="lblIndustryDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trLNTYPECODE">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right; vertical-align: top;" class="style1">
                                            LN_TYPE_CODE : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; margin-right: 0; vertical-align: top;">
                                            <asp:Label ID="lblLntypecodeDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trLNSUBTYPE">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right; vertical-align: top;" class="style1">
                                            LN_SUB_TYPE : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; margin-right: 0; vertical-align: top;">
                                            <asp:Label ID="lblLnsubtypeDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trMKT_CODE">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right; vertical-align: top;" class="style1">
                                            MKT_CODE : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; margin-right: 0; vertical-align: top;">
                                            <asp:Label ID="lblLnmktcodeDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trLoanType">
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; vertical-align: top; text-align: right;" class="style1">
                                            ประเภทสินเชื่อ : &nbsp;
                                        </td>
                                        <td width="150px" colspan="2" style="text-align: left; vertical-align: top; margin-right: 0;">
                                            <asp:Label ID="lblLoanTypeDetail" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                        </td>
                                        <%--<td width="30%" style="font-size:medium;text-align:left">
                                                &nbsp;
                                            </td>--%>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                        </td>
                                        <td style="font-size: medium; text-align: right" class="style1">
                                            เปอร์เซ็น : &nbsp;
                                        </td>
                                        <td width="150px" style="text-align: left; margin-right: 0;">
                                            <asp:TextBox ID="txtIndustryLimitPercentage" Width="100%" AutoPostBack="true" OnBlur="addCommas(this)"
                                                onfocus="removeCommas(this)" runat="server" class="form-control" Style="margin-bottom: 2px;
                                                font-size: medium; text-align: right" autocomplete="off"></asp:TextBox>
                                        </td>
                                        <td width="30%" style="font-size: medium; text-align: left">
                                            &nbsp;%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right; font-size: medium" class="style1">
                                            วงเงินอนุมัติ : &nbsp;
                                        </td>
                                        <td width="150px" style="text-align: left;">
                                            <asp:TextBox ID="txtIndustryLimitAmount" Width="100%" AutoPostBack="true" runat="server"
                                                OnBlur="addCommas(this)" onfocus="removeCommas(this)" class="form-control" Style="margin-bottom: 2px;
                                                text-align: right; font-size: medium" autocomplete="off"></asp:TextBox>
                                        </td>
                                        <td width="30%" style="text-align: left; font-size: medium">
                                            &nbsp;ล้านบาท
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right; font-size: medium" class="style1">
                                            &nbsp;
                                        </td>
                                        <td width="150px" style="text-align: left">
                                            <asp:HiddenField ID="hdfType" runat="server" />
                                        </td>
                                        <td width="30%" style="text-align: left; font-size: medium">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:LinkButton ID="btnSave" Width="90px" class="btn btn-primary btn-search" ToolTip="บันทึกข้อมูล"
                                                CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></span>&nbsp; บันทึก </asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="btnCancle" Width="90px" class="btn btn-danger" ToolTip="ยกเลิก"
                                                CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>&nbsp; ยกเลิก </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right; font-size: medium" class="style1">
                                            &nbsp;
                                        </td>
                                        <td width="150px" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td width="30%" style="text-align: left; font-size: medium">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
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
    <div class="modal fade" id="AlertBox" role="dialog" aria-labelledby="AlertBoxLabel"
        aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="width: 420px;">
            <asp:UpdatePanel ID="UpdModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header NormalSubItems">
                            <asp:Label ID="lbl_Title" runat="server" Style="font-size: medium;" Text="    " />
                        </div>
                        <div class="modal-body">
                            <table width="100%" align="center" style="border: thin solid #EEE;" class="table table-hover table-striped footable"
                                border="0" style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                    <td id="imageBox" runat="server" align="center">
                                        <asp:Image ID="Symbol_Image" runat="server" ImageUrl="" Height="100px" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Messages" runat="server" Text="Sample" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btn_OK" runat="server" CssClass="btn btn-primary ButtonStyle" OnClientClick="btn_OK_Click"
                                            Text="ใช่" data-dismiss="modal" aria-hidden="true" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_NO" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ไม่ใช่"
                                            data-dismiss="modal" aria-hidden="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
