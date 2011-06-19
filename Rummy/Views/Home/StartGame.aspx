<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.StartGameViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm("StartGame", "Home")%>
        <div data-role="header">
            <h1>Start Game</h1>
        </div>
        <div data-role="content">
            <div data-role="fieldcontain">
                <label for="PointsRequiredToWin">Points To Win:</label>
                <%= Html.TextBoxFor(Function(m) m.PointsRequiredToWin)%>
            </div>
            <!--
            <div data-role="fieldcontain">
                <label for="txtPlayerName">Player:</label>
                <input type="text" name="txtPlayerName" id="txtPlayerName" />
            </div>
            -->
        </div>
        <div data-role="footer" class="ui-bar">
            <a href="<%= Url.Action("Index", "Home") %>" data-role="button">Cancel</a>
            <input type="submit" value="Start" data-theme="a" />    
        </div>
    <% Html.EndForm()%>
</asp:Content>
