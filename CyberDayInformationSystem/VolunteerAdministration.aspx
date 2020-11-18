<%@ Page Title="Volunteer Administration" Language="C#" MasterPageFile="~/Volunteer.Master" AutoEventWireup="true" CodeBehind="VolunteerAdministration.aspx.cs" Inherits="CyberDayInformationSystem.VolunteerAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Volunteer Administration</li>
    </ul>

    <h3>Assign Volunteers</h3>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow ID="rowSelDate" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="lblSelDate" runat="server" Text="Select Event Date: " Width="200"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:DropDownList ID="ddlEventDates" runat="server" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowDateNextBtn" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <button type="button" class="btn-main blue" runat="server" onserverclick="btnSelDateNext_Click">Next ></button>
            </asp:TableCell>
        </asp:TableRow>


        <asp:TableRow ID="rowSelVol" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="lblSelVol" runat="server" Text="Select Volunteer: " Width="200"></asp:Label>
            </asp:TableCell>

            <asp:TableCell ColumnSpan="2">
                <asp:DropDownList ID="ddlVols" runat="server" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowSubmitBtn" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell ColumnSpan="2">
                <button type="button" class="btn-main blue" runat="server" onserverclick="btnBack_Click">< Back</button>
            </asp:TableCell>
            
            <asp:TableCell ColumnSpan="2">
                <button type="button" CssClass="btn-main reg" runat="server" onserverclick="btnSubmit_Click">Submit ></button>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>