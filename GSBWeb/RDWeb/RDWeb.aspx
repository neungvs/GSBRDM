<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RDWeb.aspx.vb" Inherits="GSBWeb.RDWeb" %>
<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">


 <div>
        <uc1:AutoRedirect ID="AutoRedirect1" runat="server" />
        <iframe id="theframe" runat="server" src="https://rdmprdapp.gsb/RDWeb" frameborder="0" style="overflow: hidden; height: 100%; width: 100%; position: absolute; left:0px" />
 </div>

</asp:Content>
