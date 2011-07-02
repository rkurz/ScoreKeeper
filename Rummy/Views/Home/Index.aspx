<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.IndexViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div data-role="header" data-backbtn="false">
        <h1>Rummy</h1>
        <!--<a href="<%= Url.Action("StartGame", "Home") %>" class="ui-btn-right">New Game</a>-->
        <%= Html.ActionLink("New Game", "StartGame", "Home", New With {.rand = New Random().Next}, New With {.class = "ui-btn-right"})%>
    </div>
    <div data-role="content">
        <ul class="ui-grid-c" data-role="listview" data-inset="true">
            <li class="ui-btn-up-b-important" style="height: 20px; padding-right: 15px !Important;">
                <div class="ui-block-a" style="text-align: center;">Player</div>
                <div class="ui-block-b" style="text-align: center;">Wins</div>
                <div class="ui-block-c" style="text-align: center;">Losses</div>
                <div class="ui-block-d" style="text-align: center;">Games</div>
            </li>
            <% For Each item In Model.Standings%>
                <li style="height: 20px; padding-right: 15px !Important;">
                    <div class="ui-block-a" style="text-align: center;"><%= item.Name %></div>
                    <div class="ui-block-b" style="text-align: center;"><%= item.Wins%></div>
                    <div class="ui-block-c" style="text-align: center;"><%= item.Losses%></div>
                    <div class="ui-block-d" style="text-align: center;"><%= item.GamesPlayed%></div>
                </li>
            <% Next%>
        </ul>
    </div>
    <div data-role="footer" data-position="fixed">
        <div data-role="navbar">
            <ul>
                <li><a href="#" class="ui-btn-active">Standings</a></li>
                <li><%= Html.ActionLink("History", "GameHistory", "Home", New With {.rand = New Random().Next}, Nothing)%></li>
            </ul>
        </div>
    </div>
</asp:Content>
