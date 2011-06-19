<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.IndexViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div data-role="header">
        <h1>Rummy</h1>
        <a href="<%= Url.Action("StartGame", "Home") %>" class="ui-btn-right">New Game</a>
    </div>
    <div data-role="content">
        <table width="100%" style="vertical-align: middle; text-align: center;">
            <tr>
                <th>Player</th>
                <th>Wins</th>
                <th>Losses</th>
                <th>Games Played</th>
            </tr>
            <% For Each item In Model.Standings%>
                <tr>
                    <td><%= item.Name %></td>
                    <td><%= item.Wins%></td>
                    <td><%= item.Losses%></td>
                    <td><%= item.GamesPlayed%></td>
                </tr>
            <% Next%>
        </table>
    </div>
    <div data-role="footer" data-position="fixed">
        <div data-role="navbar">
            <ul>
                <li><a href="#" class="ui-btn-active">Standings</a></li>
                <li><a href="#">History</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
