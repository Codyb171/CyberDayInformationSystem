﻿<%@ Page Title="Heat Map" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HeatMap.aspx.cs" Inherits="CyberDayInformationSystem.HeatMap" %>
<asp:Content ID="heatMap" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li><a href="AdminDashboard.aspx">Coordinator Home</a></li>
        <li><a href="AdminReports.aspx">Coordinator Reports</a></li>
        <li>Heat Map</li>
    </ul>

    <link href="AdminReports-Styling.css" rel="stylesheet" type="text/css" />
    <h3 class="text-center">Historic Data</h3>

<div class='tableauPlaceholder' id='viz1604867018924' style='position: relative'><noscript><a href='#'><img alt=' ' src='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;At&#47;AttendeesMap&#47;Dashboard1&#47;1_rss.png' style='border: none' /></a></noscript><object class='tableauViz'  style='display:none;'><param name='host_url' value='https%3A%2F%2Fpublic.tableau.com%2F' /> <param name='embed_code_version' value='3' /> <param name='site_root' value='' /><param name='name' value='AttendeesMap&#47;Dashboard1' /><param name='tabs' value='no' /><param name='toolbar' value='yes' /><param name='static_image' value='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;At&#47;AttendeesMap&#47;Dashboard1&#47;1.png' /> <param name='animate_transition' value='yes' /><param name='display_static_image' value='yes' /><param name='display_spinner' value='yes' /><param name='display_overlay' value='yes' /><param name='display_count' value='yes' /><param name='language' value='en' /><param name='filter' value='publish=yes' /></object></div>                <script type='text/javascript'>                    var divElement = document.getElementById('viz1604867018924'); var vizElement = divElement.getElementsByTagName('object')[0]; if (divElement.offsetWidth > 800) { vizElement.style.width = '1100px'; vizElement.style.height = '527px'; } else if (divElement.offsetWidth > 500) { vizElement.style.width = '1100px'; vizElement.style.height = '527px'; } else { vizElement.style.width = '100%'; vizElement.style.height = '727px'; } var scriptElement = document.createElement('script'); scriptElement.src = 'https://public.tableau.com/javascripts/api/viz_v1.js'; vizElement.parentNode.insertBefore(scriptElement, vizElement);                </script>
</asp:Content>