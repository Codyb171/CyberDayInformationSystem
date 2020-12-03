<%@ Page Title="Coordinator Reports" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminReports.aspx.cs" Inherits="CyberDayInformationSystem.AdminReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Coordinator Reports</li>
    </ul>

    <link href="AdminReports-Styling.css" rel="stylesheet" type="text/css" />

    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <h2>Select a report type:</h2>
                <asp:DropDownList ID="FunctionList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionList_SelectedIndexChanged">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">Event Data</asp:ListItem>
                    <asp:ListItem Value="2">Staff Data</asp:ListItem>
                    <asp:ListItem Value="3">Attendee Data</asp:ListItem>
                    <asp:ListItem Value="5">Heat Map</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">
                <asp:MultiView runat="server" ID="ReportSearch">
                    <asp:View runat="server" ID="DateSearch">
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell runat="server">
                                <asp:Label runat="server" ID="DateLbl" Text="Event Date: "></asp:Label>
                                <asp:TextBox runat="server" ID="DateTxt" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell runat="server">
                                <asp:Button runat="server" ID="SearchDate" CssClass="btn-main reg" Text="Search" OnClick="SearchDate_OnClick" CausesValidation="False" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>
                    <asp:View runat="server" ID="NameSearch">
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell runat="server">
                                <asp:Label runat="server" ID="FirstNameLbl" Text="First Name: "></asp:Label>
                                <asp:TextBox runat="server" ID="FirstNameTxt"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell runat="server">
                                <asp:Label runat="server" ID="LastNameLbl" Text="Last  Name: "></asp:Label>
                                <asp:TextBox runat="server" ID="LastNameTxt"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell runat="server">
                                <asp:Button runat="server" ID="SearchNameBtn" Text="Search" CssClass="btn-main reg" OnClick="SearchNameBtn_OnClick" CausesValidation="False" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>
                </asp:MultiView>
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
                <asp:Button CssClass="btn-main reg" ID="RunBtn" runat="server" Text="Run Report?" OnClick="RunBtn_Click" Visible="False" />
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
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Button CssClass="btn-main reg" ID="PrintBtn" runat="server" Text="Print Report" Visible="false" OnClientClick="return PrintReport();" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server">
                <asp:Button ID="clearBtn" CssClass="btn-main reg" runat="server" Text="Start Over?" OnClick="clearBtn_OnClick" Visible="False" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
