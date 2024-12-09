<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Exp.aspx.vb" Inherits="GSBWeb.Exp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="~/Images/gsb-banner.png" />
    <!-- Bootstrap CSS -->
    <link href="<%= Page.ResolveClientUrl("~/Styles/bootstrap.css")%>" rel="stylesheet" type="text/css" />
    <!-- Font awesome -->
    <link href="<%= Page.ResolveClientUrl("~/fonts/font-awesome-4.7.0/css/font-awesome.min.css")%>" rel="stylesheet" type="text/css" />
    <!-- Jquery JavaScript -->
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery-1.11.2.min.js") %>"></script>
    <!-- Bootstrap Core JavaScript -->
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/bootstrap.js") %>"></script>
    <!-- Jquery JavaScript -->
    <script src="<%= Page.ResolveClientUrl("~/Scripts/jquery-ui-1.10.3.custom.js")%>"
        type="text/javascript"></script>
    <!-- Date Picker CSS -->
    <link href="<%= Page.ResolveClientUrl("~/Styles/ui-lightness/datePickerjq.css")%>"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/aes.js")%>"> </script>
    <title></title>
</head>
<body>
    <table class="table table-bordered">
      <th colspan="3">Outer Table</th>
      <tr>
        <td>This is row one, column 1</td>
        <td>This is row one, column 2</td>
      </tr>
      <tr>
        <td colspan="2">
          <table class="table table-bordered">
            <th colspan="3">Inner Table</th>
            <tr>
              <td>This is row one, column 1</td>
              <td>This is row one, column 2</td>
              <td>This is row one, column 3</td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
</body>
</html>
