<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AttendeeReport.aspx.cs" Inherits="CyberDayInformationSystem.AttendeeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server">
            <asp:TableCell>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="RunBtn" runat="server" Text="Run Report?" OnClick="RunBtn_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2">
                                <asp:GridView ID="RosterGridView" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="450" HorizontalAlign ="Center">
                                    <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                     </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
