<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.GameHistoryViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div data-role="header" data-backbtn="false">
        <h1>Rummy</h1>
        <%= Html.ActionLink("New Game", "StartGame", "Home", New With {.rand = New Random().Next}, New With {.class = "ui-btn-right"})%>
    </div>
    <div data-role="content">
        <%  For Each game In Model.History %>
            <ul data-role="listview" data-inset="true">
                <li data-role="list-divider" <% If game.Status = Rummy.GameStatus.InProgress Then%>data-theme="e"<%Else %>data-theme="b"<%End If %>>
                    <a href="<%= Url.Action("ManageGame", "Home", New With {.gameId = game.GameId, .rand = new Random().Next})%>" data-rel="dialog"><%= game.PlayedOn.ToString("dddd MMM dd, yyyy")%></a>
                </li>
                <% For Each player In game.Scores%>
                    <li>
                        <%= player.PlayerName %>
                        <span class="ui-li-count"><%= player.Points %></span>
                    </li>
                <% Next%>
            </ul>
        <%  Next%>
    </div>
    <div data-role="footer" data-position="fixed">
        <div data-role="navbar">
            <ul>
                <li><%= Html.ActionLink("Standings", "Index", "Home", New With {.rand = New Random().Next}, Nothing)%></li>
                <li><%= Html.ActionLink("History", "GameHistory", "Home", New With {.rand = New Random().Next}, New With {.class = "ui-btn-active"})%></li>
            </ul>
        </div>
    </div>
</asp:Content>
