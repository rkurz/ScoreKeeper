Public Class IndexPage
    Inherits Page

    Public ReadOnly Property NewGameLink As Link
        Get
            Dim container As Div
            container = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))

            Return container.Link(Find.ByText("New Game"))
        End Get
    End Property

    Public Sub StartNewGame()
        NewGameLink.Click()
    End Sub
End Class
