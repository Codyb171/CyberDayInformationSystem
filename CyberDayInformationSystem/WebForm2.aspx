<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="CyberDayInformationSystem.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label Text="text" ID="lbl1" runat="server" />
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

                                               <asp:HyperLink runat="server" Text="Download Itinerary Creation Form" NavigateUrl="~/Uploads/Default_Itinerary.xlsx" Enabled="true"></asp:HyperLink>  
    <br />
<asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button ID="btnImport" runat="server" Text="Import" />
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
        </div>
    </form>
</body>
</html>
