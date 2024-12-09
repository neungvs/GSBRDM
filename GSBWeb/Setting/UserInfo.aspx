<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserInfo.aspx.vb" Inherits="GSBWeb.UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

<div class="container-fluid">
    <section class="container">
        <div class="container-page">
            <div class="row justify-content-center">			
			    <div class="col-sm-10">
				    <h3 class="dark-grey">Registration</h3>
				
				    <div class="form-group col-lg-12">
					    <label>Username</label>
					    <input type="" name="" class="form-control" id="" value="">
				    </div>
				
				    <div class="form-group col-lg-6">
					    <label>Password</label>
					    <input type="password" name="" class="form-control" id="" value="">
				    </div>
				
				    <div class="form-group col-lg-6">
					    <label>Repeat Password</label>
					    <input type="password" name="" class="form-control" id="" value="">
				    </div>
								
				    <div class="form-group col-lg-6">
					    <label>Email Address</label>
					    <input type="" name="" class="form-control" id="" value="">
				    </div>
				
				    <div class="form-group col-lg-6">
					    <label>Repeat Email Address</label>
					    <input type="" name="" class="form-control" id="" value="">
				    </div>			
				
				    <div class="col-sm-6">
					    <input type="checkbox" class="checkbox" />Sigh up for our newsletter
				    </div>

				    <div class="col-sm-6">
					    <input type="checkbox" class="checkbox" />Send notifications to this email
				    </div>				
			
			    </div>
		</div>
        </div>
	</section>
</div>

</asp:Content>
