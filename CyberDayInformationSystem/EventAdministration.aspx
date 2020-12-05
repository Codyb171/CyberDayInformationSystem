<%@ Page Title="Event Administration" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="EventAdministration.aspx.cs" Inherits="CyberDayInformationSystem.EventAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li>Event Administration</li>
    </ul>

    <h3>Please Select an Action</h3>

    <asp:Label ID="bug" Text="" runat="server" />

    <div class="text-center">
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                    <asp:MultiView ID="SelectedViewMode" runat="server">
                        <asp:View ID="EventView" runat="server">
                            <asp:TableCell ColumnSpan="4">
                                <asp:DropDownList ID="EventFunctionSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionSelection_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Create an Event</asp:ListItem>
                                    <asp:ListItem Value="2">Modify an Event</asp:ListItem>
                                    <asp:ListItem Value="3">Delete an Event</asp:ListItem>
                                </asp:DropdownList>
                            </asp:TableCell>
                        </asp:View>
                        <asp:View ID="TaskView" runat="server">
                            <asp:TableCell ColumnSpan="4">

                            </asp:TableCell>
                        </asp:View>
                    </asp:MultiView>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                    <asp:MultiView ID="SelectedFunction" runat="server">
                        <asp:View ID="CreateEventView" runat="server">
                            <asp:TableRow runat="server" HorizontalAlign="Left">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="EventDateLbl" runat="server" Text="Event Date (MM/DD/YYYY):"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:TextBox ID="EventDateTxt" runat="server" TextMode="Date"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="1">
                                    <asp:RequiredFieldValidator ID="EventDateValid" runat="server" ErrorMessage="Event Date Required" ControlToValidate="EventDateTxt" ForeColor="Red" Display="None">
                                    </asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Left">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:Label ID="createGVLabel" runat="server" Text="Create Itinerary: " Width="200"></asp:Label>
                                    <br />
                                    <asp:HyperLink runat="server" Text="Download Itinerary Creation Form" NavigateUrl="~/Uploads/Default_Itinerary.xlsx" Enabled="true"></asp:HyperLink>
                                    <br />
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="btnImport" runat="server" Text="Import" OnClick="ImportExcel" />
                                    <asp:BulletedList ID="BulletedList1" runat="server" BulletStyle="Numbered">
                                        <asp:ListItem Text="Download Itinerary Creation Form." />
                                        <asp:ListItem Text="Create Itinerary in MS Excel." />
                                        <asp:ListItem Text="Import completed file." />
                                        <asp:ListItem Text="Review and Create." />
                                    </asp:BulletedList>
                                    <br />
                                    <asp:Label ID="NotifLBL" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                                    <br />
                                    <asp:GridView ID="GridView1" AlternatingRowStyle-BackColor="White" BackColor="#f4efe1" runat="server" ForeColor="#33333" HeaderStyle-BackColor="#d3cdb6" CellPadding="5">
                                    </asp:GridView>
                                </asp:TableCell>

                            </asp:TableRow>

                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:Button ID="CreateBut" CssClass="btn-main reg" runat="server" Text="Create Event" CausesValidation="true" OnClick="CreateBut_Click" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:ValidationSummary ID="EventCreateValid" runat="server" DisplayMode="BulletList" Enabled="True" ForeColor="Red" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:View>
                        <%--MODIFY--%>
                        <asp:View ID="ModifyView" runat="server">
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="SelectEventLbl" runat="server" Text="Select event to modify: "></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:DropDownList ID="EventDateDDL" runat="server" OnSelectedIndexChanged="EventDateDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEventDateDDL" runat="server" ControlToValidate="EventDateDDl" Text=" - Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:Table ID="ModifyTbl" runat="server" HorizontalAlign="Center" Visible="false">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1" HorizontalAlign="Center">
                                        <asp:Label runat="server" Text="Date: "></asp:Label>
                                        <asp:TextBox ID="NewDateTxt" runat="server" Width="150" TextMode="Date"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Left">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="Label1" runat="server" Text="Update Itinerary: " Width="200"></asp:Label>
                                        <br />
                                        <asp:HyperLink runat="server" Text="Download Itinerary Creation Form" NavigateUrl="~/Uploads/Default_Itinerary.xlsx" Enabled="true"></asp:HyperLink>
                                        <br />
                                        <asp:FileUpload ID="FileUpload2" runat="server" />
                                        <%--<asp:Button ID="ModGVBtn" runat="server" Text="Import" OnClick="ImportExcel" />--%>
                                        <asp:BulletedList ID="BulletedList2" runat="server" BulletStyle="Numbered">
                                            <asp:ListItem Text="Download Itinerary Creation Form." />
                                            <asp:ListItem Text="Create Itinerary in MS Excel." />
                                            <asp:ListItem Text="Import completed file." />
                                            <asp:ListItem Text="Click Modify." />
                                        </asp:BulletedList>
                                        <br />
                                    </asp:TableCell>

                                    <asp:TableCell ColumnSpan="1">
                                        <asp:GridView ID="GridViewModify" runat="server" AlternatingRowStyle-BackColor="White" BackColor="#f4efe1" ForeColor="#33333" HeaderStyle-BackColor="#d3cdb6" CellPadding="5"></asp:GridView>
                                        <%-- <asp:CustomValidator ID="NewStartValid" runat="server" ErrorMessage="New start time must be different from old start time" ForeColor="Red" ControlToValidate="NewStartTxt" OnServerValidate="NewStartValid_ServerValidate" Display="None"></asp:CustomValidator>
                                        <asp:RegularExpressionValidator ID="NewStartExp" runat="server" ErrorMessage="Please enter in the correct format for time." ForeColor="Red" ControlToValidate="NewStartTxt"
                                            ValidationExpression="^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$" Display="None"></asp:RegularExpressionValidator>
                                        --%>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:Button ID="ModifyBut" runat="server" CssClass="btn-main reg" Text="Modify" CausesValidation="true" OnClick="ModifyBut_Click" Visible="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:ValidationSummary ID="ModEventValSum" runat="server" DisplayMode="BulletList" Enabled="True" ForeColor="Red" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:View>

                        <%--DELETE--%>
                        <asp:View ID="DeleteView" runat="server">
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="1">
                                    <asp:Label ID="EventDelLbl" runat="server" Text="Select event to delete: "></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:DropDownList ID="EventDelDDL" runat="server"></asp:DropDownList>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="1">
                                    <asp:RequiredFieldValidator ID="EventDelReq" runat="server" ErrorMessage="An event must be selected" ForeColor="Red" ControlToValidate="EventDelDDL"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:Button ID="DelBut" CssClass="btn-main reg" runat="server" Text="Delete" CausesValidation="true" OnClick="DelBut_Click" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:View>

                    </asp:MultiView>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableFooterRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                   
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
    </div>
</asp:Content>
