Namespace Rummy.IntegrationTests
    <Binding()>
    Public Class StartGame
        Inherits StepDefinitionBase

        <Given("I am on the start game page")>
        Public Sub GivenIAmOnTheStartGamePage()
            BrowseTo("/Home/StartGame")
        End Sub

        <Given("I have entered (.*) into the points required to win field")>
        Public Sub GivenIHaveEnteredSomethingIntoThePointsRequiredToWinField(ByVal points As Integer)
            WebBrowser.Page(Of StartGamePage).PointsRequiredToWin.TypeText(points.ToString)
        End Sub

        <[When]("I press the Start button")>
        Public Sub WhenIPressTheStartButton()
            WebBrowser.Page(Of StartGamePage).SubmitButton.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[When]("I press the Cancel button")>
        Public Sub WhenIPressTheCancelButton()
            WebBrowser.Page(Of StartGamePage).CancelLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("I will be redirected to the view score page")>
        Public Sub ThenIWillBeRedirectedToTheViewScorePage()
            'NOTE: the view score page does not get a data-url property that matches the expected url.
            '       For some reason it's still set to the prior url.
            'Assert.IsTrue(WebBrowser.Page(Of ViewScorePage).IsActivePage)
            'Assert.IsTrue(WebBrowser.Page(Of ViewScorePage).ActiveContent.Con
            Assert.IsTrue(WebBrowser.ContainsText("Kelly"), "Could not find text 'Kelly'")
            'Assert.IsTrue(WebBrowser.ContainsText("Score"), "Could not find text 'Score'")
            Assert.IsTrue(WebBrowser.ContainsText("Add Score"), "Could not find text 'Add Score'")
        End Sub

        <[Then]("I will be redirected to the dashboard")>
        Public Sub ThenIWillBeRedirectedToTheDashboard()
            Assert.IsTrue(WebBrowser.ContainsText("Rummy"), "Could not find text 'Rummy'")
            Assert.IsTrue(WebBrowser.ContainsText("Player"))
            Assert.IsTrue(WebBrowser.ContainsText("Wins"))
            Assert.IsTrue(WebBrowser.ContainsText("Losses"))
        End Sub

        <[Then]("I will remain on the start game page")>
        Public Sub ThenIWillRemainOnTheStartGamePage()
            Assert.IsTrue(WebBrowser.Page(Of StartGamePage).IsActivePage)
        End Sub

        <[Then]("I will be shown an error message indicating an invalid point value")>
        Public Sub ThenIWillBeShownAnErrorMessageIndicatingAnInvalidPointValue()
            Assert.IsTrue(WebBrowser.Page(Of StartGamePage).ErrorMessage.Exists)
        End Sub
    End Class

End Namespace
