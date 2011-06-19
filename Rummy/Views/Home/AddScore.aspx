<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.AddScoreViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm("AddScore", "Home")%>
        <%= Html.Hidden("gameId", Model.GameId)%>
        <%= Html.Hidden("roundNumber", Model.RoundNumber)%>
        <div data-role="header">
            <h1>Round <%= Model.RoundNumber %></h1>
        </div>
        <div data-role="content">
            <% For Each player In Model.Players%>
                <p>
                    <label for="txtPlayer_<%= player.PlayerId %>"><%= player.Name %></label>
                    <input type="number" id="txtPlayer_<%= player.PlayerId %>" name="txtPlayer_<%= player.PlayerId %>" value="<%= Model.DisplayScore(Model.GameId, Model.RoundNumber) %>" />
                </p>
            <% Next%>
        </div>
        <div data-role="footer" data-position="fixed">
            <%= Html.ActionLink("Cancel", "ViewScore", "Home", New With {.gameId = Model.GameId, .nextRoundNumber = Model.NextRoundNumber, .rand = New Random().Next}, {})%>
            <input type="submit" value="Submit" data-theme="a" />    
        </div>
    <% Html.EndForm()%>
</asp:Content>
