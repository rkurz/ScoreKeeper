Public Class PlayerService
    Private _playerRepository As PlayerRepository
    Private _gamePlayerRepository As GamePlayerRepository

    Public Sub New(ByVal context As IObjectContext)
        _playerRepository = New PlayerRepository(context)
        _gamePlayerRepository = New GamePlayerRepository(context)
    End Sub

    Public Function FindAll() As IEnumerable(Of Player)
        Return From p In _playerRepository.FindAll
               Select p
    End Function

    Public Function FindByGame(ByVal gameId As Integer) As List(Of Player)
        Return (From p In _playerRepository.FindAll
               Join gp In _gamePlayerRepository.FindAll On p.PlayerId Equals gp.PlayerId
               Where gp.GameId = gameId
               Select p).ToList
    End Function

    Public Function CreatePlayer(name As String) As Player
        Dim player As Player

        player = player.CreatePlayer(0, name)
        _playerRepository.Create(player)
        _playerRepository.SaveChanges()

        Return player
    End Function
End Class
