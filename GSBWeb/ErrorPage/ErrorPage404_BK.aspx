<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ErrorPage404_BK.aspx.vb"
    Inherits="GSBWeb.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head id="Head1" runat="server">
    <link href="../Styles/bootstrap.css" rel="stylesheet" type="text/css" />

    <title></title>



    <style type="text/css">
       
        
        
        
        
        
    </style>
    <script type="text/javascript">

        window.history.forward();
        function noBack() { window.history.forward(); }
        window.onload = 'noBack()';
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }

    </script>
</head>
<body >
    <form id="form1" runat="server" data-toggle="validator">
    <div class="container-fluid ">
        <div class="row">
            <!-- header area start -->
            <div class="headerStyle" style="background-color: #EB058C; text-align: right; height: 120px;
                margin-top: 0; margin-left: 0; padding-left: 0; padding-top: 0">
                <img class="img-responsive" src="<%= ResolveUrl("~/Images/Logo_GSB.png")%>" align="left"
                    style="text-align: left; vertical-align: top; margin-bottom: 0; margin-top: 0;
                    padding-top: 0; padding-bottom: 0; width: 120px; height: 120px" />
               <asp:Label ID="Label1" runat="server" class="labelheader"  align="right" Text="ระบบรายงานฐานข้อมูลเพื่อบริหารความเสี่ยง"
                    Font-Bold="True"  Style="text-align: right;
                    vertical-align: top;"></asp:Label>
            </div>
            <!-- header area end -->
            <!-- menubar area start -->
            <nav class="navbar navbar-default">
			  <div class="container-fluid">
				<!-- navbar for mobile display -->
				<div class="navbar-header">
				</div>
                  <!-- /.navbar-collapse -->
			  </div><!-- /.container-fluid -->
			</nav>
            <!-- menubar area end -->
            <div style="clear: both">
            </div>
            <%--  <div class="footerCopyRightStyle">
                    </div>--%>
        </div>
    </div>
    <div style="clear: both">
    </div>
    </form>
    <h3 style="margin-left:10px">
        Error 404 Page not Found.
        <asp:HyperLink ID="HyperLink1"  runat="server" NavigateUrl="../Login.aspx" Target="_self"  
            Text="กดปุ่มเพื่อย้อนกลับไปหน้า Login." Style="text-decoration: underline; color: #CC33FF"></asp:HyperLink></h3>
    </h3>
</body>
</html> 