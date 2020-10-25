<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher.Master" AutoEventWireup="true" CodeBehind="TeacherDashboard.aspx.cs" Inherits="CyberDayInformationSystem.TeacherDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    Teacher Dashboard
<div class="row">
  <div class="column" >
      Upcoming Events listed
      <asp:GridView ID="GridView1" runat="server" Width="300" Height="175"></asp:GridView>
      <asp:TextBox ID="TextBox1" runat="server" Width="300"></asp:TextBox>
  </div>

  <div class="column">
      bullet list of links
  </div>
</div>
</asp:Content>
