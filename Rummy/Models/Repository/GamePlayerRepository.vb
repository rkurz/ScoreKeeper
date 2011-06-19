Public Class GamePlayerRepository
    Inherits Repository(Of GamePlayer)

    Public Sub New(ByVal context As IObjectContext)
        MyBase.New(context, "GamePlayers")
    End Sub
End Class
