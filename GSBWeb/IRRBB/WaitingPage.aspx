<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WaitingPage.aspx.vb" Inherits="GSBWeb.WaitingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">

            //$(document).ready(function () {

            //    $('#lbltext').text("");

            //    startProcess();

            //});

            //function startProcess() {
            //    var intervalID = setInterval(updateProgress, 1000);
            //}

            //function updateProgress(a) {                 
            //               $('#lbltext').text(a);             
            //}       
      

        </script>

    <title>Waiting Page</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" AsyncPostBackTimeout="5000000"></asp:ScriptManager>

    <table>
        <tr style="height:100px">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                    <div class="modal_Pross">
                        <div class="center_Pross">
                            <img alt="" src="<%= Page.ResolveClientUrl("~/Images/LoaderRed.gif")%>" />
                        </div>
                        <div class="center_Pross">
                            <asp:Label ID="lblProcess" runat="server" Text="เริ่มการประมวลผล..." Font-Size="XX-Large" ForeColor="White"></asp:Label>
                        </div>
                    </div>
            </td>
        </tr>
    </table>

    <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="2000"></asp:Timer>

</asp:Content>

