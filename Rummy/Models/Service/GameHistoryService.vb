Public Class GameHistoryService
    Private _playerRepository As PlayerRepository
    Private _gameRepository As GameRepository
    Private _gamePlayerRepository As GamePlayerRepository

    Public Sub New(ByVal context As IObjectContext)
        _playerRepository = New PlayerRepository(context)
        _gameRepository = New GameRepository(context)
        _gamePlayerRepository = New GamePlayerRepository(context)
    End Sub

    Public Function Load() As List(Of GameHistory)
        Dim games As IEnumerable(Of Game)
        Dim report As List(Of GameHistory)
        Dim reportItem As GameHistory
        Dim gameId As Integer

        games = (From g In _gameRepository.FindAll
                 Where g.Status = "complete" OrElse g.Status = "inprogress"
                 Order By g.PlayedOn Descending
                 Select g)
        Dim players = (From gp In _gamePlayerRepository.FindAll
                       Join p In _playerRepository.FindAll On gp.PlayerId Equals p.PlayerId
                       Select New With {.PlayerId = p.PlayerId, .Name = p.Name, .GameId = gp.GameId, .Points = gp.Points, .IsWinner = gp.IsWinner}).ToList

        report = New List(Of GameHistory)
        For Each game As Game In games
            gameId = game.GameId

            reportItem = New GameHistory
            reportItem.GameId = game.GameId
            reportItem.PlayedOn = game.PlayedOn
            reportItem.Status = game.Status
            reportItem.Scores = New List(Of PlayerScore)

            'Find the player items for this game.
            For Each player In players.Where(Function(p) p.GameId = gameId)
                reportItem.Scores.Add(New PlayerScore With {.PlayerId = player.PlayerId, .PlayerName = player.Name, .Points = player.Points, .IsWinner = (String.Compare(player.IsWinner, "Y", True) = 0)})
            Next

            'Add to report.
            report.Add(reportItem)
        Next

        Return report
    End Function
End Class
