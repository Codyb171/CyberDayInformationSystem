<%@ Page Title="Update Volunteer Information" Language="C#" MasterPageFile="~/Volunteer.Master" AutoEventWireup="true" CodeBehind="VolunteerInformationModify.aspx.cs" Inherits="CyberDayInformationSystem.VolunteerInformationModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/3/jquery.inputmask.bundle.js"></script>

    <ul class="breadcrumb">
        <li><a href="VolunteerDashboard.aspx">Volunteer Home</a></li>
        <li>Update Volunteer Information</li>
    </ul>

        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="UpdateInfolbl" runat="server" Text="Please update the following information: "></asp:Label>
                   </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label ID="FirstNameLbl" runat="server" Text="First Name: " Width="200"></asp:Label>
                        <asp:TextBox ID="firstNameTxt" runat="server" Text=""></asp:TextBox>
                    </asp:TableCell>

                    <asp:TableCell>
                        <asp:RequiredFieldValidator ID="FirstNameValid" runat="server" ErrorMessage="First Name Required" ControlToValidate="firstNameTxt" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label ID="LastNameLbl" runat="server" Text="Last Name: " Width="200"></asp:Label>
                        <asp:TextBox ID="LastNameTxt" runat="server"></asp:TextBox>
                    </asp:TableCell>

                    <asp:TableCell ColumnSpan="1">
                        <asp:RequiredFieldValidator ID="LastNameValid" runat="server" ErrorMessage="Last Name Required" ControlToValidate="LastNameTxt" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label ID="EmailLbl" runat="server" Text="Email Address: " Width="200"></asp:Label>
                        <asp:TextBox ID="EmailTxt" runat="server" TextMode="Email"></asp:TextBox>
                    </asp:TableCell>

                    <asp:TableCell ColumnSpan="1">
                        <asp:RequiredFieldValidator ID="EmailValid" runat="server" ErrorMessage="Email Required" ControlToValidate="EmailTxt" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label ID="PhoneLbl" runat="server" Text="Phone: " Width="200"></asp:Label>
                        <asp:TextBox ID="PhoneTxt" runat="server" ClientIDMode="Static" TextMode="Phone" AutoCompleteType="HomePhone"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="PhoneValid" runat="server" ErrorMessage="Please enter a phone number" ControlToValidate="PhoneTxt" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                <asp:TableRow ID="RowStuPrevVol" runat="server" HorizontalAlign="Center" Visible="false">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label ID="lblPrevVol" runat="server" Text="Previous Volunteer: "></asp:Label>
                        <asp:DropDownList ID="PrevVolDDL" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="1">
                        <asp:Label ID="MealTicketLbl" runat="server" Text="Meal Ticket: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="1">
                        <asp:DropDownList ID="MealTicketDDL" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value ="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow ID="RowStuCurMajor" runat="server" HorizontalAlign="Center" Visible="false">
                    <asp:TableCell>
                        <asp:Label ID="CurMajorLbl" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="RowStuVolMajor" runat="server" HorizontalAlign="Center" Visible="false">
                    <asp:TableCell>
                        <asp:Label ID="MajorLbl" runat="server" Text="Major: "></asp:Label>
                        <asp:DropDownList ID="MajorDropDown" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Accounting</asp:ListItem>
                            <asp:ListItem>Computer Information Systems</asp:ListItem>
                            <asp:ListItem>Computer Science</asp:ListItem>
                            <asp:ListItem>Economics</asp:ListItem>
                            <asp:ListItem>Engineering</asp:ListItem>
                            <asp:ListItem>Finance</asp:ListItem>
                            <asp:ListItem>Graphic Design</asp:ListItem>
                            <asp:ListItem>Integrated Science and Technology</asp:ListItem>
                            <asp:ListItem>International Business</asp:ListItem>
                            <asp:ListItem>Management</asp:ListItem>
                            <asp:ListItem>Marketing</asp:ListItem>
                            <asp:ListItem>Media Arts and Design</asp:ListItem>
                            <asp:ListItem>International Business</asp:ListItem>
                            <asp:ListItem>Quantitative Finance</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>

                    <asp:TableCell>
                        <asp:RequiredFieldValidator ID="MajorRqdValidator" runat="server" ErrorMessage="Please Choose a Major or Select Other" ForeColor="Red" ControlToValidate="MajorDropDown">
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                 <asp:TableRow ID="RowStuCurMinor" runat="server" HorizontalAlign="Center" Visible="false">
                    <asp:TableCell>
                        <asp:Label ID="CurMinorLbl" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="RowStuVolMinor" runat="server" HorizontalAlign="Center" Visible="false">
                     <asp:TableCell>
                        <asp:Label ID="MinorLbl" runat="server" Text="Minor: "></asp:Label>
                        <asp:DropDownList ID="MinorDropDown" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Accounting</asp:ListItem>
                            <asp:ListItem>Computer Information Systems</asp:ListItem>
                            <asp:ListItem>Computer Science</asp:ListItem>
                            <asp:ListItem>Economics</asp:ListItem>
                            <asp:ListItem>Engineering</asp:ListItem>
                            <asp:ListItem>Finance</asp:ListItem>
                            <asp:ListItem>Graphic Design</asp:ListItem>
                            <asp:ListItem>Integrated Science and Technology</asp:ListItem>
                            <asp:ListItem>International Business</asp:ListItem>
                            <asp:ListItem>Management</asp:ListItem>
                            <asp:ListItem>Marketing</asp:ListItem>
                            <asp:ListItem>Media Arts and Design</asp:ListItem>
                            <asp:ListItem>International Business</asp:ListItem>
                            <asp:ListItem>Quantitative Finance</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="RowCurStuOrg" runat="server" HorizontalAlign="Center" Visible="false">
                    <asp:TableCell>
                        <asp:Label ID="CurOrgLbl" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="RowStuVolOrg" runat="server" HorizontalAlign="Center" Visible="false">
                    <asp:TableCell>
                        <asp:Label ID="OrganizationLbl" runat="server" Text="Organization: "></asp:Label>
                        <asp:DropDownList ID="OrgDropDown" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Association for Information Systems (AIS)</asp:ListItem>
                            <asp:ListItem>Association for Computing Machinery (ACM)</asp:ListItem>
                            <asp:ListItem>Unix Users Group (UUG)</asp:ListItem>
                            <asp:ListItem>Computer Forensics Group</asp:ListItem>
                            <asp:ListItem>Upsilon Pi Epsilon (UPE)</asp:ListItem>
                            <asp:ListItem>Cyber Defense Club</asp:ListItem>
                            <asp:ListItem>Women in Technology (WIT)</asp:ListItem>
                            <asp:ListItem>The Robotics Club</asp:ListItem>
                            <asp:ListItem>Other Business Organization</asp:ListItem>
                            <asp:ListItem>Other Non-Business Organization</asp:ListItem>
                        </asp:DropDownList>
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
    </asp:Table>
    <script>
        $(document).ready(function () {
            $("#PhoneTxt").inputmask({ mask: "(999) 999-9999" });
            $(":input").inputmask();
        });
    </script>

</asp:Content>
