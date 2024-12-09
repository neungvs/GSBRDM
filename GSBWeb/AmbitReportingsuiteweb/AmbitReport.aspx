<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AmbitReport.aspx.vb" Inherits="GSBWeb.AmbitReport" %>



<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
 <div>
        <uc1:AutoRedirect ID="AutoRedirect1" runat="server" />
        <iframe id ="theframe"  src="https://rdmprdweb.gsb/AmbitReportingSuiteWeb" frameborder="0" style="overflow: hidden; height: 100%; width: 100%; position: absolute; left: 0px; bottom: 0px;"></iframe>
 </div>


</asp:Content>
