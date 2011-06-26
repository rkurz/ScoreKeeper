<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.ManageGameViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div data-role="header">
        <h1>Manage Game</h1>
    </div>
    <div data-role="content">
        <a href="<%= Url.Action("ViewScore", "Home", New With {.gameId = Model.GameId, .nextRoundNumber = Model.NextRoundNumber, .rand = New Random().Next}) %>" data-role="button" data-theme="b">Resume Game</a>
        <a href="<%= Url.Action("DeleteGame", "Home", New With {.gameId = Model.GameId, .rand = New Random().Next}) %>" data-role="button">Delete Game</a>
    </div>
</asp:Content>
