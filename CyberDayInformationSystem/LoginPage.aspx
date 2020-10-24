<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CyberDayInformationSystem.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="text-center">
        <h1><%:Title%></h1>
    </div>
    <asp:Panel runat="server" DefaultButton="LoginBtn">
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="UsernameLbl" runat="server" Text="Username: " ></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="UsernameTxt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" ControlToValidate="UsernameTxt" runat="server" Text="-Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="PasswordLbl" runat="server" Text="Password: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="PasswordTxt" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="PasswordTxt" runat="server" Text="-Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click"/>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="NewUserBtn" runat="server" Text="New User?" OnClick="NewUserBtn_Click" CausesValidation="false"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="text-center">
        <asp:Label ID="LoginStat" runat="server" Text="Invalid Username or Password!" ForeColor="Red" Visible="false"></asp:Label>
    </div>
    </asp:Panel>
</asp:Content>
