<%@ Page Title="Permission Forms" Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ParentStudentForms.aspx.cs" Inherits="CyberDayInformationSystem.ParentStudentForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="PrintPanel" id="PermissionForm"> 
   <h3>Photo Release</h3>
    <p>I hereby grant CyberDay and their agents the absolute right and permission to use pictures, digital images, or videotapes of My Child, or in which My Child may be included
        in whole or in part, or reproductions thereof in color or otherwise for any lawful purpose whatsoever, including but not limited to use in any CyberDay publication or on the
        CyberDay websites, without payment or any other consideration.</p>
    <p>I hereby waive any right that I may have to inspect and/or approve the finished product or the copy that may be used in connection therewith, wherein My Child's likeness
        appears, or the use to which it may be applied.</p>
    <p>I hereby	release, discharge,	and	agree to indemnify and hold	harmless CyberDay and their agents from	all	claims,	demands, and causes	of action that I or	My Child have or may
        have by reason	of	this authorization or use	of	My	Child’s	photographic portraits,	pictures, digital images or videotapes,	including any liability	by	virtue
        of	any	blurring, distortion, alteration, optical illusion,	or use	in	composite form,	whether	intentional	or	otherwise, that may	occur or be	produced
        in	the	taking of said images or videotapes, or	in	processing	tending	towards	the	completion	of	the	finished product,	including	publication	on	the
        internet, in	brochures,	or	any	other advertisements or promotional materials.</p>

    <asp:RadioButtonList ID="photoPermission" runat="server" Font-Size="16px" CausesValidation="false">
        <asp:ListItem Value="1">By checking this box, I authorize CyberDay to use my child's photograph.</asp:ListItem>
        <asp:ListItem Value="2">By checking this box, I DO NOT authorize CyberDay to use my child's photograph.</asp:ListItem>
    </asp:RadioButtonList>

    <h3>Permission to Retain Email</h3>
    <p>CyberDay would like to follow our student participant's academic progress and be able to reach out to them to provide guidance and opportunities when we are able.</p>
    <asp:RadioButtonList ID="retainEmail" runat="server" Font-Size="16px" CausesValidation="false">
        <asp:ListItem Value="1">By checking this box, I authorize CyberDay to retain my student's email address for the purposes of tracking their
        academic progress and informing them of potential opportunities.</asp:ListItem>
        <asp:ListItem Value="2">By checking this box, I DO NOT authorize CyberDay to retain my student's email address.</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <p>I represent that I am at least eighteen (18) years of age and am fully competent to sign this Release on behalf of the student.</p>

    <asp:Table ID="tblParentInfo" runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Label ID="lblSignature" runat="server" Text="Parent Full Name: " Font-Size="16px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:TextBox ID="txtParentName" runat="server" Width="400"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameValid" runat="server" ErrorMessage="Name Required" ControlToValidate="txtParentName" ForeColor="Red" Font-Size="24px">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Label ID="lblParentEmail" runat="server" Text="Parent Email: " Font-Size="16px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:TextBox ID="txtParentEmail" runat="server" Width="400"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailValid" runat="server" ErrorMessage="Email Required" ControlToValidate="txtParentEmail" ForeColor="Red" Font-Size="24px">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
<%--        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Label ID="lblSignedDate" runat="server" Text="Today's Date: " Font-Size="16px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                <asp:TextBox ID="txtSignedDate" runat="server" Width="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DateValid" runat="server" ErrorMessage="Date Required" ControlToValidate="txtSignedDate" ForeColor="Red" Font-Size="24px">
                </asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>--%>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                <asp:Button ID="btnSubmit" CssClass="btn-main blue" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </div>
    <asp:HiddenField ID="HiddenString" runat="server" Visible="True"  />  
</asp:Content>
