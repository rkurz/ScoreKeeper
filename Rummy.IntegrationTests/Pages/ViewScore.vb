Public Class ViewScorePage
    Inherits Page

    Public ReadOnly Property AddScoreLink As Link
        Get
            Dim container As Div
            container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

            Return container.Link(Find.ByText("Add Score"))
        End Get
    End Property

    Public ReadOnly Property NewGameLink As Link
        Get
            Dim container As Div
            container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

            Return container.Link(Find.ByText("New Game"))
        End Get
    End Property

    Public ReadOnly Property StandingsLink As Link
        Get
            Dim container As Div
            container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

            Return container.Link(Find.ByText("Standings"))
        End Get
    End Property

    Public ReadOnly Property ViewScoreDetailsLink As Link
        Get
            Dim container As Div
            container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

            Return container.Link(Find.ByText("Details"))
        End Get
    End Property

    Public Function ReadPlayerScore(ByVal playerId As Integer) As String
        Dim container As Div
        container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

        Return container.Label(Find.ById(String.Format("lblScore_{0:d}", playerId))).Text
    End Function

    Public Function IsInGameOverMode() As Boolean
        Dim container As Div
        container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

        'Ensure "add score" link is hidden, "new game" link is visible, "game over" text is shown, winner highlighted in red.
        If Me.NewGameLink.Exists Then
            Return False
        End If
        If Not Me.NewGameLink.Exists Then
            Return False
        End If
        If Not Me.StandingsLink.Exists Then
            Return False
        End If

        If Not container.InnerHtml.Contains("GAME OVER") Then
            Return False
        End If

        'A div must exist with the "winner" class applied to it.
        If Not container.Div(Find.ByClass("winner")).Exists Then
            Return False
        End If

        Return True
    End Function
End Class
