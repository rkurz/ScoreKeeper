Imports System.Text

<TestClass()>
Public Class GameHistoryServiceTests
    Private _context As MockObjectContext
    Private _gameHistoryService As GameHistoryService
    Private _playerRepository As PlayerRepository
    Private _gameRepository As GameRepository
    Private _gamePlayerRepository As GamePlayerRepository

    <TestInitialize()>
    Public Sub Setup()
        _context = New MockObjectContext
        _gameHistoryService = New GameHistoryService(_context)
        _playerRepository = New PlayerRepository(_context)
        _gameRepository = New GameRepository(_context)
        _gamePlayerRepository = New GamePlayerRepository(_context)
    End Sub

    <TestMethod()>
    Public Sub Load_DeletedGamesNotIncluded()
        CreateDeletedGame(1)

        Dim history = _gameHistoryService.Load

        Assert.AreEqual(0, history.Count)
    End Sub

    <TestMethod()>
    Public Sub Load_GamesSortedByDateInDescendingOrder()
        CreateCompleteGame(1, New DateTime(2012, 1, 1))
        CreateInProgressGame(2, New DateTime(2012, 2, 1))
        CreateInProgressGame(3, New DateTime(2012, 4, 1))
        CreateCompleteGame(4, New DateTime(2012, 3, 1))

        Dim history = _gameHistoryService.Load

        Assert.AreEqual(4, history.Count)
        Assert.AreEqual(3, history(0).GameId, "Game 3 was out of order")
        Assert.AreEqual(4, history(1).GameId, "Game 4 was out of order")
        Assert.AreEqual(2, history(2).GameId, "Game 2 was out of order")
        Assert.AreEqual(1, history(3).GameId, "Game 1 was out of order")
    End Sub
    
    <TestMethod()>
    Public Sub Load_PlayerScoresCalculatedCorrectly()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateCompleteGame(1, New DateTime(2012, 1, 1))
        AssignPlayerToGame(game, playerOne, True, 5)
        AssignPlayerToGame(game, playerTwo, False, 1)

        Dim history = _gameHistoryService.Load

        Assert.AreEqual(1, history.Count)
        Dim playerOneScore = history(0).Scores.FirstOrDefault(Function(s) s.PlayerId = playerOne.PlayerId)
        Assert.IsTrue(playerOneScore.IsWinner, "PlayerOne should have won")
        Assert.AreEqual(5, playerOneScore.Points, "PlayerOne Points did not match expected value")
        Dim playerTwoScore = history(0).Scores.FirstOrDefault(Function(s) s.PlayerId = playerTwo.PlayerId)
        Assert.IsFalse(playerTwoScore.IsWinner, "PlayerTwo should not have won")
        Assert.AreEqual(1, playerTwoScore.Points, "PlayerTwo Points did not match expected value")
    End Sub

    <TestMethod()>
    Public Sub Load_PlayerCountsPerGameAreCorrect()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim gameOne = CreateCompleteGame(1, New DateTime(2012, 1, 1))
        AssignPlayerToGame(gameOne, playerOne, True, 5)
        AssignPlayerToGame(gameOne, playerTwo, False, 1)
        Dim gameTwo = CreateCompleteGame(2, New DateTime(2012, 1, 1))
        AssignPlayerToGame(gameTwo, playerOne, True, 5)
        Dim gameThree = CreateCompleteGame(3, New DateTime(2012, 1, 1))

        Dim history = _gameHistoryService.Load

        Assert.AreEqual(3, history.Count)
        Dim gameOneHistory = history.FirstOrDefault(Function(g) g.GameId = gameOne.GameId)
        Assert.IsNotNull(gameOneHistory)
        Assert.AreEqual(2, gameOneHistory.Scores.Count, "GameOne player count is not correct")
        Dim gameTwoHistory = history.FirstOrDefault(Function(g) g.GameId = gameTwo.GameId)
        Assert.IsNotNull(gameTwoHistory)
        Assert.AreEqual(1, gameTwoHistory.Scores.Count, "GameTwo player count is not correct")
        Dim gameThreeHistory = history.FirstOrDefault(Function(g) g.GameId = gameThree.GameId)
        Assert.IsNotNull(gameThreeHistory)
        Assert.AreEqual(0, gameThreeHistory.Scores.Count, "GameThree player count is not correct")
    End Sub

#Region "Helper Methods"
    Private Function CreatePlayer(id As Integer) As Player
        Dim player As Player

        player = New Player With {
                                   .PlayerId = id,
                                   .Name = String.Empty
                                 }

        _playerRepository.Create(player)
        _playerRepository.SaveChanges()

        Return player
    End Function

    Private Function CreateCompleteGame(gameId As Integer, playedOn As DateTime) As Game
        Return CreateGame(gameId, GameStatus.Complete, playedOn)
    End Function
    Private Function CreateInProgressGame(gameId As Integer, playedOn As DateTime) As Game
        Return CreateGame(gameId, GameStatus.InProgress, playedOn)
    End Function
    Private Function CreateDeletedGame(gameId As Integer) As Game
        Return CreateGame(gameId, GameStatus.Deleted, DateTime.Now)
    End Function

    Private Function CreateGame(gameId As Integer, status As GameStatus, playedOn As DateTime) As Game
        Dim game As Game

        game = New Game With {
                               .GameId = gameId,
                               .PlayedOn = playedOn,
                               .RequiredScoreToWin = Integer.MaxValue,
                               .Status = status
                             }
        _gameRepository.Create(game)
        _gameRepository.SaveChanges()

        Return game
    End Function

    Private Sub AssignPlayerToGame(game As Game, player As Player, isWinner As Boolean, points As Integer)
        Dim gamePlayer As GamePlayer
        Dim isWinnerString = If(isWinner, "Y", "N")

        gamePlayer = New GamePlayer With {
                                           .GameId = game.GameId,
                                           .PlayerId = player.PlayerId,
                                           .IsWinner = isWinnerString,
                                           .Points = points
                                         }
        _gamePlayerRepository.Create(gamePlayer)
        _gamePlayerRepository.SaveChanges()
    End Sub
#End Region

End Class
