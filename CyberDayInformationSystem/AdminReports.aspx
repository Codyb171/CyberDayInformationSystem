<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminReports.aspx.cs" Inherits="CyberDayInformationSystem.AdminReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center">Report Access</h1>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="3">
                <h2 class="text-center">Select a report type:</h2>
                <asp:RadioButtonList ID="FunctionList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionList_SelectedIndexChanged">
                    <asp:ListItem Value="1">Event Data</asp:ListItem>
                    <asp:ListItem Value="2">Staff Data</asp:ListItem>
                    <asp:ListItem Value="3">Attendee Data</asp:ListItem>
                </asp:RadioButtonList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="SelectionLbl" runat="server" Text="Event: " Visible="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:DropDownList ID="SelectionDropDown" runat="server" Visible="false" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="SelectionChoice" runat="server" ErrorMessage="Please make a selection" ControlToValidate="SelectionDropDown" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Button ID="RunBtn" runat="server" Text="Run Report?" OnClick="RunBtn_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:GridView ID="SelectedGridView" runat="server" AutoGenerateColumns="True" CellPadding="5" Width="900">
                    <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                    <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:GridView ID="StaffGridView" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="450">
                    <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                    <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                </asp:GridView>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:GridView ID="RosterGridView" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="450">
                    <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                    <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:GridView ID="AssignedGridView" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="900">
                    <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                    <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">

        </asp:TableRow>
    </asp:Table>
</asp:Content>
