<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MealTicketMgmt.aspx.cs" Inherits="CyberDayInformationSystem.MealTicketMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="AdminReports-Styling.css" rel="stylesheet" type="text/css" />
    <asp:Table runat="server">
        <asp:TableRow runat="server" HorizontalAlign="Left">
            <asp:TableCell ColumnSpan="4">
                <asp:Label ID="TertiaryGridLbl" runat="server" Text="" Visible="false" Font-Size="Large"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Left">
            <asp:TableCell ColumnSpan="4">
                <asp:GridView ID="TertiaryGridView" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="900" HorizontalAlign="Left">
                    <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                    <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Button ID="PrintBtn" runat="server" Text="Print Report" Visible="false" OnClientClick="PrintReport();" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
