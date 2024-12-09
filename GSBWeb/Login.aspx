<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="GSBWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/style.css">
    <script>
        window.history.forward();
        function noBack() { window.history.forward(); }
        window.onload = 'noBack()';
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
    
    </script>

</head>--%>

    <head id="Head1" runat="server">                  
    
        <%--<link href="Styles/bootstrap.min.css" rel="stylesheet" />--%>
        <link href="Styles/bootstrap_Tahoma.min.css" rel="Stylesheet" />

        <link href="Styles/Style_login.css" rel="stylesheet" type="text/css" />
        
        <%--<script src="Scripts/bootstrap.min_login.js" type="text/javascript"></script>--%>
        <%--<script src="Scripts/jquery-1.10.2.min_Login.js" type="text/javascript"></script>--%>
       
        <script>
               window.history.forward();
               function noBack() { window.history.forward(); }
               window.onload = 'noBack()';
               window.onpageshow = function (evt) { if (evt.persisted) noBack() }
               window.onunload = function () { void (0) }

               var ALERT_TITLE = "Error";
               var ALERT_BUTTON_TEXT = "ปิด";

               if (document.getElementById) {
                   window.alert = function (txt) {
                       createCustomAlert(txt);
                   }
               }

               function createCustomAlert(txt) {
                   d = document;

                   if (d.getElementById("modalContainer")) return;

                   mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
                   mObj.id = "modalContainer";
                   mObj.style.height = d.documentElement.scrollHeight + "px";
                   

                   alertObj = mObj.appendChild(d.createElement("div"));
                   alertObj.id = "alertBox";
                   if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
                   alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
                   alertObj.style.visiblity = "visible";

                   h1 = alertObj.appendChild(d.createElement("h1"));
                   h1.appendChild(d.createTextNode(ALERT_TITLE));

                   divh = alertObj.appendChild(d.createElement("div"));
                   divh.style.height = "30px";
                   divh.innerHTML = "&nbsp;"

                   panels = alertObj.appendChild(d.createElement("center"));
                   
                   images = panels.appendChild(d.createElement("img"));
                   images.src = "Images/NotCorrect.png";
                   images.style.width = "100px";
                   images.style.height = "100px";

                   msg = alertObj.appendChild(d.createElement("p"));
                   //msg.appendChild(d.createTextNode(txt));
                   msg.innerHTML = txt;

                   

                   btn = alertObj.appendChild(d.createElement("a"));
                   btn.id = "closeBtn";
                   btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
                   btn.href = "#";
                   btn.focus();
                   btn.onclick = function () { removeCustomAlert(); return false; }

                   divh = alertObj.appendChild(d.createElement("div"));
                   divh.style.height = "30px";
                   divh.innerHTML = "&nbsp;";
                   alertObj.style.display = "block";

               }

               function removeCustomAlert() {
                   document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
               }
               function ful() {
                   alert('Alert this pages');
               }
        </script>
        <style>
           
           #modalContainer {
	            background-color:rgba(0, 0, 0, 0.3);
	            position:absolute;
              top:0;
	            width:100%;
	            height:100%;
	            left:0px;
	            z-index:10000;
	            background-image:url(tp.png); /* required by MSIE to prevent actions on lower z-index elements */
            }

            #alertBox {
	            position:relative;
	            width:33%;
	            min-height:100px;
              max-height:400px;
	            margin-top:50px;
	            border:1px solid #fff;
	            background-color:#fff;
	            background-repeat:no-repeat;
              top:30%;
            }

            #modalContainer > #alertBox {
	            position:fixed;
            }

            #alertBox h1 {
	            margin:0;
	            font:bold 1em Raleway,arial;
	            background-color:#CCCCCC;/*f97352*/
	            color:#000;
	            border-bottom:1px solid #CCCCCC;
	            padding:10px 0 10px 5px;
            }

            #alertBox p {
	            height:50px;
	            padding-left:5px;
              padding-top:30px;
              text-align:center;
              vertical-align:middle;
            }

            #alertBox #closeBtn {
	            display:block;
	            position:relative;
	            margin:10px auto 10px auto;
	            padding:7px;
	            border:0 none;
	            width:70px;
	            text-transform:uppercase;
	            text-align:center;
	            color:#FFF;
	            background-color:#FF388C;/*f97352*/
	            border-radius: 0px;
	            text-decoration:none;
              outline:0!important;
            }

            /* unrelated styles */

            #mContainer {
	            position:relative;
	            width:400px;
	            margin:auto;
	            padding:5px;
	            border-top:2px solid #fff;
	            border-bottom:2px solid #fff;
            }

            h1,h2 {
	            margin:0;
	            padding:4px;
            }

            code {
	            font-size:1.2em;
	            color:#069;
            }

            #credits {
	            position:relative;
	            margin:25px auto 0px auto;
	            width:350px; 
	            font:0.7em verdana;
	            border-top:1px solid #000;
	            border-bottom:1px solid #000;
	            height:90px;
	            padding-top:4px;
            }

            #credits img {
	            float:left;
	            margin:5px 10px 5px 0px;
	            border:1px solid #000000;
	            width:80px;
	            height:79px;
            }

            .important {
	            background-color:#F5FCC8;
	            padding:2px;

            }

            @media (max-width: 400px) 
            {
              #alertBox {
	            position:relative;
	            width:90%;
              top:30%;
            }
        </style>
    </head>
<body>
     <div class="container">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <h1 class="text-center login-title"></h1>
                <div class="account-wall">
                    <label class="login-title">ระบบรายงานฐานข้อมูลเพื่อบริหารความเสี่ยง</label>  
                    <br/>
                    <label class="login-title">ตามมาตรฐาน TFRS9</label>  
                    <%--<label style="font-family:Tahoma;color:yellow;font-size: 18px;font-weight: 300;display: block;text-align: center;font-style: normal;">ระบบรายงานฐานข้อมูลเพื่อบริหารความเสี่ยง</label>--%> 
                    <%-- --%>               
                    <img class="profile-img" src="Images/Logo_GSB.png" alt="" />
                    <form class="form-signin" runat="server" id="form_login">
                    <%-- <asp:Label ID="label_warning" style="font-color:red;vertical-align:middle;" runat="server" type="text" 
                        class="label label-warning" Height="50px" Visible="False" ></asp:Label> --%>
                        <asp:TextBox ID="txtUser" runat="server" autocomplete="off" type="text" class="form-control" value="" placeholder="ชื่อผู้ใช้" MaxLength="30"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" autocomplete="off"  runat="server" type="password" class="form-control" value="" placeholder="รหัสผ่าน" MaxLength="50"></asp:TextBox>
                        <asp:Button ID="btnLogin" runat="server" type="submit" class="btn btn-lg btn-primary btn-block"  Text="เข้าระบบ" />
                    </form>  
                </div>           
            </div>
        </div>

    </div>

 <asp:Label ID="Result" runat="server" style="display:none;"></asp:Label>
</body>
</html>
