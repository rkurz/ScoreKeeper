Imports System.Text

<TestClass()>
Public Class StandingServiceTests
    Private _context As MockObjectContext
    Private _standingService As StandingService
    Private _playerRepository As PlayerRepository
    Private _gameRepository As GameRepository
    Private _gamePlayerRepository As GamePlayerRepository

    <TestInitialize()>
    Public Sub Setup()
        _context = New MockObjectContext
        _standingService = New StandingService(_context)
        _playerRepository = New PlayerRepository(_context)
        _gameRepository = New GameRepository(_context)
        _gamePlayerRepository = New GamePlayerRepository(_context)
    End Sub

    <TestMethod()>
    Public Sub Load_PlayerWithNoGamesPlayedDoesNotAppear()
        CreatePlayer(1)

        Dim standings = _standingService.Load

        Assert.AreEqual(0, standings.Count)
    End Sub

    <TestMethod()>
    Public Sub Load_OnlyCompleteGamesAreIncludedInStats()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim completeGame = CreateCompleteGame(1)
        AssignPlayerToGame(completeGame, playerOne, True, 5)
        AssignPlayerToGame(completeGame, playerTwo, False, 1)
        Dim incompleteGame = CreateInProgressGame(2)
        AssignPlayerToGame(incompleteGame, playerOne, True, 15)
        AssignPlayerToGame(incompleteGame, playerTwo, False, 11)
        Dim deletedGame = CreateDeletedGame(3)
        AssignPlayerToGame(deletedGame, playerOne, True, 115)
        AssignPlayerToGame(deletedGame, playerTwo, False, 111)

        Dim standings = _standingService.Load

        Assert.AreEqual(2, standings.Count)
        Dim playerOneStanding = standings.FirstOrDefault(Function(s) s.PlayerId = playerOne.PlayerId)
        Assert.IsNotNull(playerOneStanding, "PlayerOne was not present in standings")
        Assert.AreEqual(1, playerOneStanding.GamesPlayed, "PlayerOne Games Played did not match expected value")
        Assert.AreEqual(1, playerOneStanding.Wins, "PlayerOne Wins did not match expected value")
        Assert.AreEqual(0, playerOneStanding.Losses, "PlayerOne Losses did not match expected value")

        Dim playerTwoStanding = standings.FirstOrDefault(Function(s) s.PlayerId = playerTwo.PlayerId)
        Assert.IsNotNull(playerTwoStanding, "PlayerTwo was not present in standings")
        Assert.AreEqual(1, playerTwoStanding.GamesPlayed, "PlayerTwo Games Played did not match expected value")
        Assert.AreEqual(0, playerTwoStanding.Wins, "PlayerTwo Wins did not match expected value")
        Assert.AreEqual(1, playerTwoStanding.Losses, "PlayerTwo Losses did not match expected value")
    End Sub

    <TestMethod()>
    Public Sub Load_PlayerStatsCalculatedCorrectly()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim playerThree = CreatePlayer(3)
        Dim gameOne = CreateCompleteGame(1)
        AssignPlayerToGame(gameOne, playerOne, True, 5)
        AssignPlayerToGame(gameOne, playerTwo, False, 1)
        AssignPlayerToGame(gameOne, playerThree, False, 0)
        Dim gameTwo = CreateCompleteGame(2)
        AssignPlayerToGame(gameTwo, playerOne, True, 10)
        AssignPlayerToGame(gameTwo, playerTwo, False, 0)
        AssignPlayerToGame(gameTwo, playerThree, False, 4)
        Dim gameThree = CreateCompleteGame(3)
        AssignPlayerToGame(gameThree, playerOne, False, 3)
        AssignPlayerToGame(gameThree, playerTwo, True, 6)

        Dim standings = _standingService.Load

        Assert.AreEqual(3, standings.Count)
        Dim playerOneStanding = standings.FirstOrDefault(Function(s) s.PlayerId = playerOne.PlayerId)
        Assert.IsNotNull(playerOneStanding, "PlayerOne was not present in standings")
        Assert.AreEqual(3, playerOneStanding.GamesPlayed, "PlayerOne Games Played did not match expected value")
        Assert.AreEqual(2, playerOneStanding.Wins, "PlayerOne Wins did not match expected value")
        Assert.AreEqual(1, playerOneStanding.Losses, "PlayerOne Losses did not match expected value")

        Dim playerTwoStanding = standings.FirstOrDefault(Function(s) s.PlayerId = playerTwo.PlayerId)
        Assert.IsNotNull(playerTwoStanding, "PlayerTwo was not present in standings")
        Assert.AreEqual(3, playerTwoStanding.GamesPlayed, "PlayerTwo Games Played did not match expected value")
        Assert.AreEqual(1, playerTwoStanding.Wins, "PlayerTwo Wins did not match expected value")
        Assert.AreEqual(2, playerTwoStanding.Losses, "PlayerTwo Losses did not match expected value")

        Dim playerThreeStanding = standings.FirstOrDefault(Function(s) s.PlayerId = playerThree.PlayerId)
        Assert.IsNotNull(playerThreeStanding, "PlayerThree was not present in standings")
        Assert.AreEqual(2, playerThreeStanding.GamesPlayed, "PlayerThree Games Played did not match expected value")
        Assert.AreEqual(0, playerThreeStanding.Wins, "PlayerThree Wins did not match expected value")
        Assert.AreEqual(2, playerThreeStanding.Losses, "PlayerThree Losses did not match expected value")
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

    Private Function CreateCompleteGame(gameId As Integer) As Game
        Return CreateGame(gameId, GameStatus.Complete)
    End Function
    Private Function CreateInProgressGame(gameId As Integer) As Game
        Return CreateGame(gameId, GameStatus.InProgress)
    End Function
    Private Function CreateDeletedGame(gameId As Integer) As Game
        Return CreateGame(gameId, GameStatus.Deleted)
    End Function

    Private Function CreateGame(gameId As Integer, status As GameStatus) As Game
        Dim game As Game

        game = New Game With {
                               .GameId = gameId,
                               .PlayedOn = DateTime.Now,
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
