Public Class StartGamePage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/StartGame")
    End Sub

    Public ReadOnly Property PointsRequiredToWin As NumericTextField
        Get
            Return Me.ActiveContent.ElementOfType(Of NumericTextField)(Find.ById("PointsRequiredToWin"))
        End Get
    End Property

    Public ReadOnly Property SubmitButton As Button
        Get
            Return Me.ActiveContent.Button(Find.ByValue("Start"))
        End Get
    End Property

    Public ReadOnly Property CancelLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("Cancel"))
        End Get
    End Property

    Public ReadOnly Property ErrorMessage As Span
        Get
            Return Me.ActiveContent.Span(Find.ByClass("field-validation-error"))
        End Get
    End Property

    Public Sub StartNewGame(ByVal pointsRequiredToWin As String)
        Me.PointsRequiredToWin.TypeText(pointsRequiredToWin)
        Me.SubmitButton.Click()
    End Sub
End Class
