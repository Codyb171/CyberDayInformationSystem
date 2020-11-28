<%@ Page Title="Student Notes" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminStudentNotes.aspx.cs" Inherits="CyberDayInformationSystem.AdminStudentNotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Student Administration</li>
    </ul>

    <asp:DetailsView ID="studentModDtl" runat="server" HorizontalAlign="Center"
        AutoGenerateRows="True" DataKeyNames="FIRSTNAME" DefaultMode="ReadOnly"
        Height="50px" Width="301px" Visible="true" AllowPaging="True" OnPageIndexChanging="StudentModifyDtl_PageIndexChanging">
        <PagerSettings Mode="Numeric" Position="Bottom" Visible="True"/>
    </asp:DetailsView>

</asp:Content>
