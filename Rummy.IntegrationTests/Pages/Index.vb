Public Class IndexPage
    Inherits Page

    Public ReadOnly Property NewGameLink As Link
        Get
            Return Document.Link(Find.ByText("New Game"))
        End Get
    End Property

    Public Sub StartNewGame()
        NewGameLink.Click()
    End Sub
End Class
