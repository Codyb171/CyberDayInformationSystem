<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentEventReport.aspx.cs" Inherits="CyberDayInformationSystem.StudentEventReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="StudentDashboard.aspx">Student Home</a></li>
        <li>Event Data Report</li>
    </ul>

    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow ID="rowEventDropList">
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:DropDownList ID="ddlEvents" runat="server" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowGenReportBtn">
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button ID="btnGenReport" CssClass="btn-main reg" runat="server" Text="Generate Report >" OnClick="btnGenReport_Click" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="rowBack" Visible="false">
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button ID="btnRunNew" CssClass="btn-main reg" runat="server" Text="Start Over?" OnClick="btnRunNew_Click" />
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
                <asp:Button CssClass="btn-main reg" ID="PrintBtn" runat="server" Text="Print Report" Visible="false" OnClientClick="return PrintReport();" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
