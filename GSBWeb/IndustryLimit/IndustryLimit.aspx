<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="IndustryLimit.aspx.vb" Inherits="GSBWeb.IndustryLimit" %>

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
        .td-center
        {
            text-align:center;
            background:#FF388C;
            color: #FFFFFF";
  
        }
        
        span.glyphicon-link 
        {
            font-size: 1.2em;
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
                <center>
                <div class="panel panel-default" style="width:80%;">
                    <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt;
                        background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px;">
                        รายการข้อมูลเพดานเงินให้สินเชื่อ (Industry Limit)
                    </div>
                    <div class="panel-body">
                        <table align="left" width="100%" border="0">
                            <tr>
                                <td style="width: 20%">
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

                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
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
                                <td style="width: 20%;" >
                                     &nbsp;
                                </td>

                                <td style="width: 20%;text-align:right;">
                                    <asp:LinkButton ID="btnAdd"  runat="server" CausesValidation="false" class="btn btn-danger"
                                        Style="text-decoration: none; background: #FF388C;" ToolTip="เพิ่มรายการ" Width="100px"> เพิ่มรายการ <span aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
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

                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>

                              <%-- <td width="20%">
                                    &nbsp;
                                </td>--%>
                                <td width="60%" colspan="6">
                                    <div class="panel panel-default" style="border: none">
                                        <table width="100%" style="border-width: 1px; border-color: Gray">
                                            <tr width="100%">
                                                <td>
                                                    <asp:GridView ID="gvLimit" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                        AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                                        Width="100%" ShowHeaderWhenEmpty="True"  OnRowDeleting="OnRowDeleting" OnRowDataBound = "OnRowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ลำดับที่">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                                                <ItemStyle HorizontalAlign="Center"  Width="50px" BorderColor="Gray" BorderWidth="1" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" วันที่มีผลบังคับใช้">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEffectiveDate" Text='<%# Bind("EffectiveDate") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" BorderColor="Gray" BorderWidth="1" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ปรับปรุงรายการ">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" Width="40px" Height="35px" CommandName="Select"  class="btn btn-primary btn-search;" ToolTip="ปรับปรุงรายการ"
                                                                        CausesValidation="true" runat="server" Style="text-decoration: none;">
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                                                <ItemStyle HorizontalAlign="Center"  Width="50px" BorderColor="Gray" BorderWidth="1" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลบรายการ">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" Width="40px" Height="35px" CommandName="Delete" class="btn btn-danger" ToolTip="ลบรายการ"
                                                                        CausesValidation="false" runat="server" Style="text-decoration: none">
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp; 
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="Gray" BorderWidth="1" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate >
                                                            <div align="center">
                                                                ไม่พบข้อมูล
                                                            </div>
                                                        </EmptyDataTemplate>   
                                                        <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                                            Height="35px" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                                        <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                                        <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                                        <PagerSettings  PageButtonCount="10" NextPageText="ถัดไป"  PreviousPageText = "ก่อนหน้า"  FirstPageText="First" LastPageText="Last"/>
                                                        <RowStyle Font-Size="Medium" Height="40px"   BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td width="20%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                </center>
            </div>
            
         <%--   <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
