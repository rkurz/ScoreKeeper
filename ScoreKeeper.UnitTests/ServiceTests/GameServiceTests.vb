Imports System.Text

<TestClass()>
Public Class GameServiceTests
    Private _context As MockObjectContext
    Private _gameService As GameService
    Private _gameRepository As GameRepository
    Private _gamePlayerRepository As GamePlayerRepository
    Private _playerRepository As PlayerRepository
    Private _gamePointsByRoundRepository As GamePointsByRoundRepository

    <TestInitialize()>
    Public Sub Setup()
        _context = New MockObjectContext
        _gameService = New GameService(_context)
        _gameRepository = New GameRepository(_context)
        _gamePlayerRepository = New GamePlayerRepository(_context)
        _playerRepository = New PlayerRepository(_context)
        _gamePointsByRoundRepository = New GamePointsByRoundRepository(_context)
    End Sub

#Region "CreateGame"

    <TestMethod()>
    Public Sub CreateGame_SetsGameFieldsCorrectly()
        Dim playedOn = DateTime.Now
        Dim requiredScore = 7

        _gameService.CreateGame(playedOn, requiredScore, New List(Of Integer)())

        Dim game = _gameRepository.FindAll.FirstOrDefault()
        Assert.IsNotNull(game)
        Assert.AreEqual(playedOn, game.PlayedOn, "PlayedOn did not match expected value")
        Assert.AreEqual(requiredScore, game.RequiredScoreToWin, "RequiredScoreToWin did not match expected value")
    End Sub

    <TestMethod()>
    Public Sub CreateGame_SetsStatusToInProgress()

        _gameService.CreateGame(DateTime.Now, 0, New List(Of Integer)())

        Dim game = _gameRepository.FindAll.FirstOrDefault()
        Assert.IsNotNull(game)
        Assert.AreEqual(GameStatus.InProgress, game.Status, "Status did not match expected value")
    End Sub

    <TestMethod()>
    Public Sub CreateGame_AllPlayersAddedToGame()
        Dim players = New List(Of Integer)()
        players.Add(1)
        players.Add(2)
        _gameService.CreateGame(DateTime.Now, 0, players)

        Dim gamePlayerItems = _gamePlayerRepository.FindAll.ToList
        Assert.AreEqual(2, gamePlayerItems.Count)
        Assert.IsTrue(gamePlayerItems.Any(Function(p) p.PlayerId = 1))
        Assert.IsTrue(gamePlayerItems.Any(Function(p) p.PlayerId = 2))
    End Sub

    <TestMethod()>
    Public Sub CreateGame_PlayerIsAddedAsNonWinnerWithZeroPoints()
        Dim players = New List(Of Integer)()
        players.Add(1)
        _gameService.CreateGame(DateTime.Now, 0, players)

        Dim gamePlayerItem = _gamePlayerRepository.FindAll.FirstOrDefault
        Assert.IsNotNull(gamePlayerItem)
        Assert.AreEqual("N", gamePlayerItem.IsWinner)
        Assert.AreEqual(0, gamePlayerItem.Points)
    End Sub

#End Region

#Region "AddPlayerScore"

    <TestMethod()>
    Public Sub AddPlayerScore_InsertsScoreForNewRound()
        Dim player = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, player, False, 0)

        _gameService.AddPlayerScore(game.GameId, 1, player.PlayerId, 5)

        Dim playerScore = FindPlayerScoreByRound(game, player, 1)
        Assert.IsNotNull(playerScore)
        Assert.AreEqual(5, playerScore.Points)
    End Sub

    <TestMethod()>
    Public Sub AddPlayerScore_OverwritesScoreForExistingRound()
        Dim player = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, player, False, 0)

        _gameService.AddPlayerScore(game.GameId, 1, player.PlayerId, 5)
        _gameService.AddPlayerScore(game.GameId, 1, player.PlayerId, 7)

        Dim playerScore = FindPlayerScoreByRound(game, player, 1)
        Assert.IsNotNull(playerScore)
        Assert.AreEqual(7, playerScore.Points)
    End Sub

    <TestMethod()>
    Public Sub AddPlayerScore_UpdatesTotalScoreForPlayer()
        Dim player = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, player, False, 0)

        _gameService.AddPlayerScore(game.GameId, 1, player.PlayerId, 5)

        Dim totalScore = FindGamePlayer(game, player)
        Assert.IsNotNull(totalScore)
        Assert.AreEqual(5, totalScore.Points)
    End Sub

#End Region

#Region "UpdateTotalPlayerScores"

    <TestMethod()>
    Public Sub UpdateTotalPlayerScores_SetsPlayerScoreToZeroIfNoRoundScoresExist()
        Dim player = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, player, False, Integer.MinValue)

        _gameService.UpdateTotalPlayerScores(game.GameId)

        Dim playerScore = FindGamePlayer(game, player)
        Assert.IsNotNull(playerScore)
        Assert.AreEqual(0, playerScore.Points)
    End Sub

    <TestMethod()>
    Public Sub UpdateTotalPlayerScores_SetsPlayerScoreToSumOfTheirRoundScores()
        Dim player = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, player, False, Integer.MinValue)
        AddPlayerScoreByRound(game, player, 1, 10)
        AddPlayerScoreByRound(game, player, 2, 5)
        AddPlayerScoreByRound(game, player, 2, 7)

        _gameService.UpdateTotalPlayerScores(game.GameId)

        Dim playerScore = FindGamePlayer(game, player)
        Assert.IsNotNull(playerScore)
        Assert.AreEqual(22, playerScore.Points)
    End Sub

    <TestMethod()>
    Public Sub UpdateTotalPlayerScores_SetsScoreForAllPlayersInGame()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, playerOne, False, Integer.MinValue)
        AssignPlayerToGame(game, playerTwo, False, Integer.MinValue)
        AddPlayerScoreByRound(game, playerOne, 1, 10)
        AddPlayerScoreByRound(game, playerOne, 2, 5)
        AddPlayerScoreByRound(game, playerTwo, 1, 0)
        AddPlayerScoreByRound(game, playerTwo, 2, 8)

        _gameService.UpdateTotalPlayerScores(game.GameId)

        Dim playerOneScore = FindGamePlayer(game, playerOne)
        Assert.IsNotNull(playerOneScore)
        Assert.AreEqual(15, playerOneScore.Points)
        Dim playerTwoScore = FindGamePlayer(game, playerTwo)
        Assert.IsNotNull(playerTwoScore)
        Assert.AreEqual(8, playerTwoScore.Points)
    End Sub

#End Region

#Region "FindById"

    <TestMethod()>
    Public Sub FindById_ReturnsNullIfGameIdDoesNotExist()
        CreateCompleteGame(1, DateTime.Now)

        Dim game = _gameService.FindById(2)
        Assert.IsNull(game)
    End Sub

    <TestMethod()>
    Public Sub FindById_ReturnsProperGameIfGameIdExists()
        CreateCompleteGame(1, DateTime.Now)

        Dim game = _gameService.FindById(1)
        Assert.IsNotNull(game)
    End Sub
    
#End Region

#Region "FindAllCompleteOrInProgressGames"

    <TestMethod()>
    Public Sub FindAllCompleteOrInProgressGames_DoesNotReturnDeletedGames()
        Dim completeGame = CreateCompleteGame(1, DateTime.Now)
        Dim incompleteGame = CreateInProgressGame(2, DateTime.Now)
        Dim deletedGame = CreateDeletedGame(3)

        Dim games = _gameService.FindAllCompleteOrInProgressGames.ToList

        Assert.AreEqual(2, games.Count)
        Assert.IsTrue(games.Any(Function(g) g.GameId = completeGame.GameId), "Complete game should have been found")
        Assert.IsTrue(games.Any(Function(g) g.GameId = incompleteGame.GameId), "Incomplete game should have been found")
        Assert.IsFalse(games.Any(Function(g) g.GameId = deletedGame.GameId), "Deleted game should not have been found")
    End Sub

#End Region

#Region "FindNextRoundNumber"

    <TestMethod()>
    Public Sub FindNextRoundNumber_InitialRoundIsOne()
        Dim game = CreateInProgressGame(1, DateTime.Now)
        
        Dim nextRoundNumber = _gameService.FindNextRoundNumber(game.GameId)

        Assert.AreEqual(1, nextRoundNumber)
    End Sub

    <TestMethod()>
    Public Sub FindNextRoundNumber_ReturnsOneGreaterThanHighestExistingRoundNumber()
        Dim playerOne = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now)
        AssignPlayerToGame(game, playerOne, False, Integer.MinValue)
        AddPlayerScoreByRound(game, playerOne, 1, 10)
        AddPlayerScoreByRound(game, playerOne, 2, 5)

        Dim nextRoundNumber = _gameService.FindNextRoundNumber(game.GameId)

        Assert.AreEqual(3, nextRoundNumber)
    End Sub

#End Region

#Region "DeleteGame"
    <TestMethod()>
    Public Sub DeleteGame_ChangesStatusToDeleted()
        Dim game = CreateCompleteGame(1, DateTime.Now)

        _gameService.DeleteGame(game.GameId)

        Dim updatedGame = _gameService.FindById(game.GameId)
        Assert.IsNotNull(updatedGame)
        Assert.AreEqual(GameStatus.Deleted, updatedGame.Status)
    End Sub
#End Region

#Region "IsGameOver"

    <TestMethod()>
    Public Sub IsGameOver_ReturnsFalseIfGameDoesNotExist()
        Dim isGameOver = _gameService.IsGameOver(1)

        Assert.IsFalse(isGameOver)
    End Sub

    <TestMethod()>
    Public Sub IsGameOver_ReturnsFalseIfNoPlayersHaveReachedRequiredScore()
        Dim player = CreatePlayer(1)
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)
        AssignPlayerToGame(game, player, False, 40)

        Dim isGameOver = _gameService.IsGameOver(game.GameId)

        Assert.IsFalse(isGameOver)
    End Sub

    <TestMethod()>
    Public Sub IsGameOver_ReturnsTrueIfOnlyOnePlayerHasReachedRequiredScore()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)
        AssignPlayerToGame(game, playerOne, False, 40)
        AssignPlayerToGame(game, playerTwo, False, 101)

        Dim isGameOver = _gameService.IsGameOver(game.GameId)

        Assert.IsTrue(isGameOver)
    End Sub

    <TestMethod()>
    Public Sub IsGameOver_ReturnsFalseIfMultiplePlayersHaveSameHighScoreGreaterThanRequiredScore()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)
        AssignPlayerToGame(game, playerOne, False, 101)
        AssignPlayerToGame(game, playerTwo, False, 101)

        Dim isGameOver = _gameService.IsGameOver(game.GameId)

        Assert.IsFalse(isGameOver)
    End Sub

    <TestMethod()>
    Public Sub IsGameOver_ReturnsTrueIfMultiplePlayersAchieveRequiredScoreButOnlyOneHasHighScore()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)
        AssignPlayerToGame(game, playerOne, False, 101)
        AssignPlayerToGame(game, playerTwo, False, 102)

        Dim isGameOver = _gameService.IsGameOver(game.GameId)

        Assert.IsTrue(isGameOver)
    End Sub

#End Region

#Region "CompleteGame"
    <TestMethod()>
    Public Sub CompleteGame_SetsGameStatusToComplete()
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)

        _gameService.CompleteGame(game.GameId)

        Dim completedGame = _gameService.FindById(game.GameId)
        Assert.IsNotNull(completedGame)
        Assert.AreEqual(GameStatus.Complete, completedGame.Status)
    End Sub

    <TestMethod()>
    Public Sub CompleteGame_MarksPlayerWithHighScoreAsWinner()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)
        AssignPlayerToGame(game, playerOne, False, 101)
        AssignPlayerToGame(game, playerTwo, False, 50)

        _gameService.CompleteGame(game.GameId)

        Dim playerOneScore = FindGamePlayer(game, playerOne)
        Assert.IsNotNull(playerOneScore)
        Assert.IsTrue(playerOneScore.IsWinner = "Y")
        Dim playerTwoScore = FindGamePlayer(game, playerTwo)
        Assert.IsNotNull(playerTwoScore)
        Assert.IsFalse(playerTwoScore.IsWinner = "Y")
    End Sub

    <TestMethod()>
    Public Sub CompleteGame_MarksAllPlayerWithHighScoreAsWinner()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim playerThree = CreatePlayer(3)
        Dim game = CreateInProgressGame(1, DateTime.Now, 100)
        AssignPlayerToGame(game, playerOne, False, 101)
        AssignPlayerToGame(game, playerTwo, False, 50)
        AssignPlayerToGame(game, playerThree, False, 101)

        _gameService.CompleteGame(game.GameId)

        Dim playerOneScore = FindGamePlayer(game, playerOne)
        Assert.IsNotNull(playerOneScore)
        Assert.IsTrue(playerOneScore.IsWinner = "Y")
        Dim playerTwoScore = FindGamePlayer(game, playerTwo)
        Assert.IsNotNull(playerTwoScore)
        Assert.IsFalse(playerTwoScore.IsWinner = "Y")
        Dim playerThreeScore = FindGamePlayer(game, playerThree)
        Assert.IsNotNull(playerThreeScore)
        Assert.IsTrue(playerThreeScore.IsWinner = "Y")
    End Sub

#End Region


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
        Return CreateGame(gameId, GameStatus.Complete, playedOn, Integer.MaxValue)
    End Function
    Private Function CreateInProgressGame(gameId As Integer, playedOn As DateTime) As Game
        Return CreateGame(gameId, GameStatus.InProgress, playedOn, Integer.MaxValue)
    End Function
    Private Function CreateInProgressGame(gameId As Integer, playedOn As DateTime, pointsRequiredToWin As Integer) As Game
        Return CreateGame(gameId, GameStatus.InProgress, playedOn, pointsRequiredToWin)
    End Function
    Private Function CreateDeletedGame(gameId As Integer) As Game
        Return CreateGame(gameId, GameStatus.Deleted, DateTime.Now, Integer.MaxValue)
    End Function

    Private Function CreateGame(gameId As Integer, status As GameStatus, playedOn As DateTime, pointsRequiredToWin As Integer) As Game
        Dim game As Game

        game = New Game With {
                               .GameId = gameId,
                               .PlayedOn = playedOn,
                               .RequiredScoreToWin = pointsRequiredToWin,
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

    Private Sub AddPlayerScoreByRound(game As Game, player As Player, round As Integer, points As Integer)
        Dim score = New GamePointsByRound With {
                                                 .GameId = game.GameId,
                                                 .PlayerId = player.PlayerId,
                                                 .RoundNumber = round,
                                                 .Points = points
                                               }
        _gamePointsByRoundRepository.Create(score)
        _gamePointsByRoundRepository.SaveChanges()
    End Sub

    Private Function FindPlayerScoreByRound(game As Game, player As Player, round As Integer) As GamePointsByRound
        Return _gamePointsByRoundRepository.FindAll.FirstOrDefault(Function(gp) gp.GameId = game.GameId AndAlso
                                                                                gp.PlayerId = player.PlayerId AndAlso
                                                                                gp.RoundNumber = round)
    End Function

    Private Function FindGamePlayer(game As Game, player As Player) As GamePlayer
        Return _gamePlayerRepository.FindAll.FirstOrDefault(Function(gp) gp.GameId = game.GameId AndAlso
                                                                         gp.PlayerId = player.PlayerId)
    End Function
#End Region

End Class
