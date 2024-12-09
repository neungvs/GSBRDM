<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerRating.aspx.vb" Inherits="GSBWeb.Customer_Rating" MasterPageFile="~/Site.Master" %>

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
                            Customer Rating
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
                                               <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary ButtonStyle"> <span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;&nbsp;ค้นหา </asp:LinkButton>
                                               &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnDownloadTemplate" runat="server" CssClass="btn btn-primary ButtonStyle" Width="200">
                                                   <span class="glyphicon glyphicon-download"></span>&nbsp;&nbsp;&nbsp;Download Template
                                                </asp:LinkButton>                                               
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnExcelImport" runat="server" CssClass="btn btn-primary ButtonStyle" Width="200"> <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;&nbsp;นำเข้าข้อมูลจาก Excel </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="ClearDataInLine"></div>
                    <div class="NormalHeader" style="background-color: #FF388C; color: #FFFFFF;">
                        <table width="100%" border="0" id="tbCustomerRatingHeader" runat="server">
                            <tr valign="middle" style="height: 40px;">
                                <td align="left">
                                    <font color="#FFFFFF">Customer Rating</font>
                                </td>
                                <td align="right"></td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <asp:GridView ID="gvCustomerRating" runat="server" AutoGenerateColumns="False" PageSize="10"
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
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="CIF Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lbCifNumber" Text='<%# Bind("CustomerNr") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ปีที่ทดสอบภาวะวิกฤต">
                                            <ItemTemplate>
                                                <asp:Label ID="lbYear" Text='<%# Bind("Year") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="สถานการณ์ภาวะวิกฤต">
                                            <ItemTemplate>
                                                <asp:Label ID="lbScenario" Text='<%# Bind("ScenarioName") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="pd_segment เก่า">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPdSegmentOld" Text='<%# Bind("OldPdSegment") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="pd_segment ใหม่">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPdSegmentNew" Text='<%# Bind("NewPdSegment") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="95px" BorderColor="Gray" BorderWidth="1" />
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
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="วันที่ของข้อมูล">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbTime" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="CIF Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbCIFNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="สถานการณ์ภาวะวิกฤต">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbScenario" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ปีที่ทดสอบภาวะวิกฤต">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbYear" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="pd_segment เก่า">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbOldPdSegment" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Gray" BorderWidth="1" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="pd_segment ใหม่">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbNewPdSegment" runat="server"></asp:Label>
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