<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Report.aspx.vb" Inherits="GSBWeb.Report" %>
<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout ="1200">
    </asp:ScriptManager>

    <rsweb:ReportViewer ID="ReportViewer1" KeepSessionAlive="false" AsyncRendering="false" runat="server" Width="100%" Height="450px" style="text-align:left" >
    </rsweb:ReportViewer>


</asp:Content>
