<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SessionExpired_BK.aspx.vb" Inherits="GSBWeb.Expired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            /*font-family: Arial;*/
            font-family: Tahoma;
            font-size: 10pt;
        }
        .style1
        {
            font-size: large;
        }
    </style>

    <script type ="text/javascript">

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
 

</head>
<body onkeydown="cancelBack()">

<form id="form1"  runat="server" data-toggle="validator">
 



            <div class="container-fluid ">
          
                <div class="row">
                    <!-- header area start -->
                    <div class="headerStyle" style="background-color: #EB058C; text-align: right; height: 120px;
                        margin-top: 0; margin-left: 0; padding-left: 0; padding-top: 0">
                        <img class="img-responsive" src="<%= ResolveUrl("~/Images/Logo_GSB.png")%>" align="left" style="text-align: left;
                            vertical-align: top; margin-bottom: 0; margin-top: 0; padding-top: 0; padding-bottom: 0;
                            width: 120px; height: 120px" />
                        <asp:Label ID="Label1" runat="server" align="right" Text="ระบบรายงานฐานข้อมูลเพื่อบริหารความเสี่ยง"
                            Font-Bold="True" Font-Size="xx-Large" ForeColor="White" Style="text-align: right;
                            vertical-align: top"></asp:Label>
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
            
  

                         <h1>

                             Session Expired</h1>
    <p class="style1">

                             Your Session has expired&nbsp;&nbsp;
                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://rdmdevweb.gsb/AmbitReportingsuiteweb/" Target="_self" 
                                 Text="Please return to the home page." 
                                 style="text-decoration: underline; color: #CC33FF"></asp:HyperLink>
            </p>
            
  

         </form>

                         </body>
</html>
