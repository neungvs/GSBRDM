<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="IN52_RejEdit.aspx.vb" Inherits="GSBWeb.IN52_RejEdit" %>

<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        .style24
        {
            width: 10%;
            height: 20px;
        }
        .td-center
        {
            text-align:center;
            background:#FF388C;
            color: #FFFFFF";
  
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
                <div class="NormalHeader" style="/*text-align: left; font-weight: bold; font-size: medium;
                    background: #FF388C; padding-right: 5; color: #FFFFFF*/ height:30px;">
                    แก้ไขข้อมูล IN52</div>
                <table align="left" width="100%">
                    <%--   <tr>
            <td colspan="6" style="background-color: #FF388C; font-size: medium; font-weight: bold;
                text-align: left">
                <asp:Label ID="lblLN70" ForeColor="White" runat="server" Text="แก้ไขข้อมูล LN70"></asp:Label>
            </td>
        </tr>--%>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; font-size: x-small; width: 5%">
                            &nbsp;
                        </td>
                        <td align="right" style="font-weight: bold; white-space: nowrap; font-size: medium;
                            width: 20%">
                            <span>CIF_NUMBER</span> :
                        </td>
                        <td style="width: 15%; text-align: left">
                            &nbsp;
                            <asp:Label ID="lblCifNumber" runat="server" Text="Label" Style="font-size: medium;
                                white-space: nowrap;"></asp:Label>
                        </td>
                        <td align="right" class="style23" style="font-weight: bold; white-space: nowrap;
                            font-size: medium; width: 20%">
                            เลขที่บัญชี :
                        </td>
                        <td style="width: 25%; text-align: left">
                            &nbsp;
                            <asp:Label ID="lblAccno" runat="server" Text="Label" Style="font-size: medium; white-space: nowrap;"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td align="right" class="style21" style="font-weight: bold; font-size: x-small; width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td align="right" class="style21" style="font-weight: bold; font-size: x-small; width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            &nbsp;
                            <asp:Button ID="btnClose" CausesValidation="false" class="btn btn-primary ButtonGrayStyle" runat="server" Text="ยกเลิก"
                                Width="110px" />
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <%--    <tr>
            <td style="font-weight: bold; font-size: x-small; width: 10%">
                &nbsp;
            </td>
            <td align="right" style="font-weight: bold;white-space: nowrap; font-size: medium; width: 20%">
                ชื่อลูกค้า :
            </td>
            <td style="font-size: x-small; width: 20%; text-align: left">
                &nbsp;
                <asp:Label ID="lblName" runat="server" Text="Label" Style="font-size: medium;white-space: nowrap;"></asp:Label>
            </td>
            <td align="right" class="style23" style="font-weight: bold;white-space: nowrap; font-size: medium; width: 20%">
                นามสกุล :
            </td>
            <td class="style24" style="width: 20%;white-space: nowrap; text-align: left">
                &nbsp;
                <asp:Label ID="lblSurName" runat="server" Text="Label" Style="font-size: medium"></asp:Label>
            </td>
            <td style="width: 10%">
            &nbsp&nbsp
            </td>
        </tr>--%>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td align="right" class="style21" style="font-weight: bold; font-size: x-small; width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="90%" colspan="4">
                            <div class="panel panel-default" style="border: none">
                                <div class="ButtonGrayStyle" style="/*text-align: left; font-weight: bold; height: 35px;
                                    background: #FF388C; border-bottom: none; border-top-color: Gray; border-top-width: 1;*/
                                    color: #000;height:30px;vertical-align:middle;">
                                    รายการที่ถูก Reject</div>
                                <table width="100%" style="border-width: 1px; border-color: Gray">
                                    <tr width="100%">
                                        <td>
                                            <asp:GridView ID="gvIN52" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                AllowPaging="True" Style="border-width: 1px; border-color: Gray"
                                                Width="100%" ShowHeaderWhenEmpty="True" BackColor="White" 
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                GridLines="Vertical">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="เลือก">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton_Select" runat="server" ToolTip="แก้ไขข้อมูล" CommandName="Select"
                                                                CommandArgument='<%# Eval("DataRecordID")  %>' ImageUrl="~/images/pen.gif" CausesValidation="false" />
                                                            <asp:HiddenField ID="HF_DataRecordID" Value='<%# Bind("DataRecordID") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการที่">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSeq" runat="server" Text="<%#Container.DataItemIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" ชื่อรายการ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColumnName" Text='<%# Bind("ColumnName") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="HF_ColumnName" Value='<%# Bind("ColumnName") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ค่าที่ผิด">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColumnValue" Text='<%# Bind("ColumnValue") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ค่าที่ถูกต้อง">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColumnConrect" Text='<%# Bind("ColumnConrect") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่ปรับปรุงข้อมูล">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModifyDate" Text='<%# Bind("ModifyDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เหตุผลที่ Reject" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRejectReason" Text='<%# Bind("RejectReason") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center">
                                                        ไม่พบข้อมูล
                                                    </div>
                                                </EmptyDataTemplate>
                                                <PagerStyle  HorizontalAlign ="Center" BorderColor="Gray" BorderWidth="1" 
                                                    BackColor="#999999" ForeColor="Black"/>
                                                <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000084" Font-Size="Small" BorderColor="Gray" BorderWidth="1"
                                                    Height="35px" ForeColor="White" Font-Bold="True" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" BorderColor="Gray" BorderWidth="1" />
                                                <RowStyle Font-Size="Small" BackColor="#EEEEEE" BorderColor="Gray" 
                                                    BorderWidth="1" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr width="100%">
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="90%" colspan="4">
                            <asp:Panel ID="pnlEdit" runat="server" Width="100%" Visible="false">
                                <div class="panel panel-danger" width="100%">
                                    <div class="panel-heading" style="text-align: left; font-weight: bold; background-color: #F279AF;
                                        color: White">
                                        รายการที่แก้ไข</div>
                                    <table width="100%">
                                        <%--   <tr>
                <td colspan="4" style="background-color: #FF388C; font-size: medium; font-weight: bold;
                    text-align: left">
                    <asp:Label ID="Label1" ForeColor="White" runat="server" Text="รายการที่แก้ไข"></asp:Label>
                </td>
            </tr>--%>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="150px" style="text-align: right">
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
                                                &nbsp;
                                            </td>
                                            <td width="150px" style="text-align: right">
                                                <asp:Label ID="lblSeq" runat="server" Text="" Style="font-size: medium"><span>รายการที่</span> :&nbsp;</asp:Label>
                                            </td>
                                            <td width="200px" style="text-align: left;">
                                                <asp:TextBox ID="txtSeq" runat="server" Style="text-align: Center;" Width="60px"
                                                    class="form-control" disabled="disabled"></asp:TextBox>
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="150px" style="text-align: right">
                                                <asp:Label ID="lblColumnName" runat="server" Text="" Style="font-size: medium"><span>ชื่อรายการ</span> :&nbsp;</asp:Label>
                                            </td>
                                            <td width="335px" style="text-align: left">
                                                <asp:TextBox ID="txtColumnName" Width="335px" runat="server" class="form-control"
                                                    disabled="disabled" Style="background-color: #f3f3f3"></asp:TextBox>
                                                <asp:HiddenField ID="HF_ColumnValue" runat="server" />
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="150px" style="text-align: right">
                                                <asp:Label ID="lblValue" runat="server" Text="" Style="font-size: medium"><span>ค่าที่ผิด</span> :&nbsp;</asp:Label>
                                            </td>
                                            <td width="335px" style="text-align: left">
                                                <asp:TextBox ID="txtValue" Width="335px" runat="server" class="form-control" disabled="disabled"
                                                    Style="background-color: #f3f3f3"></asp:TextBox>
                                            </td>
                                            <td width="50%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;
                                            </td>
                                            <td width="150px" style="text-align: right; vertical-align: top">
                                                <asp:Label ID="lblEditValue" runat="server" Text="" Style="font-size: medium"><span>ค่าที่ถูกต้อง</span> :&nbsp;</asp:Label>
                                            </td>
                                            <td width="335px" style="text-align: left; vertical-align: top">
                                                <asp:TextBox ID="txtEditValue" Width="335px" MaxLength="8" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                                            </td>
                                            <td width="50%" style="text-align: left; vertical-align: top;" rowspan="2">
                                                &nbsp;
                                                <asp:RequiredFieldValidator ID="rfFormat" ForeColor="Red" Style="color: Red; display: list-item"
                                                    Display="Dynamic" runat="server" ControlToValidate="txtEditValue" ErrorMessage="กรุณากรอกข้อมุล"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rgLenght" ForeColor="Red" Enabled="false" Style="color: Red;
                                                    display: list-item" runat="server" ControlToValidate="txtEditValue" Display="Dynamic"
                                                    ErrorMessage="ข้อมูลต้องเป็น  8 หลัก" SetFocusOnError="True">
                                                    <asp:Label ID="lblrgEx" Text="ข้อมูลต้องเป็น  8 หลัก" runat="server" /><br />
                                                </asp:RegularExpressionValidator>&nbsp; <asp:RegularExpressionValidator ID="rgFormat" ForeColor="Red" Style="color: Red;
                                                    display: list-item" runat="server" ControlToValidate="txtEditValue" Enabled="false"
                                                    Display="Dynamic" ErrorMessage="ข้อมูลต้องเป็น  8 หลัก" SetFocusOnError="True"></asp:RegularExpressionValidator><asp:ValidationSummary
                                                        ID="ValidationSummary1" DisplayMode="List" ShowSummary="false" runat="server"
                                                        ForeColor="Red" Style="color: Red; margin: 0; padding: 0;" ShowMessageBox="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td>
                                            </td>
                                            <td width="200px" style="text-align: left;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp; </td><td width="150px" style="text-align: right">
                                            </td>
                                            <td width="200px" style="text-align: left">
                                                &nbsp; </td><td width="50%">
                                                &nbsp; </td></tr><tr>
                                            <td width="5%">
                                            </td>
                                            <td colspan="2" style="text-align: left; font-size: medium">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; หมายเหตุ
                                                : คำอธิบายรูปแบบข้อมูลสามารถดูได้โดย <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="false">คลิกที่นี่ </asp:LinkButton></td><td width="50%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td width="5%">
                            &nbsp; </td><td width="150px" style="text-align: right">
                        </td>
                        <td width="200px" style="text-align: left">
                            &nbsp; </td><td width="50%">
                            &nbsp; </td></tr><tr>
                        <td width="5%">
                        </td>
                        <td width="150px" colspan="2" style="text-align: right">
                            <asp:LinkButton ID="btnSave" class="btn btn-primary btn-search" ToolTip="บันทึกข้อมูล"
                                CausesValidation="true" runat="server" Style="text-decoration: none">
                        <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp; 
                        บันทึก   
                            </asp:LinkButton>&nbsp; <asp:LinkButton ID="btnCancle" class="btn btn-danger" ToolTip="ยกเลิกการแก้ไขข้อมูล"
                                CausesValidation="false" runat="server" Style="text-decoration: none">
                        <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp; 
                        ยกเลิก
                            </asp:LinkButton></td><td width="50%">
                            &nbsp; </td></tr><tr>
                        <td>
                            &nbsp; </td><td>
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel> 
            
            </td>
            <td width="5%">
                &nbsp; </td></tr></table></div><br /><asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" style="width: 800px">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header NormalHeader" style="/*background: #FF388C*/; color: Black; text-align: left;
                            font-weight: bold">
                            <asp:Label ID="lblModalTitle" Style="font-size: medium" runat="server" Text="sssssss"></asp:Label></div><div class="modal-body">
                            <table align="center" style="border: thin solid #C0C0C0; margin-left: 0px; margin-top: 0px;
                                margin-right: 0px; padding-top: 0;" width="100%" cssclass="table table-hover table-striped"
                                cssclass="footable">
                                <tr style="background: #CCC; color: Black; font-weight: bold">
                                    <td style="text-align: center; font-size: medium; border: thin solid #CCC;" width="250px">
                                        <span lang="TH">ชื่อรายการ</span> </td><td style="text-align: center; border: thin solid #CCC;">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">ชื่อฟิลด์</span> </td><td style="text-align: center; border: thin solid #CCC;">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">รูปแบบข้อมูล</span> </td></tr><tr style="background-color: #EEE">
                                    <td style="text-align: left; vertical-align: top; font-size: medium; border: thin solid #CCC;"
                                        width="200px">
                                        <span lang="TH">รหัสบัญชีของธนาคาร </span></td><td style="text-align: left; font-size: medium; vertical-align: top; border: thin solid #CCC;">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">GL_ACCOUNT_ID</span> </td><td style="text-align: left; font-size: medium; border: thin solid #CCC; vertical-align: top">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">ห้ามเป็นค่าว่าง </span></td></tr><tr style="background-color: #CCC">
                                    <td style="text-align: left; font-size: medium; vertical-align: top; border: thin solid #CCC;"
                                        width="200px">
                                        <span lang="TH">อัตราดอกเบี้ยปัจจุบันที่ลูกค้าจ่าย </span></td><td style="text-align: left; font-size: medium; vertical-align: top; border: thin solid #CCC;">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">CUR_GROSS_RATE</span> </td><td style="text-align: left; font-size: medium; border: thin solid #CCC; vertical-align: top">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">1. เป็นตัวเลข <br />2.  มีขนาดไม่เกิน 10 (รวมทศนิยม) </span></td></tr><tr style="background-color: #EEE">
                                    <td style="text-align: left; font-size: medium; vertical-align: top; border: thin solid #CCC;"
                                        width="200px">
                                        <span lang="TH">วันที่ครบกำหนดส่งมอบ</span> </td><td style="text-align: left; font-size: medium; vertical-align: top; border: thin solid #CCC;">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">SETTLEMENT_DATE</span> </td><td style="text-align: left; border: thin solid #CCC; vertical-align: top">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">1. เป็นตัวเลข 8 หลัก <br />2. อยู่ในรูปแบบ ววดดปปปป โดยที่ ปปปป คือ ปี ค.ศ. </span></td></tr><tr style="background-color: #CCC">
                                    <td style="text-align: left; border: thin solid #CCC; font-size: medium; vertical-align: top;"
                                        width="200px">
                                        <span lang="TH">วันที่เริ่มดำเนินการกับธนาคารของแต่ละบัญชี</span> </td><td style="text-align: left; vertical-align: top; border: thin solid #CCC;">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">ORIGINATION_DATE</span> </td><td style="text-align: left; border: thin solid #CCC; vertical-align: top">
                                        <span lang="TH" style="font-family: Tahoma; font-size: medium">1. เป็นตัวเลข 8 หลัก <br />2. อยู่ในรูปแบบ ววดดปปปป โดยที่ ปปปป คือ ปี ค.ศ.</span> </td></tr></table></div><div class="modal-footer" style="text-align: center">
                            <button class="btn btn-info ButtonGrayStyle" style="/*background: #FF388C*/" data-dismiss="modal" aria-hidden="true">
                                ออก</button></div></div></ContentTemplate></asp:UpdatePanel></div></div></asp:Content>