<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of ScoreKeeper.ViewScoreViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= ""%>
    <div data-role="header" data-backbtn="false">
        <%  If Model.IsGameOver Then %>
            <%= Html.ActionLink("Standings", "Index", "Home", {}, {})%>
        <%  End If%>
        <h1>Score</h1>
        <% If Not Model.IsGameOver Then%>
            <%= Html.ActionLink("Add Score", "AddScore", "Home", New With {.gameId = Model.GameId, .roundNumber = Model.NextRoundNumber}, New With {.class = "ui-btn-right"})%>        
        <% Else%>
            <a href="<%= Url.Action("StartGame", "Home") %>" class="ui-btn-right">New Game</a>
        <% End If%>
    </div>
    <div data-role="content">
        <div class="score-container">
            <table>
                <tr>
                    <td>Round</td>
                    <% For Each item In Model.Players%>
                        <td>
                            <%= item.Name %>
                        </td>
                    <% Next%>
                    <td>&nbsp;</td>
                </tr>
                <% For round As Integer = 1 To Model.NextRoundNumber - 1%>
                    <tr>
                        <td><%= round%></td>
                        <% For Each item In Model.Players%>
                            <td><%= Model.FindScore(item.PlayerId, round) %></td>
                        <% Next%>
                        <td><%= Html.ActionLink("Edit", "AddScore", "Home", New With {.gameId = Model.GameId, .roundNumber = round}, {})%></td>
                    </tr>
                <% Next%>
                <tr>
                    <td>Total</td>
                    <% For Each item In Model.Players%>
                        <td><%= Model.FindScore(item.PlayerId) %></td>
                    <% Next%>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <%  If Model.IsGameOver Then %>
                <div>
                    <h1>GAME OVER</h1>
                </div>
            <%  End If%>
        </div>
    </div>
    <div data-role="footer" data-position="fixed">
        <div data-role="navbar">
            <ul>
                <!-- NOTE: the use of random value in querystring prevents jquery mobile page cacheing.  Might also be able to solve this by turning off Ajax caching on the target page. -->
                <li><%= Html.ActionLink("Score", "ViewScore", "Home", New With {.gameId = Model.GameId, .nextRoundNumber = Model.NextRoundNumber, .rand = New Random().Next}, {})%></li>
                <li><%= Html.ActionLink("Details", "ViewScoreDetails", "Home", New With {.gameId = Model.GameId, .nextRoundNumber = Model.NextRoundNumber, .rand = New Random().Next}, New With {.class = "ui-btn-active"})%></li>
            </ul>
        </div>
    </div>
</asp:Content>
