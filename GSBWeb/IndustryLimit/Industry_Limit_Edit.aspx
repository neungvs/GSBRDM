<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Industry_Limit_Edit.aspx.vb" Inherits="GSBWeb.Industry_Limit_Edit" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 204px;
        }
    </style>
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
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF">
                    ปรับปรุงข้อมูลเพดานเงินให้สินเชื่อ (Industry Limit)</div>
                <table align="left" width="100%">
                    <tr>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 60%" colspan="4">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="60%" colspan="4">
                            <asp:Panel ID="pnlEdit" runat="server" Width="100%" Visible="true">
                                <div class="panel panel-danger" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="4" style="text-align: left; vertical-align: top; text-align: left; font-weight: bold;
                                                font-size: medium;">
                                                <div class="NormalHeader" style="border: thin solid #C0C0C0;">
                                                    <asp:Label ID="lblNameType" runat="server" Text=""></asp:Label>
                                                </div>
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
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trISICCODE">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="lblSeq" runat="server" Text="" Style="font-size: medium"><span>ISICCODE : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="200px" style="text-align: left;">
                                                <asp:Label ID="lblISICCODE" runat="server" Style="font-size: medium" Text=""></asp:Label>
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trISICSUB">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1" width="200">
                                                <asp:Label ID="lblColumnName" runat="server" Text="" Style="font-size: medium" Width="200px">ISICCODESUBLEVEL : &nbsp;</asp:Label>
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                <asp:Label ID="lblISICCODESUBLEVEL" Style="font-size: medium" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trIndustry">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="Label1" runat="server" Text="" Style="vertical-align: top"><span>ภาคธุรกิจ : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="300px" colspan="2" style="text-align: left">
                                                <asp:Label ID="lblIndustry" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trLN_TYPE_CODE">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="Label2" runat="server" Text="" Style="vertical-align: top"><span>LN_TYPE_CODE : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="300px" colspan="2" style="text-align: left">
                                                <asp:Label ID="lblLN_TYPE_CODE" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trLN_SUB_TYPE">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="Label4" runat="server" Text="" Style="vertical-align: top"><span>LN_SUB_TYPE : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="300px" colspan="2" style="text-align: left">
                                                <asp:Label ID="lblLN_SUB_TYPE" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trMKT_CODE">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="Label8" runat="server" Text="" Style="vertical-align: top"><span>MKT_CODE : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="300px" colspan="2" style="text-align: left">
                                                <asp:Label ID="lblMKT_CODE" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trLoanType">
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; vertical-align: top; font-size: medium" class="style1"
                                                width="200">
                                                <asp:Label ID="Label10" runat="server" Text="" Style="vertical-align: top"><span>ประเภทสินเชื่อ : &nbsp;</span></asp:Label>
                                            </td>
                                            <td width="300px" colspan="2" style="text-align: left">
                                                <asp:Label ID="lblLoanType" runat="server" Style="font-size: larger" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="200" style="text-align: right" class="style1">
                                                <asp:HiddenField ID="hdfID" runat="server" />
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                <asp:HiddenField ID="HF_effectiveDate" runat="server" />
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: left; vertical-align: top; text-align: left; font-weight: bold;
                                                font-size: medium;">
                                                <div class="NormalHeader" style="border: thin solid #C0C0C0;">
                                                    <asp:Label ID="lblEditValue" runat="server" Text="เพดานเงินให้สินเชื่อ (Industry Limit)"> </asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td style="font-size: medium; text-align: right" class="style1">
                                                เปอร์เซ็น : &nbsp;
                                            </td>
                                            <td width="200px" style="text-align: left;">
                                                <asp:TextBox ID="txtIndustryLimitPercentage" AutoPostBack="true" OnBlur="addCommas(this)"
                                                    onfocus="removeCommas(this)" runat="server" class="form-control" Style="margin-bottom: 2px;
                                                    font-size: medium; text-align: right" Width="200px" autocomplete="off"></asp:TextBox>
                                            </td>
                                            <td width="50%" style="font-size: medium; text-align: left">
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
                                            <td width="200px" style="text-align: left">
                                                <asp:TextBox ID="txtIndustryLimitAmount" AutoPostBack="true" OnBlur="addCommas(this)"
                                                    onfocus="removeCommas(this)" runat="server" class="form-control" Style="text-align: right;
                                                    font-size: medium" Width="200px" autocomplete="off"></asp:TextBox>
                                            </td>
                                            <td width="50%" style="text-align: left; font-size: medium">
                                                &nbsp;ล้านบาท
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td colspan="3" style="text-align: center">
                                                <asp:UpdatePanel ID="pnlSubmit" runat="server" UpdateMode="Conditional" style="display: inline;">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="btnSave" Width="90px" class="btn btn-primary btn-search" ToolTip="บันทึกข้อมูล"
                                                            CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></span>&nbsp; บันทึก </asp:LinkButton>&nbsp;
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:LinkButton ID="btnCancle" Width="90px" class="btn btn-danger" ToolTip="ยกเลิก"
                                                    CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>&nbsp; ยกเลิก </asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" class="style1">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="pnlSubmit">
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
