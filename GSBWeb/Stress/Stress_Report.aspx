<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Stress_Report.aspx.vb" Inherits="GSBWeb.Stress_Report" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style11 {
            height: 22pt;
            width: 2181px;
        }
        .auto-style23 {
            height: 23px;
        }
        .auto-style25 {
            width: 2344px;
        }
        .auto-style26 {
            width: 42px;
        }
        .auto-style27 {
            width: 42px;
            height: 22pt;
        }
        .auto-style28 {
            width: 66px;
        }
        .auto-style29 {
            width: 66px;
            height: 22pt;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
            <div class="row">
                <div class="col-md-8 col-md-offset-2"> 
                    <div class="panel panel-default">
                        <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt;
                            background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px;">
                            รายงานการทดสอบภาวะวิกฤติ (Stress Test) และการทดสอบแบบจำลอง (Back Test)
                        </div>
                        <div class="panel-body">
                             <asp:Panel ID="pnlReport" runat="server" />
                        </div>
<%--                        <div class="panel-heading" style="text-align: left; font-weight: bold; font-size: large;
                                background: #FF99FF; padding-right: 5;">
                                Header 1
                            </div>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10; text-decoration: none;"><i class="fa fa-caret-right"></i>First item</a>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;"><i class="fa fa-caret-right"></i>Second item</a>
                            <a href="#" class="list-group-item" style="text-align: left; padding-right: 10;"><i class="fa fa-caret-right"></i>Third item</a                                     
                        </div>--%>
<%--                        <div class="panel-footer" style="text-align: left; font-weight: bold; font-size: medium;
                            background: #CCCCCC; padding-right: 5; color: #000000; ">
                            หมายเหตุ: ในกรณีที่ต้องการ Zoom ขยายข้อมูลรายงานตารางหรือกราฟ สามารถดำเนินการได้ผ่านการ Export รายงานในรูปแบบ Excel, PDF, TIFF, Word
                        </div>--%>
                    </div>



                </div>

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
