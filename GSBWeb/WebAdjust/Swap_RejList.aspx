<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Swap_RejList.aspx.vb" Inherits="GSBWeb.Swap_RejList" %>

<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .gridview
        {
            margin-bottom: 0px;
        }
        .style19
        {
            width: 100%;
        }
        .style20
        {
            width: 33px;
        }
        .style21
        {
            width: 219px;
        }
        .style22
        {
            width: 144px;
        }
        .style23
        {
            width: 164px;
        }
        .style24
        {
            width: 195px;
        }
        #T3
        {
            width: 1297px;
        }
        .td-center
        {
            text-align:center;
            background:#FF388C;
            color: #FFFFFF";
  
        }
    </style>

     <script type="text/javascript">

         function Input_Eng(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode;
             if (charCode >= 3585) {
                 return false;
             } else {
                 var iChars = "~`!#$%^&*+=-[]\\\';,{}|\":<>?^()";
                 var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
                 var ch = String.fromCharCode(charCode);
                 if (iChars.indexOf(ch) == -1) {
                     return true;
                 }
                 else {
                     return false;
                 }
             }
         }


         function spacialCaractor(evt) {
             var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
             var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
             var ch = String.fromCharCode(charCode);
             if (iChars.indexOf(ch) == -1) {
                 return true;
             }
             else {
                 return false;
             }
         }


         function ChangeValue(value1) {

             return value1.replace('"', '').replace('[', '').replace(']', '');
         }


</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

<uc1:AutoRedirect ID="AutoRedirect" runat="server" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="NormalHeader" style="/*text-align: left; font-size: medium; font-weight: bold;
                    background: #FF388C; padding-right: 5; color: #FFFFFF*/ height:30px;">
                    ข้อมูลรายการที่ถูก Reject Swap</div>
                <table align="left" width="100%">
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td align="right" style="font-weight: bold; font-size: medium; white-space: nowrap">
                            CIF_NUMBER :&nbsp;
                        </td>
                        <td style="font-weight: bold; text-align: left; white-space: nowrap">
                             <asp:TextBox ID="txtCifNum" class="form-control" runat="server" Width="200px" Enabled="true" onkeyup="this.value=this.value.replace(/[^0-9a-zA-Z_/]/g,'');"    onkeypress="return Input_Eng(event);"  autocomplete="off"></asp:TextBox>

                        </td>
                        <td align="right" style="font-weight: bold; font-size: medium">
                            เลขที่บัญชี :&nbsp;
                        </td>
                        <td style="font-weight: bold; font-size: medium">
                                <asp:TextBox ID="txtAccNo" class="form-control" onkeyup="this.value=this.value.replace(/[^0-9a-zA-Z_/]/g,'');"    onkeypress="return Input_Eng(event);"  runat="server" Width="200px" autocomplete="off"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;
                              <asp:Button ID="btnSearch" class="btn btn-primary ButtonGrayStyle" runat="server" Text="ค้นหา"
                                Width="110px" />
                            &nbsp&nbsp
                            <asp:Button ID="btnCancle" class="btn btn-primary ButtonGrayStyle" runat="server" Text="ยกเลิก"
                                Width="110px" />
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td align="right" style="font-weight: bold; font-size: medium">
                            ชื่อลูกค้า : &nbsp;
                        </td>
                        <td style="font-weight: bold; font-size: x-small">
                                  <asp:TextBox ID="txtCifName" class="form-control" onkeypress="return spacialCaractor(event);" onkeyup="this.value=  ChangeValue(this.value.replace(/[1234567890๑๒๓๔฿๕๖๗๘๙๐~`!#$%^&*+=-]|[\\\';,/{}/|:<>?@_^.()]|[[]]|[/]/g,''));"  runat="server" Width="200px" Enabled="true" autocomplete="off"></asp:TextBox>

                        </td>
                        <td align="right" style="font-weight: bold; font-size: medium">
                            นามสกุล : &nbsp;
                        </td>
                        <td style="font-weight: bold; font-size: x-small">
                                   <asp:TextBox ID="txtCifSurName" class="form-control" onkeypress="return spacialCaractor(event);" onkeyup="this.value=  ChangeValue(this.value.replace(/[1234567890๑๒๓๔฿๕๖๗๘๙๐~`!#$%^&*+=-]|[\\\';,/{}/|:<>?@_^.()]|[[]]|[/]/g,''));"  runat="server" Width="200px" autocomplete="off"></asp:TextBox>
                        </td>
                        <td style="font-weight: bold; font-size: x-small">
                          
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 5%">
                                &nbsp;
                            </td>
                    </tr>
                     <tr>
                        <td class="style25">
                            &nbsp;
                        </td>
                        <td class="style26">
                            &nbsp;
                        </td>
                        <td class="style26">
                            &nbsp;
                        </td>
                        <td class="style26">
                            &nbsp;
                        </td>
                        <td class="style26">
                            &nbsp;
                        </td>
                        <td style="text-align:right" class="style26">
                             <asp:LinkButton ID="btnReport" runat="server" CausesValidation="false" class="btn btn-danger ButtonGrayStyle"
                                Style="text-decoration: none; /*background: #FF388C;*/" ToolTip="ออกรายงาน" Width="70px">
                        Print
                     
                        <span aria-hidden="true" class="glyphicon glyphicon-print"></span>
                            </asp:LinkButton>
                        </td>
                        <td class="style25">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td width="90%" colspan="5">
                            <asp:GridView ID="gvSwap" runat="server" AutoGenerateColumns="False" PageSize="20"
                                AllowPaging="True" Width="100%" ShowHeaderWhenEmpty="True" 
                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="3" GridLines="Vertical">
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CIF_NUMBER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCifNumber" Text='<%# Bind("CIF_NUMBER") %>' runat="server" CommandName="Select"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" เลขที่บัญชี">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hpPositionID"  runat="server" CommandName="Select"></asp:LinkButton>
                                            <asp:HiddenField ID="HF_TMP_SwapID" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black"/>
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="ชื่อลูกค้า นามสกุล" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="CustName" Text='<%# Bind("CustName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Left" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="StartDate" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" Text='<%# Bind("StartDate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="EndDate" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" Text='<%# Bind("EndDate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RecPrincipalAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecPrincipalAmount" Text='<%# Bind("RecPrincipalAmount") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BorderColor="#EEEEEE" BackColor="#CCCCCC" BorderWidth="1" CssClass="td-center"
                                            ForeColor="Black" Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" BorderColor="Gray" BorderWidth="1" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">
                                        ไม่พบข้อมูล
                                    </div>
                                </EmptyDataTemplate>
                                <PagerStyle  HorizontalAlign ="Center" BorderColor="Gray" BorderWidth="1" 
                                    BackColor="#999999" ForeColor="Black"/>
                                <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#000084" Height="35px" Font-Bold="True" 
                                    ForeColor="White" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <RowStyle Font-Size="Small" BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>

        
         <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
