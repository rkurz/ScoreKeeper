Public Class AddScorePage
    Inherits Page

    Public Function IsActivePage() As Boolean
        Dim containerDiv As Div

        containerDiv = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))
        If containerDiv.GetAttributeValue("data-url").Contains("Home/AddScore") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public ReadOnly Property Points(ByVal playerId As Integer) As NumericTextField
        Get
            Return Document.ElementOfType(Of NumericTextField)(Find.ById(String.Format("txtPlayer_{0:d}", playerId)))
        End Get
    End Property
    'Public ReadOnly Property Points(ByVal playerId As Integer) As TextField
    '    Get
    '        'Return Document.Element(Find.ById(String.Format("txtPlayer_{0:d}", playerId))).As(Of TextField)()
    '        'Return DirectCast(Document.Element(Find.ById(String.Format("txtPlayer_{0:d}", playerId))), TextField)
    '        'Return Document.TextField(Find.ById(String.Format("txtPlayer_{0:d}", playerId)))
    '    End Get
    'End Property

    Public ReadOnly Property SubmitButton As Button
        Get
            Return Document.Button(Find.ByValue("Submit"))
        End Get
    End Property

End Class
