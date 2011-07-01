Public Class GameService
    Private _gameRepository As GameRepository
    Private _gamePlayerRepository As GamePlayerRepository
    Private _gamePointsByRoundRepository As GamePointsByRoundRepository
    Private _playerService As PlayerService

    Public Sub New(ByVal context As IObjectContext)
        _gameRepository = New GameRepository(context)
        _gamePlayerRepository = New GamePlayerRepository(context)
        _gamePointsByRoundRepository = New GamePointsByRoundRepository(context)
        _playerService = New PlayerService(context)
    End Sub

    Public Function CreateGame(ByVal playedOn As DateTime, ByVal requiredScore As Integer, ByVal playerIds As List(Of Integer)) As Game
        Dim game As Game

        game = game.CreateGame(playedOn, GameStatus.InProgress, requiredScore)

        _gameRepository.Create(game)
        _gameRepository.SaveChanges()

        For Each playerId As Integer In playerIds
            _gamePlayerRepository.Create(GamePlayer.CreateGamePlayer(game.GameId, playerId, "N", 0))
            _gamePlayerRepository.SaveChanges()
        Next

        Return game
    End Function

    Public Function FindPointDetails(ByVal gameId As Integer) As List(Of GamePointsByRound)
        Return _gamePointsByRoundRepository.FindAll.Where(Function(g) g.GameId = gameId).ToList
    End Function

    Public Function FindPointDetails(ByVal gameId As Integer, ByVal roundNumber As Integer) As List(Of GamePointsByRound)
        Return _gamePointsByRoundRepository.FindAll.Where(Function(g) g.GameId = gameId AndAlso g.RoundNumber = roundNumber).ToList
    End Function

    Public Function FindPlayerDetails(ByVal gameId As Integer) As List(Of GamePlayer)
        Return _gamePlayerRepository.FindAll.Where(Function(gp) gp.GameId = gameId).ToList
    End Function

    Public Sub AddPlayerScore(ByVal gameId As Integer, ByVal roundNumber As Integer, ByVal playerId As Integer, ByVal points As Integer)
        Dim item As GamePointsByRound

        'Update player score by round.
        item = _gamePointsByRoundRepository.FindAll.Where(Function(g) g.GameId = gameId AndAlso g.RoundNumber = roundNumber AndAlso g.PlayerId = playerId).SingleOrDefault
        If item Is Nothing Then
            item = GamePointsByRound.CreateGamePointsByRound(gameId, playerId, roundNumber, points)
            _gamePointsByRoundRepository.Create(item)
            _gamePointsByRoundRepository.SaveChanges()
        Else
            item.Points = points
            _gamePointsByRoundRepository.Update(item)
            _gamePointsByRoundRepository.SaveChanges()
        End If

        'Update total player scores.
        UpdateTotalPlayerScores(gameId)
    End Sub

    'Calculate total scores (by counting round scores) for each player.
    Public Sub UpdateTotalPlayerScores(ByVal gameId As Integer)
        Dim players As List(Of Player)
        Dim playerScoresByRound As List(Of GamePointsByRound)
        Dim score As Integer
        Dim playerId As Integer
        Dim gamePlayerItem As GamePlayer

        playerScoresByRound = _gamePointsByRoundRepository.FindAll.Where(Function(g) g.GameId = gameId).ToList
        players = _playerService.FindByGame(gameId)
        For Each player As Player In players
            playerId = player.PlayerId

            score = playerScoresByRound.Where(Function(ps) ps.PlayerId = playerId).Sum(Function(ps) ps.Points)
            gamePlayerItem = _gamePlayerRepository.FindAll.Where(Function(gp) gp.GameId = gameId AndAlso gp.PlayerId = playerId).SingleOrDefault
            If gamePlayerItem IsNot Nothing Then
                gamePlayerItem.Points = score
                _gamePlayerRepository.Update(gamePlayerItem)
                _gamePlayerRepository.SaveChanges()
            Else
                gamePlayerItem = GamePlayer.CreateGamePlayer(gameId, playerId, False, score)
                _gamePlayerRepository.Create(gamePlayerItem)
                _gamePlayerRepository.SaveChanges()
            End If
        Next
    End Sub

    Public Function FindById(ByVal gameId As Integer) As Game
        Return _gameRepository.FindAll.Where(Function(g) g.GameId = gameId).SingleOrDefault
    End Function

    Public Function FindNextRoundNumber(ByVal gameId As Integer) As Integer
        Dim items As List(Of GamePointsByRound)

        items = _gamePointsByRoundRepository.FindAll.Where(Function(g) g.GameId = gameId).ToList
        If items.Count > 0 Then
            Return items.Max(Function(g) g.RoundNumber) + 1
        Else
            Return 1
        End If
    End Function

    Public Function IsGameOver(ByVal gameId As Integer) As Boolean
        Dim game As Game
        Dim playerScores As List(Of GamePlayer)
        Dim highestScore As Integer

        'Get all scores for this game.
        game = FindById(gameId)
        If game IsNot Nothing Then
            playerScores = _gamePlayerRepository.FindAll.Where(Function(gp) gp.GameId = gameId AndAlso gp.Points >= game.RequiredScoreToWin).ToList

            If playerScores.Count = 1 Then
                'Only one player has scored more than the required number of points.
                Return True
            ElseIf playerScores.Count > 1 Then
                'There are several players with enough points to win.
                highestScore = playerScores.Max(Function(gp) gp.Points)

                If playerScores.Where(Function(gp) gp.Points = highestScore).Count = 1 Then
                    'Only one player has the high score. Therefore game over.
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Public Sub CompleteGame(ByVal gameId As Integer)
        Dim playerScores As List(Of GamePlayer)
        Dim highestScore As Integer
        Dim winners As List(Of GamePlayer)
        Dim game As Game

        playerScores = _gamePlayerRepository.FindAll.Where(Function(gp) gp.GameId = gameId).ToList
        highestScore = playerScores.Max(Function(gp) gp.Points)

        'Determine who won the game and set their game player record IsWinner flag to Y
        winners = playerScores.Where(Function(gp) gp.Points = highestScore).ToList()
        For Each winner As GamePlayer In winners
            winner.IsWinner = "Y"
            _gamePlayerRepository.Update(winner)
        Next
        _gamePlayerRepository.SaveChanges()

        'Set game status to finished
        game = FindById(gameId)
        If game IsNot Nothing Then
            game.Status = GameStatus.Complete
            _gameRepository.Update(game)
            _gameRepository.SaveChanges()
        End If
    End Sub

    Public Sub DeleteGame(ByVal gameId As Integer)
        Dim game As Game

        game = FindById(gameId)
        If game IsNot Nothing Then
            game.Status = GameStatus.Deleted
            _gameRepository.Update(game)
            _gameRepository.SaveChanges()
        End If
    End Sub
End Class
