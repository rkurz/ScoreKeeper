
Public Enum GameStatus
    InProgress
    Complete
    Deleted
End Enum

Partial Public Class Game
    Public Property Status As GameStatus
        Get
            Return DirectCast([Enum].Parse(GetType(GameStatus), Me.StatusString, True), GameStatus)
        End Get
        Set(ByVal value As GameStatus)
            Me.StatusString = value.ToString
        End Set
    End Property

    Public Shared Function CreateGame(ByVal playedOn As DateTime, ByVal status As GameStatus, ByVal requiredScoreToWin As Integer) As Game
        Return Game.CreateGame(0, playedOn, status.ToString, requiredScoreToWin)
    End Function
End Class
