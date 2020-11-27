<%@ Page Title="Parent Information" Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ParentInformationModify.aspx.cs" Inherits="CyberDayInformationSystem.ParentInformationModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li><a href="ParentDashboard.aspx">Parent Home</a></li>
        <li>Parent Information</li>
    </ul>

    <%-- Show the parent that is logged in's contact information to allow them to edit it --%>

    <asp:TableRow runat="server" HorizontalAlign="Center">
        <asp:TableCell>
            <asp:DetailsView ID="parentModifyDtl" AllowPaging="true" AutoGenerateRows="false" 
                CssClass="table table-bordered table-striped table-hover" runat="server" 
                AutoGenerateEditButton="true" OnModeChanging="parentModifyDtl_ModeChanging" OnItemUpdating="parentModifyDtl_ItemUpdating">  
                <Fields>  
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />  
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />  
                    <asp:BoundField DataField="EmailAdd" HeaderText="Email" />  
                    <asp:BoundField DataField="Phone" HeaderText="Phone Number" />  
                </Fields>  
            </asp:DetailsView> 
<%--<%--<%--<%--<%--<%--            <asp:MultiView ID="SelectedFunction" runat="server">
<%--                <asp:View ID="CreateView" runat="server">
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
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="EmailLbl" runat="server" Text="Email: " Width="200"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="EmailTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EmailValid" runat="server" ErrorMessage="Email Required" ControlToValidate="EmailTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="PhoneLbl" runat="server" Text="Phone: " Width="200"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="PhoneTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="PhoneValid" runat="server" ErrorMessage="Phone Required" ControlToValidate="PhoneTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                   
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="ClearBtnEdit" CssClass="btn-main reg" runat="server" Text="Clear Form" OnClick="ClearBtn_Click" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="SaveBtnEdit" CssClass="btn-main reg" runat="server" Text="Save Student" OnClick="SaveBtn_Click" CausesValidation="true" />
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:View>--%>
              <%--<%--  <asp:View ID="ModifyView" runat="server">
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="4">
                            <%--<asp:DetailsView ID="parentModifyDtl" runat="server" HorizontalAlign="Center"
                                AutoGenerateRows="True" DataKeyNames="GUARDIANID" DefaultMode="ReadOnly"
                                Height="50px" Width="301px" Visible="true">
                                <PagerSettings Mode="Numeric" Position="Bottom" Visible="True" />
                            </asp:DetailsView>--%>
<%--            <asp:DetailsView ID="parentModifyDtl" HorizontalAlign="Center" AutoGenerateRows="false" CssClass="table table-bordered table-striped table-hover" runat="server">  
                <Fields>  
                    <asp:BoundField DataField="FirstName" HeaderText="First Name: " />  
                    <asp:BoundField DataField="LastName" HeaderText="Last Name: " />  
                    <asp:BoundField DataField="Email" HeaderText="Email Address: " />  
                    <asp:BoundField DataField="Phone" HeaderText="Phone: " />   
                </Fields>  
            </asp:DetailsView> 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="2">
                            <asp:Button ID="EditParentBtn" CssClass="btn-main reg" runat="server" Text="Edit Parent" Visible="False" OnClick="EditParentBtn_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:View>
                <asp:View ID="EditView" runat="server">
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="1">
                            <asp:Label ID="EditFirstName" runat="server" Text="First Name: " Width="100px"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:TextBox ID="FirstNameTxt" runat="server" Width="200px"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="1">
                            <asp:RequiredFieldValidator ID="EditFirstNameRqd" runat="server" ErrorMessage="First Name Required" ControlToValidate="FirstNameTxt" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="1">
                            <asp:Label ID="EditLastName" runat="server" Text="Last Name: " Width="100px"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:TextBox ID="LastNameTxt" runat="server" Width="200px"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="1">
                            <asp:RequiredFieldValidator ID="EditLastNameRqd" runat="server" ErrorMessage="Last Name Required" ControlToValidate="LastNameTxt" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="1">
                            <asp:Label ID="EditEmail" runat="server" Text="Email: " Width="100px"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:TextBox ID="EmailTxt" runat="server" Width="200px" TextMode="Email"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="1">
                            <asp:RequiredFieldValidator ID="EditEmailRqd" runat="server" ErrorMessage="Email Required" ControlToValidate="EmailTxt" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="1">
                            <asp:Label ID="EditPhoneLbl" runat="server" Text="Phone: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                            <asp:TableCell ColumnSpan="2">
                            <asp:TextBox ID="PhoneTxt" runat="server" Width="200px" TextMode="Phone"></asp:TextBox>
                        </asp:TableCell>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="1">
                            <asp:RequiredFieldValidator ID="EditPhoneValid" runat="server" ErrorMessage="Phone Required" ForeColor="Red" ControlToValidate="PhoneTxt"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="4">
                            <asp:Button ID="UpdateBtn" CssClass="btn-main reg" runat="server" Text="Update" OnClick="UpdateBtn_Click1" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label ID="EditLabelStatus" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="ClearBtn" CssClass="btn-main reg" runat="server" Text="Clear Form" OnClick="ClearBtn_Click" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="SaveBtn" CssClass="btn-main reg" runat="server" Text="Save Student" OnClick="SaveBtn_Click" CausesValidation="true" />
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
           </asp:Multiview>--%>
        </asp:TableCell>
    </asp:TableRow>

</asp:Content>
