Public Class PlayerRepository
    Inherits Repository(Of Player)

    Public Sub New(ByVal context As IObjectContext)
        MyBase.New(context, "Players")
    End Sub
End Class
