<%@ Page Title="Volunteer Administration" Language="C#" MasterPageFile="~/Volunteer.Master" AutoEventWireup="true" CodeBehind="VolunteerAdministration.aspx.cs" Inherits="CyberDayInformationSystem.VolunteerAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Volunteer Administration</li>
    </ul>

    <h3>Select a Function</h3>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow ID="rowFunctionBtn" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnAssignVol" CssClass="btn-main reg" runat="server" Text="Assign Volunteers" CausesValidation="false" OnClick="btnAssignVol_Click" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnDelVol" CssClass="btn-main reg" runat="server" Text="Unassign Volunteers" CausesValidation="false" OnClick="btnDelVol_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:MultiView ID="SelectedFunction" runat="server">
                    <asp:View ID="AssignView" runat="server">
                         <asp:TableRow ID="rowSelDate" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblSelDate" runat="server" Text="Select Event Date: " Width="200"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlEventDates" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowDateNextBtn" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnSelNext" CssClass="btn-main blue" runat="server" Text="Next >" OnClick="btnSelDateNext_Click"/>
                            </asp:TableCell>
                        </asp:TableRow>


                        <asp:TableRow ID="rowSelVol" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblSelVol" runat="server" Text="Select Volunteer: " Width="200"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlVols" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowSubmitBtn" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnBack" CssClass="btn-main blue" runat="server" Text="< Back" OnClick="btnBack_Click" />
                            </asp:TableCell>
            
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnSubmit" CssClass="btn-main reg" runat="server" Text="Submit >" OnClick="btnSubmit_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowBtnReset" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnReset" CssClass="btn-main reg" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>

                    <asp:View ID="UnassignView" runat="server">
                         <asp:TableRow ID="rowEditDate" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblSelectDate" runat="server" Text="Select Event Date: " Width="200"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlDates" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowNext" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnNext" CssClass="btn-main blue" runat="server" Text="Next >" OnClick="btnNext_Click" />
                            </asp:TableCell>
                        </asp:TableRow>


                        <asp:TableRow ID="rowVol" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblVol" runat="server" Text="Select Volunteer: " Width="200"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlVol" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowBtns" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnReturn" CssClass="btn-main blue" runat="server" Text="< Back" OnClick="btnReturn_Click" />
                            </asp:TableCell>
            
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnUnassign" CssClass="btn-main reg" runat="server" Text="Submit >" OnClick="btnUnassign_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowReset" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnRestart" CssClass="btn-main reg" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblStat" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>
                </asp:MultiView>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>