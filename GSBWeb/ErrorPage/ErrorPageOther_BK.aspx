﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ErrorPageOther_BK.aspx.vb" Inherits="GSBWeb.ErrorPageOther" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script src="<%= Page.ResolveClientUrl("~/Scripts/aes.js")%>"
        type="text/javascript"></script>

    <script src="Scripts/aes.js"></script>

    <title>RDM_Report</title>
    <%--    <asp:ContentPlaceHolder ID="head" runat="server">
    </asp:ContentPlaceHolder>--%>
    <style type="text/css">
        @font-face {
            /*font-family: 'DBHelvethaicaXMedCondv3_2';*/
            font-family: Tahoma;
            /*src: url('../fonts/DBHelvethaicaXMedCondv3_2.woff') format('woff');*/
        }

        html, body {
            /*font-family: 'DBHelvethaicaXMedCondv3_2';*/
            font-family: Tahoma;
            height: 100%;
        }


        .paddingmarginZero {
            padding: 0;
            margin: 0;
        }


        .headerStyle {
            font-family: Tahoma;
            background-color: #EC068D;
            padding: 20px 20px 30px 20px;
            height: 200;
            /*font-family: 'DBHelvethaicaXMedCondv3_2';*/
        }

        .sidebarStyle {
            background-color: #A7D97E;
            height: Auto;
            text-align: center;
        }

        .contentAreaStyle {
            background-color: #FFFFFF;
            height: Auto !important;
            text-align: center;
            position: relative;
            min-height: 440px;
            max-height: 100%;
        }


        .bodyWrap {
            width: 100%;
            height: auto;
        }


        .header, .content, .footer {
            width: 100%;
        }


        .modal_Pross {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center_Pross img {
            height: 128px;
            width: 128px;
        }


        .wrapper {
            min-height: 100%;
            height: auto !important;
            height: 100%;
            margin: 0 auto -142px; /* the bottom margin is the negative value of the footer's height */
        }

        .page-wrap {
            min-height: 100%; /* equal to footer height */
            margin-bottom: -142px;
        }

            .site-footer, .page-wrap:after {
                height: 60px;
                position: relative;
                bottom: 0px;
                background-color: #FFFFFF;
                width: 100%;
                margin: 0px;
                padding: 0px;
            }



        #footer .content p {
            margin-bottom: 0;
        }

        .labelheader {
            font-family: Tahoma;
            font-weight: normal;
            /*font-family: DBHelvethaicaXMedCondv3_2;*/
            font-size: 350%;
            color: White;
        }

        a {
            font-weight: normal;
            /*font-family: monospace;*/
            font-family: Tahoma;
            font-size: 100%;
        }
    </style>

    <script type="text/javascript">

        window.history.forward();
        function noBack() { window.history.forward(); }
        window.onload = 'noBack()';
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }

        $(document).ready(function () {

            $.datepicker.regional['th'] = {
                changeMonth: true,
                changeYear: true,
                //defaultDate: GetFxupdateDate(FxRateDateAndUpdate.d[0].Day),
                yearOffSet: 543,
                showOn: "button",
                buttonImage: '../images/calendar.gif',
                buttonImageOnly: true,
                showButtonPanel: true,
                closeText: 'ยกเลิก',
                currentText: 'วันนี้',
                buttonText: 'เลือกวันที่',
                dateFormat: 'dd/mm/yy',
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                //constrainInput: true,
                prevText: 'ก่อนหน้า',
                nextText: 'ถัดไป',
                yearRange: '-20:+20',
                beforeShow: function (input, inst) {
                    $(this).datepicker('option', 'defaultDate', new Date());
                }
            };

            $.datepicker.setDefaults($.datepicker.regional['th']);

            /* This would select second division only*/
            $(".datePic").datepicker($.datepicker.regional["th"]); // Set ภาษาที่เรานิยามไว้ด้านบน 

            //setButtonDateToday
            jQuery.datepicker._gotoToday = function (id) {
                var today = new Date();
                var dateRef = jQuery("<td><a>" + today.getDate() + "</a></td>");
                this._selectDay(id, today.getMonth(), today.getFullYear(), dateRef);
            };

            function pageLoad() {
                $(".datePic").datepicker($.datepicker.regional["th"]); // Set ภาษาที่เรานิยามไว้ด้านบน

                //setButtonDateToday
                jQuery.datepicker._gotoToday = function (id) {
                    var today = new Date();
                    var dateRef = jQuery("<td><a>" + today.getDate() + "</a></td>");
                    this._selectDay(id, today.getMonth(), today.getFullYear(), dateRef);
                };

            }


        });

    </script>
</head>
<body>
    <form id="form1" runat="server" style="height: 100%" data-toggle="validator">
        <div class="container-fluid ">
            <div class="row">
                <!-- header area start -->
                <div class="headerStyle" style="background-color: #EB058C; text-align: right; height: 120px; margin-top: 0; margin-left: 0; padding-left: 0; padding-top: 0">
                    <img class="img-responsive" src="<%= ResolveUrl("~/Images/Logo_GSB.png")%>" align="left"
                        style="text-align: left; vertical-align: top; margin-bottom: 0; margin-top: 0; padding-top: 0; padding-bottom: 0; width: 120px; height: 120px" />
                    <asp:Label ID="Label1" runat="server" class="labelheader" align="right" Text="ระบบรายงานฐานข้อมูลเพื่อบริหารความเสี่ยง"
                        Style="text-align: right; vertical-align: top;"></asp:Label>
                </div>

                <!-- menubar area end -->
                <div class="contentAreaStyle">

                    <h3 style="margin-left: 10px">พบข้อผิดพลาดกรุณาติดต่อเจ้าหน้าที่ 
                             <asp:HyperLink ID="HyperLink1" runat="server"
                                 NavigateUrl="../Login.aspx" Target="_self"
                                 Text="กดปุ่มเพื่อย้อนกลับไปหน้า Login."
                                 Style="text-decoration: underline; color: #CC33FF"></asp:HyperLink></h3>
                    <%--                 <asp:ContentPlaceHolder ID="BodyContent" runat="server"> 
                </asp:ContentPlaceHolder>--%>
                </div>
            </div>
        </div>

    </form>


</body>
</html>

