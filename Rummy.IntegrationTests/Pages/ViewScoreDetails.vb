Public Class ViewScoreDetailsPage
    Inherits Page

    Public Function IsActivePage() As Boolean
        Dim containerDiv As Div

        containerDiv = Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))
        If containerDiv.GetAttributeValue("data-url").Contains("Home/ViewScoreDetails") Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
