<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of ScoreKeeper.ViewScoreViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= ""%>

    <div data-role="header" data-backbtn="false">
        <%  If Model.IsGameOver Then %>
            <%= Html.ActionLink("Standings", "Index", "Home", Nothing, Nothing)%>
        <%  End If%>
        <h1>Score</h1>
        <% If Not Model.IsGameOver Then%>
            <%= Html.ActionLink("Add Score", "AddScore", "Home", New With {.gameId = Model.GameId, .roundNumber = Model.NextRoundNumber}, New With {.class = "ui-btn-right"})%>        
        <% Else%>
            <%= Html.ActionLink("New Game", "StartGame", "Home", New With {.rand = New Random().Next}, New With {.class = "ui-btn-right"})%>        
        <% End If%>
    </div>
    <div data-role="content">
        <ul data-role="listview" class="ui-grid-a" data-inset="true">
            <% For Each item In Model.Players%>
                <li style="height: 30px; padding-right: 15px !Important;" <% If Model.IsGameOver AndAlso Model.IsWinner(item.PlayerId) Then%> class="winner" <% End If %>>
                    <div class="ui-block-a score-name" style="text-align: center;"><%= item.Name %></div>
                    <div class="ui-block-b score-value" style="text-align: center;">
                        <label class="score-value" id="lblScore_<%= item.PlayerId %>"><%= Model.FindScore(item.PlayerId) %></label>
                    </div>
                </li>
            <% Next%>
        </ul>
        <div class="score-container">
            <% If Model.NextRoundNumber > 1 Then%>
                <div>
                    <label id="lblRound" class="score-round">After round: <%= Model.NextRoundNumber-1 %></label>
                </div>
            <% End If%>
            <%  If Model.IsGameOver Then %>
                <div>
                    <h1>GAME OVER</h1>
                </div>
            <%  End If%>
        </div>
        <!--
        <br />
        <br />
        <ul data-role="listview">
            <% For Each item In Model.Players%>
                <li <% If Model.IsGameOver AndAlso Model.IsWinner(item.PlayerId) Then%> class="winner" <% End If %>>
                    <label class="score-name"><%= item.Name %></label>
                    <span class="ui-li-count score-value"><%= Model.FindScore(item.PlayerId) %></span>
                </li>
            <% Next%>
        </ul>
        -->
        <!--
        <div class="score-container">
            <div>
            <% For Each item In Model.Players%>
                <div <% If Model.IsGameOver AndAlso Model.IsWinner(item.PlayerId) Then%> class="winner" <% End If %>>
                    <label class="score-name"><%= item.Name %>:</label>&nbsp;
                    <label class="score-value" id="lblScore_<%= item.PlayerId %>"><%= Model.FindScore(item.PlayerId) %></label>
                </div>
            <% Next%>
            <% If Model.NextRoundNumber > 1 Then%>
                <div>
                    <label id="lblRound" class="score-round">After round: <%= Model.NextRoundNumber-1 %></label>
                </div>
            <% End If%>
            </div>
            <%  If Model.IsGameOver Then %>
                <div>
                    <h1>GAME OVER</h1>
                </div>
            <%  End If%>
        </div>
        -->
    </div>
    
    <div data-role="footer" data-position="fixed">
        <div data-role="navbar">
            <ul>
                <!-- NOTE: the use of random value in querystring prevents jquery mobile page cacheing.  Might also be able to solve this by turning off Ajax caching on the target page. -->
                <li><%= Html.ActionLink("Score", "ViewScore", "Home", New With {.gameId = Model.GameId, .nextRoundNumber = Model.NextRoundNumber, .rand = New Random().Next}, New With {.class = "ui-btn-active"})%></li>
                <li><%= Html.ActionLink("Details", "ViewScoreDetails", "Home", New With {.gameId = Model.GameId, .nextRoundNumber = Model.NextRoundNumber, .rand = New Random().Next}, {})%></li>
            </ul>
        </div>
    </div>
</asp:Content>
