<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Itinerary.aspx.cs" Inherits="CyberDayInformationSystem.Itinerary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HyperLink runat="server" Text="Download Itinerary Creation Form" NavigateUrl="~/Uploads/Default_Itinerary.xlsx" Enabled="true"></asp:HyperLink>  
    <br />
<asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button ID="btnImport" runat="server" Text="Import" OnClick="ImportExcel" />
    <asp:BulletedList ID="BulletedList1" runat="server" BulletStyle="Numbered">
        <asp:ListItem Text="Download Itinerary Creation Form." />
        <asp:ListItem Text="Create Itinerary in MS Excel." />
        <asp:ListItem Text="Import completed file." />
        <asp:ListItem Text="Review and commit to database." />
    </asp:BulletedList>
<br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
<br />
<asp:GridView ID="GridView1" AlternatingRowStyle-BackColor="White" BackColor="#f4efe1" runat="server" ForeColor="#33333" HeaderStyle-BackColor="#d3cdb6" CellPadding="5">
</asp:GridView>
        <br />
        <asp:Button ID="ButtonCommit" runat="server" Text="Commit" OnClick="ButtonCommit_Click"/>
    <br /><br />

</asp:Content>
