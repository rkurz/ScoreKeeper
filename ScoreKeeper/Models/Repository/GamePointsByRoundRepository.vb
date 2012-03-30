Public Class GamePointsByRoundRepository
    Inherits Repository(Of GamePointsByRound)

    Public Sub New(ByVal context As IObjectContext)
        MyBase.New(context, "GamePointsByRounds")
    End Sub
End Class
