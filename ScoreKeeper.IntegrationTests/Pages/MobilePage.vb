Public Class MobilePage
    Inherits Page

    Public Sub New(ByVal url As String)
        _actualUrl = url
    End Sub

    Private _actualUrl As String
    ''' <summary>
    ''' Contains the url path for this page.  Jquery mobile pages can be combined (ie/ dynamically loaded via ajax) so the 
    ''' url in the browser may not match the url required to navigate to this page.
    ''' </summary>
    Public ReadOnly Property ActualUrl As String
        Get
            Return _actualUrl
        End Get
    End Property

    Public ReadOnly Property IsActivePage As Boolean
        Get
            'TODO - This should probably check exact url value instead of just if it's "contained".
            If Me.ActiveContent.GetAttributeValue("data-url").Contains(Me.ActualUrl) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property ActiveContent As Div
        Get
            Return Document.Div(Find.ByClass("ui-page ui-body-c ui-page-active"))
        End Get
    End Property

    ''' <summary>
    ''' Gets the content area for a page opened as a dialog window.
    ''' </summary>
    ''' <remarks>
    ''' We could get rid of this if can "find" the div using only part of the class (ie/ a div that contains "ui-page-active")
    ''' </remarks>
    Public ReadOnly Property ActiveDialogContent As Div
        Get
            Return Document.Div(Find.ByClass("ui-dialog ui-page ui-body-a ui-page-active"))
        End Get
    End Property
End Class
