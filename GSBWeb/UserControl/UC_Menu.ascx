<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Menu.ascx.vb" Inherits="GSBWeb.UC_Menu" %>
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
    <asp:Panel ID="pnlMenu" runat="server"/>
    
<%--  <ul class="nav navbar-nav" style="font-weight: bold;">
        <li id="ligl" runat="server"><a href="<%= Page.ResolveClientUrl("~/RDM_Reports/RDM_Report.aspx")%>" style="text-decoration: inherit">RDM REPORT</a></li>
        <li id="ddAj" class="dropdown" runat="server">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="text-decoration: inherit">WEBADJUST<b class="caret"></b></a>
            <ul class="dropdown-menu">
                <li class="dropdown" id="liAj" runat="server">
                    <a href="<%= Page.ResolveClientUrl("~/WebAdjust/DataAdjustment.aspx")%>" style="text-decoration: inherit">ADJUSTMENT</a>
                </li>
                <li id="liSt" class="dropdown" runat="server">

                    <a href="<%= Page.ResolveClientUrl("~/Setting/SettingAmount.aspx")%>" style="text-decoration: inherit">ตั้งค่าระบบ</a>

                </li>

            </ul>
        </li>
 
        <li id="li" class="dropdown" runat="server">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="text-decoration: inherit">CPMWEBADJUST<b class="caret"></b></a>
            <ul class="dropdown-menu">
                <li class="dropdown" id="liId" runat="server">
                    <a href="<%= Page.ResolveClientUrl("~/IndustryLimit/IndustryLimit.aspx")%>" style="text-decoration: inherit">INDUSTRYLIMIT </a>
                </li>
                <li id="li3" class="dropdown" runat="server">

                    <a href="<%= Page.ResolveClientUrl("~/ReportViewer/Report.aspx?ReportURL=/RDM_Report/Report_CPM_SLL")%>" style="text-decoration: inherit">จัดอันดับลูกหนี้ ( SLL)</a>

                </li>

            </ul>
        </li>


        <li id="liambit" runat="server"><a href="<%= Page.ResolveClientUrl("~/AmbitReportingsuiteweb/AmbitReport.aspx")%>" style="text-decoration: inherit">AMBITREPORTINGSUITEWEB</a></li>
                   
        <li id="lirdweb" runat="server"><a href="<%= Page.ResolveClientUrl("~/RDWeb/RDWeb.aspx")%>" style="text-decoration: inherit">RDWeb</a></li>
        <li id="lipe" runat="server"><a href="<%= Page.ResolveClientUrl("~/Periods/RDM_Periods.aspx")%>" style="text-decoration: inherit">กำหนดงวด</a></li>
                    
    </ul>
--%>
    <%--<ul class="nav navbar-nav navbar-right">
        <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="text-decoration: inherit">
                <span class="glyphicon glyphicon-user"></span>&nbsp;&nbsp;
    <strong>User</strong>
                <span class="glyphicon glyphicon-chevron-down"></span>
            </a>
            <ul class="dropdown-menu">
                <li>
                    <div class="navbar-login">
                        <div class="row">
                            <div class="col-lg-4">
                                <p class="text-center">
                                    <span class="glyphicon glyphicon-user icon-size"></span>
                                </p>
                            </div>
                            <div>
                                <asp:Label ID="lblUsername" runat="server" Text="Label"></asp:Label>


                            </div>
                        </div>
                    </div>
                </li>
                <li class="divider navbar-login-session-bg" style="text-decoration: inherit"></li>
                <li><a href="<%= Page.ResolveClientUrl("~/Login.aspx?info=0")%>" style="text-decoration: inherit">Sign Out <span class="glyphicon glyphicon-log-out pull-right"></span></a></li>


            </ul>

        </li>

    </ul>--%>

    
</div><!-- /.navbar-collapse -->
