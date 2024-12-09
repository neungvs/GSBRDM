<%@ Page Title="WebAdjust" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="RDM_Report.aspx.vb" Inherits="GSBWeb.RDM_Report" %>
<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style8 {
            font-size: medium;
        }

        .style10 {
            font-family: Tahoma;
            font-size: medium;
        }

        .style11 {
            font-family: Tahoma;
            font-size: medium;
        }
        table{
            font-weight:normal;     
            font-size: ;
           
          
        }
    </style>
    <script type="text/javascript">


        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-2"></div>
                <div class="col-md-8" style="align-content:center;">

                    <asp:GridView ID="gvReport" runat="server"  CssClass= "table table-striped table-bordered table-condensed"  class="table table-striped" AllowPaging="True" AutoGenerateColumns="False" EnableModelValidation="True" PageSize="100" ShowHeaderWhenEmpty="True" Style="border-width: 1px; border-color: Gray" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="รายการที่">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeq" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle BorderColor="Gray" BorderWidth="1" CssClass="td-center" HorizontalAlign="Center" />
                                <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center"  Width="10%"  Font-Size="Medium"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" ชื่อรายงาน">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HF_Directory" ToolTip="ดูรายละเอียด" Enabled="true" runat="server" NavigateUrl='<%#Bind("Directory")%>'
                                        Text='<%# Bind("RepoName") %>' ForeColor="Blue" ></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Center" />
                                <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="left" Width="20%"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียด">
                                <ItemTemplate>
                                    <asp:Label ID="lblRepoDecription" runat="server" Text='<%# Bind("RepoDecription") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle BorderColor="Gray" BorderWidth="1" CssClass="td-center" HorizontalAlign="Center" />
                                <ItemStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Left" Font-Size="Medium"/>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center">
                                ไม่พบข้อมูล
                            </div>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#FF388C" BorderColor="Gray" BorderWidth="1" Font-Size="Medium" ForeColor="White" Height="50px" Width="100px" />
                        <PagerStyle BorderColor="Gray" BorderWidth="1" HorizontalAlign="Right" />
                        <EmptyDataRowStyle BorderColor="Gray" BorderWidth="1" />
                        <AlternatingRowStyle BackColor="#FFE8EE" BorderColor="Gray" BorderWidth="1" />
                        <RowStyle BackColor="#FFCEDB" BorderColor="Gray" BorderWidth="1" Font-Size="Small" />
                    </asp:GridView>

                </div>
                <div class="col-sm-2"></div>
                
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

