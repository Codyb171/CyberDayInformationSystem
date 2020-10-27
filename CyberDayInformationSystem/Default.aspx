<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CyberDayInformationSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>What is CyberDay?</h1>
        <p class="lead">CyberDay is a technological mentorship program designated for middle school students to learn more about JMU's CIS/BSAN program while developing advanced competencies and diagnostic skills to correct software problems.</p>
        <p><a href="About.aspx" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Inspiring a New Generation of Technology Professionals</h2>
            <p>
                Our goal with CyberDay is to give students who come from a non-technological background a chance to learn more about how technology is utilized to solve modern-day business problems.
            </p>
            <p>
                <a class="btn btn-default" href="About.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Interactive and Engaging Activities</h2>
            <p>
                We will be hosting several activities including: animation programming, team-building exercises, and informational sessions about careers in business and technology. 
                Lunch will also be provided for all of our CyberDay participants!
            </p>
            <p>
                <a class="btn btn-default" href="About.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Interested in Registering?</h2>
            <p>
                If you are a middle school teacher that is interested in registering your students for our program, please create an account using our sign up button on the top right!
                Fellow JMU staff and students who are interested in volunteering for CyberDay should also create an account.
            </p>
            <p>
                <a class="btn btn-default" href="Contact.aspx">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
