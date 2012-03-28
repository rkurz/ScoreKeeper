Imports System.Text

<TestClass()>
Public Class PlayerServiceTests
    Private _context As MockObjectContext
    Private _playerService As PlayerService
    Private _playerRepository As PlayerRepository
    Private _gameRepository As GameRepository
    Private _gamePlayerRepository As GamePlayerRepository

    <TestInitialize()>
    Public Sub Setup()
        _context = New MockObjectContext
        _playerService = New PlayerService(_context)
        _playerRepository = New PlayerRepository(_context)
        _gameRepository = New GameRepository(_context)
        _gamePlayerRepository = New GamePlayerRepository(_context)
    End Sub

#Region "FindAll"
    <TestMethod()>
    Public Sub FindAll_ReturnsEmptyListIfNoPlayersExist()
        Dim players As List(Of Player)

        players = _playerService.FindAll.ToList

        Assert.AreEqual(0, players.Count)
    End Sub

    <TestMethod()>
    Public Sub FindAll_ReturnsAllPlayers()
        Dim players As List(Of Player)

        CreatePlayer(1)
        CreatePlayer(2)
        CreatePlayer(3)

        players = _playerService.FindAll.ToList

        Assert.AreEqual(3, players.Count)
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = 1))
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = 2))
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = 3))
    End Sub
#End Region

#Region "FindByGame"
    <TestMethod()>
    Public Sub FindByGame_ReturnsEmptyListIfGameDoesNotExist()
        Dim players As List(Of Player)

        players = _playerService.FindByGame(1)

        Assert.AreEqual(0, players.Count)
    End Sub

    <TestMethod()>
    Public Sub FindByGame_ReturnsEmptyListIfGameHasNoPlayers()
        Dim game = CreateGame(1)

        Dim players = _playerService.FindByGame(game.GameId)

        Assert.AreEqual(0, players.Count)
    End Sub

    <TestMethod()>
    Public Sub FindByGame_ReturnsPlayersAssignedToGame()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim game = CreateGame(1)
        AssignPlayerToGame(game, playerOne)
        AssignPlayerToGame(game, playerTwo)

        Dim players = _playerService.FindByGame(game.GameId)

        Assert.AreEqual(2, players.Count)
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = playerOne.PlayerId))
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = playerTwo.PlayerId))
    End Sub

    <TestMethod()>
    Public Sub FindByGame_DoesNotReturnPlayersNotAssignedToGame()
        Dim playerOne = CreatePlayer(1)
        Dim playerTwo = CreatePlayer(2)
        Dim playerThree = CreatePlayer(3)
        Dim targetGame = CreateGame(1)
        AssignPlayerToGame(targetGame, playerOne)
        AssignPlayerToGame(targetGame, playerTwo)
        Dim otherGame = CreateGame(2)
        AssignPlayerToGame(otherGame, playerThree)

        Dim players = _playerService.FindByGame(targetGame.GameId)

        Assert.AreEqual(2, players.Count)
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = playerOne.PlayerId))
        Assert.IsTrue(players.Exists(Function(p) p.PlayerId = playerTwo.PlayerId))
        Assert.IsFalse(players.Exists(Function(p) p.PlayerId = playerThree.PlayerId))
    End Sub
#End Region

#Region "CreatePlayer"
    <TestMethod()>
    Public Sub CreatePlayer_RecordIsPersisted()
        _playerService.CreatePlayer("Alice")

        Dim recordWasPersisted = _playerRepository.FindAll.Any(Function(p) p.Name = "Alice")
        Assert.IsTrue(recordWasPersisted)
    End Sub
#End Region

#Region "DeletePlayer"
    <TestMethod()>
    Public Sub DeletePlayer_RecordIsRemoved()
        Dim recordExists As Boolean
        Dim player = CreatePlayer(1)

        recordExists = _playerRepository.FindAll.Any(Function(p) p.PlayerId = player.PlayerId)
        Assert.IsTrue(recordExists, "Player should exist")

        _playerService.DeletePlayer(player)
        recordExists = _playerRepository.FindAll.Any(Function(p) p.PlayerId = player.PlayerId)
        Assert.IsFalse(recordExists, "Player should have been removed")
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

    Private Function CreateGame(gameId As Integer) As Game
        Dim game As Game

        game = New Game With {
                               .GameId = gameId,
                               .PlayedOn = DateTime.Now,
                               .RequiredScoreToWin = Integer.MaxValue,
                               .Status = GameStatus.Complete
                             }
        _gameRepository.Create(game)
        _gameRepository.SaveChanges()

        Return game
    End Function

    Private Sub AssignPlayerToGame(game As Game, player As Player)
        Dim gamePlayer As GamePlayer

        gamePlayer = New GamePlayer With {
                                           .GameId = game.GameId,
                                           .PlayerId = player.PlayerId,
                                           .IsWinner = False,
                                           .Points = 0
                                         }
        _gamePlayerRepository.Create(gamePlayer)
        _gamePlayerRepository.SaveChanges()
    End Sub
#End Region

End Class
