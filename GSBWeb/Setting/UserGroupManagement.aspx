<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserGroupManagement.aspx.vb" Inherits="GSBWeb.UserGroupManagement" %>
<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>  
<%--<%@ Register Src="~/UserControl/alert_and_confirm.ascx" TagName="AlertBox"  %> --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style type="text/css">
        .btn-primary
        {}
        th
        {
            text-align:center;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function printing() 
        {
            var panel = ""
            var WithScreen = (window.screen.width / 20);
            var HeightScreen = (window.screen.height / 20);
            var printWindow = window.open('', '', 'height=600,width=1200,left=' + WithScreen + ',top=' + HeightScreen + ',screenX=' + WithScreen + ',screenY=' + HeightScreen);
            printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;}body{margin:1.6cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');
            printWindow.document.write(document.getElementById('<%= div_Panel.ClientID %>').innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print()
        }
        function cannotclose() { 
            
        }
    </script>
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rows">
                <div class="col-md-8 col-md-offset-2" >
                   
                    <div class="GroupRoundCorrner">
                     <div class="NormalHeader">
                        กำหนดกลุ่มงาน
                     </div>
                    <table width="100%" >
                         <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr class="RowHeight " style="height:35px;">
                            <td align="right" class="TableHeader">
                               รหัสกลุ่มงาน
                            </td>
                            <td align="center" width="10" class="TableHeader">
                                :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="GroupID" runat="server" Width="100px" MaxLength="5" 
                                    AutoPostBack="True" style="height:30px;" CssClass="TextBoxRoundCorrner" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;
<%--                                    <asp:LinkButton runat="server" ID="btn_Add" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;&nbsp;เพิ่ม
                                    </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                 <asp:LinkButton runat="server" ID="btn_Search" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;&nbsp;ตกลง
                                 </asp:LinkButton>--%>
                                <%--<asp:Button ID="btn_Search" runat="server" OnClientClick="btn_Search_Click" CssClass="btn btn-primary ButtonStyle" Text="ตกลง" />  --%>                        
                            </td>
                        </tr>
                        <tr class="RowHeight TableHeader" style="height:35px;">
                            
                           <td align="right" class="TableHeader">
                                <font color="#FF0000">*</font>ชื่อกลุ่มงานภาษาไทย
                            </td>
                            <td align="center" width="10" class="TableHeader">
                                :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Group_Name_th" runat="server" Width="300px" style="height:30px;" CssClass="TextBoxRoundCorrner"></asp:TextBox>                    
                                <asp:LinkButton runat="server" ID="btn_Add" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;&nbsp;เพิ่ม
                                </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="btn_Search" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;&nbsp;ค้นหา
                                </asp:LinkButton>
                             </td>
                        </tr>
                        <tr class="RowHeight TableHeader" style="height:35px;">
                           <td align="right" class="TableHeader">
                                ชื่อกลุ่มงานภาษาอังกฤษ
                            </td>
                            <td align="center" width="10" class="TableHeader">
                                :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Group_name_en" runat="server" Width="300px" style="height:30px;" CssClass="TextBoxRoundCorrner"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr class="TableFooter"  >
                            <td colspan="3" align="right" style="padding: 0px 30px 0px 0px;">
                                <asp:LinkButton runat="server" ID="Delete" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ
                                </asp:LinkButton>
                                <%--<asp:Button CssClass="btn btn-primary ButtonStyle " ID="Delete" name="Delete"  runat="server" Text="ลบ" />--%>
                                &nbsp;&nbsp;&nbsp;
                                
                                <asp:LinkButton runat="server" ID="Submit" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;&nbsp;บันทึก
                                </asp:LinkButton>
                                <%--<asp:Button CssClass="btn btn-primary ButtonStyle" ID="Submit" name="Submit" runat="server" Text="บันทึก" />--%>
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="Cancel" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                                </asp:LinkButton>
                                <%--<asp:Button CssClass="btn btn-primary ButtonStyle" ID="Cancel" name="Cancel" runat="server" Text="ยกเลิก" />--%>
                            </td>
                        </tr>
                        <tr>
                           <td colspan="3" align="left">
                                <div style="width:15px;display:inline;">&nbsp;</div>หมายเหตุ : รายการที่มี ดอกจันสีแดง (<font color="#FF0000">*</font>) หมายถึง ต้องบันทึก
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" >&nbsp;</td>
                        </tr>
                    </table>
                    </div>
                    <table width="100%" border="0">
                        <tr>
                            <td style="height:35px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td  style="height:30px;" align="right">
                                <asp:LinkButton runat="server" ID="btn_Print"  CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-print"></span>&nbsp;&nbsp;&nbsp;พิมพ์
                                </asp:LinkButton>
                                <%--<asp:Button ID="btn_Print" CssClass="btn btn-primary ButtonStyle" runat="server" Text="พิมพ์" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" id="groupUserdata" name="groupUserdata" runat="server">
                                <asp:GridView ID="gv_GroupUser" runat="server" AutoGenerateColumns="False" PageSize="100"
                                    AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                    Width="100%" ShowHeaderWhenEmpty="True" SelectedRowStyle-BackColor="#CCCCCC">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="รหัสกลุ่มงาน">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="GroupCodedata"  runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("GroupCode")  %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center"  Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ชื่อกลุ่มงาน">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="GroupName_TH" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("GroupName_TH") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
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
                        <tr>
                            <td style="height:35px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="modal fade" id="AlertBox" role="dialog" aria-labelledby="AlertBoxLabel" aria-hidden="true">
                <div class="modal-dialog" style="width:400px;">
                    <asp:UpdatePanel ID="UpdModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header NormalSubItems" >
                                    <asp:Label ID="lbl_Title" runat="server" style="font-size:medium;" text="    " />
                                </div>
                                <div class="modal-body">
                                    <table width="100%" align="center" style="border: thin solid #EEE;" 
                                           class="table table-hover table-striped footable" border="0" 
                                           style="margin: 0 0 0 0;padding: 0 0 0 0;">
                                        <tr style="background-color:#FFF;color:#000;font-weight:bold;">
                                            <td id="imageBox" runat="server" align="center">
                                                <asp:Image ID="Symbol_Image" runat="server" ImageUrl="" Height="100px" 
                                                    Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center"><asp:Label ID="Messages" runat="server" Text="Sample" /></td>
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
            <div id="div_Panel" style="display:none;" runat="server">
                
            </div>
            <asp:UpdateProgress ID="Progress" runat="server" style="z-index:999;">
                <ProgressTemplate>
                    <div class="modal_Pross" style="z-index:1500;">
                        <div class="center_Pross" style="margin-top:100px;">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
