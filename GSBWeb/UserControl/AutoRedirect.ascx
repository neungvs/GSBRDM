<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AutoRedirect.ascx.vb" Inherits="GSBWeb.AutoRedirect" %>
<script type="text/javascript">
    var timeRefresh;
    var timeInterval;
    var currentTime;
    var expressTime;
    var ckey;
    var csession;

    expressTime = "<%=ExpressDate %>";
    currentTime = "<%=LoginDate %>";
    ckey = "<%=CKey%>";

    //window.alert(ckey);
    csession = encode(ckey, expressTime);
    //window.alert(csession);
    //window.alert( decode(ckey, csession));
    //document.write(csession+"<BR>");

    //setCookie("express", expressTime);
    setCookie("express", csession);
    timeRefresh = setInterval("Refresh()", 1000);

    // Refresh this page to check session is expire or timeout.
    function Refresh() {
        var current = decode(ckey, getCookie("express")); //getCookie("express");
        var date = current.split(" ")[0];
        var time = current.split(" ")[1];
        var scriptDate = new Date();
        var year = scriptDate.getFullYear();
        var month = scriptDate.getMonth() + 1;
        var day = scriptDate.getDate();
        var hour = scriptDate.getHours();
        var min = scriptDate.getMinutes();
        var second = scriptDate.getSeconds();
        if (Date.UTC(year, month, day, hour, min, second) >=
           Date.UTC(date.split("-")[0], date.split("-")[1], date.split("-")[2],
           time.split(":")[0], time.split(":")[1], time.split(":")[2])) {
            clearInterval(timeRefresh);
            Redirect();
        }
    }


    function Redirect() {
        window.location.replace("../Login.aspx");
        //var _url = window.location.origin ? window.location.origin + '/' : window.location.protocol + '/' + window.location.host + '/';
        //return _url + "Login.aspx";

    }

    function getRootUrl() {

        return window.location.origin ? window.location.origin + '/' : window.location.protocol + '/' + window.location.host + '/';

    }

    // Retrieve cookie by name.
    function getCookie(name) {
        var arg = name + "=";
        var aLen = arg.length;
        var cLen = document.cookie.length;
        var i = 0;
        while (i < cLen) {
            var j = i + aLen;
            if (document.cookie.substring(i, j) == arg) {
                return getCookieVal(j);
            }
            i = document.cookie.indexOf(" ", i) + 1;
            if (i == 0) break;
        }
        return;
    }



    function getCookieVal(offSet) {
        var endStr = document.cookie.indexOf(";", offSet);
        if (endStr == -1) {
            endStr = document.cookie.length;
        }
        return unescape(document.cookie.substring(offSet, endStr));
    }

    // Assign values to cookie variable.
    function setCookie(name, value) {
        document.cookie = name + "=" + escape(value);
    }


    function encode(cryptonkey,cdata) {
        var ckey = cryptonkey; //$("#cryptonkey").val();
        var y = cdata; //document.getElementById(control).value;
        var encrypted = CryptoJS.AES.encrypt(y, ckey);
        //document.getElementById("crypton").value = encrypted;
        //window.alert(encrypted);
        return encrypted;
    }

    function decode(cryptonkey,cdata) {
        var ckey = cryptonkey; //$("#cryptonkey").val();
        var y = cdata; //document.getElementById(control).value;
        var decrypted = CryptoJS.AES.decrypt(y, ckey);
        decrypted = decrypted.toString(CryptoJS.enc.Utf8);
        //document.getElementById("cryptoff").value = decrypted;
        //window.alert(decrypted);
        return decrypted;
    }
   
</script>