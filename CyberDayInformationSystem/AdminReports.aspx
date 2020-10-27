<%@ Page Title="Admin Reports" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminReports.aspx.cs" Inherits="CyberDayInformationSystem.AdminReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="AdminReports-Styling.css" rel="stylesheet" type="text/css" />

    <h1>Report Access</h1>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <h2>Select a report type:</h2>
                <asp:RadioButtonList ID="FunctionList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionList_SelectedIndexChanged">
                    <asp:ListItem Value="1">Event Data</asp:ListItem>
                    <asp:ListItem Value="2">Staff Data</asp:ListItem>
                    <asp:ListItem Value="3">Attendee Data</asp:ListItem>
                    <asp:ListItem Value="4">Meal Ticket Data</asp:ListItem>
                </asp:RadioButtonList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Label ID="SelectionLbl" runat="server" Text="Event: " Visible="false"></asp:Label>
                <asp:DropDownList ID="SelectionDropDown" runat="server" Visible="false"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
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
                <asp:Panel runat="server" ID="printPanel" CssClass="PrintPanel">
                    <asp:Table runat="server" ID="ReportTable" HorizontalAlign="Center">
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label ID="SelectedGridLbl" runat="server" Text="" Visible="false" Font-Size="Larger"></asp:Label>
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
                            <asp:Label ID="SecondaryGrid1Lbl" runat="server" Text="" Visible="false" Font-Size="Larger"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="SecondaryGrid2Lbl" runat="server" Text="" Visible="false" Font-Size="Larger"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="2">
                            <asp:GridView ID="SecondaryGridView1" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="450">
                                <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                                <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                            </asp:GridView>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:GridView ID="SecondaryGridView2" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="450">
                                <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                                <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label ID="TertiaryGridLbl" runat="server" Text="" Visible="false" Font-Size="Larger"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="4">
                            <asp:GridView ID="TertiaryGridView" runat="server" AutoGenerateColumns="true" CellPadding="5" Width="900">
                                <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                                <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>

                   </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Button ID="PrintBtn" runat="server" Text="Print Report" Visible="false" OnClientClick="return PrintReport();" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>