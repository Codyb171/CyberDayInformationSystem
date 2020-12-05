<%@ Page Title="Student Information" Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="StudentInformationModify.aspx.cs" Inherits="CyberDayInformationSystem.StudentInformationModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="ParentDashboard.aspx">Parent Home</a></li>
        <li>Student Information</li>
    </ul>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="EditStudentLbl" runat="server" Text="Update Student Information: "></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="EditFirstName" runat="server" Text="First Name: " Width="100px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="EditFirstNameTxt" runat="server" Width="200px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="EditFirstNameRqd" runat="server" ErrorMessage="First Name Required" ControlToValidate="EditFirstNameTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="EditLastName" runat="server" Text="Last Name: " Width="100px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="EditLastNameTxt" runat="server" Width="200px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="EditLastNameRqd" runat="server" ErrorMessage="Last Name Required" ControlToValidate="EditLastNameTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="EditAge" runat="server" Text="Age: " Width="100px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="EditAgeTxt" runat="server" Width="200px" TextMode="Number"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="EditAgeRqd" runat="server" ErrorMessage="Age Required" ControlToValidate="EditAgeTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="EditAgeRangeRqd" runat="server" ErrorMessage="Invalid Age" MaximumValue="100" MinimumValue="1"
                    Type="Integer" ForeColor="Red" ControlToValidate="EditAgeTxt">
                </asp:RangeValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="EditGenderLbl" runat="server" Text="Gender: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:DropDownList ID="EditGenderList" runat="server">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                    <asp:ListItem>Non-binary</asp:ListItem>
                    <asp:ListItem>Prefer Not To Identify</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="EditGenderValid" runat="server" ErrorMessage="Gender Required" ForeColor="Red" ControlToValidate="EditGenderList"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="EditAttendeeLbl" runat="server" Text="Previous Attendee?"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:DropDownList ID="EditAttendeeBtn" runat="server">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="EditAttendeeValid" runat="server" ErrorMessage="Required Field" ControlToValidate="EditAttendeeBtn" ForeColor="Red"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="StudentEmailLbl" runat="server" Text="Email Address: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:TextBox ID="StudentEmailTxt" runat="server" Text=""></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Button ID="UpdateBtn" CssClass="btn-main reg" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Label ID="EditLabelStatus" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
