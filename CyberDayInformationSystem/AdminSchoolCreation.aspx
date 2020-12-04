<%@ Page Title="School Creation" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminSchoolCreation.aspx.cs" Inherits="CyberDayInformationSystem.AdminSchoolCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
<script src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/3/jquery.inputmask.bundle.js"></script>
    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Attendees</li>
        <li>School Creation</li>
    </ul>

    <asp:Table ID="tblSchoolInfo" runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblName" runat="server" Text="School Name:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ErrorMessage="Name is required" ForeColor="Red" ControlToValidate="txtName"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblAdd1" runat="server" Text="Address 1:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtAdd1" runat="server" Width="300px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAdd1" runat="server" ErrorMessage="Address Line 1 is required" ForeColor="Red" ControlToValidate="txtAdd1"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblAdd2" runat="server" Text="Address 2:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtAdd2" runat="server" Width="300px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtCity" runat="server" Width="300px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCity" runat="server" ErrorMessage="City is required" ForeColor="Red" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtState" runat="server" Width="60px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorState" runat="server" ErrorMessage="State is required" ForeColor="Red" ControlToValidate="txtState"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblZip" runat="server" Text="Zip:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtZip" runat="server" Width="125px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorZip" runat="server" ErrorMessage="Zip is required" ForeColor="Red" ControlToValidate="txtZip"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="1">
                <asp:Label ID="lblPhone" runat="server" Text="Phone Number:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="1">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="Phone is required" ForeColor="Red" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnClear" CssClass="btn-main reg" runat="server" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btnSubmit" CssClass="btn-main reg" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <script>
        $(document).ready(function () {
            $("#txtPhone").inputmask({ mask: "(999) 999-9999" });
            $(":input").inputmask();
        });
    </script>
</asp:Content>
