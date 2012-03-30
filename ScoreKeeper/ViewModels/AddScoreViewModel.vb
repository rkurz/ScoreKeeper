Public Class AddScoreViewModel
    Public Property GameId As Integer
    Public Property RoundNumber As Integer
    Public Property NextRoundNumber As Integer
    Public Property Players As List(Of Player)
    Public Property PlayerScores As List(Of GamePointsByRound)

    Public Function FindScore(ByVal playerId As Integer, ByVal roundNumber As Integer) As Integer
        Try
            Return PlayerScores.Where(Function(p) p.PlayerId = playerId AndAlso p.RoundNumber = roundNumber).SingleOrDefault.Points
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Returns and empty string instead of 0 so that when entering scores, the user will not have to delete the 0
    Public Function DisplayScore(ByVal playerId As Integer, ByVal roundNumber As Integer) As String
        Dim score As Integer
        score = FindScore(playerId, roundNumber)
        If score <> 0 Then
            Return score.ToString
        Else
            Return String.Empty
        End If
    End Function
End Class
