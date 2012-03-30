Public Class IndexPage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/Index")
    End Sub

    Public ReadOnly Property NewGameLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("New Game"))
        End Get
    End Property

    Public ReadOnly Property GameHistoryLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("History"))
        End Get
    End Property

    Public Sub StartNewGame()
        NewGameLink.Click()
    End Sub
End Class
