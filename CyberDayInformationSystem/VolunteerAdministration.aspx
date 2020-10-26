<%@ Page Title="" Language="C#" MasterPageFile="~/Volunteer.Master" AutoEventWireup="true" CodeBehind="VolunteerAdministration.aspx.cs" Inherits="CyberDayInformationSystem.VolunteerAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Volunteer Creation</h1>
    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="4">
                <asp:RadioButtonList ID="FunctionSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionSelection_SelectedIndexChanged" >
                    <asp:ListItem Value="1">Create a Volunteer</asp:ListItem>
                    <asp:ListItem Value="2">Assign Volunteer</asp:ListItem>
                </asp:RadioButtonList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" HorizontalAlign="Center">
            <asp:TableCell>
                <asp:MultiView ID="SelectedFunction" runat="server">
                    <asp:View ID="CreateView" runat="server">
                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="1">
                                <asp:Label ID="lblFName" runat="server" Text="First Name: " Width="200"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="1">
                                <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:RequiredFieldValidator ID="fNameValid" runat="server" ErrorMessage="First Name Required" ControlToValidate="txtFName" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="1">
                                <asp:Label ID="lblLName" runat="server" Text="Last Name: " Width="200"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="1">
                                <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:RequiredFieldValidator ID="lNameValid" runat="server" ErrorMessage="Last Name Required" ControlToValidate="txtLName" ForeColor="Red"> </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="1">
                                <asp:Label ID="lblPhoneNum" runat="server" Text="Phone Number: " Width="200"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="1">
                                <asp:TextBox ID="txtPhoneNum" runat="server"></asp:TextBox>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:RequiredFieldValidator ID="RequiredAge" runat="server" ErrorMessage="Phone Number Required" ControlToValidate="txtPhoneNum" ForeColor="Red"> </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="1">
                                <asp:Label ID="lblEmail" runat="server" Text="Email: " Width="200"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="1">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ErrorMessage="Email Required" ControlToValidate="txtEmail" ForeColor="Red"> </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="1">
                                <asp:Label ID="lblShirtSize" runat="server" Text="T-Shirt Size: " Width="200"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="2">
                                <asp:DropDownList ID="ddlShirtSize" runat="server">
                                    <asp:ListItem>Extra-Small</asp:ListItem>
                                    <asp:ListItem>Small</asp:ListItem>
                                    <asp:ListItem>Medium</asp:ListItem>
                                    <asp:ListItem>Large</asp:ListItem>
                                    <asp:ListItem>Extra-Large</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>

                            <asp:TableCell ColumnSpan="1">
                                <asp:RequiredFieldValidator ID="ShirtSize" runat="server" ErrorMessage="Pick a T-Shirt Size" ControlToValidate="TshirtSizeList" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:RadioButtonList ID="VolunteerType" runat="server" OnSelectedIndexChanged="VolunteerType_SelectedIndexChanged" >
                                    <asp:ListItem Value="1">Student</asp:ListItem>
                                    <asp:ListItem Value="2">Faculty/Staff</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell runat="server" ColumnSpan="4">
                                <asp:MultiView ID="StuVolView" runat="server">
                                    <asp:View runat="server">
                                        <asp:TableRow runat="server" HorizontalAlign="Center">
                                            <asp:TableCell ColumnSpan="1">
                                                <asp:Label ID="lblMajor" runat="server" Text="Major: " Width="200"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell ColumnSpan="1">
                                                <asp:TextBox ID="txtMajor" runat="server"></asp:TextBox>
                                            </asp:TableCell>

                                            <asp:TableCell ColumnSpan="2">
                                                <asp:RequiredFieldValidator ID="StuMajor" runat="server" ErrorMessage="Please Enter Your Major" ControlToValidate="txtMajor" ForeColor="Red">
                                                </asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        
                                        <asp:TableRow runat="server" HorizontalAlign="Center">
                                            <asp:TableCell ColumnSpan="1">
                                                <asp:Label ID="lblMinor" runat="server" Text="Minor (if applicable): " Width="200"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:TextBox ID="txtMinor" runat="server"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow runat="server" HorizontalAlign="Center">
                                            <asp:TableCell ColumnSpan="1">
                                                <asp:Label ID="lblOrg" runat="server" Text="Organization Affiliation: " Width="200"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtOrg" runat="server"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow runat="server" HorizontalAlign="Center">
                                            <asp:TableCell>
                                                <asp:Label ID="lblPrevVol" runat="server" Text="Are you a previous volunteer?"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RadioButtonList ID="PrevVol" runat="server" OnSelectedIndexChanged="PrevVol_SelectedIndexChanged" >
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:View>
                                </asp:MultiView>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Form" OnClick="btnClear_Click" CausesValidation="false" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnPopulate" runat="server" Text="Populate Form" OnClick="btnPopulate_Click" CausesValidation="false" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnSave" runat="server" Text="Save Student" OnClick="btnSave_Click"  CausesValidation="true" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" HorizontalAlign="Center">
                            <asp:TableCell>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:View>

                    <asp:View ID="ModifyView" runat="server">
                    </asp:View>
                </asp:MultiView>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>