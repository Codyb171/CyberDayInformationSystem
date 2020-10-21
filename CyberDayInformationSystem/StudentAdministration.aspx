﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher.Master" AutoEventWireup="true" CodeBehind="StudentAdministration.aspx.cs" Inherits="CyberDayInformationSystem.StudentAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h1>Student Creation</h1>
        <div class="text-center">
            <asp:Table runat="server" HorizontalAlign="Center">
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="4">
                        <asp:RadioButtonList ID="FunctionSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionSelection_SelectedIndexChanged">
                            <asp:ListItem Value="1">Create a Student</asp:ListItem>
                            <asp:ListItem Value="2">Modify a Student</asp:ListItem>
                            <asp:ListItem Value="3">Claim Unassigned Students</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:MultiView ID="SelectedFunction" runat="server">
                            <asp:View ID="CreateView" runat="server">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="FirstNameLbl" runat="server" Text="First Name: " Width="200"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="FirstNameTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="FirstNameValid" runat="server" ErrorMessage="First Name Required" ControlToValidate="FirstNameTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="LastNameLbl" runat="server" Text="Last Name: " Width="200"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="LastNameTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="LastNameValid" runat="server" ErrorMessage="Last Name Required" ControlToValidate="LastNameTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="AgeLbl" runat="server" Text="Age: " Width="200"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="AgeTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="RequiredAge" runat="server" ErrorMessage="Age Required" ControlToValidate="AgeTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="AgeRange" runat="server" ErrorMessage="Invalid Age" MaximumValue="100" MinimumValue="1"
                                            Type="Integer" ForeColor="Red" ControlToValidate="AgeTxt">
                                        </asp:RangeValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell>
                                        <asp:Label ID="GenderLbl" runat="server" Text="Gender: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="GenderDropDown" runat="server">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Non-binary</asp:ListItem>
                                            <asp:ListItem>Perfer Not To Identify</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="Gender" runat="server" ErrorMessage="Gender Required" ForeColor="Red" ControlToValidate=""></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
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
                                        <asp:Button ID="ClearBtn" runat="server" Text="Clear Form" OnClick="ClearBtn_Click" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="PopulateBtn" runat="server" Text="Populate Form" OnClick="PopulateBtn_Click" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="SaveBtn" runat="server" Text="Save Student" OnClick="SaveBtn_Click" CausesValidation="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="UserInfoLbl" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>
                            <asp:View ID="ModifyView" runat="server">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="FindStudentFirstName" runat="server" Text="First Name: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="SearchByTagFN" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="FindStudentLastName" runat="server" Text="Last Name: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="SearchByTagLN" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Button ID="SearchByTagButton" runat="server" Text="Search"
                                            OnClick="SearchByTagButton_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:DetailsView ID="studentModifyDtl" runat="server" HorizontalAlign="Center"
                                            AutoGenerateRows="True" DataKeyNames="STUDENTID" DefaultMode="ReadOnly"
                                            Height="50px" Width="301px" Visible="true" AllowPaging="True" OnPageIndexChanging="studentModifyDtl_PageIndexChanging">
                                            <PagerSettings Mode="Numeric" Position="Bottom" Visible="True"/>
                                        </asp:DetailsView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Button ID="EditStudentBtn" runat="server" Text="Edit Student" Visible="false" OnClick="EditStudentBtn_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>
                            <asp:View ID="EditView" runat="server">
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
                                        <asp:TextBox ID="EditAgeTxt" runat="server" Width="200px"></asp:TextBox>
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
                                        <asp:Label ID="EditSizeLbl" runat="server" Text="T-Shirt Size: " Width="100px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="EditSizeList" runat="server" Width="200px">
                                            <asp:ListItem>Extra-Small</asp:ListItem>
                                            <asp:ListItem>Small</asp:ListItem>
                                            <asp:ListItem>Medium</asp:ListItem>
                                            <asp:ListItem>Large</asp:ListItem>
                                            <asp:ListItem>Extra-Large</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EditTeacher" runat="server" Text="Teacher: " Width="100px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="TeacherDropDown" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="TeacherDropDown_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EditSchool" runat="server" Text="School: " Width="100px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="SchoolDropDown" runat="server" Enabled="false" Width="200px"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Button ID="UpdateBtn" runat="server" Text="Save" OnClick="UpdateBtn_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Label ID="EditLabelStatus" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>
                            <asp:View runat="server">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Label ID="UnclaimedLbl" runat="server" Text="Claim Students" Font-Size="Large"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="Student" runat="server" Text="Students: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="StudentList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="StudentList_SelectedIndexChanged"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:GridView ID="StudentGridView" runat="server" AutoGenerateColumns="True" CellPadding="5" Width="450">
                                            <HeaderStyle BackColor="#0066cc" Font-Bold="true" ForeColor="White" />
                                            <RowStyle BackColor="#bfdfff" ForeColor="Black" />
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Button ID="ClaimBtn" runat="server" Text="Claim Student?" OnClick="ClaimBtn_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:Label ID="ClaimStatusLbl" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>
                        </asp:MultiView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
</asp:Content>