<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ModuleManagement.aspx.vb" Inherits="GSBWeb.ModuleManagement" %>
<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        th
        { 
            text-align:center; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function printing() {
            var panel = ""
            var WithScreen = (window.screen.width / 20);
            var HeightScreen = (window.screen.height / 20);
            var printWindow = window.open('', '', 'height=600,width=1200,left=' + WithScreen + ',top=' + HeightScreen + ',screenX=' + WithScreen + ',screenY=' + HeightScreen);
            var SwitchLevelValue = document.getElementById('<%= SwitchLevel.ClientID %>').value
            if ((SwitchLevelValue >= 2) && (SwitchLevelValue < 4)) 
            {
                printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;size:landscape;}body{margin-left:0.0cm;margin-right:0.0cm;margin-top:0.0cm;margin-buttom:0.0cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');
            }
            else if (SwitchLevelValue == 4) 
            {
                printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;size:landscape;}body{margin-left:0.5cm;margin-right:0.5cm;margin-top:0.5cm;margin-buttom:0.5cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');    
            }
            else 
            {
                printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;size:portrait;}body{margin-left:0.5cm;margin-right:0.5cm;margin-top:1.0cm;margin-buttom:1.0cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');
            }
            printWindow.document.write(document.getElementById('<%= div_Panel.ClientID %>').innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print()
        }
    </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager><uc1:AutoRedirect ID="AutoRedirect" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rows">
                <div class="col-md-8 col-md-offset-2">
                    
                    <div >
                        <div class="panel panel-default">
                        <div class="NormalHeader" id="Page_Header_Content" runat="server">
                        
                        </div>
                        <table width="100%" border="0" id="DataForm">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="height:35px;">
                                <td align="center">
                                    <asp:DropDownList ID="SwitchLevel" runat="server" AutoPostBack="True" style="height:30px;width:300px;" CssClass="TextBoxRoundCorrner">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td id="modulelv" runat="server">
                                   <table width="100%" border="0">
                                        <tr id="menulevel0" runat="server" style="height:35px;">
                                            <td align="right" style="height:35px;"><font color="#FF0000">*</font>เมนูระดับ 0</td>
                                            <td align="center" width="10px" style="height:35px;">:</td>
                                            <td align="left" style="height:35px;">
                                                <asp:DropDownList ID="dd_Menulevel_0" runat="server" Width="300px" 
                                                    AutoPostBack="True" Height="30px" CssClass="TextBoxRoundCorrner">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="menulevel1" runat="server" style="height:35px;">
                                            <td align="right" style="height:35px;"><label id="menu_star1" runat="server"><font color="#FF0000">*</font></label>เมนูระดับ 1</td>
                                            <td align="center" width="10px" style="height:35px;">:</td>
                                            <td align="left" style="height:35px;">
                                                <asp:DropDownList ID="dd_Menulevel_1" runat="server" Width="300px" 
                                                    AutoPostBack="True" Height="30px" CssClass="TextBoxRoundCorrner">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="menulevel2" runat="server" style="height:35px;">
                                            <td align="right" style="height:35px;"><label id="menu_star2" runat="server"><font color="#FF0000">*</font></label>เมนูระดับ 2</td>
                                            <td align="center" width="10px" style="height:35px;">:</td>
                                            <td align="left" style="height:35px;">
                                               <asp:DropDownList ID="dd_Menulevel_2" runat="server" Width="300px" 
                                                    AutoPostBack="True" Height="30px" CssClass="TextBoxRoundCorrner">
                                               </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="menulevel3" runat="server" style="height:35px;">
                                            <td align="right" style="height:35px;"><label id="menu_star3" runat="server"><font color="#FF0000">*</font></label>เมนูระดับ 3</td>
                                            <td align="center" width="10px" style="height:35px;">:</td>
                                            <td align="left" style="height:35px;">
                                                <asp:DropDownList ID="dd_Menulevel_3" runat="server" Width="300px" 
                                                    AutoPostBack="True" Height="30px" CssClass="TextBoxRoundCorrner">
                                               </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="height:35px;visibility:hidden">
                                            <td id="lvaid" runat="server" align="right"></td>
                                            <td align="center" width="10px">:</td>
                                            <td align="left">
                                               <asp:TextBox ID="LVIID" runat="server" Width="50" Height="30" MaxLength="5" 
                                                    CssClass="TextBoxRoundCorrner" AutoPostBack="True"></asp:TextBox>&nbsp;&nbsp;&nbsp;<label runat="server" id="Error_InputLVIID" style="font-size:8pt;"></label>
                                            </td>
                                        </tr>
                                        <tr style="height:35px;">
                                            <td id="lvaname_th" runat="server" align="right"></td>
                                            <td align="center" width="10px">:</td>
                                            <td align="left">
                                                <input type="text" id="LVIName_th" name="LVIName_th" runat="server" style="width:300px;height:30px;" class="TextBoxRoundCorrner" />
                                            </td>
                                        </tr>
<%--                                        <tr style="height:35px;">
                                            <td id="lvaname_en" runat="server" align="right"></td>
                                            <td align="center" width="10px">:</td>
                                            <td align="left">
                                                <input type="text" id="LVIName_en" name="LVIName_en" runat="server" style="width:300px;height:30px;" class="TextBoxRoundCorrner" />
                                            </td>
                                        </tr>--%>
                                         <tr style="height:35px;">
                                            <td align="right"><label id="menu_starpath" runat="server" visible="false"><font color="#FF0000"></font></label>Path โปรแกรม</td>
                                            <td align="center" width="10px">:</td>
                                            <td align="left">
                                                <input type="text" id="LVIPath" name="LVIPath" runat="server" style="width:300px;height:30px;" class="TextBoxRoundCorrner" />

                                            </td>
                                        </tr>
                                         <tr id="lvshow" runat="server">
                                            <td align="right"><label id="menu_starseq" runat="server"><font color="#FF0000">*</font></label>ลำดับที่แสดง</td>
                                            <td align="center" width="10px">:</td>
                                            <td align="left">
                                                <%--<input type="text" id="LVIMenuseq" name="LVIMenuseq" runat="server" style="width:50px;height:30px;" class="TextBoxRoundCorrner" />--%>
                                                <asp:TextBox ID="LVIMenuseq" runat="server" Width="50" Height="30" 
                                                    CssClass="TextBoxRoundCorrner" AutoPostBack="True"></asp:TextBox>&nbsp;&nbsp;&nbsp;<label runat="server" id="Error_InputLVIMenuseq" style="font-size:8pt;"></label>
                                            </td>
                                        </tr>
                                   </table>
                                </td>
                            </tr>                      
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:LinkButton ID="Delete" name="Delete" runat="server" style="text-decoration:none;" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="Delete" name="Delete" runat="server" Text="ลบ" CssClass="btn btn-primary ButtonStyle" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="Sav" runat="server" style="text-decoration:none;" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;&nbsp;บันทึก
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="Sav" name="Sav" runat="server" Text="บันทึก" CssClass="btn btn-primary ButtonStyle" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="Cancel" runat="server" style="text-decoration:none;" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="Cancel" name="Canecl" runat="server" Text="ยกเลิก" CssClass="btn btn-primary ButtonStyle" />--%>
                                    <div style="width:15px;display:inline;">&nbsp;</div>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left"><div style="width:15px;display:inline;">&nbsp;</div>หมายเหตุ : รายการที่มี ดอกจันสีแดง (<font color="#FF0000">*</font>) หมายถึง ต้องบันทึก</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        </div>
                        <table width="100%" border="0">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:LinkButton ID="btn_Prints" runat="server" style="text-decoration:none;" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-print"></span>&nbsp;&nbsp;&nbsp;พิมพ์
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="btn_Prints" runat="server" CssClass="btn btn-primary ButtonStyle" Text="พิมพ์" />--%>
                                </td>
                            </tr>
                             <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gv_level0" runat="server" AutoGenerateColumns="False" PageSize="100"
                                     AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                     Width="50%" ShowHeaderWhenEmpty="True" HeaderStyle-HorizontalAlign="Center" 
                                     HeaderStyle-VerticalAlign="Middle" HorizontalAlign="Center" SelectedRowStyle-BackColor="#CCCCCC">
                                    <Columns>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 0" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode_level0"  runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID")  %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" Width="120px"  CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center"  Width="120px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="ชื่อเมนูภาษาไทย" Visible="false">
                                        <ItemTemplate>
                                           <div style="margin-left:30px;"> 
                                                <asp:LinkButton ID="lbGroupname_TH_level0" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameTH")  %>'></asp:LinkButton>                                 
                                           </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="ชื่อเมนู">
                                        <ItemTemplate>
                                           <div style="margin-left:30px;"> 
                                                <asp:LinkButton ID="lbGroupname_EN_level0" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameEN")  %>'></asp:LinkButton>                                 
                                           </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลำดับที่แสดง" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Number_Level0" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("MenuSeq")  %>'></asp:LinkButton>                                 
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center" Width="120px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate >
                                            <div align="center">
                                                ไม่พบข้อมูล
                                            </div>
                                        </EmptyDataTemplate>   
                                        <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                            Height="35px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                        <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                        <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                        <PagerSettings  PageButtonCount="10" NextPageText="ถัดไป"  PreviousPageText = "ก่อนหน้า"  FirstPageText="First" LastPageText="Last"/>
                                        <RowStyle Font-Size="Medium" Height="40px"   BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                        <SelectedRowStyle BackColor="#CCCCCC" />
                                    </asp:GridView>

                                    <asp:GridView ID="gv_level1" runat="server" AutoGenerateColumns="False" PageSize="100"
                                     AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                     Width="100%" ShowHeaderWhenEmpty="True" HeaderStyle-HorizontalAlign="Center" 
                                     HeaderStyle-VerticalAlign="Middle" HorizontalAlign="Center" SelectedRowStyle-BackColor="#CCCCCC">
                                    <Columns>
                                        <asp:TemplateField HeaderText="รหัสเมนูระดับ 0"  Visible ="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbGroupCode0_level1" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level0")  %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" Width="120px"  CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center"  Width="120px" BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสเมนูระดับ 1" Visible ="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbGroupCode1_level1" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID")  %>'></asp:LinkButton>                                 
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center" Width="120px"  BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="80%" HeaderText="ชือเมนูระดับ 1">
                                            <ItemTemplate>
                                                <div style="margin-left:30px;">
                                                    <asp:LinkButton ID="lbGroupName_TH_level1" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameTH")  %>'></asp:LinkButton>                                 
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อเมนูภาษาอังกฤษระดับ 1" Visible="false">
                                        <ItemTemplate>
                                           <div style="margin-left:30px;"> 
                                                <asp:LinkButton ID="lbGroupname_EN_level1" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameEN")  %>'></asp:LinkButton>                                 
                                           </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลำดับที่แสดง">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Number_Level1" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("MenuSeq")  %>'></asp:LinkButton>                                 
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                            <ItemStyle HorizontalAlign="Center" Width="120px"  BorderColor="Gray" BorderWidth="1" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate >
                                        <div align="center">
                                            ไม่พบข้อมูล
                                        </div>
                                    </EmptyDataTemplate>   
                                    <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                        Height="35px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                    <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                    <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                    <PagerSettings  PageButtonCount="10" NextPageText="ถัดไป"  PreviousPageText = "ก่อนหน้า"  FirstPageText="First" LastPageText="Last"/>
                                    <RowStyle Font-Size="Medium" Height="40px"   BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                    <SelectedRowStyle BackColor="#CCCCCC" />
                                    </asp:GridView>

                                    <asp:GridView ID="gv_level2" runat="server" AutoGenerateColumns="False" PageSize="100"
                                     AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                     Width="100%" ShowHeaderWhenEmpty="True" HeaderStyle-HorizontalAlign="Center" 
                                     HeaderStyle-VerticalAlign="Middle" HorizontalAlign="Center" SelectedRowStyle-BackColor="#CCCCCC">
                                    <Columns>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 0" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode0_level2" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level0")  %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" Width="120px"  CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center"  Width="120px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 1" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode1_level2" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level1")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 2" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode2_level2" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80%"  HeaderText="ชือเมนูระดับ 2">
                                        <ItemTemplate>
                                            <div style="margin-left:30px;">
                                                <asp:LinkButton ID="lbGroupName_TH_level2" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameTH")  %>'></asp:LinkButton>                                 
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อเมนูภาษาอังกฤษระดับ 2" Visible="false">
                                        <ItemTemplate>
                                           <div style="margin-left:30px;"> 
                                                <asp:LinkButton ID="lbGroupname_EN_level2" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameEN")  %>'></asp:LinkButton>                                 
                                           </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลำดับที่แสดง">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Number_Level2" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("MenuSeq")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate >
                                            <div align="center">
                                                ไม่พบข้อมูล
                                            </div>
                                        </EmptyDataTemplate>   
                                        <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                            Height="35px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                        <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                        <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                        <PagerSettings  PageButtonCount="10" NextPageText="ถัดไป"  PreviousPageText = "ก่อนหน้า"  FirstPageText="First" LastPageText="Last"/>
                                        <RowStyle Font-Size="Medium" Height="40px"   BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                        <SelectedRowStyle BackColor="#CCCCCC" />
                                    </asp:GridView>

                                    <asp:GridView ID="gv_level3" runat="server" AutoGenerateColumns="False" PageSize="100"
                                     AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                     Width="100%" ShowHeaderWhenEmpty="True" HeaderStyle-HorizontalAlign="Center" 
                                     HeaderStyle-VerticalAlign="Middle" HorizontalAlign="Center" SelectedRowStyle-BackColor="#CCCCCC">
                                    <Columns>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 0" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode0_level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level0")  %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" Width="110px"  CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center"  Width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 1" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode1_level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level1")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 2" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode2_level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level2")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="รหัสเมนูระดับ 3" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode3_level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80%" HeaderText="ชือเมนูระดับ 3">
                                        <ItemTemplate>
                                            <div style="margin-left:30px;">
                                                <asp:LinkButton ID="lbGroupName_TH_level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameTH")  %>'></asp:LinkButton>                                 
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อเมนูภาษาอังกฤษระดับ 3" Visible="false">
                                        <ItemTemplate>
                                           <div style="margin-left:30px;"> 
                                                <asp:LinkButton ID="lbGroupname_EN_level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameEN")  %>'></asp:LinkButton>                                 
                                           </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลำดับที่แสดง">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Number_Level3" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("MenuSeq")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate >
                                            <div align="center">
                                                ไม่พบข้อมูล
                                            </div>
                                        </EmptyDataTemplate>   
                                        <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                            Height="35px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                        <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                        <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                        <PagerSettings  PageButtonCount="10" NextPageText="ถัดไป"  PreviousPageText = "ก่อนหน้า"  FirstPageText="First" LastPageText="Last"/>
                                        <RowStyle Font-Size="Medium" Height="40px"   BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                        <SelectedRowStyle BackColor="#CCCCCC" />
                                    </asp:GridView>

                                    <asp:GridView ID="gv_level4" runat="server" AutoGenerateColumns="False" PageSize="100"
                                     AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                     Width="100%" ShowHeaderWhenEmpty="True" HeaderStyle-HorizontalAlign="Center" 
                                     HeaderStyle-VerticalAlign="Middle" HorizontalAlign="Center" SelectedRowStyle-BackColor="#CCCCCC">
                                    <Columns>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 0" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode0_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level0")  %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" Width="110px"  CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center"  Width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 1" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode1_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level1")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" Width="110px" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 2" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode2_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level2")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="รหัสเมนูระดับ 3" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode3_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID_Level3")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสเมนูระดับ 4" Visible ="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbGroupCode4_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleID")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="110px"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80%" HeaderText="ชือเมนูระดับ 4">
                                        <ItemTemplate>
                                            <div style="margin-left:30px;">
                                                <div style="margin-left:30px;">
                                                    <asp:LinkButton ID="lbGroupName_TH_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameTH")  %>'></asp:LinkButton>                                 
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อเมนูภาษาอังกฤษระดับ 4" Visible="false">
                                        <ItemTemplate>
                                           <div style="margin-left:30px;"> 
                                                <asp:LinkButton ID="lbGroupname_EN_level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("ModuleNameEN")  %>'></asp:LinkButton>                                 
                                           </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Left"  BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลำดับที่แสดง">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Number_Level4" runat="server" ForeColor="#000000" Font-Underline="false" CommandName="Select" Text='<%# Bind("MenuSeq")  %>'></asp:LinkButton>                                 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" BorderColor="Gray" BorderWidth="1" CssClass="td-center" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px"  BorderColor="Gray" BorderWidth="1" />
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
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
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
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
