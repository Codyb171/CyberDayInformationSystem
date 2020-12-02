<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CyberDayInformationSystem.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>What is CyberDay?</h1>
        <p class="lead">CyberDay is a technological mentorship program designated for middle school students to learn more about JMU's CIS/BSAN program while developing advanced competencies and diagnostic skills to correct software problems.</p>
        <p><a href="About.aspx" class="btn-main blue">Learn more &raquo;</a></p>
    </div>

    <asp:Table runat="server">
        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <h2>Inspiring a New Generation of Technology Professionals</h2>
            </asp:TableCell>

            <asp:TableCell>
                <h2>Interactive and Engaging Activities</h2>
            </asp:TableCell>

            <asp:TableCell>
                <h2>Interested in Registering?</h2>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow HorizontalAlign="Justify" VerticalAlign="Top">
            <asp:TableCell>
                <p>Our goal with CyberDay is to give students who come from a non-technological background a chance to learn more about how technology is utilized to solve modern-day business problems.</p>
            </asp:TableCell>

            <asp:TableCell>
                <p>We will be hosting several activities including: animation programming, team-building exercises, and informational sessions about careers in business and technology.<br /><br />
                    Lunch will also be provided for all of our CyberDay participants!</p>
            </asp:TableCell>

            <asp:TableCell>
                <p>If you are a middle school teacher that is interested in registering your students for our program, please create an account using our sign up button on the top right!<br /><br />
                    JMU staff and students who are interested in volunteering for CyberDay should also create an account.</p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
