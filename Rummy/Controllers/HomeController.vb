Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private _gameService As GameService
    Private _playerService As PlayerService
    Private _standingService As StandingService
    Private _gameHistoryService As GameHistoryService

    Public Sub New()
        Dim db As RummyEntities
        Dim oc As ObjectContextAdapter

        db = New RummyEntities
        oc = New ObjectContextAdapter(db)

        _gameService = New GameService(oc)
        _playerService = New PlayerService(oc)
        _standingService = New StandingService(oc)
        _gameHistoryService = New GameHistoryService(oc)
    End Sub

    Function Index() As ActionResult
        Dim model As IndexViewModel

        model = New IndexViewModel
        model.Standings = _standingService.Load()

        Return View(model)
    End Function

    Function GameHistory() As ActionResult
        Dim model As GameHistoryViewModel

        model = New GameHistoryViewModel
        model.History = _gameHistoryService.Load

        Return View(model)
    End Function

    Function ManageGame(ByVal gameId As Integer) As ActionResult
        Dim model As ManageGameViewModel

        model = New ManageGameViewModel
        model.GameId = gameId
        model.NextRoundNumber = _gameService.FindNextRoundNumber(gameId)

        Return View(model)
    End Function

    Function DeleteGame(ByVal gameId As Integer) As ActionResult
        _gameService.DeleteGame(gameId)

        Return RedirectToAction("GameHistory")
    End Function

    Function StartGame() As ActionResult
        Dim model As StartGameViewModel

        model = New StartGameViewModel
        model.EligiblePlayers = _playerService.FindAll.ToList

        Return View(model)
    End Function

    <HttpPost()>
    Function StartGame(ByVal model As StartGameViewModel) As ActionResult
        Dim game As Game

        If model.SelectedPlayers Is Nothing OrElse model.SelectedPlayers.Count = 0 Then
            ModelState.AddModelError("SelectedPlayers", "You must select at least one player.")
            model.EligiblePlayers = _playerService.FindAll.ToList
            Return View(model)
        End If

        If model.PointsRequiredToWin <= 0 Then
            ModelState.AddModelError("PointsRequiredToWin", "You must enter a value greater than zero.")
            model.EligiblePlayers = _playerService.FindAll.ToList
            Return View(model)
        End If

        game = _gameService.CreateGame(DateTime.Now, model.PointsRequiredToWin, model.SelectedPlayers)

        Return RedirectToAction("ViewScore", New With {.gameId = game.GameId, .nextRoundNumber = 1})
    End Function

    Function ViewScore(ByVal gameId As Integer, ByVal nextRoundNumber As Integer) As ActionResult
        Dim viewScoreModel As ViewScoreViewModel

        viewScoreModel = CreateViewScoreViewModel(gameId, nextRoundNumber)

        Return View(viewScoreModel)
    End Function

    Function ViewScoreDetails(ByVal gameId As Integer, ByVal nextRoundNumber As Integer) As ActionResult
        Dim viewScoreModel As ViewScoreViewModel

        viewScoreModel = CreateViewScoreViewModel(gameId, nextRoundNumber)

        Return View(viewScoreModel)
    End Function

    Private Function CreateViewScoreViewModel(ByVal gameId As Integer, ByVal nextRoundNumber As Integer) As ViewScoreViewModel
        Dim viewScoreModel As ViewScoreViewModel

        viewScoreModel = New ViewScoreViewModel
        viewScoreModel.GameId = gameId
        viewScoreModel.NextRoundNumber = nextRoundNumber
        viewScoreModel.Players = _playerService.FindByGame(gameId)
        viewScoreModel.PlayerScores = _gameService.FindPointDetails(gameId)
        viewScoreModel.IsGameOver = _gameService.IsGameOver(gameId)
        viewScoreModel.GamePlayers = _gameService.FindPlayerDetails(gameId)

        Return viewScoreModel
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
        Dim players As List(Of Player)
        Dim textboxId As String
        Dim points As Integer
        Dim isGameOver As Boolean
        Dim nextRoundNumber As Integer

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

        nextRoundNumber = _gameService.FindNextRoundNumber(gameId)
        Return RedirectToAction("ViewScore", New With {.gameId = gameId, .nextRoundNumber = nextRoundNumber})
    End Function

    Function EditPlayer() As ActionResult
        Dim model As EditPlayerViewModel
        model = New EditPlayerViewModel
        model.PlayerId = 0
        model.PlayerName = String.Empty

        Return View(model)
    End Function

    <HttpPost()>
    Function EditPlayer(ByVal model As EditPlayerViewModel) As ActionResult
        Dim player As Player

        If String.IsNullOrWhiteSpace(model.PlayerName) Then
            ModelState.AddModelError("PlayerName", "You must enter a name value.")
            Return View(model)
        End If

        If model.PlayerId > 0 Then
            'Edit existing player.
            'TODO - IMPLEMENT THIS WHEN NEEDED
        Else
            'Create new player.
            player = _playerService.CreatePlayer(model.PlayerName)
        End If

        Return RedirectToAction("StartGame")
    End Function

End Class
