<%@ Page Title="ตั้งค่าระบบ" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="SettingAmount.aspx.vb" Inherits="GSBWeb.SettingAmount" %>

<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function addCommas(clientID) {
      
            var nStr = document.getElementById(clientID.id).value;

            numcheck = /^[]?\d{1,18}(\.\d{1,4})?$/;

            if (numcheck.test(nStr) == false) {
                document.getElementById(clientID.id).value = "";
            }
            else {

                nStr += '';
                x = nStr.split('.');
                if (!x[0]) {
                    x[0] = "0";

                }
                x1 = x[0];
                if (!x[1]) {
                    x[1] = "00";
                }
                //check lenght
                else {
                    if (x[1].length == 1)
                        x[1] = x[1] + '0';
                }


                x2 = x.length > 1 ? '.' + x[1].substr(0, 2) : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + ',' + '$2');
                }

                document.getElementById(clientID.id).value = x1 + x2;

            }



            return true;
        }



        function checkKeyaddCommas(clientID, event) {


            if (event.keyCode == 13) {

                if (confirm("คุณต้องการบัญทึกข้อมูลใช้หรือไม่!") == true) {
                    x = "You pressed OK!";
                    return true;

                } else {

                    return false;
                }
  

            }

            return true;

        }





        function removeCommas(clientID) {


            var nStr = document.getElementById(clientID.id).value;
            nStr = nStr.replace(/,/g, '');
            document.getElementById(clientID.id).value = nStr;

            $(clientID).select();

            return true;

        }

      
    </script>

         
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">


<uc1:AutoRedirect ID="AutoRedirect" runat="server" />


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="panel panel-default">

        <div class="NormalHeader" style="/*text-align: left; font-size: 18pt; font-weight: normal;
            background: #FF388C; padding-right: 5; color: #FFFFFF;*/height:30px;">
            การตั้งค่าจำนวนวงเงิน (Adjustment)
        </div>
        <table align="left" width="100%">
            <tr>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td align="right" style="font-weight: bold; font-size: medium; white-space: nowrap">
                    การตั้งค่าจำนวนวงเงิน :&nbsp;
                </td>
                <td style="font-weight: bold; text-align: left; white-space: nowrap; width: 200;
                    margin-right: 0; padding-right: 0">
                    <asp:TextBox ID="txtAmount"   OnBlur="addCommas(this)" onfocus="removeCommas(this)" onkeypress="return checkKeyaddCommas(this,event);"  class="form-control" style="text-align:right; margin-right:0; padding-right:0; width:100%" runat="server" Enabled="true" autocomplete="off"> </asp:TextBox>

                </td>
                <td align="right" style="font-weight: bold; text-align: left; font-size: medium;
                    margin-left: 0; padding-left: 0">
                    &nbsp; บาท
                </td>
                <td style="font-weight: bold; font-size: medium">
                    <asp:Button ID="btnSave" class="btn btn-primary ButtonGrayStyle" runat="server" Text="บันทึก" 
                        Width="110px" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 10%">
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td align="right" style="font-weight: bold; font-size: medium">
                    &nbsp;
                </td>
                <td style="font-weight: bold; font-size: x-small">
                    &nbsp;
                </td>
                <td align="right" style="font-weight: bold; font-size: medium">
                    &nbsp;&nbsp;
                </td>
                <td style="font-weight: bold; font-size: x-small">
                    &nbsp;
                </td>
                <td style="font-weight: bold; font-size: x-small">
                    &nbsp&nbsp
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
            <tr style="text-align: left; border-width: 1; border-color: Gray">
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td width="80%" colspan="5" style="border: 1px solid Gray; background-color: #DDD;
                    font-weight: normal; font-size:14pt; text-align: left; color: Black" height="40px">
                    วงเงินที่กำหนดในปัจจุบัน
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
            <tr style="text-align: left; border-width: 1; border-color: Gray; font-size: medium">
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td width="80%" style="border: 1px solid Gray; font-size: medium; text-align: left;"
                    colspan="5" height="35px">
                    <asp:Label ID="lblAmount"  runat="server" Text="Label" ></asp:Label>
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>

    </div>
    <br />
      <asp:UpdateProgress ID="UpdateProgress1" style="display: none" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
