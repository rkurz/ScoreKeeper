Public Class StartGamePage
    Inherits Page

    Public Function IsActivePage() As Boolean
        Dim containerDiv As Div

        containerDiv = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))
        If containerDiv.GetAttributeValue("data-url").Contains("Home/StartGame") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public ReadOnly Property PointsRequiredToWin As TextField
        Get
            Return Document.TextField(Find.ById("PointsRequiredToWin"))
        End Get
    End Property

    Public ReadOnly Property SubmitButton As Button
        Get
            Return Document.Button(Find.ByValue("Start"))
        End Get
    End Property

    Public ReadOnly Property CancelLink As Link
        Get
            Return Document.Link(Find.ByText("Cancel"))
        End Get
    End Property

    Public Sub StartNewGame(ByVal pointsRequiredToWin As String)
        Me.PointsRequiredToWin.TypeText(pointsRequiredToWin)
        Me.SubmitButton.Click()
    End Sub
End Class
