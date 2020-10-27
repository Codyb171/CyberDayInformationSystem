<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="EventAdministration.aspx.cs" Inherits="CyberDayInformationSystem.EventAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center">
        <h1>Event Administration</h1>
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                    <asp:RadioButtonList ID="EventorTask" runat="server" AutoPostBack="True" OnSelectedIndexChanged="EventorTask_OnSelectedIndexChanged">
                        <asp:ListItem Value="1">Manage Event</asp:ListItem>
                        <asp:ListItem Value="2">Manage Tasks</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                    <asp:MultiView ID="SelectedViewMode" runat="server">
                        <asp:View ID="EventView" runat="server">
                            <asp:TableCell ColumnSpan="4">
                                <asp:RadioButtonList ID="EventFunctionSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionSelection_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Create an Event</asp:ListItem>
                                    <asp:ListItem Value="2">Modify an Event</asp:ListItem>
                                    <asp:ListItem Value="3">Delete an Event</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:View>
                        <asp:View ID="TaskView" runat="server">
                            <asp:TableCell ColumnSpan="4">
                                <asp:RadioButtonList ID="TaskFunctionSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionSelection_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Create a Task</asp:ListItem>
                                    <asp:ListItem Value="2">Modify a Task</asp:ListItem>
                                    <asp:ListItem Value="3">Delete a Task</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:View>
                    </asp:MultiView>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                    <asp:MultiView ID="SelectedFunction" runat="server">
                        <asp:View ID="CreateEventView" runat="server">
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="EventDateLbl" runat="server" Text="Event Date: " Width="200"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:TextBox ID="EventDateTxt" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="1">
                                    <asp:RequiredFieldValidator ID="EventDateValid" runat="server" ErrorMessage="Event Date Required" ControlToValidate="EventDateTxt" ForeColor="Red" Display="None">
                                    </asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="EventTimeStartLbl" runat="server" Text="Start Time (hh:mm): " Width="200"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:TextBox ID="EventTimeTxt" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="1">
                                    <asp:RequiredFieldValidator ID="EventTimeReq" runat="server" ErrorMessage="Start Time Required" ControlToValidate="EventTimeTxt" ForeColor="Red" Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="EventTimeValid" runat="server" ErrorMessage="Please enter in the correct format for time." ForeColor="Red" ControlToValidate="EventTimeTxt"
                                        ValidationExpression="^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$" Display="None">
                                    </asp:RegularExpressionValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="EventTimeEndLbl" runat="server" Text="End Time (hh:mm): " Width="200"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:TextBox ID="EndTimeTxt" runat="server"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="1">
                                    <asp:RequiredFieldValidator ID="EndTimeReq" runat="server" ErrorMessage="End Time Required" ControlToValidate="EndTimeTxt" ForeColor="Red" Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="EndTimeValid" runat="server" ErrorMessage="Please enter in the correct format for time." ForeColor="Red" ControlToValidate="EndTimeTxt"
                                        ValidationExpression="^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$" Display="None">
                                    </asp:RegularExpressionValidator>
                                    <asp:CompareValidator ID="CheckTime" runat="server" ErrorMessage="End Time must be later than Start Time"
                                        ControlToValidate="EndTimeTxt"
                                        ControlToCompare="EventTimeTxt"
                                        Operator="GreaterThanEqual"
                                        Type="Date" Display="None">
                                    </asp:CompareValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="EventLocationLbl" runat="server" Text="Event Location: " Width="200"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:DropDownList ID="EventLocationDDL" runat="server"></asp:DropDownList>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="1">
                                    <asp:RequiredFieldValidator ID="EventLocationReq" runat="server" ErrorMessage="Event Location Required" ControlToValidate="EventLocationDDL" ForeColor="Red" Display="None">
                                    </asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:Button ID="CreateBut" runat="server" Text="Create Event" CausesValidation="true" OnClick="CreateBut_Click" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:ValidationSummary ID="EventCreateValid" runat="server" DisplayMode="BulletList" Enabled="True" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:View>

                        <asp:View ID="ModifyView" runat="server">
                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="SelectEventLbl" runat="server" Text="Select event to modify: "></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:DropDownList ID="EventDateDDL" runat="server" OnSelectedIndexChanged="EventDateDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:Table ID="ModifyTbl" runat="server" HorizontalAlign="Center" Visible="false">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                            <asp:Label runat="server" Text="Original Title: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="OldTitleTxt" runat="server" ReadOnly="true"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                            <asp:Label runat="server" Text="New Title: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="NewTitleTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="NewTitleReq" runat="server" ErrorMessage="A title is required" ForeColor="Red" ControlToValidate="NewTitleTxt"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="NewTitleValid" runat="server" ErrorMessage="New title must be different from old title" ForeColor="Red" ControlToValidate="NewTitleTxt" OnServerValidate="NewTitleValid_ServerValidate"></asp:CustomValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                            <asp:Label runat="server" Text="Original Start Time: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="OldStartTxt" runat="server" ReadOnly="true"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                            <asp:Label runat="server" Text="New Start Time: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="NewStartTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="NewStartReq" runat="server" ErrorMessage="A start time is required" ForeColor="Red" ControlToValidate="NewStartTxt"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="NewStartValid" runat="server" ErrorMessage="New start time must be different from old start time" ForeColor="Red" ControlToValidate="NewStartTxt" OnServerValidate="NewStartValid_ServerValidate"></asp:CustomValidator>
                                        <asp:RegularExpressionValidator ID="NewStartExp" runat="server" ErrorMessage="Please enter in the correct format for time." ForeColor="Red" ControlToValidate="NewStartTxt"
                                            ValidationExpression="^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                            <asp:Label runat="server" Text="Original End Time: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="OldEndTxt" runat="server" ReadOnly="true"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="2">
                                            <asp:Label runat="server" Text="New End Time: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:TextBox ID="NewEndTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:RequiredFieldValidator ID="NewEndReq" runat="server" ErrorMessage="An end time is required" ForeColor="Red" ControlToValidate="NewEndTxt"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="NewEndValid" runat="server" ErrorMessage="New end time must be different from old end time" ForeColor="Red" ControlToValidate="NewEndTxt" OnServerValidate="NewEndValid_ServerValidate"></asp:CustomValidator>
                                        <asp:RegularExpressionValidator ID="NewEndExp" runat="server" ErrorMessage="Please enter in the correct format for time." ForeColor="Red" ControlToValidate="NewEndTxt"
                                            ValidationExpression="^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                        <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="The end time must be greater than the start time." ForeColor="Red" ControlToValidate="NewEndTxt" OnServerValidate="TimeRangeValid_ServerValidate"></asp:CustomValidator>--%>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <asp:TableRow runat="server" HorizontalAlign="Center">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:Button ID="ModifyBut" runat="server" Text="Modify" CausesValidation="true" OnClick="ModifyBut_Click" Visible="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:View>

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
                                    <asp:Button ID="DelBut" runat="server" Text="Delete" CausesValidation="true" OnClick="DelBut_Click" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:View>

                    </asp:MultiView>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow runat="server" HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="4">
                    <asp:Label ID="NotifLBL" runat="server" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
    </div>
</asp:Content>
