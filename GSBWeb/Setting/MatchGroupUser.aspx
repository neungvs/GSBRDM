<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MatchGroupUser.aspx.vb" Inherits="GSBWeb.MatchGroupUser" %>
<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
      <style type="text/css">
        .td-center
        {
            text-align:center;
            background:#FF388C;
            color: #FFFFFF;
  
        }

          .btn-primary
          {}

    </style>
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
    </script>
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rows">
                <div class="col-md-8 col-md-offset-2" >
                    
                    <%----%>
                    <div class="panel panel-default">
                    <div class="NormalHeader">
                        กำหนดผู้ใช้งานเข้ากับกลุ่มงาน
                    </div>
                        <div class="ClearDataInLine"></div>
                        <table width="100%" >
                            <tr style="height:50px;">
                                <td align="right" >
                                    <font color="#FF0000">*</font>รหัสพนักงาน
                                </td>
                                <td align="center" width="10px">:</td>
                                <td align="left">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td align="left" width="100px"><input type="text" id="User_ID" runat="server" style="width:100px;height:30px;" class="TextBoxRoundCorrner" /></td>
                                            <td align="left">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btn_Delete_User" runat="server" CssClass="btn btn-primary ButtonStyle" Visible="false">
                                                    <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ
                                                </asp:LinkButton>
                                                <%--<asp:Button ID="btn_Delete_User" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ลบ" />--%>
                                                &nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="Submit" runat="server" CssClass="btn btn-primary ButtonStyle">
                                                    <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;&nbsp;ตกลง
                                                </asp:LinkButton>
                                                <%--<asp:Button ID="Submit" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ตกลง" />--%>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btn_Cancel_User" runat="server" CssClass="btn btn-primary ButtonStyle">
                                                    <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                                                </asp:LinkButton>
                                                <%--<asp:Button ID="btn_Cancel_User" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ยกเลิก" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height:50px;">
                                <td align="right" >ชื่อผู้ใช้งาน</td>
                                <td align="center" >:</td>
                                <td align="left">
                                    <input type="text" id="username" runat="server"  disabled="disabled" style="width:450px;height:30px;" class="TextBoxRoundCorrner"/> 
                                </td>
                            </tr>   
                            <tr valign="middle" height="60px">
                                <td colspan="3" align="left" ><div style="width:15px;display:inline;">&nbsp;</div>หมายเหตุ : รายการที่มี ดอกจันสีแดง (<font color="#FF0000">*</font>) หมายถึง ต้องบันทึก</td>
                            </tr>
                        </table>
                    </div>
                    <div class="ClearDataInLine"></div>
                    <div class="NormalHeader" style="background-color:#FF388C;color:#FFFFFF;">
                        <table width="100%" border="0">
                            <tr valign="middle" style="height:40px;">
                                <td align="left">
                                    <font color="#FFFFFF">กลุ่มงาน</font>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="btn_Print_Group" runat="server"  CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-print"></span>&nbsp;&nbsp;&nbsp;พิมพ์
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btn_Excel" runat="server"  CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-print"></span>&nbsp;&nbsp;&nbsp;Excel
                                    </asp:LinkButton>

                                    <%--<asp:Button ID="btn_Print_Group" runat="server" CssClass="btn btn-primary ButtonStyle" Text="พิมพ์" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btn_Delete_Group" runat="server"  CssClass="btn btn-primary ButtonStyle" Visible="false">
                                        <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="btn_Delete_Group" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ลบ" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btn_Save" runat="server"  CssClass="btn btn-primary ButtonStyle">
                                         <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;&nbsp;บันทึก
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="btn_Save" runat="server" CssClass="btn btn-primary ButtonStyle"  Text="บันทึก" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btn_Cancel_Group" runat="server" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="btn_Cancel_Group" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ยกเลิก" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <asp:GridView ID="gv_GroupUser" runat="server" AutoGenerateColumns="False" PageSize="100"
                                AllowPaging="True" EnableModelValidation="True" Style="border-width: 1px;  border-color: Gray;"
                                Width="100%" ShowHeaderWhenEmpty="True" HeaderStyle-HorizontalAlign="Center" 
                                    HeaderStyle-VerticalAlign="Middle" HorizontalAlign="Center" 
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>            
                                               <div>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="30px">
                                                                <asp:ImageButton Width="13px" Height="13px" ID="ib_Checkbox" runat="server" CommandName="Select" ImageUrl="~/Images/checkbox _UnChecked.png" />
                                                            </td>
                                                            <td align="right" width="50px">
                                                                <asp:Label ID="GroupcodeData" runat="server" Text='<%# Bind("GroupCode") %>' ></asp:Label>-
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="GroupnameData" runat="server" Text='<%# Bind("GroupName_TH") %>' ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>                                                    
                                               </div>
                                            </ItemTemplate>                                           
                                            <ItemStyle HorizontalAlign="left"  Width="95px" BorderColor="Gray" BorderWidth="1" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate >
                                        <div align="center">
                                            ไม่พบข้อมูล
                                        </div>
                                    </EmptyDataTemplate>   
                                    <HeaderStyle BackColor="#FFE8EE" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                        Height="35px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                    <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                    <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                    <PagerSettings  PageButtonCount="10" NextPageText="ถัดไป"  PreviousPageText = "ก่อนหน้า"  FirstPageText="First" LastPageText="Last"/>
                                    <RowStyle Font-Size="Medium" Height="40px"   BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" />
                                    <FooterStyle BackColor="#FFE8EE" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                        Height="35px" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <div class="ClearDataInLine"></div>
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
            <div runat="server" id="div_Panel" style="display:none">
            
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
                <ProgressTemplate>
                    <div class="modal_Pross" style="z-index:1500;" >
                        <div class="center_Pross" style="margin-top:100px;">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


