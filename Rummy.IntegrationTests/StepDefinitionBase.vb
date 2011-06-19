
Public Class StepDefinitionBase
    Public Const BaseUrl As String = "http://localhost:49240"

    Public Shared ReadOnly Property WebBrowser As Browser
        Get
            'WatiN.Core.Settings.WaitUntilExistsTimeOut = 120 'Wait for 120 seconds to see if controls exist.
            If Not ScenarioContext.Current.ContainsKey("browser") Then
                Dim browser As IE = New IE
                ScenarioContext.Current("browser") = browser
                browser.AutoClose = True
                browser.Visible = False
            End If
            Return DirectCast(ScenarioContext.Current("browser"), Browser)
        End Get
    End Property

    Public Shared Sub BrowseTo(ByVal relativeUrl As String)
        WebBrowser.GoTo(BaseUrl & relativeUrl)
        WebBrowser.WaitForComplete()
    End Sub

    Public Shared Sub AssertUrl(ByVal relativeUrl As String)
        Assert.AreEqual(BaseUrl & relativeUrl, WebBrowser.Url)
    End Sub
End Class
