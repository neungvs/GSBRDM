<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TreeSetting.aspx.vb" Inherits="GSBWeb.TreeSetting" %>
<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        
        function SetPostback() 
        {
            var sendpost = window.event.srcElement;

            if ((sendpost.tagName == "INPUT") && (sendpost.type == "checkbox")) 
            {
                __doPostBack("", "")
            }
        }
        function printing() 
        {
            var panel = ""
            var WithScreen = (window.screen.width / 20);
            var HeightScreen = (window.screen.height / 20);
            var printWindow = window.open('', '', 'height=600,width=1200,left=' + WithScreen + ',top=' + HeightScreen + ',screenX=' + WithScreen + ',screenY=' + HeightScreen);
            printWindow.document.write('<html><head><title></title><style type="text/css">@page{margin:0;}body{margin-top:1.6cm;margin-bottom:1.6cm;margin-left:1cm;margin-right:1cm;-webkit-print-color-adjust: exact !important;}</style></head><body>');
            printWindow.document.write(document.getElementById('<%= div_Panel.ClientID %>').innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print()
        }
//        function Checking() 
//        {
//            alert(document.getElementById("TableHeight").style.height);
//            if (document.getElementById("TableHeight").style.height > 30) {
//             
//                
//                document.getElementById("<%= Headerform.ClientID %>").size = "13pt";
//            }
//            else 
//            {
//                document.getElementById("<%= Headerform.ClientID %>").size = "16pt";
//            }
//            if (document.getElementById("<%= Headerform.ClientID %>").innerHTML =  "") 
//            {
//                Checking()
//            }
//        }
        
    </script>
    <style>
      .ParentNodestyle
      {
          display:inline;
          vertical-align:middle;
          text-align:left;
          table-layout:fixed;
          padding-left:0px;
          
          }
      td, th {
    padding: 0;
    vertical-align: top;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-md-8 col-md-offset-2">
                    
                   
                   <div class="panel panel-default">
                        <div class="NormalHeader" style="/*text-align: left; font-weight: bold; font-size: medium;
                        background: #FF388C; padding-right: 5; color: #FFFFFF*/ height:30px;">
                        กำหนดกลุ่มงานกับเมนูงาน
                        </div>
                        <div class="ClearDataInLine"></div>
                        <table width="100%" border="0">
                            <tr style="height:35px;">
                                <td align="right"><font color="#FF0000">*</font>กลุ่มงาน</td>
                                <td align="center" width="10px">:</td>
                                <td width="300px" align="left">
                                    <asp:DropDownList ID="cb_listgroup" runat="server" Height="30px" Width="370px" CssClass="TextBoxRoundCorrner">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:LinkButton ID="btn_Submit" runat="server" CssClass="btn btn-primary ButtonStyle">
                                        <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;&nbsp;ตกลง
                                    </asp:LinkButton>
                                    <%--<asp:Button ID="btn_Submit" name="btn_Save" runat="server" 
                                        CssClass="btn btn-primary ButtonStyle"  Width="70px" Text="ตกลง" OnClientClick="Checking()" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                   
                                </td>
                            </tr>
                             <tr>
                                <td colspan="5">
                                    
                                </td>
                            </tr>
                        </table>
                        <div class="ClearDataInLine"></div>
                   </div> 
                   <div class="ClearDataInLine"></div>                  
                   <table width="100%" border="0">
                    <tr id="TableHeight" class = "NormalHeader" style="height:40px; padding-left:0;background-color:#FF388C;">
                         <td width="40px" align="left" style="padding-left:20px;">
                            &nbsp;
                        </td>
                        <td align="left" width="410px">
                             <div class="NormalHeader" id="Headerform" runat="server" style="padding: 0px 0px 0px 0px;font-size:9.7pt;font-weight:bold;background-color:#FF388C;color:#FFF;" >&nbsp;</div>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="btn_Print"  CssClass="btn btn-primary ButtonStyle" runat="server">
                                <span class="glyphicon glyphicon-print"></span>&nbsp;&nbsp;&nbsp;พิมพ์
                            </asp:LinkButton>
                            <%--<asp:Button ID="btn_Print" CssClass="btn btn-primary ButtonStyle" runat="server" Text="พิมพ์" />--%>
                            &nbsp;
                            <asp:LinkButton ID="btn_Delete"  CssClass="btn btn-primary ButtonStyle" runat="server">  
                                <span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;&nbsp;ลบ
                            </asp:LinkButton>
                            <%--<asp:Button ID="btn_Delete" CssClass="btn btn-primary ButtonStyle" runat="server" Text="ลบ" />--%>
                            &nbsp;
                            <asp:LinkButton ID="Save"  CssClass="btn btn-primary ButtonStyle" runat="server">
                                 <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;&nbsp;บันทึก
                            </asp:LinkButton>
                            <%--<asp:Button ID="Save" CssClass="btn btn-primary ButtonStyle" runat="server"  Text="บันทึก" />--%>
                            &nbsp;
                            <asp:LinkButton ID="btn_Cancel"  CssClass="btn btn-primary ButtonStyle" runat="server">
                                <span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;&nbsp;ยกเลิก
                            </asp:LinkButton>
                            &nbsp;
                            <%--<asp:Button ID="btn_Cancel" CssClass="btn btn-primary ButtonStyle" runat="server" Text="ยกเลิก" />--%>
                        </td>
                    </tr>
                       <tr style="height:50px">
                           <td align="left" colspan="3" style="padding-left:20px;width:40px;">
                               <asp:CheckBox ID="CheckedAll" runat="server" AutoPostBack="True" Text="เลือกทั้งหมด" />
                           </td>
                       </tr>
                       <tr>
                           <td colspan="3" >
                               <asp:TreeView ID="Tv_AllData" runat="server" ExpandDepth="0" ExpandImageToolTip="Expand {0}" Font-Bold="False" ShowCheckBoxes="All" Width="100%" style="text-align:left;vertical-align:top">
                                   <NodeStyle ForeColor="Black"  />
                                   <%-- BackColor="#FFE8EE" --%>
                                   <ParentNodeStyle CssClass="ParentNodestyle" ForeColor="Black" VerticalPadding="0px" />
                                   <%-- BackColor="#FFCEDB"  --%>
                                   <RootNodeStyle ForeColor="Black"/>
                                   <%-- BackColor="#FFCEDB"  --%>
                               </asp:TreeView>
                           </td>
                       </tr>
                       <tr>
                           <td colspan="3">&nbsp;</td>
                       </tr>
                       <tr>
                           <td colspan="3">
                               <asp:LinkButton ID="lb_setfocus" runat="server"></asp:LinkButton>
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td colspan="3">&nbsp;</td>
                       </tr>
                  </table>
                  
                   <div>
                        
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
