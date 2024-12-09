<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ModifyIndustryLimit.aspx.vb" Inherits="GSBWeb.ModifyIndustryLimit" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
        
        
        
        .TextCenter
        {
            text-align: center;
        }
        
        
        .TextRight
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".datePic").datepicker($.datepicker.regional["th"]); // Set ภาษาที่เรานิยามไว้ด้านบน

        }


        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function Input_Eng(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 3585) {
                return false;
            } else {
                var iChars = "~`!#$%^&*+=-[]\\\';/{}|\":<>?^()";
                var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
                var ch = String.fromCharCode(charCode);
                if (iChars.indexOf(ch) == -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }




  


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="NormalHeader" style="/*text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF*/">
                    ปรับปรุงข้อมูลเพดานเงินให้สินเชื่อ (Industry Limit)</div>
                <table align="left" width="100%">
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td align="right" style="font-weight: bold; font-size: medium; white-space: nowrap;
                            width: 15%">
                            ISICCODE :&nbsp;
                        </td>
                        <td style="font-weight: bold; text-align: left; white-space: nowrap; width: 15%">
                            <asp:TextBox ID="txtisicCode" onkeyup="this.value=this.value.replace(/[^0-9a-zA-Z]/g'');"
                                onkeypress="return Input_Eng(event);" class="form-control" runat="server" Width="200px"
                                Enabled="true" autocomplete="off"></asp:TextBox>
                        </td>
                        <td align="right" style="font-weight: bold; font-size: medium; width: 15%">
                            ISICCODESUBLEVEL :&nbsp;
                        </td>
                        <td style="font-weight: bold; font-size: medium; width: 15%">
                            <asp:TextBox ID="txtisicSub" onkeyup="this.value=this.value.replace(/[^0-9a-zA-Z]/g'');"
                                onkeypress="return Input_Eng(event);" autocomplete="off" class="form-control"
                                runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 20%">
                            <asp:Button ID="btnSearch" CausesValidation="false" class="btn btn-primary" runat="server"
                                Text="ค้นหา" BackColor="#FF388C" Width="110px" />
                            &nbsp&nbsp
                            <asp:Button ID="btnCancle" class="btn btn-primary" CausesValidation="false" runat="server"
                                Text="ยกเลิก" BackColor="#FF388C" Width="110px" />
                        </td>
                        <td style="width: 10%">
                            <asp:HiddenField ID="HF_effectiveDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="text-align: right; width: 20%">
                            <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="false" class="btn btn-danger"
                                Style="text-decoration: none; background: #FF388C;" ToolTip="เพิ่มข้อมูล" Width="100px"> เพิ่มข้อมูล <span aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="80%" colspan="5">
                            <div class="panel panel-default" style="border: none">
                                <table width="100%" style="border-width: 1px; border-color: Gray">
                                    <tr width="100%">
                                        <td>
                                            <asp:GridView ID="gvLimit" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px; border-color: Gray;"
                                                Width="100%" ShowHeaderWhenEmpty="True" OnRowDeleting="OnRowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ISICCODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblISICCODE" runat="server" Text='<%# Bind("ISICCODE") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfID" Value='<%# Bind("ID") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ISICCODESUBLEVEL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblISICCODESUBLEVEL" Text='<%# Bind("ISICCODESUBLEVEL") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ภาคธุรกิจ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndustry" Text='<%# Bind("Industry") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ประเภทสินเชื่อ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoanType" Text='<%# Bind("LoanType") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IndustryLimitPercentage <br/> (%)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndustryLimitPercentage" Text='<%# Bind("IndustryLimitPercentage") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IndustryLimitAmount <br/> (ล้านบาท)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndustryLimitAmount" Text='<%# Bind("IndustryLimitAmount","{0:n2}") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                          <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LN_TYPE_CODE" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLN_TYPE_CODE" Text='<%# Bind("LN_TYPE_CODE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LN_SUB_TYPE" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLN_SUB_TYPE" Text='<%# Bind("LN_SUB_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LN_MKT_CODE" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLN_MKT_CODE" Text='<%# Bind("LN_MKT_CODE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblType" Text='<%# Bind("Type") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" BorderColor="Gray"
                                                            BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ปรับปรุงข้อมูล">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" Width="40px" Height="35px" CommandName="Select" class="btn btn-primary btn-search;"
                                                                ToolTip="ปรับปรุงข้อมูล" CausesValidation="false" runat="server" Style="text-decoration: none;"> <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp; </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" Width="125px"
                                                            CssClass="td-center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ลบข้อมูล">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" Width="40px" Height="35px" CommandName="Delete" class="btn btn-danger"
                                                                ToolTip="ลบข้อมูล" CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp; </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center">
                                                        ไม่พบข้อมูล
                                                    </div>
                                                </EmptyDataTemplate>
                                                <PagerStyle HorizontalAlign="Right" BorderColor="Gray" BorderWidth="1" />
                                                <EmptyDataRowStyle BorderColor="Gray" BorderWidth="1" />
                                                <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                                    Height="35px" ForeColor="White" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                                <RowStyle Font-Size="Medium" Height="40px" BackColor="#FFCEDB" BorderColor="Gray"
                                                    BorderWidth="1" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td width="80%" colspan="5">
                            <div class="panel panel-danger" width="100%" style="border-color: Gray">
                                <table width="100%">
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                        </td>
                                        <td width="80px" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td width="50px" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="130px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="5%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:Label ID="Label3" runat="server" Text="" Style="font-size: medium"><span>เห็นชอบ</span></asp:Label>
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:Label ID="Label4" runat="server" Text="" Style="font-size: medium"><span>อนุมัติ</span></asp:Label>
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="130px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="5%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                            <asp:Label ID="lblSeq" runat="server" Text="" Style="font-size: medium"><span>มติคณะจัดการ</span> :&nbsp;</asp:Label>
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:CheckBox ID="chkIsAgree1" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:CheckBox ID="chkIsApprove1" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td width="50px">
                                            <asp:Label ID="Label5" runat="server" Text="" Style="font-size: medium"><span>ครั้งที่ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtApproveID1" AutoPostBack="true" MaxLength="4" onpaste="return false"
                                                onkeypress="return NumberOnly(event);" Width="40px" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rftxtApproveID1" ForeColor="Red" Style="color: Red; display: list-item; font-size:medium"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtApproveID1" ErrorMessage="กรุณาระบุข้อมูลครั้งที่อนุมัติของมติคณะจัดการ">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td width="50px">
                                            <asp:Label ID="Label8" runat="server" Text="" Style="font-size: medium"><span>วันที่ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtApproveDate1" Width="90px" AutoPostBack="true" Style="background-color: #f3f3f3"
                                                onKeyPress="return false;" onKeyDown="return false" autocomplete="off" CssClass="datePic"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td width="130px">
                                            <asp:Label ID="Label11" runat="server" Text="" Style="font-size: medium"><span>วันที่มีผลบังคับใช้ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtEffectiveDate1" Width="90px" AutoPostBack="true" Style="background-color: #f3f3f3"
                                                onKeyPress="return false;" onKeyDown="return false" autocomplete="off" CssClass="datePic"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left;" width="3px">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Style="color: Red; display: list-item; font-size:medium"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtEffectiveDate1" 
                                                    ErrorMessage="กรุณาระบุวันที่มีผลบังคับใช้" Width="16px">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                            <asp:Label ID="Label1" Width="260px" runat="server" Text="มติคณะกรรมการบริหารความเสี่ยง : "
                                                Style="font-size: medium"></asp:Label>
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:CheckBox ID="chkIsAgree2" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:CheckBox ID="chkIsApprove2" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td width="50px">
                                            <asp:Label ID="Label2" runat="server" Text="" Style="font-size: medium"><span>ครั้งที่ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtApproveID2" AutoPostBack="true" Width="40px" onpaste="return false"
                                                onkeypress="return NumberOnly(event);" MaxLength="4" runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rftxtApproveID2" ForeColor="Red" Style="color: Red; display: list-item; font-size:medium"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtApproveID2" ErrorMessage="กรุณาระบุข้อมูลครั้งที่อนุมัติของมติคณะกรรมการบริหารความเสี่ยง">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td width="50px">
                                            <asp:Label ID="Label6" runat="server" Text="" Style="font-size: medium"><span>วันที่ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtApproveDate2" AutoPostBack="true" Width="90px" Style="background-color: #f3f3f3"
                                                onKeyPress="return false;" onKeyDown="return false" CssClass="datePic" autocomplete="off"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td width="130px">
                                            <asp:Label ID="Label7" runat="server" Text="" Style="font-size: medium"><span>วันที่มีผลบังคับใช้ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtEffectiveDate2" Width="90px" Style="background-color: #f3f3f3"
                                                AutoPostBack="true" onKeyPress="return false;" onKeyDown="return false" autocomplete="off"
                                                runat="server" CssClass="datePic"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left;" width="3px">
                                            <%--          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Style="color: Red; display: list-item; font-size:medium"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtEffectiveDate2" 
                                                    ErrorMessage="" Width="16px">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                            <asp:Label ID="Label9" runat="server" Text="" Style="font-size: medium"><span>มติคณะกรรมการธนาคาร</span> :&nbsp;</asp:Label>
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:CheckBox ID="chkIsAgree3" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:CheckBox ID="chkIsApprove3" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td width="50px">
                                            <asp:Label ID="Label10" runat="server" Text="" Style="font-size: medium"><span>ครั้งที่ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtApproveID3" AutoPostBack="true" Width="40px" onpaste="return false"
                                                onkeypress="return NumberOnly(event);" MaxLength="4" runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rftxtApproveID3" ForeColor="Red" Style="color: Red; display: list-item; font-size:medium"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtApproveID3" ErrorMessage="กรุณาระบุข้อมูลครั้งที่อนุมัติของมติคณะกรรมการธนาคาร">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td width="50px">
                                            <asp:Label ID="Label12" runat="server" Text="" Style="font-size: medium"><span>วันที่ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtApproveDate3" AutoPostBack="true" Width="90px" Style="background-color: #f3f3f3"
                                                onKeyPress="return false;" onKeyDown="return false" CssClass="datePic" autocomplete="off"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td width="130px">
                                            <asp:Label ID="Label13" runat="server" Text="" Style="font-size: medium"><span>วันที่มีผลบังคับใช้ </span></asp:Label>
                                        </td>
                                        <td width="100px" style="text-align: left">
                                            <asp:TextBox ID="txtEffectiveDate3" Width="90px" Style="background-color: #f3f3f3"
                                                onKeyPress="return false;" AutoPostBack="true" onKeyDown="return false" autocomplete="off"
                                                runat="server" CssClass="datePic"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left;" width="3px">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Style="color: Red; display: list-item; font-size:medium"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtEffectiveDate3" 
                                                    ErrorMessage="" Width="16px">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            &nbsp;
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowSummary="false"
                                                runat="server" ForeColor="Red" Style="color: Red; margin: 0; padding: 0;" ShowMessageBox="True" />
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="130px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="5%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center" colspan="11">
                                            <asp:UpdatePanel ID="pnlSubmit" runat="server" UpdateMode="Conditional" style="display: inline;">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="btnSave" Width="90px" class="btn btn-primary btn-search" ToolTip="บันทึกข้อมูล"
                                                        CausesValidation="true" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></span>&nbsp;
                                                            บันทึก </asp:LinkButton>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnClose" Width="90px" class="btn btn-danger" ToolTip="ยกเลิกการแก้ไขข้อมูล"
                                                        CausesValidation="false" runat="server" Style="text-decoration: none"> <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>&nbsp;
                                                            ยกเลิก </asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                        <td width="260px" style="text-align: right">
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            &nbsp;
                                        </td>
                                        <td width="80px" style="text-align: center">
                                            &nbsp;
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="50px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="130px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="5%">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
<%--            <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="pnlSubmit">
                <ProgressTemplate>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
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
