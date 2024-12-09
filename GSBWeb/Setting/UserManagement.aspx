<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserManagement.aspx.vb" Inherits="GSBWeb.UserManagement1" %>
<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    
.vcenter {
display: inline-block;
vertical-align: middle;
float: none;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function printing() 
        {
            var panel = ""
            var WithScreen = (window.screen.width / 20) ;
            var HeightScreen = (window.screen.height / 20);
            var printWindow = window.open('', '', 'height=600,width=1200,left='+WithScreen+',top='+HeightScreen+',screenX='+WithScreen+',screenY='+HeightScreen); 
            printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;}body{margin:1.6cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');
            printWindow.document.write(document.getElementById('<%= div_Panel.ClientID %>').innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
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
                <div class="col-md-8 col-md-offset-2">
                    
                    <div class="panel panel-default">
                        <div class="NormalHeader">
                         กำหนดผู้ใช้
                        </div>
                        <center>
                            <div class="container">
                                <div style="height:10px;"></div>
                                <div class="row">
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>รหัสพนักงาน</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;">
                                        <asp:TextBox ID="EmployeeID" runat="server" Height="30" Width="370" CssClass="TextBoxRoundCorrner"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="btn_Search_User" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="true"> <span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;&nbsp;ค้นหา </asp:LinkButton>
                                    </div>
                                </div>
                                <div style="height:10px;"></div>
                                <div class="row" >                                   
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>ชื่อผู้ใช้</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;"><asp:TextBox ID="Username" runat="server" Height="30" Width="370 " CssClass="TextBoxRoundCorrner" ReadOnly="true"></asp:TextBox></div>
                                </div>
                                <div style="height:10px;"></div>
                                <div class="row">
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>ชื่อ ภาษาอังกฤษ</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;"><asp:TextBox ID="FirstnameEN" runat="server" Height="30" Width="370" CssClass="TextBoxRoundCorrner" ReadOnly="true"></asp:TextBox></div>
                                </div>
                                <div style="height:10px;"></div>
                                <div class="row">
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>นามสกุล ภาษาอังกฤษ</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;"><asp:TextBox ID="LastnameEN" runat="server" Height="30" Width="370" CssClass="TextBoxRoundCorrner" ReadOnly="true"></asp:TextBox></div>
                                </div>
                                <div style="height:10px;"></div>
                                <div class="row">
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>ชื่อ ภาษาไทย</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;"><asp:TextBox ID="FirstnameTH" runat="server" Height="30" Width="370" CssClass="TextBoxRoundCorrner" ReadOnly="true"></asp:TextBox></div>
                                </div>
                                <div style="height:10px;"></div>
                                <div class="row">
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>นามสกุล ภาษาไทย</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;"><asp:TextBox ID="LastnameTH" runat="server" Height="30" Width="370" CssClass="TextBoxRoundCorrner" ReadOnly="true"></asp:TextBox></div>
                                </div>
                                <div style="height:10px;"></div>
<%--                                <div class="row">
                                    <div class="col-sm-2 vcenter" style="text-align:right;"><font color="#FF0000">*</font>กลุ่มงาน</div>
                                    <div class="col-sm-1 vcenter" style="text-align:center;width:10px;">:</div>
                                    <div class="col-sm-7 vcenter" style="text-align:left;"><asp:DropDownList ID="GroupData" runat="server" Width="370" Height="30" CssClass="TextBoxRoundCorrner" /></div>
                                </div>
                                <div style="height:10px;"></div>--%>
                                <div class="row">
                                    <div class="col-sm-8" style="text-align:center;">
<%--                                        <asp:LinkButton ID="btn_Save" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false"> <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;&nbsp;บันทึก </asp:LinkButton>
                                        &nbsp;&nbsp;--%>
                                        <asp:LinkButton ID="btn_AddData" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false"> <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;&nbsp;เพิ่ม </asp:LinkButton>
                                       <%-- <asp:Button ID="btn_AddData" runat="server" CssClass="btn btn-primary ButtonStyle" Text="เพิ่ม" />--%>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="btn_Delete" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false" > <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ </asp:LinkButton>
                                        &nbsp;&nbsp;
                                         <asp:LinkButton ID="btn_Cancel" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false" > <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก </asp:LinkButton>
                                        <%--<asp:Button ID="btn_Cancel" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ยกเลิก" />--%>
                                    </div>                                    
                                </div>
                                <div style="height:10px;"></div>
                            </div>
                        </center>
                    </div>
                    <div>
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
                                        <td align="center" id="Userdata" name="groupUserdata" runat="server">
                                            <asp:GridView ID="gv_User" runat="server" AutoGenerateColumns="False" PageSize="200"
                                                AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                                Width="60%" ShowHeaderWhenEmpty="True" SelectedRowStyle-BackColor="#CCCCCC">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="รหัสพนักงาน">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="EmployeeID"  runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("EmployeeID")  %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center"  Width="95px" BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ชื่อผู้ใช้งาน">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="UserName" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("UserName") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ชื่อ">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="FirstNameTH" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("FirstNameTH") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="นามสกุล">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LastNameTH" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("LastNameTH") %>'></asp:LinkButton>
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
                                    <table width="100%" align="center" style="margin: 0 0 0 0; padding: 0 0 0 0;" 
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
                                                <asp:Button ID="btn_OK" runat="server" CssClass="btn btn-primary ButtonStyle" OnClientClick="btn_OK_Click" Text="ใช่" data-dismiss="modal" aria-hidden="true" data-keyboard="false"/>
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
            <div id="div_Panel" style="display:none" runat="server">
                
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
