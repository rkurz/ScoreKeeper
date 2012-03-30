Public Class ManageGamePage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/ManageGame")
    End Sub

    Public Function ResumeGameLink() As Link
        Return Me.ActiveDialogContent.Link(Find.ByText("Resume Game"))
    End Function

    Public Function DeleteGameLink() As Link
        Return Me.ActiveDialogContent.Link(Find.ByText("Delete Game"))
    End Function
End Class
