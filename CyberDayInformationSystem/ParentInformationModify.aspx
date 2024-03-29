﻿<%@ Page Title="Parent Information" Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ParentInformationModify.aspx.cs" Inherits="CyberDayInformationSystem.ParentInformationModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/3/jquery.inputmask.bundle.js"></script>

    <ul class="breadcrumb">
        <li><a href="ParentDashboard.aspx">Parent Home</a></li>
        <li>Parent Information</li>
    </ul>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Label ID="UpdateInfolbl" runat="server" Text="Please update the following information: "></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Label ID="FirstNameLbl" runat="server" Text="First Name: " Width="200"></asp:Label>
                        <asp:TextBox ID="firstNameTxt" runat="server" Text=""></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:RequiredFieldValidator ID="FirstNameValid" runat="server" ErrorMessage="First Name Required" ControlToValidate="firstNameTxt" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Label ID="LastNameLbl" runat="server" Text="Last Name: " Width="200"></asp:Label>
                        <asp:TextBox ID="LastNameTxt" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:RequiredFieldValidator ID="LastNameValid" runat="server" ErrorMessage="Last Name Required" ControlToValidate="LastNameTxt" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Label ID="EmailLbl" runat="server" Text="Email Address: " Width="200"></asp:Label>
                        <asp:TextBox ID="EmailTxt" runat="server" TextMode="Email"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:RequiredFieldValidator ID="EmailValid" runat="server" ErrorMessage="Email Required" ControlToValidate="EmailTxt" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Label ID="PhoneLbl" runat="server" Text="Phone: " Width="200"></asp:Label>
                        <asp:TextBox ID="PhoneTxt" runat="server" ClientIDMode="Static" TextMode="Phone" AutoCompleteType="HomePhone"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="PhoneValid" runat="server" ErrorMessage="Please enter a phone number" ControlToValidate="PhoneTxt" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell>
                            <asp:Label ID="UpdateSuccessfulLbl" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Button ID="UpdateBtn" CssClass="btn-main reg" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <script>
        $(document).ready(function () {
            $("#PhoneTxt").inputmask({ mask: "(999) 999-9999" });
            $(":input").inputmask();
        });
    </script>
</asp:Content>
