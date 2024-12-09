<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="11CounterpartyList.aspx.vb" Inherits="GSBWeb._10DimensionSapproach" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
   <script type="text/javascript">
       function GetSelectedTextValue()
       {
            var DrlScenario = document.getElementById("<%=DrlScenario.ClientID%>");
            var Drlsimcom = document.getElementById("<%=Drlsimcom.ClientID%>");
            var Drloem = document.getElementById("<%=Drloem.ClientID%>");
            if (DrlScenario.value == 0)
            {
                alert("กรุณาเลือก สถานการณ์ (Scenario)");
                return false;
            }
            if (Drlsimcom.value == 0)
            {
                alert("กรุณาเลือก วิธีการปรับลดความเสี่ยง");
                return false;
            }
            if (Drloem.value == 0)
            {
                alert("กรุณาเลือก วิธีการคำนวณสำหรับตราสารอนุพันธ์");
                return false;
            }
            return true;
       }

       function GetTypeReport_Counterparties() {

           var vHdfTypeReport = document.getElementById("<%=HdfTypeReport.ClientID%>");
           var vBtnCounterparties = document.getElementById("BtnCounterparties");
           var vBtnExposure = document.getElementById("BtnExposure");

           
           vHdfTypeReport.value = "Counterparties"
           vBtnCounterparties.style.backgroundColor = "deepskyblue"
           vBtnExposure.style.backgroundColor = "lightgray"
           
           return false;

       }

       function GetTypeReport_Exposure() {

           var vHdfTypeReport = document.getElementById("<%=HdfTypeReport.ClientID%>");
           var vBtnCounterparties = document.getElementById("BtnCounterparties");
           var vBtnExposure = document.getElementById("BtnExposure");

           vHdfTypeReport.value = "Exposure"
           vBtnCounterparties.style.backgroundColor = "lightgray"
           vBtnExposure.style.backgroundColor = "deepskyblue"

           return true;
       }


        $(document).ready(function () {
            $("a[title='MHTML (web archive)']").parent().hide();  
            $("a[title='TIFF file']").parent().hide();
            $("a[title='XML file with report data']").parent().hide();
            $("a[title='CSV (comma delimited)']").parent().hide();
        });
   </script>
  <h1>Counterparty List</h1> </br>
        <table>
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
             <tr>
                <td>วิธีการปรับลดความเสี่ยง</td>
                <td>
                    <asp:DropDownList ID="Drlsimcom" runat="server" DataTextField="SIMP_COMP" DataValueField="SIMP_COMP">
                    </asp:DropDownList>
                 </td>
                <td></td>
                <td>วิธีการคำนวณสำหรับตราสารอนุพันธ์</td>
                <td class="auto-style1">
                    <asp:DropDownList ID="Drloem" runat="server" DataTextField="OEM_CEM" DataValueField="OEM_CEM">
                    </asp:DropDownList>
                 </td>
            </tr>
           
            <tr>
                <td>&nbsp;</td>
                <td></td>
                <td>
                    &nbsp;</td>
                <td colspan="2" align="right">
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" />
                    <asp:Button ID="btnOk" runat="server" OnClientClick="return GetSelectedTextValue()" Text="ตกลง" />
                </td>
            </tr>
        </table><br>


        <asp:Panel ID="Panel1" runat="server" Visible="false">

            <table>
                <tr>
                    <td>
                        <table>
                            <tr> <td colspan="2" align="center">Show</td> </tr>
                            <tr>
                                <td>                                
                                     <input type="button" ID="BtnCounterparties" OnClick=GetTypeReport_Counterparties() value="Counterparties" style="height:30px;"  />
                                </td>  
                                <td> 
                                    <input type="button" ID="BtnExposure" OnClick=GetTypeReport_Exposure() value="Exposure" style="height:30px;" /> 
                                </td>  
                                <asp:HiddenField ID="HdfTypeReport" runat="server" />
                            </tr>
                            <tr> <td colspan="2">Show Top X Items</td> </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="DrlTop" runat="server">
                                        <asp:ListItem>Top 1</asp:ListItem>
                                        <asp:ListItem>Top 5</asp:ListItem>
                                        <asp:ListItem>Top 10</asp:ListItem>
                                        <asp:ListItem>Top 20</asp:ListItem>
                                        <asp:ListItem>Top 25</asp:ListItem>
                                        <asp:ListItem>Top 50</asp:ListItem>
                                        <asp:ListItem>Top 100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr> <td colspan="2">Sort By Measure</td> </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="DrlSort" runat="server">
                                        <asp:ListItem>RWA</asp:ListItem>
                                        <asp:ListItem>EAD</asp:ListItem>
                                        <asp:ListItem>Specific Provision</asp:ListItem>
                                        <asp:ListItem>Number of Exposures</asp:ListItem>
                                        <asp:ListItem>Number of Tranches</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                       </table>
                    </td>
                    <td></td>
                    <td>
                        <table>
                            <tr> <td>Begin With:</td> </tr>
                            <tr>
                                <td> <asp:TextBox ID="TxtBegin" runat="server"></asp:TextBox> </td>  
                            </tr>
                            <tr> <td>or Contain:</td> </tr>
                            <tr>
                                 <td> <asp:TextBox ID="TxtContain" runat="server"></asp:TextBox> </td>  
                            </tr>
                       </table>
                    </td>
                    <td></td>
                    <td>
                        <table>
                            <tr> 
                                <td>
                                    <table>
                                        <tr> 
                                            <td>Asset Type</td> 
                                            <td>CRM Type</td>
                                            <td>Obligor Type</td>
                                        </tr>
                                        <tr>
                                            <td> <asp:DropDownList ID="DrlAssetType" runat="server" DataTextField="ASSETTYPEDESC" DataValueField="ASSETTYPEDESC" Width="200px"></asp:DropDownList> </td>  
                                            <td> <asp:DropDownList ID="DrlCRMType" runat="server" DataTextField="CRMTYPEDESC" DataValueField="CRMTYPEDESC" Width="200px"></asp:DropDownList> </td>  
                                            <td> <asp:DropDownList ID="DrlObligorType" runat="server" DataTextField="MARKETPARTICIPANTTYPEDESC" DataValueField="MARKETPARTICIPANTTYPEDESC" Width="200px"></asp:DropDownList> </td>  
                                        </tr>
                                        <tr> 
                                            <td>Arrears Status</td> 
                                            <td>External Credit Rating</td>
                                            <td>Obligor Industry Group</td>
                                        </tr>
                                        <tr>
                                            <td> <asp:DropDownList ID="DrlArrearsStatus" runat="server" DataTextField="ARREARSSTATUSDESC" DataValueField="ARREARSSTATUSDESC" Width="200px"></asp:DropDownList> </td>  
                                            <td> <asp:DropDownList ID="DrlExternalCreditRating" runat="server" DataTextField="EXTERNALCREDITRATINGTYPEDESC" DataValueField="EXTERNALCREDITRATINGTYPEDESC" Width="200px"></asp:DropDownList> </td>  
                                            <%--<td> <asp:DropDownList ID="DrlSpecifiedApproach"  runat="server" DataTextField="APPROACHDESC" DataValueField="APPROACHDESC" Width="200px"></asp:DropDownList> </td>--%>  
                                            <td> <asp:DropDownList ID="DrlObligorIndustryGroup" runat="server" DataTextField="INDUSTRYGROUPDESC" DataValueField="INDUSTRYGROUPDESC" Width="200px"></asp:DropDownList> </td>  

                                        </tr>
                                        <tr> 
                                            <td>Exposure Currency</td> 
                                            <td>Country of Origin</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td> <asp:DropDownList ID="DrlExposureCurrency" runat="server" DataTextField="CURRENCYDESC" DataValueField="CURRENCYDESC" Width="200px"></asp:DropDownList> </td>  
                                            <td> <asp:DropDownList ID="DrlCountryOfOrigin" runat="server" DataTextField="COUNTRYDESC" DataValueField="COUNTRYDESC" Width="200px"></asp:DropDownList> </td>  
                                            <td> &nbsp; </td>  
                                        </tr>
                                   </table>
                                </td> 
                            </tr>
                       </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td></td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnOkCondition" runat="server" Text="ตกลง" />
                    </td>
                </tr>
            </table><br>

        </asp:Panel>

   <div style="height:100vh;">
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">           
        </rsweb:ReportViewer> 
    </div>
</asp:Content>
