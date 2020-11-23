<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Itenerary.aspx.cs" Inherits="CyberDayInformationSystem.Itenerary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         
<asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button ID="btnImport" runat="server" Text="Import" OnClick="ImportExcel" />
<hr />
<asp:GridView ID="GridView1" runat="server">
</asp:GridView>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Button ID="ButtonCommit" runat="server" Text="Commit" OnClick="ButtonCommit_Click"/>
    <br /><br />
    <asp:HyperLink runat="server" Text="Click here for a new Itenerary form" NavigateUrl="~/Uploads/test2.xlsx" Enabled="true"></asp:HyperLink>
      

</asp:Content>
