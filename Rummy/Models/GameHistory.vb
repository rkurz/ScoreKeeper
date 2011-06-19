Public Class GameHistory
    Public Property GameId As Integer
    Public Property PlayedOn As DateTime
    Public Property Scores As List(Of PlayerScore)
End Class


Public Class PlayerScore
    Public Property PlayerId As Integer
    Public Property PlayerName As String
    Public Property Points As Integer
    Public Property IsWinner As Boolean
End Class