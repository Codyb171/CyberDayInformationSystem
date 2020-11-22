<%@ Page Title="Teacher Reports" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="TeacherReports.aspx.cs" Inherits="CyberDayInformationSystem.TeacherReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="TeacherDashboard.aspx">Teacher Home</a></li>
        <li>Teacher Reports</li>
    </ul>

    <h3 class="text-center">Please Select a Report Type</h3>

    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:RadioButtonList ID="FunctionList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionList_SelectedIndexChanged">
                    <asp:ListItem Value="1">Event Data</asp:ListItem>
                    <asp:ListItem Value="2">Student Data</asp:ListItem>
                </asp:RadioButtonList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell runat="server" >
                <asp:MultiView ID="SearchByView" runat="server">
                    <asp:View ID="ListView" runat="server">
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
                    </asp:View>
                    <asp:View ID="SearchBoxView" runat="server">
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="FindStudentFirstName" runat="server" Text="First Name: "></asp:Label>
                                <asp:TextBox ID="SearchByTagFN" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="FindStudentLastName" runat="server" Text="Last Name: "></asp:Label>
                                <asp:TextBox ID="SearchByTagLN" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="SearchByTagButton" CssClass="btn-main reg" runat="server" Text="Search"
                                    OnClick="SearchByTagButton_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:DetailsView ID="studentModifyDtl" runat="server" HorizontalAlign="Center"
                                    AutoGenerateRows="True" DataKeyNames="STUDENTID" DefaultMode="ReadOnly"
                                    Height="50px" Width="301px" Visible="true" AllowPaging="True" OnPageIndexChanging="StudentModifyDtl_PageIndexChanging">
                                    <PagerSettings Mode="Numeric" Position="Bottom" Visible="True" />
                                </asp:DetailsView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>
                </asp:MultiView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Button ID="RunBtn" CssClass="btn-main reg" runat="server" Text="Run Report?" OnClick="RunBtn_Click" />
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
                <asp:Button ID="PrintBtn" CssClass="btn-main reg" runat="server" Text="Print Report" Visible="false" OnClientClick="return PrintReport();"  />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>