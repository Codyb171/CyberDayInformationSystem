<%@ Page Title="" Language="C#" MasterPageFile="~/Volunteer.Master" AutoEventWireup="true" CodeBehind="VolunteerAdministration.aspx.cs" Inherits="CyberDayInformationSystem.VolunteerAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Assign Volunteers</h1>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow ID="rowSelDate" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblSelDate" runat="server" Text="Select Event Date: " Width="200"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:DropDownList ID="ddlEventDates" runat="server" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowDateNextBtn" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnSelDateNext" runat="server" Text="Next >" OnClick="btnSelDateNext_Click" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowSelectedDate" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell>
                <asp:Label ID="lblSelectedDate" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowSelVol" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell>
                <asp:Label ID="lblSelVol" runat="server" Text="Select Volunteer: " Width="200"></asp:Label>
            </asp:TableCell>

            <asp:TableCell>
                <asp:DropDownList ID="ddlVols" runat="server" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowSubmitBtn" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit >" OnClick="btnSubmit_Click" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>