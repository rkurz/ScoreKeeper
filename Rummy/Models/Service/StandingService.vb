Public Class StandingService
    Private _playerRepository As PlayerRepository
    Private _gamePlayerRepository As GamePlayerRepository
    Private _gameRepository As GameRepository

    Public Sub New(ByVal context As IObjectContext)
        _playerRepository = New PlayerRepository(context)
        _gamePlayerRepository = New GamePlayerRepository(context)
        _gameRepository = New GameRepository(context)
    End Sub

    Public Function Load() As List(Of Standing)
        Return (From p In _playerRepository.FindAll
                Join gp In _gamePlayerRepository.FindAll On gp.PlayerId Equals p.PlayerId
                Join g In _gameRepository.FindAll On g.GameId Equals gp.GameId
                Where g.Status = "complete"
                Group By p.PlayerId, p.Name
                Into Wins = Sum(If(gp.IsWinner = "Y", 1, 0)), Losses = Sum(If(gp.IsWinner = "Y", 0, 1)), GamesPlayed = Count(gp.GameId)
                Order By Wins Descending
                Select New Standing With {.PlayerId = PlayerId, .Name = Name, .GamesPlayed = GamesPlayed, .Wins = Wins, .Losses = Losses}).ToList
    End Function
End Class
