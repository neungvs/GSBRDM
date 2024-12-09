﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MacroEconomicFactor.aspx.vb" Inherits="GSBWeb.MacroEconomicFactor" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        th {
            text-align: center;
        }
    </style>

    <script>
        // JavaScript function to enable the button when a file is selected
        function toggleButtonState(fileInput) {
            var button = document.getElementById('<%= btnUpload.ClientID %>');
            button.disabled = !fileInput.value;
        }
    </script>
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
                            Macro Economic Factor
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
                        <table width="100%" border="0" id="tbImportMevHeader" runat="server">
                            <tr valign="middle" style="height: 40px;">
                                <td align="left">
                                    <font color="#FFFFFF">Macro Economic Factor</font>
                                </td>
                                <td align="right">&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btn_Delete_Group" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false">
                                        <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btn_Save" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false">
                                         <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;&nbsp;บันทึก
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btn_Cancel_Group" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false">
                                        <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <table width="100%" id="tbFactorHeader" runat="server">
                                    <tr style="height: 50px;">
                                        <td align="right">
                                            <font color="#FF0000">*</font>Factor
                                        </td>
                                        <td align="center" width="10px">:</td>
                                        <td align="left">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td align="left" width="100px">
                                                        <asp:DropDownList ID="ddlFactor" runat="server" Height="30px" CssClass="TextBoxRoundCorrner">
                                                        </asp:DropDownList></td>
                                                    <td align="left">&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnSearchByFactor" runat="server" CssClass="btn btn-primary ButtonStyle">
                                                    <span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;&nbsp;ค้นหา
                                                </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="btnDownloadTemplate" runat="server" CssClass="btn btn-primary ButtonStyle" Width="200">
                                                    <span class="glyphicon glyphicon-download"></span>&nbsp;&nbsp;&nbsp;Download Template
                                                 </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="btn_Excel_Import" runat="server" CssClass="btn btn-primary ButtonStyle" Width="200">
                                                    <span class="glyphicon glyphicon-import"></span>&nbsp;&nbsp;&nbsp;นำเข้าข้อมูลจาก Excel
                                                 </asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary ButtonStyle" Width="100">
                                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;&nbsp;เพิ่ม (Add)
                                                 </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tbFactorDetail" runat ="server">
                                    <tr>
                                        <td align="right">ชื่อ Factor&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
                                        <td align="left">
                                            <asp:Label ID="lbHeaderFactorName" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right">หน่วย&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
                                        <td align="left">
                                            <asp:Label ID="lbHeaderFactorUnit" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right">รายละเอียด&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
                                        <td align="left">
                                            <asp:Label ID="lbHeaderFactorDesc" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvMacroEconomicFactor" runat="server" AutoGenerateColumns="False" PageSize="10"
                                    AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px; border-color: Gray;"
                                    Width="100%" ShowHeaderWhenEmpty="True" SelectedRowStyle-BackColor="#CCCCCC"  OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="รายการที่">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeq" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                <asp:Label ID="lbTimeId" Text='<%# Bind("TimeId") %>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lbFactorId" Text='<%# Bind("FactorId") %>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lbStressMonth" Text='<%# Bind("StressMonth") %>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lbStressYear" Text='<%# Bind("StressYear")  %>' runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center ChangeHeaderforMarketReport1" Width="10%" VerticalAlign="Middle" Font-Size="Medium" Height="20px" />
                                            <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" Height="20px" Font-Size="Small" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ปีที่ทดสอบภาวะวิกฤต">
                                            <ItemTemplate>
                                                <asp:Label ID="lbYear" runat="server"  Text='<%# Bind("StressYear") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="เดือน">
                                            <ItemTemplate>
                                                <asp:Label ID="lbMonth" Text='<%# Bind("StressMonth") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
 <%--                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="FactorId">
                                            <ItemTemplate>
                                                                                         <asp:Label ID="lbFactorId" Text='<%# Bind("FactorId") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Fartor Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbFactorValue" Text='<%# Bind("FactorValue") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ปรับปรุงข้อมูล">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" Width="40px" Height="35px" CommandName="Edit" class="btn btn-primary btn-search;" ToolTip="ปรับปรุงข้อมูล"
                                                    CausesValidation="true" runat="server" Style="text-decoration: none;" CommandArgument='<%# Container.DisplayIndex %>'>
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลบข้อมูล">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" Width="40px" Height="35px" CommandName="Delete" class="btn btn-danger" ToolTip="ลบข้อมูล"
                                                    CausesValidation="false" runat="server" Style="text-decoration: none">
                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
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
                                    <PagerSettings PageButtonCount="10" NextPageText="ถัดไป" PreviousPageText="ก่อนหน้า" FirstPageText="First" LastPageText="Last" />
                                    <RowStyle Font-Size="Medium" Height="40px" BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                    <SelectedRowStyle BackColor="#CCCCCC" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <div class="ClearDataInLine"></div>
                </div>
            </div>

            <div class="modal" id="myModal" role="dialog" aria-labelledby="AlertBoxLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 80%;">
                    <asp:UpdatePanel ID="UpdModal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header NormalSubItems">
                                    <asp:Label ID="lblModalTitle" runat="server" Style="font-size: medium;" Text="Import Excel" />
                                </div>
                                <div class="modal-body">
                                    <table width="100%" align="center" style="border: thin solid #EEE;"
                                        class="table table-hover table-striped footable" border="0" style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                        <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                            <td align="center">
                                                <div>
                                                    <h2>Upload Excel File</h2>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:FileUpload ID="fileUpload" runat="server" onchange="toggleButtonState(this)" /></td>
                                                        </tr>
                                                    </table>
                                                    <asp:Button ID="btnUpload" runat="server" Text="Import" OnClick="btnUpload_Click" CssClass="btn btn-primary ButtonStyle" Enabled="False" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false" />
                                                <asp:GridView ID="grvImportExcel" runat="server" AutoGenerateColumns="False" PageSize="5"
                                                    AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px; border-color: Gray;"
                                                    Width="100%" ShowHeaderWhenEmpty="True" SelectedRowStyle-BackColor="#CCCCCC">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="รายการที่">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSeq" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center ChangeHeaderforMarketReport1" Width="10%" VerticalAlign="Middle" Font-Size="Medium" Height="20px" />
                                                            <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" Height="20px" Font-Size="Small" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="TIMEID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbTimeId" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Stress_Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbStressYear" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Stress_Month">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbStressMonth" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="FactorID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbFactorId" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="FartorValue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbFartorValue" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle" HeaderText="Error Detail">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbErrorDetail" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50%" BorderColor="Gray" BorderWidth="1" />
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
                                                    <PagerSettings PageButtonCount="10" NextPageText="ถัดไป" PreviousPageText="ก่อนหน้า" FirstPageText="First" LastPageText="Last" />
                                                    <RowStyle Font-Size="Medium" Height="40px" BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnCancelImport" runat="server" OnClick="btnCancelImport_Click" CssClass="btn btn-primary ButtonStyle" Text="ยกเลิก" data-dismiss="modal" aria-hidden="true" Visible="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>

                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:AsyncPostBackTrigger ControlID="grvImportExcel" EventName="PageIndexChanging" />
                            <asp:PostBackTrigger ControlID="btnCancelImport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

                  
            <div class="modal" id="myAddEditModal" role="dialog" aria-labelledby="AlertBoxLabel" aria-hidden="true">
           <div class="modal-dialog" style="width: 50%;">
               <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                   <ContentTemplate>
                       <div class="modal-content">
                           <div class="modal-header NormalSubItems">
                               <asp:Label ID="lbAddEditTitle" runat="server" Style="font-size: medium;" Text="Add/Edit" />
                           </div>
                           <div class="modal-body">
                               <table width="100%" align="center" style="border: thin solid #EEE;"
                                   class="table table-hover table-striped footable" border="0" style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                   <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                       <td align="center">
                                           <div>
                                               <table>
                                                   <tr>
                                                       <td style="text-align: right; font-size: medium">ชื่อตัวแปรทางเศรษฐกิจ&nbsp:&nbsp
                                                       </td>
                                                       <td style="text-align: left">
                                                           <asp:DropDownList ID="ddlAddEditFactor" runat="server" Height="30px" CssClass="TextBoxRoundCorrner">
                                                           </asp:DropDownList>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td style="text-align: right; font-size: medium">ปี&nbsp:&nbsp
                                                       </td>
                                                       <td style="text-align: left">
                                                           <asp:TextBox ID="txtAddEditYear" runat="server" class="form-control" Style="margin-bottom: 2px; font-size: medium; text-align: left"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td style="text-align: right; font-size: medium">เดือน&nbsp:&nbsp
                                                       </td>
                                                       <td style="text-align: left">
                                                           <asp:TextBox ID="txtAddEditMonth" runat="server" class="form-control" Style="margin-bottom: 2px; font-size: medium; text-align: left"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td style="text-align: right; font-size: medium">Factor Value&nbsp:&nbsp
                                                       </td>
                                                       <td style="text-align: left">
                                                           <asp:TextBox ID="txtAddEditFactorValue" runat="server" class="form-control" Style="margin-bottom: 2px; font-size: medium; text-align: left"></asp:TextBox>
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
                                           <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="false" />
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
                                    <table width="100%" align="center" style="border: thin solid #EEE;"
                                        class="table table-hover table-striped footable" border="0"
                                        style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                        <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                            <td id="imageBox" runat="server" align="center">
                                                <asp:Image ID="Symbol_Image" runat="server" ImageUrl="" Height="100px"
                                                    Width="100px" />
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

        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownloadTemplate" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>