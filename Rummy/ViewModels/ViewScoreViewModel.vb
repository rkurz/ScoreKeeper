Public Class ViewScoreViewModel
    Public Property GameId As Integer
    Public Property NextRoundNumber As Integer
    Public Property Players As List(Of Player)
    Public Property PlayerScores As List(Of GamePointsByRound)
    Public Property GamePlayers As List(Of GamePlayer)
    Public Property IsGameOver As Boolean

    Public Function FindScore(ByVal playerId As Integer) As Integer
        Return PlayerScores.Where(Function(p) p.PlayerId = playerId).Sum(Function(p) p.Points)
    End Function

    Public Function FindScore(ByVal playerId As Integer, ByVal roundNumber As Integer) As Integer
        Try
            Return PlayerScores.Where(Function(p) p.PlayerId = playerId AndAlso p.RoundNumber = roundNumber).SingleOrDefault.Points
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function IsWinner(ByVal playerId As Integer) As Boolean
        Dim item As GamePlayer

        item = GamePlayers.Where(Function(gp) gp.PlayerId = playerId).SingleOrDefault
        If item IsNot Nothing Then
            If String.Compare(item.IsWinner, "Y", True) = 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
End Class
