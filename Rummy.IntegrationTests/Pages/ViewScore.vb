Public Class ViewScorePage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/ViewScore")
    End Sub

    Public ReadOnly Property AddScoreLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("Add Score"))
        End Get
    End Property

    Public ReadOnly Property NewGameLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("New Game"))
        End Get
    End Property

    Public ReadOnly Property StandingsLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("Standings"))
        End Get
    End Property

    Public ReadOnly Property ViewScoreDetailsLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("Details"))
        End Get
    End Property

    Public Function ReadPlayerScore(ByVal playerId As Integer) As String
        Return Me.ActiveContent.Label(Find.ById(String.Format("lblScore_{0:d}", playerId))).Text
    End Function

    Public Function IsInGameOverMode() As Boolean
        'Ensure "add score" link is hidden, "new game" link is visible, "game over" text is shown, winner highlighted in red.
        If Me.AddScoreLink.Exists Then
            Return False
        End If
        If Not Me.NewGameLink.Exists Then
            Return False
        End If
        If Not Me.StandingsLink.Exists Then
            Return False
        End If

        If Not Me.ActiveContent.InnerHtml.Contains("GAME OVER") Then
            Return False
        End If

        'A div must exist with the "winner" class applied to it.
        If Not Me.ActiveContent.Div(Find.ByClass("winner")).Exists Then
            Return False
        End If

        Return True
    End Function
End Class
