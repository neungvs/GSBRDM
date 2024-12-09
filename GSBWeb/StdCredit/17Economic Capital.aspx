<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="17Economic Capital.aspx.vb" Inherits="GSBWeb._16ExposureSTDcomp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ctl01 {
            background-color: #ff33ff;
            border-top: solid 10px #ff33ff;
            color: #333;
            font-size: .85em;
            /*font-family: "Segoe UI", Verdana, Helvetica, Sans-Serif;*/
            font-family: Tahoma;
            margin: 0;
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="1200">
    </asp:ScriptManager>

    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<%--    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>--%>
    <script type="text/javascript" src="../Scripts/bootstrap-multiselect.js" ></script>
    
    <style>
        .multiselect-container .dropdown-menu {
            height: 100px !important;
            overflow-y: auto !important;
            /*height: 100px;*/
            /*font-family: cursive;*/
        }
    </style>
    <script type="text/javascript">
        function GetSelectedTextValue() {
            var DrlScenario = document.getElementById("<%=DrlScenario.ClientID%>");
            if (DrlScenario.value == 0) {
                alert("กรุณาเลือก สถานการณ์ (Scenario)");
                return false;
            }

            return true;
        }
        $(document).ready(function () {
            $("a[title='MHTML (web archive)']").parent().hide();
            $("a[title='TIFF file']").parent().hide();
            $("a[title='XML file with report data']").parent().hide();
            $("a[title='CSV (comma delimited)']").parent().hide();
        });

        $(function () {
            $('[id*=lstPercentiles]').multiselect({
                includeSelectAllOption: true
            });
            $("#Btntest").click(function () {
                alert($(".multiselect-selected-text").html());
            });
        });

    </script>
    <style>
        .multiselect-container {
            height: 200px !important;
            overflow-y: auto !important;
        }
    </style>
    <div class="contentTableAreaStyle">
        <div>
            <h1>Economic Capital by Confidence Level</h1>
            <table class="tabletypereport1">
                <tr>
                    <td>วันที่ (Time)</td>
                    <td>
                        <asp:DropDownList ID="DrlTimeYear" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="DrlTimeMonth" runat="server"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>สถานการณ์ (Scenario)</td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="DrlScenario" runat="server" DataTextField="SCENARIODESC" DataValueField="SCENARIOID">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br>
        </div>
        <div>
            <asp:Panel ID="Panel1" runat="server" Style="height: 150px">

                <table class="tabletypereport1">
                    <tr>
                        <td>Percentiles</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstPercentiles" runat="server" SelectionMode="Multiple" Style="height: 100px !important; overflow-y: auto !important;">
                                <asp:ListItem Text="0.01%" Value="0.01%" />
                                <asp:ListItem Text="0.1%" Value="0.1%" />
                                <asp:ListItem Text="1%" Value="1%" />
                                <asp:ListItem Text="2%" Value="2%" />
                                <asp:ListItem Text="3%" Value="3%" />
                                <asp:ListItem Text="4%" Value="4%" />
                                <asp:ListItem Text="5%" Value="5%" />
                                <asp:ListItem Text="6%" Value="6%" />
                                <asp:ListItem Text="7%" Value="7%" />
                                <asp:ListItem Text="8%" Value="8%" />
                                <asp:ListItem Text="9%" Value="9%" />
                                <asp:ListItem Text="10%" Value="10%" />
                                <asp:ListItem Text="11%" Value="11%" />
                                <asp:ListItem Text="12%" Value="12%" />
                                <asp:ListItem Text="13%" Value="13%" />
                                <asp:ListItem Text="14%" Value="14%" />
                                <asp:ListItem Text="15%" Value="15%" />
                                <asp:ListItem Text="16%" Value="16%" />
                                <asp:ListItem Text="17%" Value="17%" />
                                <asp:ListItem Text="18%" Value="18%" />
                                <asp:ListItem Text="19%" Value="19%" />
                                <asp:ListItem Text="20%" Value="20%" />
                                <asp:ListItem Text="21%" Value="21%" />
                                <asp:ListItem Text="22%" Value="22%" />
                                <asp:ListItem Text="23%" Value="23%" />
                                <asp:ListItem Text="24%" Value="24%" />
                                <asp:ListItem Text="25%" Value="25%" />
                                <asp:ListItem Text="26%" Value="26%" />
                                <asp:ListItem Text="27%" Value="27%" />
                                <asp:ListItem Text="28%" Value="28%" />
                                <asp:ListItem Text="29%" Value="29%" />
                                <asp:ListItem Text="30%" Value="30%" />
                                <asp:ListItem Text="31%" Value="31%" />
                                <asp:ListItem Text="32%" Value="32%" />
                                <asp:ListItem Text="33%" Value="33%" />
                                <asp:ListItem Text="34%" Value="34%" />
                                <asp:ListItem Text="35%" Value="35%" />
                                <asp:ListItem Text="36%" Value="36%" />
                                <asp:ListItem Text="37%" Value="37%" />
                                <asp:ListItem Text="38%" Value="38%" />
                                <asp:ListItem Text="39%" Value="39%" />
                                <asp:ListItem Text="40%" Value="40%" />
                                <asp:ListItem Text="41%" Value="41%" />
                                <asp:ListItem Text="42%" Value="42%" />
                                <asp:ListItem Text="43%" Value="43%" />
                                <asp:ListItem Text="44%" Value="44%" />
                                <asp:ListItem Text="45%" Value="45%" />
                                <asp:ListItem Text="46%" Value="46%" />
                                <asp:ListItem Text="47%" Value="47%" />
                                <asp:ListItem Text="48%" Value="48%" />
                                <asp:ListItem Text="49%" Value="49%" />
                                <asp:ListItem Text="50%" Value="50%" />
                                <asp:ListItem Text="51%" Value="51%" />
                                <asp:ListItem Text="52%" Value="52%" />
                                <asp:ListItem Text="53%" Value="53%" />
                                <asp:ListItem Text="54%" Value="54%" />
                                <asp:ListItem Text="55%" Value="55%" />
                                <asp:ListItem Text="56%" Value="56%" />
                                <asp:ListItem Text="57%" Value="57%" />
                                <asp:ListItem Text="58%" Value="58%" />
                                <asp:ListItem Text="59%" Value="59%" />
                                <asp:ListItem Text="60%" Value="60%" />
                                <asp:ListItem Text="61%" Value="61%" />
                                <asp:ListItem Text="62%" Value="62%" />
                                <asp:ListItem Text="63%" Value="63%" />
                                <asp:ListItem Text="64%" Value="64%" />
                                <asp:ListItem Text="65%" Value="65%" />
                                <asp:ListItem Text="66%" Value="66%" />
                                <asp:ListItem Text="67%" Value="67%" />
                                <asp:ListItem Text="68%" Value="68%" />
                                <asp:ListItem Text="69%" Value="69%" />
                                <asp:ListItem Text="70%" Value="70%" />
                                <asp:ListItem Text="71%" Value="71%" />
                                <asp:ListItem Text="72%" Value="72%" />
                                <asp:ListItem Text="73%" Value="73%" />
                                <asp:ListItem Text="74%" Value="74%" />
                                <asp:ListItem Text="75%" Value="75%" />
                                <asp:ListItem Text="76%" Value="76%" />
                                <asp:ListItem Text="77%" Value="77%" />
                                <asp:ListItem Text="78%" Value="78%" />
                                <asp:ListItem Text="79%" Value="79%" />
                                <asp:ListItem Text="80%" Value="80%" />
                                <asp:ListItem Text="81%" Value="81%" />
                                <asp:ListItem Text="82%" Value="82%" />
                                <asp:ListItem Text="83%" Value="83%" />
                                <asp:ListItem Text="84%" Value="84%" />
                                <asp:ListItem Text="85%" Value="85%" />
                                <asp:ListItem Text="86%" Value="86%" />
                                <asp:ListItem Text="87%" Value="87%" />
                                <asp:ListItem Text="88%" Value="88%" />
                                <asp:ListItem Text="89%" Value="89%" />
                                <asp:ListItem Text="90%" Value="90%" />
                                <asp:ListItem Text="91%" Value="91%" />
                                <asp:ListItem Text="92%" Value="92%" />
                                <asp:ListItem Text="93%" Value="93%" />
                                <asp:ListItem Text="94%" Value="94%" />
                                <asp:ListItem Text="95%" Value="95%" />
                                <asp:ListItem Text="96%" Value="96%" />
                                <asp:ListItem Text="97%" Value="97%" />
                                <asp:ListItem Text="97.5%" Value="97.5%" />
                                <asp:ListItem Text="98%" Value="98%" />
                                <asp:ListItem Text="98.5%" Value="98.5%" />
                                <asp:ListItem Text="99%" Value="99%" />
                                <asp:ListItem Text="99.5%" Value="99.5%" />
                                <asp:ListItem Text="99.9%" Value="99.9%" />
                                <asp:ListItem Text="99.99%" Value="99.99%" />
                            </asp:ListBox>

                            <asp:Button ID="BtnPercentilesSelect" runat="server" Text="ตกลง" />
                            <asp:Button ID="BtnCancel" runat="server" Text="ยกเลิก" />
                        </td>

                    </tr>
                </table>
                <br>
            </asp:Panel>
        </div>
        <div style="height: 100vh;">
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
