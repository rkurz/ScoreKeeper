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

    Public ReadOnly Property PlayerCheckbox(ByVal playerId As Integer) As CheckBox
        Get
            Return Me.ActiveContent.CheckBox(Find.ById(String.Format("cbPlayer_{0:d}", playerId)))
        End Get
    End Property

    Public ReadOnly Property PlayerLabel(ByVal playerName As String) As Label
        Get
            Return Me.ActiveContent.Label(Find.ByText(playerName))
        End Get
    End Property

    Public Sub StartNewGame(ByVal pointsRequiredToWin As String)
        Me.PointsRequiredToWin.TypeText(pointsRequiredToWin)
        Me.PlayerCheckbox(1).Click()
        Me.PlayerCheckbox(2).Click()
        Me.SubmitButton.Click()
    End Sub
End Class
