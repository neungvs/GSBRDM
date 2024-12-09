<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserLogData.aspx.vb" Inherits="GSBWeb.UserManagement" %>
<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function pageLoad() 
        {
            $(".datePic").datepicker($.datepicker.regional["th"]);
        }
        function printing() {
            var panel = ""
            var WithScreen = (window.screen.width / 20);
            var HeightScreen = (window.screen.height / 20);
            var printWindow = window.open('', '', 'height=600,width=1200,left=' + WithScreen + ',top=' + HeightScreen + ',screenX=' + WithScreen + ',screenY=' + HeightScreen);
            printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;size:landscape;}body{margin:1.6cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');
            printWindow.document.write(document.getElementById('<%= div_Panel.ClientID %>').innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print()
        }
    </script>
    <style>
        th
        {text-align:center;vertical-align:middle;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    &nbsp;&nbsp;&nbsp;
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rows">
                <div class="col-md-8 col-md-offset-2">
                    
                    <center>
                    <div class="panel panel-default">
                    <div class="NormalHeader" style="height:30px;">
                        รายงาน Log การทำงาน
                    </div>
                    <table width="100%" border="0" cellspacing="3" >
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" border="0">
                                    <tr style="height:35px;" valign="middle">
                                        <td align="right" ><font color="#FF0000">*</font>หัวข้อกิจกรรม</td>
                                        <td width="10px" align="center">:</td>
                                        <td ><asp:DropDownList ID="dd_Activation" runat="server" style="width:300px;height:30px;" class="TextBoxRoundCorrner" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height:35px;" valign="middle"> 
                            <td style="padding-left:25px;">
                                 <table align="center" >
                                    <tr valign="middle">
                                        <td align="right"><font color="#FF0000">*</font>ตั้งแต่วันที่</td>
                                        <td width="10px" align="center">:</td>
                                        <td>
                                            <asp:TextBox ID="txt_StartDate" runat="server" Width="100px" AutoPostBack="true" 
                                                    onKeyPress="return false" onKeyDown="return false" autocomplete="off" 
                                                    CssClass="datePic TextBoxRoundCorrner" >
                                            </asp:TextBox>
                                        </td>
                                        <td width="30px">&nbsp;</td>
                                        <td align="right"><font color="#FF0000">*</font>จนถึงวันที่ </td>
                                        <td width="10px" align="center">:</td>
                                        <td>
                                            <asp:TextBox ID="txt_EndDate" runat="server" Width="100px" AutoPostBack ="true"
                                                    onKeyPress="return false" onKeyDown="return false" autocomplete="off"
                                                    CssClass="datePic TextBoxRoundCorrner">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                 </table>
                            </td> 
                        </tr>
                        <tr valign="middle" style="height:35px;">
                            <td >&nbsp;</td>
                        </tr>
                        <tr valign="middle" style="height:35px;">
                          <td align="right" style="padding-right:50px;">
                                <asp:LinkButton runat="server" ID="btn_Submit"  CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;&nbsp;ตกลง
                                </asp:LinkButton>
                                <%--<asp:Button runat="server" ID="btn_Submit" Text="ตกลง" CssClass="btn btn-primary ButtonStyle" />--%>
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="btn_Cancel" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                                </asp:LinkButton>
                                <%--<asp:Button runat="server" ID="btn_Cancel" Text="ยกเลิก" CssClass="btn btn-primary ButtonStyle" />--%>

                          </td>  
                        </tr>
                        <tr style="height:35px;">
                            <td align="left" style="padding-left:30px;">หมายเหตุ : รายการที่มี ดอกจันสีแดง (<font color="#FF0000">*</font>) หมายถึง ต้องบันทึก</td>
                        </tr>
                        <%--<tr class="RowHeight">
                            <td align="right" style="color:White;font-size:small;text-align:right;" width="100px">
                                UserID 
                            </td>
                            <td align="center" width="10px" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="UID" name="UID" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight2">
                            <td style="text-align:right;color:White;font-size:small;">
                                User Name
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="UName" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight">
                            <td style="text-align:right;color:White;font-size:small;">
                                Password
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text1" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight2">
                            <td style="text-align:right;color:White;font-size:small;">
                                EmployeeID
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text2" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight">
                            <td style="text-align:right;color:White;font-size:small;">
                                Firstname
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text3" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight2">
                            <td style="text-align:right;color:White;font-size:small;">
                                Lastname
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text4" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight">
                            <td style="text-align:right;color:White;font-size:small;">
                                Firstname Thai
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text5" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight2">
                            <td style="text-align:right;color:White;font-size:small;">
                                Position
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text6" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight">
                            <td style="text-align:right;color:White;font-size:small;">
                                Company
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="text" id="Text7" name="UName" runat="server" style="width:500px;font-size:small;" />
                            </td>
                        </tr>
                        <tr class="RowHeight2">
                            <td style="text-align:right;color:White;font-size:small;">
                                Group
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="radio" name="privilage" value="1" checked="checked" /><label style="color:White;padding:0px 0px 0px 5px;">Admin</label>
                                <input type="radio" name="privilage" value="2" /><label style="color:White;padding:0px 0px 0px 5px;">User</label>
                            </td>
                        </tr>
                        <tr class="RowHeight">
                            <td style="text-align:right;color:White;font-size:small;">
                                Disabled
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                                <input type="radio" name="display" value="0" checked="checked" /><label style="color:White;padding:0px 0px 0px 5px;">Enable</label>
                                <input type="radio" name="display" value="1" /><label style="color:White;padding:0px 0px 0px 5px;">Disable</label>
                            </td>
                        </tr>
                        <tr class="RowHeight2">
                            <td style="text-align:right;color:White;font-size:small;">
                                Userflag
                            </td>
                            <td align="center" style="color:White;font-size:small;">
                                :
                            </td>
                            <td align="left">
                               <select>
                                    <option value="0">System Application</option>
                                    <option value="1">LDap</option>
                               </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center"><button type="button">Submit</button>&nbsp;&nbsp;&nbsp;<button type="button">Cancel</button></td>
                        </tr>--%>
                    </table>
                    </div>
                    <table width="100%" border="0">
                        <tr style="height:35px;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:35px;">
                            <td align="right">
                                <asp:LinkButton runat="server" ID="btn_Print" Text="พิมพ์" CssClass="btn btn-primary ButtonStyle">
                                    <span class="glyphicon glyphicon-print"></span>&nbsp;&nbsp;&nbsp;พิมพ์
                                </asp:LinkButton>
                                <%--<asp:Button runat="server" ID="btn_Print" Text="พิมพ์" CssClass="btn btn-primary ButtonStyle" />--%>
                             </td>
                        </tr>
                        <tr style="height:35px;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:35px;" valign="middle">
                            <td style="background:#FF388C;padding-left:27px;color:#FFF;" class="NormalHeader">หัวข้อกิจกรรม : <asp:Label ID="lbl_Header_Ativity" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gv_Logdata" runat="server" AutoGenerateColumns="False" PageSize="100" 
                                    AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                    Width="100%" ShowHeaderWhenEmpty="True" >
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ลำดับ">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_No"  runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("Number")  %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center"  Width="95px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="วันและเวลา">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_DateTime" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Eval("DTmStamp","{0:dd/MM/yyyy HH:mm:ss tt}") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center"  BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ผู้ใช้งาน">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_User_Code" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("EmployeeID") %>'></asp:LinkButton> 
                                                &nbsp;-&nbsp;
                                                <asp:LinkButton ID="lb_User_Name" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("UserName_TH") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="กิจกรรม">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Activity" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("UserActivity") %>'></asp:LinkButton>
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
                        <tr style="height:35px;"><td>&nbsp;</td></tr>
                        <tr style="height:35px;"><td>&nbsp;<asp:LinkButton ID="lb_SetFocus" runat="server">&nbsp;</asp:LinkButton>&nbsp;</td></tr>
                        <tr style="height:35px;"><td>&nbsp;</td></tr>
                    </table>
                    </center>
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
                                                <asp:Button ID="btn_NO" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ไม่ใช่" data-dismiss="modal" aria-hidden="true" />
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
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
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
