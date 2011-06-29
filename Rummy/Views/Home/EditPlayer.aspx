<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage(Of Rummy.EditPlayerViewModel)" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm("EditPlayer", "Home")%>
        <%= Html.Hidden("playerId", Model.PlayerId)%>
        <div data-role="header" data-backbtn="false">
            <h1>Edit Player</h1>
        </div>
        <div data-role="content">
            <div data-role="fieldcontain">
                <label for="PlayerName">Name:</label>
                <%= Html.TextBoxFor(Function(m) m.PlayerName)%>
                <%= Html.ValidationMessageFor(Function(m) m.PlayerName)%>
            </div>
            <div class="ui-grid-a">
                <div class="ui-block-a">
                    <a href="<%= Url.Action("StartGame", "Home") %>" data-role="button">Cancel</a>
                </div>
                <div class="ui-block-b">
                    <input type="submit" value="Save" data-theme="b" />
                </div>
            </div>
        </div>
    <% Html.EndForm()%>
</asp:Content>
