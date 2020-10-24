<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="EventAdministration.aspx.cs" Inherits="CyberDayInformationSystem.EventAdministration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center">
        <h1>Event Administration</h1>
        <div class="text-center">
            <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="4">
                        <asp:RadioButtonList ID="FunctionSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FunctionSelection_SelectedIndexChanged">
                            <asp:ListItem Value="1">Create an Event</asp:ListItem>
                            <asp:ListItem Value="2">Modify an Event</asp:ListItem>
                            <asp:ListItem Value="3">Delete an Event</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:MultiView ID="SelectedFunction" runat="server">
                            
                            <asp:View ID="CreateView" runat="server">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EventNameLbl" runat="server" Text="Event Name: " Width="200"></asp:Label>                                        
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="EventNameTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EventNameValid" runat="server" ErrorMessage="Event Name Required" ControlToValidate="EventNameTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EventDateLbl" runat="server" Text="Event Date: " Width="200"></asp:Label>                                        
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:DropDownList ID="EventDateDDL" runat="server">
                                            <%--<asp:ListItem Value="0">Please select a date</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <%--<asp:SqlDataSource ID="EventDateSRC" runat="server" ConnectionString="<%$ ConnectionStrings:INFO %>" SelectCommand="SELECT EVENTDATE, EVENTID FROM EVENT">
                                        </asp:SqlDataSource>--%>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EventDateValid" runat="server" ErrorMessage="Event Date Required" ControlToValidate="EventDateDDL" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EventTimeLbl" runat="server" Text="Event Time (hh:mm): " Width="200"></asp:Label>                                        
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:TextBox ID="EventTimeTxt" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EventTimeReq" runat="server" ErrorMessage="Event Time Required" ControlToValidate="EventTimeTxt" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="EventTimeValid" runat="server" ErrorMessage="Please enter in the correct format for time." ForeColor="Red" ControlToValidate="EventTimeTxt"
                                            ValidationExpression="^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$">
                                        </asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="EventLocationLbl" runat="server" Text="Event Location: " Width="200"></asp:Label>                                        
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:DropDownList ID="EventLocationDDL" runat="server">
                                            <%--<asp:ListItem Value="0">Please select a location</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <%--<asp:SqlDataSource ID="EventLocationSRC" runat="server" ConnectionString="<%$ ConnectionStrings:INFO %>" SelectCommand="SELECT (BUILDING + ' ' + ROOMNUMBER) AS Room, ROOMID FROM ROOMRESERVATIONS">
                                        </asp:SqlDataSource>--%>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EventLocationReq" runat="server" ErrorMessage="Event Location Required" ControlToValidate="EventLocationDDL" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Button ID="CreateBut" runat="server" Text="Create" CausesValidation="true" OnClick="CreateBut_Click"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>

                            <asp:View ID="ModifyView" runat="server">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="SelectEventLbl" runat="server" Text="Select event to modify: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:DropDownList ID="EventDDL" runat="server" OnSelectedIndexChanged="EventDDL_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="0">Please select an event.</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <%--<asp:SqlDataSource ID="EventSRC" runat="server" ConnectionString="<%$ ConnectionStrings:INFO %>" SelectCommand="SELECT TITLE, TASKID FROM EVENTTASKS">
                                        </asp:SqlDataSource>--%>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EventDDLReq" runat="server" ErrorMessage="An event must be selected" ForeColor="Red" ControlToValidate="EventDDL"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Button ID="ModifyBut" runat="server" Text="Modify" CausesValidation="true" OnClick="ModifyBut_Click"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>

                            <asp:View ID="DeleteView" runat="server">
                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Label ID="Label1" runat="server" Text="Select event to delete: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:DropDownList ID="EventDelDDL" runat="server" OnSelectedIndexChanged="EventDelDDL_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="0">Please select an event.</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RequiredFieldValidator ID="EventDelReq" runat="server" ErrorMessage="An event must be selected" ForeColor="Red" ControlToValidate="EventDelDDL"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="server" HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="1">
                                        <asp:Button ID="DelBut" runat="server" Text="Delete" CausesValidation="true" OnClick="DelBut_Click"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:View>

                        </asp:MultiView>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableFooterRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell>
                        <asp:Label ID="successLBL" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableFooterRow>

            </asp:Table>

        </div>
    </div>



</asp:Content>
