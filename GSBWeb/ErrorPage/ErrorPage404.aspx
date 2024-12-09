<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ErrorPage/Site2.Master" CodeBehind="ErrorPage404.aspx.vb" Inherits="GSBWeb.zErrorPage404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">

        window.history.forward();
        function noBack() { window.history.forward(); }
        window.onload = 'noBack()';
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }

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
    <h3 style="margin-left: 10px">Error 404 Page not Found.
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../Login.aspx" Target="_self"
            Text="กดปุ่มเพื่อย้อนกลับไปหน้า Login." Style="text-decoration: underline; color: #CC33FF"></asp:HyperLink></h3>
    </h3>
</asp:Content>
