<%@ Page Title="WebAdjust" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="RDM_Periods.aspx.vb" Inherits="GSBWeb.RDM_Periods" %>
<%@ Register src="~/UserControl/AutoRedirect.ascx" tagname="AutoRedirect" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table{
            font-weight:normal;     
            font-size:medium ;
            border: 1px solid #ddd;
            }
        
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
        padding: 8px;
        height:35px;
        line-height: 1.42857143;
        vertical-align: middle;
        border: 1px solid #ddd;
        }      
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".datePic").datepicker($.datepicker.regional["th"]); // Set ภาษาที่เรานิยามไว้ด้านบน

        }


        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function Input_Eng(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 3585) {
                return false;
            } else {
                var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?^()";
                var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
                var ch = String.fromCharCode(charCode);
                if (iChars.indexOf(ch) == -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }




  


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <uc1:AutoRedirect ID="AutoRedirect" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-2">                    
                </div>
                <div class="col-md-8" style="align-content:center;">
                    <table class="table table-striped" AllowPaging="True" 
                        AutoGenerateColumns="False" EnableModelValidation="True" PageSize="100" 
                        ShowHeaderWhenEmpty="True" 
                        Style="border: 1px none #EEE;" Width="100%">
                        <tr style="background-color: #CCC; text-align: center;">
                            <th style="text-align: center;">ลำดับที่</th>
                            <th style="text-align: center;">รายละเอียด</th> 
                            <th style="text-align: center;">งวดการรัน</th>
                            <th style="text-align: center;">แก้ไขงวดการรัน</th>
                          </tr>
                        <tr style="vertical-align:middle;">
                            <td >1</td>
                            <td>กำหนดรอบการรัน RDM_ETL</td> 
                            <td>
                                <asp:TextBox ID="txtPeriodbox" runat="server"  ReadOnly="true" Width="120px" style="background-color: #f3f3f3"></asp:TextBox>
                            </td> 
                            <td>
                                <asp:TextBox ID="txtApproveDate1" Width="120px" AutoPostBack="true" style="background-color: #f3f3f3" onKeyPress="return false;" onKeyDown="return false" autocomplete="off" CssClass="datePic" runat="server"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button CssClass="btn btn-default ButtonStyle" style="height:30px;"  ID="btnSave" runat="server" Text=" Save " />
                            </td>
                        </tr>               
                    </table>
                </div>
                <div class="col-sm-2"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

