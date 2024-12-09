<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ErrorPage/Site2.Master" CodeBehind="SessionExpired.aspx.vb" Inherits="GSBWeb.zSessionExpired" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">

        function cancelBack() {
            if ((event.keyCode == 8 ||
                (event.keyCode == 37 && event.altKey) ||
                (event.keyCode == 39 && event.altKey))
                &&
                (event.srcElement.form == null || event.srcElement.isTextEdit == false)
            ) {
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }

        window.onload = window.history.forward(0);

    </script>
    <nav class="navbar navbar-default">
        <div class="container-fluid">
            <!-- navbar for mobile display -->
            <div class="navbar-header">
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid -->
    </nav>
    <div style="clear: both">
    </div>
    <h1>Session Expired</h1>
    <h3>
        Your Session has expired&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://rdmdevweb.gsb/AmbitReportingsuiteweb/" Target="_self"
            Text="Please return to the home page." Style="text-decoration: underline; color: #CC33FF"></asp:HyperLink>
    </h3>
</asp:Content>
