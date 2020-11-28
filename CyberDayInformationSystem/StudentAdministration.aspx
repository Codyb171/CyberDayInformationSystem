<%@ Page Title="Student Administration" Language="C#" MasterPageFile="~/Teacher.Master" AutoEventWireup="true" CodeBehind="StudentAdministration.aspx.cs" Inherits="CyberDayInformationSystem.StudentAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Student Administration</li>
    </ul>
    
    <h3>Please Select a Task</h3>

    <div class="text-center">
        <div class="text-center">
            <asp:Table runat="server" HorizontalAlign="Center">
                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="2">
                        <asp:Button ID="btnCreateStu" CssClass="btn-main reg" runat="server" Text="Create A Student" CausesValidation="false" OnClick="btnCreateStuClick" />
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Button ID="btnModStu" CssClass="btn-main reg" runat="server" Text="Modify A Student" CausesValidation="false" OnClick="btnModStuClick" />
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
                                        <asp:TextBox ID="AgeTxt" runat="server" TextMode="Number"></asp:TextBox>
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
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="GenderLbl" runat="server" Text="Gender: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="GenderDropDown" runat="server">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Non-binary</asp:ListItem>
                                            <asp:ListItem>Prefer Not To Identify</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="Gender" runat="server" ErrorMessage="Gender Required" ForeColor="Red" ControlToValidate="GenderDropDown"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                               <asp:TableRow runat="server" HorizontalAlign="Center">
                                   <asp:TableCell runat="server" ColumnSpan="4">
                                       <asp:MultiView ID="createTshirt" runat="server">
                                           <asp:View runat="server">
                                               <asp:TableRow runat="server" HorizontalAlign="Center" Visible="False">
                                                   <asp:TableCell ColumnSpan="1">
                                                       <asp:Label ID="tshirtSizeLbl" runat="server" Text="T-Shirt Size: " Width="200"></asp:Label>
                                                   </asp:TableCell>
                                                   <asp:TableCell ColumnSpan="1">
                                                       <asp:DropDownList ID="TshirtSizeList" runat="server">
                                                           <asp:ListItem>Extra-Small</asp:ListItem>
                                                           <asp:ListItem>Small</asp:ListItem>
                                                           <asp:ListItem>Medium</asp:ListItem>
                                                           <asp:ListItem>Large</asp:ListItem>
                                                           <asp:ListItem>Extra-Large</asp:ListItem>
                                                       </asp:DropDownList>
                                                   </asp:TableCell>
                                                   <asp:TableCell ColumnSpan="2">
                                                       <asp:RequiredFieldValidator ID="ShirtSize" runat="server" ErrorMessage="Pick a T-Shirt Size" ControlToValidate="TshirtSizeList" ForeColor="Red" Enabled="False">
                                                       </asp:RequiredFieldValidator>
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                           </asp:View>
                                       </asp:MultiView>
                                   </asp:TableCell>
                               </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="PreviousLbl" runat="server" Text="Previous Attendee?"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="PreAttendBtn" runat="server">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="PreAttendValid" runat="server" ErrorMessage="Required Field" ControlToValidate="PreAttendBtn" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="MealLbl" runat="server" Text="Meal Voucher?"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="MealBtn" runat="server">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="MealValid" runat="server" ErrorMessage="Required Field" ForeColor="Red" ControlToValidate="MealBtn"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell runat="server" ColumnSpan="4">
                                        <asp:MultiView ID="CoordinatorView" runat="server">
                                            <asp:View runat="server">
                                                <asp:TableRow runat="server">
                                                    <asp:TableCell ColumnSpan="1">
                                                        <asp:Label ID="TeacherLbl" runat="server" Text="Teacher: " Width="200"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2">
                                                        <asp:DropDownList ID="TeacherDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TeacherDropDown_SelectedIndexChanged"></asp:DropDownList>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="1">
                                                        <asp:RequiredFieldValidator ID="TeacherListValid" runat="server" ErrorMessage="Please Assign a Teacher" ControlToValidate="TeacherDropDown" ForeColor="Red">
                                                        </asp:RequiredFieldValidator>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server">
                                                    <asp:TableCell ColumnSpan="1">
                                                        <asp:Label ID="SchoolLbl" runat="server" Text="School: " Width="200"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2">
                                                        <asp:DropDownList ID="SchoolDropDown" runat="server" Enabled="false">
                                                        </asp:DropDownList>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="1">
                                                        <asp:RequiredFieldValidator ID="SchoolValid" runat="server" ErrorMessage="Please Pick a School" ControlToValidate="SchoolDropDown" ForeColor="Red">
                                                        </asp:RequiredFieldValidator>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:View>
                                        </asp:MultiView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="noteLbl" runat="server" Text="Allergies, Medical Conditions?"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="noteTxt" runat="server" TextMode="MultiLine" Wrap="True" MaxLength="100"></asp:TextBox>
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
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                       <asp:Label ID="ShowLbl" runat="server" Text="Show Credentials?  " Visible="false"></asp:Label>
                                        <asp:CheckBox ID="ShowPassCheck" runat="server" AutoPostBack="true" OnCheckedChanged="ShowPassCheck_CheckedChanged" Visible="false"/>
                                        <br />
                                        <br />
                                        <asp:Label ID="StudentUserLbl" runat="server" Text="Student Username: " Visible="false"></asp:Label>
                                        <asp:Label ID="UsernameLbl" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Label ID="StudentUserPass" runat="server" Text="Student Password: " Visible="false"></asp:Label>
                                        <asp:Label ID="PasswordLbl" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />                                        <br />
                                        <asp:Label ID="ParentUserLbl" runat="server" Text="Parent Username: " Visible="false"></asp:Label>
                                        <asp:Label ID="UsernameLblPar" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Label ID="ParentUserPass" runat="server" Text="Parent Password: " Visible="false"></asp:Label>
                                        <asp:Label ID="PasswordLblPar" runat="server" Text="" Visible="false"></asp:Label>
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
                                        <asp:Button ID="SearchByTagButton" CssClass="btn-main reg" runat="server" Text="Search"
                                            OnClick="SearchByTagButton_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:DetailsView ID="studentModifyDtl" runat="server" HorizontalAlign="Center"
                                            AutoGenerateRows="True" DataKeyNames="STUDENTID" DefaultMode="ReadOnly"
                                            Height="50px" Width="301px" Visible="true" AllowPaging="True" OnPageIndexChanging="StudentModifyDtl_PageIndexChanging">
                                            <PagerSettings Mode="Numeric" Position="Bottom" Visible="True"/>
                                        </asp:DetailsView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="EditStudentBtn" CssClass="btn-main reg" runat="server" Text="Edit Student" Visible="False" OnClick="EditStudentBtn_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="DeleteStudentBtn" CssClass="btn-main reg" runat="server" Text="Delete Student?" Visible="False" OnClick="DeleteStudentBtn_OnClick" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>
<%--                            <asp:View ID="EditView" runat="server">
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
                                    <asp:TableCell runat="server" ColumnSpan="4">
                                        <asp:MultiView ID="editTshirtView" runat="server">
                                            <asp:View runat="server">
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
                                            </asp:View>
                                        </asp:MultiView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EditAttendeeLbl" runat="server" Text="Previous Attendee?"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RadioButtonList ID="EditAttendeeBtn" runat="server">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="EditAttendeeValid" runat="server" ErrorMessage="Required Field" ControlToValidate="EditAttendeeBtn" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EditMeal" runat="server" Text="Meal Voucher?"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RadioButtonList ID="EditMealBtn" runat="server">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="EditMealValid" runat="server" ErrorMessage="Required Field" ForeColor="Red" ControlToValidate="EditMealBtn"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EditTeacher" runat="server" Text="Teacher: " Width="100px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="EditTeacherDropDown" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="TeacherDropDown_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="EditTeacherValid" runat="server" ErrorMessage="Required Field" ForeColor="Red" ControlToValidate="EditTeacherDropDown"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EditSchool" runat="server" Text="School: " Width="100px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:DropDownList ID="EditSchoolDropDown" runat="server" Enabled="false" Width="200px"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
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
                            </asp:View>--%>
                        </asp:MultiView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
</asp:Content>
