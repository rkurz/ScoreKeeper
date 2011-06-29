<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.StartGameViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm("StartGame", "Home")%>
        <div data-role="header" data-backbtn="false">
            <h1>Start Game</h1>
        </div>
        <div data-role="content">
            <div data-role="fieldcontain">
                <label for="PointsRequiredToWin">Points To Win:</label>
                <%= Html.TextBoxFor(Function(m) m.PointsRequiredToWin, New With {.type = "number"})%>
                <%= Html.ValidationMessageFor(Function(m) m.PointsRequiredToWin) %>
            </div>
            
            <div data-role="fieldcontain">
                <fieldset data-role="controlgroup">
                    <legend>Choose Players:</legend>
                    <!-- NOTE: Due to a bug in Jquery Mobile, a list of checkboxes with the same name will only allow 1 selected value (ie/ like a radio list).
                               To get around this, we have to use the mess that follows -->
                    <% For Each player In Model.EligiblePlayers%>
                        <input type="hidden" name="SelectedPlayers.Index" value="<%=player.PlayerId %>" />
                        <input type="checkbox" id="cbPlayer_<%=player.PlayerId %>" name="SelectedPlayers[<%=player.PlayerId %>]" value="<%=player.Playerid %>" />
                        <label for="cbPlayer_<%=player.PlayerId %>"><%= player.Name%></label>
                    <% Next%>
                </fieldset>
                <%= Html.ValidationMessageFor(Function(m) m.SelectedPlayers)%>
            </div>
            
        </div>
        <div data-role="footer" class="ui-bar">
            <a href="<%= Url.Action("Index", "Home") %>" data-role="button">Cancel</a>
            <input type="submit" value="Start" data-theme="a" />    
        </div>
    <% Html.EndForm()%>
</asp:Content>
