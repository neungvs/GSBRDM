<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestFunction.aspx.vb" Inherits="GSBWeb.TestFunction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="Scripts/alertify.js" type="text/javascript"></script>
     <link href="Styles/alertify.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:Button ID="Errorpoit" Text="Click" runat="server" />

       <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
 <script type="text/javascript">
     function errors() {
         alertify.error("SampleError");
     }
</script>
<%--<script type="text/javascript">
    errors();
</script>--%>