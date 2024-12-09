<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupReport1.aspx.vb" Inherits="PopupReport1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

<%--        <title>xxxx</title>--%>


    <style>
        .button-disabled{
          background-color: #cccccc !important;
          opacity: 0.80 !important;
          pointer-events: none !important;
          /*display:none !important;*/
        }
    </style>


    <style>
        body {
            background-color: #ec098c;
        }

        #form1 {
            height: 120px;
        }
    </style>


        <div style="width: 300px;  text-align: center" class="container">
            <div class="form-group" >
                <asp:Label runat="server" ID="lblReportname" 
                ForeColor="White" Width="300px">แบบรายงานประเมินความเสี่ยงด้านอัตราดอกเบี้ยสกุลเงินบาท</asp:Label>
            </div>
            <div class="form-group" >
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlmonth" runat="server" class="form-control">
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
                </div>

                <br />
                <span>
               
                </span>

                <br />

                <span>
                    <asp:Label ID="Label1" runat="server" ForeColor="Black"></asp:Label>

                </span>



            </div>

        </div>

    <%--</form>--%>

    </asp:Content>


