﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ScenarioWeight.aspx.vb" Inherits="GSBWeb.Scenario_Weight" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        th {
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="1200">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rows">
                <div class="col-md-8 col-md-offset-2">
                    <div class="panel panel-default">
                        <div class="NormalHeader">
                            Scenario Weight
                        </div>
                        <table width="100%">
                            <tr style="height: 50px;">
                                <td align="right">
                                    <font color="#FF0000">*</font>วันที่ของข้อมูล
                                </td>
                                <td align="center" width="10px">:</td>
                                <td align="left">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td align="left" width="100px">
                                                <asp:DropDownList ID="ddlTime" runat="server" Height="30px" Width="150px" CssClass="TextBoxRoundCorrner">
                                                </asp:DropDownList></td>
                                            <td align="left">&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btn_Searchr" runat="server" CssClass="btn btn-primary ButtonStyle">
                                                    <span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;&nbsp;ค้นหา
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="ClearDataInLine"></div>
                    <div class="NormalHeader" style="background-color: #FF388C; color: #FFFFFF;">
                        <table width="100%" border="0" id="tbScenarioWeightHeader" runat="server">
                            <tr valign="middle" style="height: 40px;">
                                <td align="left">
                                    <font color="#FFFFFF">Scenario Weight</font>
                                </td>
                                <td align="right">&nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary ButtonStyle" Width="100">
                                                    <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;&nbsp;เพิ่ม (Add)
                                                 </asp:LinkButton>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <asp:GridView ID="gvScenarioWeight" runat="server" AutoGenerateColumns="False" PageSize="10"
                                    AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px; border-color: Gray;"
                                    Width="100%" ShowHeaderWhenEmpty="True" SelectedRowStyle-BackColor="#CCCCCC" OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="รายการที่">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeq" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                <asp:Label ID="lbTimeId" Text='<%# Bind("TimeId") %>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lbScenarioId" Text='<%# Bind("ScenarioId") %>' runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อสถาณการณ์">
                                            <ItemTemplate>
                                                <asp:Label ID="lbScenarioName" Text='<%# Bind("ScenarioName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="น้ำหนัก (%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbWeight" Text='<%# Bind("Weight") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ปรับปรุงข้อมูล">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" Width="40px" Height="35px" CommandName="Edit" class="btn btn-primary btn-search;" ToolTip="ปรับปรุงข้อมูล"
                                                    CausesValidation="true" runat="server" Style="text-decoration: none;" CommandArgument='<%# Container.DisplayIndex %>'>
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;&nbsp;
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลบข้อมูล">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" Width="40px" Height="35px" CommandName="Delete" class="btn btn-danger" ToolTip="ลบข้อมูล"
                                                    CausesValidation="false" runat="server" Style="text-decoration: none">
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">
                                            ไม่พบข้อมูล
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                    <PagerStyle HorizontalAlign="Right" BorderColor="Gray" BorderWidth="1" />
                                    <EmptyDataRowStyle BorderColor="Gray" BorderWidth="1" />
                                    <PagerSettings PageButtonCount="10" NextPageText="ถัดไป" PreviousPageText="ก่อนหน้า" FirstPageText="First" LastPageText="Last" />
                                    <RowStyle Font-Size="Medium" BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                    <SelectedRowStyle BackColor="#CCCCCC" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <div class="ClearDataInLine"></div>
                </div>
            </div>

            <div class="modal" id="myModal" role="dialog" aria-labelledby="AlertBoxLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 50%;">
                    <asp:UpdatePanel ID="UpdModal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header NormalSubItems">
                                    <asp:Label ID="lblModalTitle" runat="server" Style="font-size: medium;" Text="Add/Edit" />
                                </div>
                                <div class="modal-body">
                                    <table width="100%" align="center" style="border: thin solid #EEE;margin: 0 0 0 0; padding: 0 0 0 0;" class="table table-hover table-striped footable" border="0">
                                        <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                            <td align="center">
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: right; font-size: medium">ชื่อสถาณการณ์&nbsp:&nbsp
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:DropDownList ID="ddlAddEditScenario" runat="server" Height="30px" CssClass="TextBoxRoundCorrner">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; font-size: medium">น้ำหนัก (%)&nbsp:&nbsp
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtAddEditWeight" runat="server" class="form-control" Style="margin-bottom: 2px; font-size: medium; text-align: left"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                <asp:Button ID="btnSaveAdd" OnClick="btnSaveAdd_Click" runat="server" Text="Save" CssClass="btn btn-primary ButtonStyle" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary ButtonStyle" data-dismiss="modal" aria-hidden="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSaveAdd" />
                            <%--                            <asp:AsyncPostBackTrigger ControlID="grvImportExcel" EventName="PageIndexChanging" />
                            <asp:PostBackTrigger ControlID="btnCancelImport" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="modal fade" id="AlertBox" role="dialog" aria-labelledby="AlertBoxLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 400px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header NormalSubItems">
                                    <asp:Label ID="lbl_Title" runat="server" Style="font-size: medium;" Text="    " />
                                </div>
                                <div class="modal-body">
                                    <table width="100%" align="center" style="border: thin solid #EEE; margin: 0 0 0 0; padding: 0 0 0 0;"
                                        class="table table-hover table-striped footable" border="0">
                                        <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                            <td id="imageBox" runat="server" align="center">
                                                <asp:Image ID="Symbol_Image" runat="server" ImageUrl="" Height="100px" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Messages" runat="server" Text="Sample" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_OK" runat="server" CssClass="btn btn-primary ButtonStyle" OnClientClick="btn_OK_Click" Text="ใช่" data-dismiss="modal" aria-hidden="true" data-keyboard="false" />
                                                &nbsp;&nbsp;&nbsp;
                                          <asp:Button ID="btn_NO" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ไม่ใช่" data-dismiss="modal" aria-hidden="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>