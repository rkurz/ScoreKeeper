Public Class EditPlayerPage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/EditPlayer")
    End Sub

    Public ReadOnly Property PlayerNameTextfield As TextField
        Get
            Return Me.ActiveContent.TextField(Find.ById("PlayerName"))
        End Get
    End Property

    Public ReadOnly Property ErrorMessage As Span
        Get
            Return Me.ActiveContent.Span(Find.ByClass("field-validation-error"))
        End Get
    End Property

    Public ReadOnly Property SubmitButton As Button
        Get
            Return Me.ActiveContent.Button(Find.ByValue("Save"))
        End Get
    End Property

    Public ReadOnly Property CancelLink As Link
        Get
            Return Me.ActiveContent.Link(Find.ByText("Cancel"))
        End Get
    End Property
End Class
