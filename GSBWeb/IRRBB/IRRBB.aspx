<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="IRRBB.aspx.vb" Inherits="GSBWeb.IRRBB" %>

<%@ Register Src="~/UserControl/AutoRedirect.ascx" TagName="AutoRedirect" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 7px;
        }

        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            /*style="text-align:left;Height:40px"*/
        }

            /* Style the buttons that are used to open the tab content */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
        }
    </style>

    <script type="text/javascript">

        function ChangeText(oFileInput, sTargetID) {
            document.getElementById(sTargetID).value = oFileInput.files[0].name;
            //            console.log(oFileInput.files[0].name);
        }

        function UploadClick() {
            document.getElementById("BodyContent_divPage1").hidden = false;
            document.getElementById("BodyContent_divPage2").hidden = true;
            document.getElementById("BodyContent_divPage3").hidden = true;
            document.getElementById("BodyContent_divPage4").hidden = true;
            document.getElementById("BodyContent_divPage5").hidden = true;
            sessionStorage.setItem('Page', "1");
        }

        function ParameterClick() {



            document.getElementById("BodyContent_divPage1").hidden = true;
            document.getElementById("BodyContent_divPage2").hidden = false;
            document.getElementById("BodyContent_divPage3").hidden = true;
            document.getElementById("BodyContent_divPage4").hidden = true;
            document.getElementById("BodyContent_divPage5").hidden = true;
            sessionStorage.setItem('Page', "2");
        }

        function Parameter2Click() {

            //var m_value = document.getElementById("<%=ddlMonth.ClientID%>");  
            //var y_value = document.getElementById("<%=ddlYear.ClientID%>");  
            //var m_getvalue = m_value.options[m_value.selectedIndex].value;  
            //var y_getvalue = y_value.options[y_value.selectedIndex].value;  
            //document.getElementById("<%=DdlMonthStart.ClientID%>").value = m_getvalue
            //document.getElementById("<%=DdlYearStart.ClientID%>").value = y_getvalue
            //document.getElementById("<%=DdlMonthEnd.ClientID%>").value = m_getvalue
            //document.getElementById("<%=DdlYearEnd.ClientID%>").value = y_getvalue

            //document.getElementById("<%=DdlMonthStart.ClientID%>").disabled = true;
            //document.getElementById("<%=DdlYearStart.ClientID%>").disabled = true;

            document.getElementById("BodyContent_divPage1").hidden = true;
            document.getElementById("BodyContent_divPage2").hidden = true;
            document.getElementById("BodyContent_divPage3").hidden = false;
            document.getElementById("BodyContent_divPage4").hidden = true;
            document.getElementById("BodyContent_divPage5").hidden = true;
            sessionStorage.setItem('Page', "3");
        }

        function ReportClick() {
            document.getElementById("BodyContent_divPage1").hidden = true;
            document.getElementById("BodyContent_divPage2").hidden = true;
            document.getElementById("BodyContent_divPage3").hidden = true;
            document.getElementById("BodyContent_divPage4").hidden = false;
            document.getElementById("BodyContent_divPage5").hidden = true;
            sessionStorage.setItem('Page', "4");
        }

        function TemplateClick() {

            document.getElementById("BodyContent_divPage1").hidden = true;
            document.getElementById("BodyContent_divPage2").hidden = true;
            document.getElementById("BodyContent_divPage3").hidden = true;
            document.getElementById("BodyContent_divPage4").hidden = true;
            document.getElementById("BodyContent_divPage5").hidden = false;

            $(".datePic").datepicker($.datepicker.regional["th"]);

            sessionStorage.setItem('Page', "5");
        }

        function OpenPopup(Report) {
            popup(Report);

        }

        function pageLoad() {
            $(".datePic").datepicker($.datepicker.regional["th"]);
        }

        function popup(url) {
            var width = 450;
            var height = 250;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ', height=' + height;
            params += ', top=' + top + ', left=' + left;
            params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', addressbar=no';
            params += ', scrollbars=no';
            params += ', status=no';
            params += ', toolbar=no';
            newwin = window.open(url, '_blank', params);
            //if (window.focus) { newwin.focus() }
            return false;
        }

        $(document).ready(function () {

            var a = sessionStorage.getItem('Page');

            if (a == "1") {

                UploadClick();
            }
            else if (a == "2") {
                ParameterClick();
            }
            else if (a == "3") {
                Parameter2Click();
            }
            else if (a == "4") {
                ReportClick();
            }

            document.getElementById('<%= TxtHeading.ClientID %>').disabled = true;
            document.getElementById('<%= chEdit.ClientID %>').checked = false;

            //$("#progressbar").progressbar({ value: 50 }); 
            //var intervalID = setInterval(updateProgress, 1000);



        });

        function startProcess() {
            var intervalID = setInterval(updateProgress, 1000);
        }

        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "value");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);

            }
        }




        //function parasave()
        //{
        //    var fu = document.getElementById("BodyContent_FUInput")
        //    var _reportunit = document.getElementById("BodyContent_ddlCurrency").value
        //    var _graphbackward = document.getElementById("BodyContent_DdlMonthBack").value
        //    var _pathreport = fu.files[0].name;
        //    var _path = fu.value;
        //    var _niimonth = document.getElementById("BodyContent_TxtEffect").value
        //    var _reportheading = document.getElementById("BodyContent_TxtHeading").value
        //    PageMethods.ParaSave(OnSuccess);
        //}

        //function OnSuccess(response)
        //{
        //    C(response);
        //}


        function CheckCFO(rbn) {
            if (rbn == "CFO") {

                document.getElementById('<%= TxtOtherSource.ClientID %>').value = ""
                document.getElementById('<%= TxtNumOtherSource.ClientID %>').value = ""

            }
            else if (rbn == "Other") {

                document.getElementById('<%= TxtNumCFO.ClientID %>').value = ""


            }
            return false;
        }

        function EnableTxt() {

            if (document.getElementById('<%= chEdit.ClientID %>').checked) {

                document.getElementById('<%= TxtHeading.ClientID %>').disabled = false;
            }
            else {

                document.getElementById('<%= TxtHeading.ClientID %>').disabled = true;
            }

            return false;
        }

        function showalert() {

            $('#AlertBox').modal();
            return false;
        }


        function hidealert() {

            $('#AlertBox').modal('hide');
            return false;
        }

        function test() {


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <%--<uc1:AutoRedirect ID="AutoRedirect" runat="server" />--%>


    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" AsyncPostBackTimeout="5000000"></asp:ScriptManager>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-md-8 col-md-offset-2">
                    <div class="NormalHeader" style="/*text-align: left; font-weight: normal; font-size: 18pt; background: #FF388C; padding-right: 5; color: #FFFFFF; */height: 30px;">
                        IRRBB
                    </div>
                    <br />
                    <div style="text-align: left; height: 40px" class="tab">
                        <button type="button" class="tablinks" id="LPage1" onclick="UploadClick();">Upload File</button>
                        <button type="button" class="tablinks" id="LPage2" onclick="ParameterClick();">ตั้งค่า Parameter</button>
                        <button type="button" class="tablinks" id="LPage3" onclick="Parameter2Click();">บันทึกข้อมูลความเสี่ยงอัตราดอกเบี้ย</button>
                        <button type="button" class="tablinks" id="LPage4" onclick="ReportClick();">ผลรายงาน</button>
                        <button type="button" class="tablinks" id="LPage5" onclick="TemplateClick();">Upload & Download Template</button>
                        <%--                        <asp:LinkButton ID="LPage1" runat="server" BackColor="Pink" ForeColor="Black" Text="Upload File" Height="40px" Width="120px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Underline="true" Style="text-align: center; vertical-align: middle; display: table-cell;"></asp:LinkButton>
                        <asp:LinkButton ID="LPage2" runat="server" BackColor="Gray" ForeColor="White" Text="ตั้งค่า Parameter" Height="40px" Width="120px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" Style="text-align: center; vertical-align: middle; display: table-cell;"></asp:LinkButton>
                        <asp:LinkButton ID="LPage3" runat="server" BackColor="Gray" ForeColor="White" Text="บันทึกข้อมูลเพื่อคำนวณค่า NII" Height="40px" Width="120px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" Style="text-align: center; vertical-align: middle; display: table-cell;"></asp:LinkButton>
                        <asp:LinkButton ID="LPage4" runat="server" BackColor="Gray" ForeColor="White" Text="ผลรายงาน" Height="40px" Width="120px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" Style="text-align: center; vertical-align: middle; display: table-cell;"></asp:LinkButton>--%>
                    </div>
                    <div style="text-align: left; height: 800px" id="divPage1" runat="server" visible="true">
                        <div style="text-align: left; font-size: xx-large">
                            Upload File
                        </div>
                        <br />
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-3" style="text-align: right" for="ddlMonth">เดือน</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                        <asp:ListItem Enabled="true" Text="เลือกเดือน" Value=""></asp:ListItem>
                                        <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMonth" ErrorMessage="กรุณาเลือกเดือน"
                                        SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <label class="control-label col-md-1" style="text-align: right" for="ddlYear">ปี</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlYear" runat="server" class="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlYear" ErrorMessage="กรุณาเลือกปี"
                                        SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">Upload รายงานที่ออกจากระบบ ALM</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="txtATM1">ALM 1</label>
                                        <div class="col-md-3">

                                            <asp:TextBox ID="txtATM1" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:FileUpload runat="server" ID="fuATM1" class="form-control" Style='display: none' onchange="ChangeText(this, 'BodyContent_txtATM1');" />
                                            <%--<asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" OnClick = "Async_Upload_File" />--%>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="fuATM1" ErrorMessage="กรุณาเลือกไฟล์"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="validateUploadData" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
                                                ControlToValidate="fuATM1" runat="server" ForeColor="Red" ErrorMessage="Format File ไม่ถูกต้อง" Display="Dynamic" />

                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnATM1" Text="Browse File" class="form-control" OnClientClick="javascript:document.getElementById('BodyContent_fuATM1').click(); return false;" />

                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="txtATM2">ALM 2</label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtATM2" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:FileUpload runat="server" ID="fuATM2" class="form-control" Style='display: none' onchange="ChangeText(this, 'BodyContent_txtATM2');" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="fuATM2" ErrorMessage="กรุณาเลือกไฟล์"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="validateUploadData" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
                                                ControlToValidate="fuATM2" runat="server" ForeColor="Red" ErrorMessage="Format File ไม่ถูกต้อง" Display="Dynamic" />
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnATM2" Text="Browse File" class="form-control" OnClientClick="javascript:document.getElementById('BodyContent_fuATM2').click(); return false;" />
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">Upload รายงานที่มีการปรับยอดแล้ว (Adjusted)</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="txtATM1Adjusted">ALM 1_Adjusted*</label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtATM1Adjusted" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:FileUpload runat="server" ID="fuATM1Adjusted" class="form-control" Style='display: none' onchange="ChangeText(this, 'BodyContent_txtATM1Adjusted');" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="fuATM1Adjusted" ErrorMessage="กรุณาเลือกไฟล์"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="validateUploadData" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
                                                ControlToValidate="fuATM1Adjusted" runat="server" ForeColor="Red" ErrorMessage="Format File ไม่ถูกต้อง" Display="Dynamic" />
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnATM1Adjusted" Text="Browse File" class="form-control" OnClientClick="javascript:document.getElementById('BodyContent_fuATM1Adjusted').click(); return false;" />
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="txtATM2Adjusted">ALM 2_Adjusted*</label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtATM2Adjusted" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:FileUpload runat="server" ID="fuATM2Adjusted" class="form-control" Style='display: none' onchange="ChangeText(this, 'BodyContent_txtATM2Adjusted');" />
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator6" runat="server" ControlToValidate="fuATM2Adjusted" ErrorMessage="กรุณาเลือกไฟล์"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="validateUploadData" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
                                                ControlToValidate="fuATM2Adjusted" runat="server" ForeColor="Red" ErrorMessage="Format File ไม่ถูกต้อง" Display="Dynamic" />
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnATM2Adjusted" Text="Browse File" class="form-control" OnClientClick="javascript:document.getElementById('BodyContent_fuATM2Adjusted').click(); return false;" />
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnUpload" Text="Upload" ValidationGroup="validateUploadData" class="form-control" OnClick="btnUpload_Click" />

                                </div>
                                <div class="col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="BtnCancel" Text="Cancel" class="form-control" />
                                </div>
                                <div class="col-md-3">
                                    &nbsp;
                                    <%--<asp:Label ID="LblProcess" runat="server" Style="font-size: medium;" Text="test" />--%>
                                </div>
                            </div>
                        </div>
                        <hr style="border: 1px solid black;" />
                        <br />
                        <div style="text-align: left; font-size: xx-large">
                            สถานะการ Upload File
                        </div>
                        <br />
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-3" style="text-align: right" for="ddlMonthStatus">เดือน</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlMonthStatus" runat="server" class="form-control">
                                        <asp:ListItem Enabled="true" Text="เลือกเดือน" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="control-label col-md-1" style="text-align: right" for="ddlYearStatus">ปี</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlYearStatus" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="BthSearchStatus" Text="Search" class="form-control" />

                                </div>
                                <div class="col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="Button2" Text="Cancel" class="form-control" />
                                </div>
                                <div class="col-md-3">
                                    &nbsp;
                                </div>

                            </div>



                        </div>

                        <asp:GridView ID="gvTable" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" Visible="true" PageSize="10">
                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Height="30px" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่ Upload">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessDate" Text='<%#Eval("PROCESS_DATE") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายงานประจำเดือน">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataAsof" Text='<%#Eval("DATA_ASOF") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PROCESS_BYNAME" HeaderText="ผู้ดำเนินการ">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROCESS_STATUS" HeaderText="สถานะ">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DIFF_FILE" HeaderText="DIFF" Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BthDownloadDiff" Text="Download" runat="server" Visible='<%#(Not IsDBNull(Eval("DIFF_FILE")) And Not Eval("DIFF_FILE") = "") %>' CommandName="Download" CommandArgument='<%#Eval("DATA_ASOF") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                    </div>


                    <div style="text-align: left; height: 800px" id="divPage2" runat="server" hidden="hidden">
                        <div class="panel panel-default">
                            <div class="panel-heading">หน่วยที่แสดงรายงาน</div>
                            <div class="panel-body">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="currency">หน่วย </label>
                                        <div class="col-md-3">
                                            <%--<asp:TextBox ID="txtCurreny" runat="server" class="form-control"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlCurrency" runat="server" class="form-control">
                                                <asp:ListItem Text="ล้านบาท" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="บาท" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="control-label" style="text-align: right" for="currency">สำหรับรายงาน ALM1 และ ALM2</label>
                                        <div class="col-md-6">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="panel-heading">กราฟเปรียบเทียบผลกระทบการเปลี่ยนแปลงอัตราดอกเบี้ยต่อรายได้ดอกเบี้ยสุทธิ</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label">กราฟที่ต้องการแสดงย้อนหลัง</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="DdlMonthBack" runat="server" class="form-control">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="panel-heading">ผลกระทบจากการปรับอัตราดอกเบี้ยของธนาคาร</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-1">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-4" style="text-align: right">
                                            <label class="control-label">ผลกระทบการคำนวณ NII ในอีก</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="TxtEffect" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label">เดือน</label>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="panel-heading">หัวรายงาน</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="txtNumOtherSoucre">หัวรายงาน</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="TxtHeading" runat="server" class="form-control" disabled="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:CheckBox ID="chEdit" runat="server" onchange="EnableTxt();" Text="Edit" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="BthUploadInput" Text="Save" class="form-control" OnClick="BthUploadInput_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="BthCancelInput" Text="Cancel" class="form-control" OnClick="BthCancelInput_Click" />
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%--</fieldset>--%>
                        </div>


                    </div>



                    <div style="text-align: left; height: 800px" id="divPage3" runat="server" hidden="hidden">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-group">
                                    <label class="control-label col-md-3" style="text-align: right" for="ddlmonthnii">เดือน</label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlmonthnii" runat="server" class="form-control">
                                            <asp:ListItem Enabled="true" Text="เลือกเดือน" Value=""></asp:ListItem>
                                            <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlMonthnii" ErrorMessage="กรุณาเลือกเดือน"
                                            SetFocusOnError="True" Display="Dynamic" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <label class="control-label col-md-1" style="text-align: right" for="ddlYearnii">ปี</label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlYearnii" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlYear" ErrorMessage="กรุณาเลือกปี"
                                            SetFocusOnError="True" Display="Dynamic" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button runat="server" ID="BthShowNii" Text="แสดงข้อมูล" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="panel-heading">เกณฑ์เพดานความเสี่ยง</div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-4" style="text-align: right">
                                        <label class="control-label">สัญญานเตือนระดับความเสี่ยง ร้อยละ</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtAlertRisk" runat="server" class="form-control" onkeypress="return (event.charCode >= 48 && event.charCode <= 57) || event.charCode == 46"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">&nbsp;</div>
                                    <div class="col-md-2">&nbsp;</div>
                                    <div class="col-md-2">&nbsp;</div>
                                    <div class="col-md-4" style="text-align: right">
                                        <label class="control-label">เพดานความเสี่ยง ร้อยละ</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtCeilingRisk" runat="server" class="form-control" onkeypress="return (event.charCode >= 48 && event.charCode <= 57) || event.charCode == 46"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>

                            <div class="panel-heading">แหล่งข้อมูลรายได้สุทธิ</div>
                            <div class="panel-body">

                                <div class="form-group">
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-2">
                                        <label class="radio-inline">
                                            <asp:RadioButton runat="server" ID="RbnCFO" GroupName="CFO" Checked="true" onchange="CheckCFO('CFO');" />รายงาน CFO</label>
                                    </div>
                                    <div class="col-md-2">
                                        &nbsp;
                                                <%--<label class="control-label" for="txtNumCFO">จำนวน</label>--%>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="txtNumCFO">จำนวน</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtNumCFO" runat="server" class="form-control" onkeypress="return (event.charCode >= 48 && event.charCode <= 57) || event.charCode == 46"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="UnitMillion">ล้านบาท</label>
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-2">
                                        <label class="radio-inline">
                                            <asp:RadioButton runat="server" ID="RbnOther" GroupName="CFO" onchange="CheckCFO('Other');" />แหล่งที่มาอื่น</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtOtherSource" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="txtNumOtherSoucre">จำนวน</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtNumOtherSource" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57 && event.charCode = 190"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="UnitMillion2">ล้านบาท</label>
                                    </div>
                                </div>


                            </div>

                            <div class="panel-heading">เปรียบเทียบรายงานการประเมินความเสี่ยงด้านอัตราดอกเบี้ยสกุลเงิน(T-X)</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="ddlMonth">เดือนตั้งต้น</label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="DdlMonthStart" runat="server" class="form-control">
                                                <asp:ListItem Enabled="true" Text="เลือกเดือน" Value=""></asp:ListItem>
                                                <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlMonth" ErrorMessage="กรุณาเลือกเดือน"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <label class="control-label col-md-1" style="text-align: right" for="ddlYear">ปี</label>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="DdlYearStart" runat="server" class="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlYear" ErrorMessage="กรุณาเลือกปี"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3" style="text-align: right" for="ddlMonth">เดือนเปรียบเทียบ</label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="DdlMonthEnd" runat="server" class="form-control">
                                                <asp:ListItem Enabled="true" Text="เลือกเดือน" Value=""></asp:ListItem>
                                                <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlMonth" ErrorMessage="กรุณาเลือกเดือน"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <label class="control-label col-md-1" style="text-align: right" for="ddlYear">ปี</label>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="DdlYearEnd" runat="server" class="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlYear" ErrorMessage="กรุณาเลือกปี"
                                                SetFocusOnError="True" ValidationGroup="validateUploadData" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>


                                    </div>


                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="BthSaveNii" Text="Save" class="form-control" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="BthCancelNii" Text="Cancel" class="form-control" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="BthProcess" Text="Process" class="form-control" />
                                            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                                        </div>
                                        <%--<div id="progressbar"></div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div style="height: 20px">
                        &nbsp;
                    </div>
                    <div style="text-align: left; height: 800px" id="divPage4" runat="server" hidden="hidden">
                        <div class="panel-body">

                            <div class="form-horizontal">
                                <%--                                <div class="form-group">
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="BtnReport1" runat="server" Text="แบบรายงานเปรียบเทียบ ALM Report7-1 และ ALM Report7-2" BorderStyle="None" BackColor="White"  />
                                    </div>
                                </div>--%>

                                <div class="form-group">
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-8">
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text="รายงานเปรียบเทียบการประเมินความเสี่ยงด้านอัตราดอกเบี้ยสกุลเงินบาท (T-X)"></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-8">
                                        <label style="font: bold">รายงานเปรียบเทียบการประเมินความเสี่ยงด้านอัตราดอกเบี้ยสกุลเงินบาท</label>
                                        <%--<asp:Button ID="Button10" runat="server" Font-Bold="true" Text="รายงานเปรียบเทียบการประเมินความเสี่ยงด้านอัตราดอกเบี้ยสกุลเงินบาท" BorderStyle="None" BackColor="White" Enabled="false" />--%>
                                        <%--                                        <asp:Button ID="BtnReport2_1" runat="server" Text="รายงานเปรียบเทียบ ALM Report 7-1 (Adjust-Unadjust)" BorderStyle="None" BackColor="White" />
                                        <asp:Button ID="BtnReport2_2" runat="server" Text="รายงานเปรียบเทียบ ALM Report 7-2 (Adjust-Unadjust)" BorderStyle="None" BackColor="White" />--%>
                                        <div style="height: 5px">
                                            &nbsp;
                                        </div>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="รายงานเปรียบเทียบ ALM Report 7-1 (Adjust-Unadjust)"></asp:LinkButton>
                                        <div style="height: 5px">
                                            &nbsp;
                                        </div>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="รายงานเปรียบเทียบ ALM Report 7-2 (Adjust-Unadjust)"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-8">
                                        <%--<asp:Button ID="BtnReport3" runat="server" Text="รายงานวิเคราะห์ความเสี่ยง" BorderStyle="None" BackColor="White" />--%>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="รายงานวิเคราะห์ความเสี่ยง"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<asp:Panel ID="pnlReport" runat="server" />--%>
                    </div>

                    <div style="text-align: left; height: 800px" id="divPage5" runat="server" hidden="hidden">
                        <div class="panel panel-default">
                            <div class="panel-heading">การทดสอบภาวะวิกฤติด้านอัตราดอกเบี้ย</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-3" style="text-align: right">
                                            <label class="control-label">Template IRRBB</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="TxtInput" runat="server" class="form-control"></asp:TextBox>
                                            <%--<asp:FileUpload runat="server" ID="FUInput" class="form-control" onchange="ChangeText(this, 'BodyContent_TxtInput');"/>--%>
                                            <%--                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator9" runat="server" ControlToValidate="FUInput"
                                                SetFocusOnError="True" Display="Dynamic" ForeColor="Red">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="([a-zA-Z0-9()\s_\\.\-:])+(.xls|.xlsx)$"
                                                ControlToValidate="FUInput" runat="server" ForeColor="Red" ErrorMessage="ชื่อ File ไม่ถูกต้อง" Display="Dynamic" />

                                        </div>
                                        <div class="col-md-2">
                                            <asp:FileUpload runat="server" ID="FUInput" class="form-control" Style="display: none;" onchange="ChangeText(this, 'BodyContent_TxtInput');" />
                                            <asp:Button runat="server" ID="BtnBrowseTemplate" Text="Browse" class="form-control" OnClientClick="javascript:document.getElementById('BodyContent_FUInput').click(); return false;" />
                                            &nbsp;&nbsp;
                                            <asp:Button runat="server" ID="BtnUploadTemplate" Text="Upload File" class="form-control" OnClick="BtnUploadTemplate_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-2">
                                            <%--<asp:Button runat="server" ID="Button1" Text="Upload File" class="form-control" OnClientClick="javascript:document.getElementById('BodyContent_FUInput').click(); return false;" OnClick="BtnUploadTemplate_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-heading">ประวัติการ Upload Template File</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <table align="center">
                                            <tr valign="middle">
                                                <td align="right"><font color="#FF0000">*</font>ตั้งแต่วันที่</td>
                                                <td width="10px" align="center">:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_StartDate" runat="server" Width="100px"
                                                        onKeyPress="return false" onKeyDown="return false" autocomplete="off"
                                                        CssClass="datePic TextBoxRoundCorrner">
                                                    </asp:TextBox>
                                                </td>
                                                <td width="30px">&nbsp;</td>
                                                <td align="right"><font color="#FF0000">*</font>จนถึงวันที่ </td>
                                                <td width="10px" align="center">:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_EndDate" runat="server" Width="100px"
                                                        onKeyPress="return false" onKeyDown="return false" autocomplete="off"
                                                        CssClass="datePic TextBoxRoundCorrner">
                                                    </asp:TextBox>
                                                </td>
                                                <td width="30px" align="center">&nbsp;</td>
                                                <td>
                                                    <asp:Button runat="server" ID="BtnSearchTemplate" Text="Search" class="form-control" OnClick="BtnSearchTemplate_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="form-group">
                                        <asp:GridView ID="GVloadTemplate" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" Visible="true" PageSize="10">
                                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ลำดับ">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่ Upload">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUploadDate" Text='<%#Eval("UPLOAD_DATE") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อไฟล์">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFilename" Text='<%#Eval("FILENAME") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UPLOADBY" HeaderText="ผู้ดำเนินการ">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                            <div class="panel-heading">Download Tempalte (ต้นฉบับ)</div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-8">
                                            <asp:LinkButton ID="LnkTemplate01" runat="server" Text="1.Download การทดสอบภาวะวิกฤติด้านอัตราดอกเบี้ย(Template IRRBB) Version เริ่มต้น"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-8">
                                            <asp:LinkButton ID="LnkTemplate02" runat="server" Text="2.Download การทดสอบภาวะวิกฤติด้านอัตราดอกเบี้ย(Template IRRBB) Version ล่าสุด"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-8">
                                            <asp:LinkButton ID="LnkTemplate03" runat="server" Text="3.Download Template รายงานที่มีการปรับยอดแล้ว(Adjust)"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>

            </div>


            <%--            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>

            <div class="modal fade" id="AlertBox" role="dialog" aria-labelledby="AlertBoxLabel"
                aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="width: 420px;">
                    <asp:UpdatePanel ID="UpdModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header NormalSubItems">
                                    <asp:Label ID="lbl_Title" runat="server" Style="font-size: medium;" Text="    " />
                                </div>
                                <div class="modal-body">
                                    <table width="100%" align="center" style="border: thin solid #EEE;" class="table table-hover table-striped footable"
                                        border="0" style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                        <tr style="background-color: #FFF; color: #000; font-weight: bold;">
                                            <td id="imageBox" runat="server" align="center">
                                                <asp:Image ID="Symbol_Image" runat="server" ImageUrl="" Height="100px" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Messages" runat="server" Text="Sample" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_OK" runat="server" CssClass="btn btn-primary ButtonStyle" OnClientClick="hidealert();"
                                                    Text="ใช่" data-dismiss="modal" aria-hidden="true" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btn_NO" runat="server" CssClass="btn btn-primary ButtonStyle" Text="ไม่ใช่"
                                                    data-dismiss="modal" aria-hidden="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="modal fade" id="AlertReport" role="dialog" aria-labelledby="AlertBoxLabel"
                aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="width: 420px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header NormalSubItems">
                                    <asp:Label ID="HeaderReport1" runat="server" Style="font-size: medium;" Text="Export File" />
                                </div>
                                <div class="modal-body">
                                    <table width="100%" align="center" style="border: thin solid #EEE;" class="table table-hover table-striped footable"
                                        border="0" style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="MessageReport" runat="server" Text="Sample" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:DropDownList ID="DdlMonthReport" runat="server" class="form-control">
                                                    <asp:ListItem Text="มกราคม" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="กุมภาพันธ์" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="มีนาคม" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="เมษายน" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="พฤษภาคม" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="มิถุนายน" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="กรกฎาคม" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="สิงหาคม" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="กันยายน" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center">
                                                <asp:DropDownList ID="DdlYearReport" runat="server" class="form-control"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="BtnExport" runat="server" CssClass="btn btn-primary ButtonStyle"
                                                    Text="Export File" data-dismiss="modal" aria-hidden="true" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="BtnReportCancel" runat="server" CssClass="btn btn-primary ButtonStyle" Text="Cancel"
                                                    data-dismiss="modal" aria-hidden="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:Timer ID="ProcesTimer" runat="server" Interval="1000" Enabled="False"></asp:Timer>

        </ContentTemplate>

        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID = "btnAsyncUpload" EventName = "Click" />--%>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="BthUploadInput" />
            <asp:PostBackTrigger ControlID="BthSaveNii" />
            <asp:PostBackTrigger ControlID="BtnExport" />
            <asp:PostBackTrigger ControlID="BtnReportCancel" />
            <asp:PostBackTrigger ControlID="BtnUploadTemplate" />
            <asp:PostBackTrigger ControlID="BtnSearchTemplate" />
            <asp:PostBackTrigger ControlID="LnkTemplate01" />
            <asp:PostBackTrigger ControlID="LnkTemplate02" />
            <asp:PostBackTrigger ControlID="LnkTemplate03" />
            <asp:AsyncPostBackTrigger ControlID="btn_OK" EventName="Click" />
            <%--            <asp:AsyncPostBackTrigger ControlID="BthProcess" EventName = "Click" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="btnProcessData" />--%>
        </Triggers>


    </asp:UpdatePanel>

</asp:Content>
