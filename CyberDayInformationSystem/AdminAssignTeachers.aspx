<%@ Page Title="Teacher Administration" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAssignTeachers.aspx.cs" Inherits="CyberDayInformationSystem.AdminAssignTeachers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Attendees</li>
        <li>Teacher Administration</li>
    </ul>

    <h3>Select a Function</h3>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow ID="rowFunctionBtn" runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnAssignTeach" CssClass="btn-main reg" runat="server" Text="Assign Teachers" CausesValidation="false" OnClick="btnAssignTeach_Click" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnDelTeach" CssClass="btn-main reg" runat="server" Text="Unassign Teachers" CausesValidation="false" OnClick="btnDelTeach_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:MultiView ID="SelectedFunction" runat="server">
                    <asp:View ID="AssignView" runat="server">

                        <asp:TableRow ID="rowAssignDate" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblEventDate" runat="server" Text="Event Date: "></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlEventDates" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowAssignNextBtn" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnAssignNext" CssClass="btn-main blue" runat="server" Text="Next >" OnClick="btnAssignNext_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowAssignSelTeach" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblAssignSelTeach" runat="server" Text="Select Teacher:"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlAssignTeachers" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowAssignSubmitBtn" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnAssignBack" runat="server" CssClass="btn-main blue" Text="< Back" OnClick="btnAssignBack_Click" />
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnAssignSubmit" runat="server" CssClass="btn-main reg" Text="Submit >" OnClick="btnAssignSubmit_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                         <asp:TableRow ID="rowReset" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnReset" CssClass="btn-main reg" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblAssignStatus" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>

                    <asp:View ID="UnassignView" runat="server">
                        <asp:TableRow ID="rowRemoveDate" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblDate" runat="server" Text="Event Date: "></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlDates" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowRemoveNextBtn" runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnRemoveNext" CssClass="btn-main blue" runat="server" Text="Next >" OnClick="btnRemoveNext_Click"  />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowRemoveSelTeach" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="lblRemoveSelTeach" runat="server" Text="Select Teacher:"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlRemoveTeacher" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowRemoveSubmitBtn" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnRemoveBack" CssClass="btn-main blue" runat="server" Text="< Back" OnClick="btnRemoveBack_Click" />
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button ID="btnRemoveSubmit" runat="server" CssClass="btn-main reg" Text="Submit >" OnClick="btnRemoveSubmit_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                         <asp:TableRow ID="rowRestart" runat="server" HorizontalAlign="Center" Visible="false">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Button ID="btnRestart" CssClass="btn-main reg" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblRemoveStatus" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>
                </asp:MultiView>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
