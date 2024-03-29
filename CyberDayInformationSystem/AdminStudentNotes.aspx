﻿<%@ Page Title="Student Notes" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminStudentNotes.aspx.cs" Inherits="CyberDayInformationSystem.AdminStudentNotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Student Administration</li>
    </ul>

    <asp:MultiView ID="CurView" runat="server">
        <asp:View ID="StudentSearchView" runat="server">
            <asp:Table runat="server" HorizontalAlign="Center">
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
            </asp:Table>
        </asp:View>
        <asp:View ID="StudentDetailView" runat="server">
            <asp:Table ID="tblSelect" runat="server" HorizontalAlign="Center">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        <asp:DetailsView ID="studentModDtl" runat="server" HorizontalAlign="Center"
                            AutoGenerateRows="True" DataKeyNames="STUDENTID" DefaultMode="ReadOnly"
                            Height="50px" Width="301px" Visible="true" AllowPaging="True" OnPageIndexChanging="StudentModifyDtl_PageIndexChanging">
                            <PagerSettings Mode="Numeric" Position="Bottom" Visible="True" />
                        </asp:DetailsView>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <asp:Button ID="btnAddNotes" CssClass="btn-main reg" runat="server" Text="Add Notes" OnClick="btnAddNotes_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:View>

        <asp:View ID="AddNotesView" runat="server">
            <asp:Table ID="tblAdd" runat="server" HorizontalAlign="Center">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" ColumnSpan="4">
                        <asp:DetailsView ID="StudentNoteView" runat="server" Height="50px" Width="301px" AutoGenerateRows="True" DataKeyNames="NOTEID"
                            DefaultMode="ReadOnly" Visible="True" AllowPaging="True" HorizontalAlign="Center"
                            OnPageIndexChanging="StudentNoteView_OnPageIndexChanging">
                            <PagerSettings Mode="NextPrevious" Position="Bottom" Visible="True"></PagerSettings>
                        </asp:DetailsView>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="4">
                        <asp:Label ID="lblStuFName" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblStuLName" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="lblAddNote" runat="server" Text="Note: (limit 100 characters)"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="2">
                        <asp:TextBox ID="txtAddNote" runat="server" Width="450px" Height="50px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        <asp:Button ID="btnSubmit" CssClass="btn-main reg" runat="server" Text="Save Note" OnClick="btnSubmit_Click" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        <asp:Button ID="btnBack" CssClass="btn-main reg" runat="server" Text="Select New" OnClick="btnBack_Click" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
