<%@ Page Title="User Status" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BadSession.aspx.cs" Inherits="CyberDayInformationSystem.BadSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="StatusLbl" runat="server" Text="User Logged Out Successfully!" Font-Size="XX-Large"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="AltLbl" runat="server" Text="Have a wonderful day!" Font-Size="Larger"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Button ID="RedirectBtn" runat="server" Text="Return to Login?" OnClick="RedirectBtn_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
