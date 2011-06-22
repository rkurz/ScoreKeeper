Public Class AddScorePage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/AddScore")
    End Sub

    Public ReadOnly Property Points(ByVal playerId As Integer) As NumericTextField
        Get
            Return Me.ActiveContent.ElementOfType(Of NumericTextField)(Find.ById(String.Format("txtPlayer_{0:d}", playerId)))
        End Get
    End Property

    Public ReadOnly Property SubmitButton As Button
        Get
            Return Me.ActiveContent.Button(Find.ByValue("Submit"))
        End Get
    End Property

End Class
