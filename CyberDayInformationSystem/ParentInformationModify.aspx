<%@ Page Title="Parent Information" Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ParentInformationModify.aspx.cs" Inherits="CyberDayInformationSystem.ParentInformationModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="ParentDashboard.aspx">Parent Home</a></li>
        <li>Parent Information</li>
    </ul>

    <%-- Show the parent that is logged in's contact information to allow them to edit it --%>
    <asp:TableRow runat="server" HorizontalAlign="Center">
        <asp:TableCell>
            <asp:DetailsView ID="parentModifyDtl" AllowPaging="true" AutoGenerateRows="false" 
                CssClass="table table-bordered table-striped table-hover" runat="server" 
                AutoGenerateEditButton="true" OnModeChanging="parentModifyDtl_ModeChanging" OnItemUpdating="parentModifyDtl_ItemUpdating">  
                <Fields>  
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />  
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />  
                    <asp:BoundField DataField="EmailAdd" HeaderText="Email" />  
                    <asp:BoundField DataField="Phone" HeaderText="Phone Number" />  
                </Fields>  
            </asp:DetailsView> 
        </asp:TableCell>
    </asp:TableRow>

</asp:Content>
