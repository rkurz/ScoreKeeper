Public Class GameRepository
    Inherits Repository(Of Game)

    Public Sub New(ByVal context As IObjectContext)
        MyBase.New(context, "Games")
    End Sub
End Class
