<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RDMMenu.aspx.vb" Inherits="GSBWeb.RDMMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server" >
     <h1>Menu Standrd Credit Report</h1>
    <table style="width:100%">
        <tr>
            <td style="width:20%">&nbsp;</td>
            <td>
                <div style="text-align:left">
                    <%--<li>--%>
                        <%--<h4> 0. <a href="report/ReportTest.aspx">Run Report Test</a></h4>--%>
                        <h4> ***<a href="InternalEx.aspx">Internal & External </a></h4>
                        <h4> 1. <a href="1CRDashboard.aspx">Credit Risk Dashboard</a></h4>
                        <h4> 2. <a href="2DimensionArrearsStatus.aspx">Dimension Report - Breakdown By Arrears Status</a></h4>
                        <h4> 3. <a href="3DimensionAssetType.aspx">Dimension Report - Breakdown By Asset Type</a></h4>
                        <h4> 4. <a href="4DimensionCountryOfOrigin.aspx">Dimension Report - Breakdown By Country Of Origin</a></h4>
                        <h4> 5. <a href="5DimensionCRM.aspx">Dimension Report - Breakdown By CRM Type</a></h4>
                        <h4> 6. <a href="6DimensionExposure.aspx">Dimension Report - Breakdown By Exposure Currency</a></h4>
                        <h4> 7. <a href="7DimensionCRating.aspx">Dimension Report - Breakdown By External Credit Rating</a></h4>
                        <h4> 8. <a href="8DimensionIgroup.aspx">Dimension Report - Breakdown By Obligor Industry Group</a></h4>
                        <h4> 9. <a href="9DimensionOtype.aspx">Dimension Report - Breakdown By Obligor Type</a></h4>
                        <h4> 10. <a href="10DimensionSpecifiedApproach.aspx">Dimension Report - Breakdown By Specified Approach</a></h4>
                        <h4> 11. <a href="11CounterpartyList.aspx">Counterparty List</a></h4>
                        <h4> 12. <a href="12CounterpartyExposureStatement.aspx">Counterparty Exposure Statement</a></h4>
                        <h4> 13. <a href="13ExposureTrancheStatement.aspx">Exposure Tranche Statement</a></h4>
                        <%--<h4> 14. <a href="13ExposureTranche.aspx">Exposure Statement Approach - AIRB</a></h4>--%>
                        <%--<h4> 15. <a href="14ExposureAIRB.aspx">Exposure Statement Approach - FIRB</a></h4>--%>
                        <%--<h4> 16. <a href="15ExposureFIRB.aspx">Exposure Statement Approach - Std Comp</a></h4>--%>
                        <h4> 14. <a href="17Economic Capital.aspx">Economic Capital by Confidence Level</a></h4>
                        <h4> 15. <a href="18LossDist.aspx">Loss Distribution</a></h4>
                        <h4> 19. <a href="19RiskTree.aspx">Risk Tree Overview</a></h4>
                        <h4> 16. <a href="20RegulatoryCapital.aspx">Regulatory Capital Summary</a></h4>
                        <h4> 17. <a href="21CapitalAdequacy.aspx">Capital Adequacy By Risk</a></h4>
                        <h4> 18. <a href="22CapitalOrganization.aspx">Capital Adequacy By Organization Structure</a></h4>
                    <%--</li>--%> 
                </div>
            </td>
        </tr>

    </table>
</asp:Content>
