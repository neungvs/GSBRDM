<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SettingReceiveMailReport_2_5.aspx.vb" Inherits="GSBWeb.SettingReceiveMailReport_2_5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <%@ register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
      <style type="text/css">
        .style21
        {
            width: 211px;
        }
        .style23
        {
            width: 130px;
        }
        .auto-style9
        {
            width: 77px;
        }
        .td-center
        {
            text-align: center;
            background: #FF388C;
            color: #FFFFFF;
        }
        
        .btn-primary
        {
        }
    </style>
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div class="rows">
                <div class="col-md-8 col-md-offset-2">
                    <div class="panel panel-default">
                        <div class="NormalHeader">
                            กำหนดผู้รับ Email SLL
                        </div>
                        <div class="ClearDataInLine">
                        </div>
                        <div class="form-group">
                            <table width="100%">
                                <tr style="height: 50px;">
                                    <td style="font-size: medium; text-align: right" class="style1">
                                        <font color="#FF0000">*</font>Email
                                    </td>
                                    <td align="center" width="10px">
                                        :
                                    </td>
                                    <td align="left">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="left" width="250px">
                                                    <asp:TextBox ID="txtEmail" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnSave" Width="90px" class="btn btn-primary btn-search" ToolTip="บันทึกข้อมูล"
                                                        CausesValidation="true" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></span>&nbsp; บันทึก </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnCancle" Width="90px" class="btn btn-danger" ToolTip="ยกเลิก"
                                                        CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>&nbsp; ยกเลิก </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign="middle" height="6px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="3" align="left">
                                       <%-- <asp:RegularExpressionValidator ID="rgetxtEmail" CssClass="help-block" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="กรอก Email ไม่ถูกต้อง" SetFocusOnError="True"
                                            Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">กรอก Email ไม่ถูกต้อง</asp:RegularExpressionValidator>--%>
                                        <asp:RequiredFieldValidator ID="reqtxtEmail" runat="server" CssClass="help-block"
                                            ControlToValidate="txtEmail" ErrorMessage="Email ห้ามเป็นค่าว่าง" SetFocusOnError="True"
                                            Display="None" />
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="help-block" runat="server"
                                            DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr valign="middle" height="6px">
                                    <td colspan="3" align="left">
                                        <div style="width: 15px; display: inline;">
                                            <asp:HiddenField ID="hdEmail" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="ClearDataInLine">
                    </div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <asp:GridView ID="gvEmail" runat="server" AutoGenerateColumns="False" PageSize="10"
                                    AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px; border-color: Gray;"
                                    Width="100%" ShowHeaderWhenEmpty="True" OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับที่">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center" Width="20px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" Text='<%# Bind("Email") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" CssClass="td-left" />
                                            <ItemStyle HorizontalAlign="Left" Width="400px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText=" ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" Text='<%# Bind("ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" CssClass="td-left" />
                                            <ItemStyle HorizontalAlign="Left" Width="400px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="ปรับปรุงรายการ">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" Width="40px" Height="35px" CommandName="Select" class="btn btn-primary btn-search;"
                                                    ToolTip="ปรับปรุงรายการ" CausesValidation="false" runat="server" Style="text-decoration: none;">
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center" Width="40px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลบรายการ">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" Width="40px" Height="35px" CommandName="Delete" class="btn btn-danger"
                                                    ToolTip="ลบรายการ" CausesValidation="false" runat="server" Style="text-decoration: none">
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp; 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center" Width="40px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">
                                            ไม่พบข้อมูล
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                        Height="35px" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                    <PagerStyle HorizontalAlign="Right" BorderColor="Gray" BorderWidth="1" />
                                    <EmptyDataRowStyle BorderColor="Gray" BorderWidth="1" />
                                    <PagerSettings PageButtonCount="10" NextPageText="ถัดไป" PreviousPageText="ก่อนหน้า"
                                        FirstPageText="First" LastPageText="Last" />
                                    <RowStyle Font-Size="Medium" Height="40px" BackColor="#FFCEDB" BorderColor="Gray"
                                        BorderWidth="1" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <div class="ClearDataInLine">
                    </div>
                </div>
            </div>
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
                                                <asp:Button ID="btn_OK" runat="server" CausesValidation="false" CssClass="btn btn-primary ButtonStyle"
                                                    OnClientClick="btn_OK_Click" Text="ใช่" data-dismiss="modal" aria-hidden="true" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btn_NO" CausesValidation="false" runat="server" CssClass="btn btn-primary ButtonStyle"
                                                    Text="ไม่ใช่" data-dismiss="modal" aria-hidden="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div runat="server" id="div_Panel" style="display: none">
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div class="modal_Pross" style="z-index: 1500;">
                                <div class="center_Pross" style="margin-top: 100px;">
                                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <script type="text/javascript">
                        function extendedValidatorUpdateDisplay(obj) {
                            // Appelle la méthode originale
                            if (typeof originalValidatorUpdateDisplay === "function") {
                                originalValidatorUpdateDisplay(obj);
                            }
                            // Récupère l'état du control (valide ou invalide) 
                            // et ajoute ou enlève la classe has-error
                            var control = document.getElementById(obj.controltovalidate);
                            if (control) {
                                var isValid = true;
                                for (var i = 0; i < control.Validators.length; i += 1) {
                                    if (!control.Validators[i].isvalid) {
                                        isValid = false;
                                        break;
                                    }
                                }
                                if (isValid) {
                                    $(control).closest(".form-group").removeClass("has-error");
                                } else {
                                    $(control).closest(".form-group").addClass("has-error");
                                }
                            }
                        }
                        // Remplace la méthode ValidatorUpdateDisplay
                        var originalValidatorUpdateDisplay = window.ValidatorUpdateDisplay;
                        window.ValidatorUpdateDisplay = extendedValidatorUpdateDisplay;
                    </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
