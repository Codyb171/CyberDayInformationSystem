﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Volunteer.Master" AutoEventWireup="true" CodeBehind="VolunteerAdministration.aspx.cs" Inherits="CyberDayInformationSystem.VolunteerAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Assign Volunteers</h1>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblSelDate" runat="server" Text="Select Event Date: " Width="200"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:DropDownList ID="ddlEventDates" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="dateNextBtn" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnSelDateNext" runat="server" Text="Next >" OnClick="btnSelDateNext_Click" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="selectedDate" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell>
                <asp:Label ID="lblSelectedDate" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowSelEvent" runat="server" HorizontalAlign="Center" Visible="false">

            <asp:TableCell>
                <asp:Label ID="lblSelEvent" runat="server" Text="Select Event: " Width="200"></asp:Label>
            </asp:TableCell>

            <asp:TableCell>
                <asp:DropDownList ID="ddlEventTasks" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="eventNextBtn" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnSelEventNext" runat="server" Text="Next >" OnClick="btnSelEventNext_Click" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="selectedEvent" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell>
                <asp:Label ID="lblSelectedEvent" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowSelVol" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell>
                <asp:Label ID="lblSelVol" runat="server" Text="Select Volunteer: " Width="200"></asp:Label>
            </asp:TableCell>

            <asp:TableCell>
                <asp:DropDownList ID="ddlVols" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="submitBtn" runat="server" HorizontalAlign="Center" Visible="false">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Sbumit >" OnClick="btnSubmit_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>