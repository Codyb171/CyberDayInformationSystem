<%@ Page Title="User Creation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserCreation.aspx.cs" Inherits="CyberDayInformationSystem.UserCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h1>User Creation Tool</h1>
    </div>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:RadioButtonList ID="UserTypeSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UserTypeSelection_SelectedIndexChanged">
                    <asp:ListItem Value="1">Teacher</asp:ListItem>
                    <asp:ListItem Value="2">Staff Volunteer</asp:ListItem>
                    <asp:ListItem Value="3">Student Volunteer</asp:ListItem>
                </asp:RadioButtonList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table ID="DefaultTable" runat="server" HorizontalAlign="Center" Visible="false">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="TitleLbl" runat="server" Text="Title:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="TitleDropDown" runat="server">
                    <asp:ListItem>Mr.</asp:ListItem>
                    <asp:ListItem>Mrs.</asp:ListItem>
                    <asp:ListItem>Ms.</asp:ListItem>
                    <asp:ListItem>Dr.</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="FirstNameLbl" runat="server" Text="First Name: " Width="200"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="FirstNameTxt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="FirstNameValid" runat="server" ErrorMessage="First Name Required" ControlToValidate="FirstNameTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="LastNameLbl" runat="server" Text="Last Name: " Width="200"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="LastNameTxt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="LastNameValid" runat="server" ErrorMessage="Last Name Required" ControlToValidate="LastNameTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="PhoneLbl" runat="server" Text="Phone Number:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="PhoneTxt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="PhoneValid" runat="server" ErrorMessage="Please enter a phone number" ControlToValidate="PhoneTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="EmailLbl" runat="server" Text="Email Address:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="EmailTxt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="EmailValid" runat="server" ErrorMessage="Please enter an Email Address" ControlToValidate="EmailTxt" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

    <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:MultiView ID="SelectedView" runat="server">
                    <asp:View ID="TeacherView" runat="server">
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Label ID="SchoolLbl" runat="server" Text="School: " Width="200"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="SchoolDropDown" runat="server">
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:RequiredFieldValidator ID="SchoolValidator" runat="server" ErrorMessage="Please Pick a School" ControlToValidate="SchoolDropDown" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Label ID="GradeLbl" runat="server" Text="Grade Taught:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="GradeDropDown" runat="server">
                                    <asp:ListItem Value="6">6th Grade</asp:ListItem>
                                    <asp:ListItem Value="7">7th Grade</asp:ListItem>
                                    <asp:ListItem Value="8">8th Grade</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:RequiredFieldValidator ID="GradeValidator" runat="server" ErrorMessage="Please Select a Grade to Teach" ForeColor="Red" ControlToValidate="GradeDropDown">
                                </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>
                    <asp:View ID="StudentView" runat="server">
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Label ID="tshirtSizeLbl" runat="server" Text="T-Shirt Size: " Width="200"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="TshirtSizeList" runat="server">
                                    <asp:ListItem>Extra-Small</asp:ListItem>
                                    <asp:ListItem>Small</asp:ListItem>
                                    <asp:ListItem>Medium</asp:ListItem>
                                    <asp:ListItem>Large</asp:ListItem>
                                    <asp:ListItem>Extra-Large</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:RequiredFieldValidator ID="ShirtSize" runat="server" ErrorMessage="Pick a T-Shirt Size" ControlToValidate="TshirtSizeList" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Label ID="MajorLbl" runat="server" Text="Major: "></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="MajorDropDown" runat="server">
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
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Label ID="MinorLbl" runat="server" Text="Minor: "></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="MinorDropDown" runat="server">
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
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Label ID="OrganizationLbl" runat="server" Text="Organization: "></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="OrgDropDown" runat="server">
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
                    </asp:View>
                </asp:MultiView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="PasswordLbl1" runat="server" Text="Enter a Password:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="PasswordTxt1" runat="server" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="PasswordValid" runat="server" ErrorMessage="Must Enter a Password" ForeColor="Red" ControlToValidate="PasswordTxt1">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Label ID="PasswordLbl2" runat="server" Text="Confirm Password:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="PasswordTxt2" runat="server" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:CompareValidator ID="PasswordMatch" runat="server" ErrorMessage="Passwords Don't Match" ControlToValidate="PasswordTxt1" ControlToCompare="PasswordTxt2" Operator="Equal" ForeColor="Red"></asp:CompareValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="3">
                <asp:Button ID="CreateBtn" runat="server" Text="Create User" OnClick="CreateBtn_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="3">
                <asp:Label ID="UserInfoLbl" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

