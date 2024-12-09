<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataSet_Market_Report.aspx.vb" Inherits="GSBWeb.DataSet_Market_Report" %>
<%@ Register src="../UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect1" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-8 col-md-offset-2">
                    <div class="NormalHeader" style="/*text-align: left; font-weight: bold; font-size: medium;
                        background: #FF388C; padding-right: 5; color: #FFFFFF*/ height:40px;">
                        รายงาน DataSet Market
                    </div>
                </div>
                <table width="70%" align="center" border="0">
                    <tr style="vertical-align:middle;height:60px;">
                        <td align="right">งวดข้อมูล&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlMonth" runat="server" >
                                <asp:ListItem Value="01">มกราคม</asp:ListItem>
                                <asp:ListItem Value="02">กุมภาพันธ์</asp:ListItem>
                                <asp:ListItem Value="03">มีนาคม</asp:ListItem>
                                <asp:ListItem Value="04">เมษายน</asp:ListItem>
                                <asp:ListItem Value="05">พฤษภาคม</asp:ListItem>
                                <asp:ListItem Value="06">มิถุนายน</asp:ListItem>
                                <asp:ListItem Value="07">กรกฎาคม</asp:ListItem>
                                <asp:ListItem Value="08">สิงหาคม</asp:ListItem>
                                <asp:ListItem Value="09">กันยายน</asp:ListItem>
                                <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                                <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlYear" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="119px" CssClass="btn btn-primary ButtonGrayStyle"/>
                        </td>
                    </tr>
                    <tr>
                       <td colspan="2" >
                         <%-- <asp:GridView ID="gvReport" runat="server"  
                                CssClass= "table table-striped table-bordered table-condensed"  
                                class="table table-striped" AllowPaging="True" AutoGenerateColumns="False"   
                                PageSize="100" ShowHeaderWhenEmpty="True" 
                                Style="border-width: 1px; border-color: Gray" Width="100%" 
                                HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField  HeaderText="รายการที่">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeq" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderWidth="1" CssClass="ChangeHeader"  VerticalAlign="Middle"  />
                                        <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"  Font-Size="Medium"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อรายการ">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HF_Directory" ToolTip="ดูรายละเอียด" Enabled="true" runat="server" NavigateUrl='<%# Eval("ReportName","~/DataSet/Reports/{0}") %>'
                                                Text='<%# Bind("ReportName") %>' ForeColor="Blue" ></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center" CssClass="dt-center ChangeHeader" VerticalAlign="Middle"    />
                                        <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="left" VerticalAlign="Middle" Width="20%"/>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>                   
                                    <div align="center">
                                        ไม่พบข้อมูล    
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#FF388C" BorderColor="Gray" BorderWidth="1" Font-Size="Medium" VerticalAlign="Middle" HorizontalAlign="Center" ForeColor="White" Height="50px" Width="100px" />
                                <PagerStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Right" />
                                <EmptyDataRowStyle BorderColor="Gray" BorderWidth="1" />
                                <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                <RowStyle BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" Font-Size="Small" />
                            </asp:GridView> --%>
                            <asp:GridView ID="gvReport"  runat="server" AutoGenerateColumns="False" PageSize="100"
                                            AllowPaging="True" EnableModelValidation="True"  Style="border-width: 1px; border-color: Gray;"
                                            Width="890px" HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                            <asp:TemplateField  HeaderText="รายการที่">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeq" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center ChangeHeaderforMarketReport1" Width="10%" VerticalAlign="Middle" Font-Size="Medium" Height="20px" />
                                                <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" Height="20px"  Font-Size="Small"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อรายการ">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HF_Directory" ToolTip="ดูรายละเอียด" Enabled="true" runat="server" NavigateUrl='<%# Eval("ReportName","~/DataSet/Reports/{0}") %>'
                                                        Text='<%# Bind("ReportName") %>' ForeColor="Blue" ></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderWidth="1" CssClass="td-center ChangeHeaderforMarketReport2" VerticalAlign="Middle" Font-Size="Medium" Height="20px" />
                                                <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="left" VerticalAlign="Middle" Height="20px" Width="30%"/>
                                            </asp:TemplateField>
                                    </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">
                                        ไม่พบข้อมูล
                                    </div>
                                </EmptyDataTemplate>
                                <PagerStyle  HorizontalAlign ="Right" />
                                <HeaderStyle BackColor="#FF388C" Font-Size="Medium" BorderColor="Gray" BorderWidth="1"
                                    Height="35px" ForeColor="White" VerticalAlign="Top" />
                                <PagerStyle  HorizontalAlign ="Right" BorderColor="Gray" BorderWidth="1"/>
                                <EmptyDataRowStyle  BorderColor="Gray" BorderWidth="1"/>
                                <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                                <RowStyle Font-Size="Medium" Height="40px" BackColor="#FFCEDB" BorderColor="Gray"
                                    BorderWidth="1" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>               
            </div>

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

    </asp:UpdatePanel>

    
</asp:Content>
