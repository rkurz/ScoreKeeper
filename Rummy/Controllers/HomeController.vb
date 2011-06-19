Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private _gameService As GameService
    Private _playerService As PlayerService
    Private _standingService As StandingService

    Public Sub New()
        Dim db As RummyEntities
        Dim oc As ObjectContextAdapter

        db = New RummyEntities
        oc = New ObjectContextAdapter(db)

        _gameService = New GameService(oc)
        _playerService = New PlayerService(oc)
        _standingService = New StandingService(oc)
    End Sub

    Function Index() As ActionResult
        Dim model As IndexViewModel

        model = New IndexViewModel
        model.Standings = _standingService.Load()

        Return View(model)
    End Function

    Function StartGame() As ActionResult
        Return View()
    End Function

    <HttpPost()>
    Function StartGame(ByVal model As StartGameViewModel) As ActionResult
        Dim players As List(Of Player)
        Dim viewScoreModel As ViewScoreViewModel
        Dim game As Game

        'TODO - Eventually all users to choose players!!
        players = _playerService.FindAll.ToList

        game = _gameService.CreateGame(DateTime.Now, model.PointsRequiredToWin, players)

        viewScoreModel = New ViewScoreViewModel
        viewScoreModel.GameId = game.GameId
        viewScoreModel.NextRoundNumber = 1
        viewScoreModel.Players = _playerService.FindByGame(game.GameId)
        viewScoreModel.PlayerScores = _gameService.FindPointDetails(game.GameId)
        viewScoreModel.IsGameOver = False
        viewScoreModel.GamePlayers = _gameService.FindPlayerDetails(game.GameId)

        Return RedirectToAction("ViewScore", New With {.gameId = game.GameId, .nextRoundNumber = 1})
        'Return View("ViewScore", viewScoreModel)
    End Function

    Function ViewScore(ByVal gameId As Integer, ByVal nextRoundNumber As Integer) As ActionResult
        Dim viewScoreModel As ViewScoreViewModel

        viewScoreModel = New ViewScoreViewModel
        viewScoreModel.GameId = gameId
        viewScoreModel.NextRoundNumber = nextRoundNumber
        viewScoreModel.Players = _playerService.FindByGame(gameId)
        viewScoreModel.PlayerScores = _gameService.FindPointDetails(gameId)
        viewScoreModel.IsGameOver = _gameService.IsGameOver(gameId)
        viewScoreModel.GamePlayers = _gameService.FindPlayerDetails(gameId)

        Return View(viewScoreModel)
    End Function

    Function ViewScoreDetails(ByVal gameId As Integer, ByVal nextRoundNumber As Integer) As ActionResult
        Dim viewScoreModel As ViewScoreViewModel

        viewScoreModel = New ViewScoreViewModel
        viewScoreModel.GameId = gameId
        viewScoreModel.NextRoundNumber = nextRoundNumber
        viewScoreModel.Players = _playerService.FindByGame(gameId)
        viewScoreModel.PlayerScores = _gameService.FindPointDetails(gameId)
        viewScoreModel.IsGameOver = _gameService.IsGameOver(gameId)
        viewScoreModel.GamePlayers = _gameService.FindPlayerDetails(gameId)

        Return View(viewScoreModel)
    End Function

    Function AddScore(ByVal gameId As Integer, ByVal roundNumber As Integer) As ActionResult
        Dim model As AddScoreViewModel

        model = New AddScoreViewModel
        model.Players = _playerService.FindByGame(gameId)
        model.RoundNumber = roundNumber
        model.NextRoundNumber = _gameService.FindNextRoundNumber(gameId)
        model.GameId = gameId
        model.PlayerScores = _gameService.FindPointDetails(gameId, roundNumber)

        Return View(model)
    End Function

    <HttpPost()>
    Function AddScore(ByVal gameId As Integer, ByVal roundNumber As Integer, ByVal formCollection As FormCollection)
        Dim viewScoreModel As ViewScoreViewModel
        Dim players As List(Of Player)
        Dim textboxId As String
        Dim points As Integer
        Dim isGameOver As Boolean

        players = _playerService.FindByGame(gameId)
        For Each player As Player In players
            textboxId = String.Format("txtPlayer_{0:d}", player.PlayerId)
            If String.IsNullOrWhiteSpace(formCollection(textboxId)) Then
                points = 0
            Else
                Try
                    points = Convert.ToInt32(formCollection(textboxId))
                Catch ex As Exception
                    points = 0
                End Try
            End If

            _gameService.AddPlayerScore(gameId, roundNumber, player.PlayerId, points)
        Next

        isGameOver = _gameService.IsGameOver(gameId)
        If isGameOver Then
            _gameService.CompleteGame(gameId)
        End If

        viewScoreModel = New ViewScoreViewModel
        viewScoreModel.GameId = gameId
        viewScoreModel.NextRoundNumber = _gameService.FindNextRoundNumber(gameId)
        viewScoreModel.Players = players
        viewScoreModel.PlayerScores = _gameService.FindPointDetails(gameId)
        viewScoreModel.IsGameOver = isGameOver
        viewScoreModel.GamePlayers = _gameService.FindPlayerDetails(gameId)

        Return View("ViewScore", viewScoreModel)
    End Function
End Class
