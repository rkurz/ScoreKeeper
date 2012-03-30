
Namespace Rummy.IntegrationTests

    <Binding()> _
    Public Class ViewScore
        Inherits StepDefinitionBase

        <Given("A game is in progress that requires (.*) points to win")> _
        Public Sub GivenAGameIsInProgress(ByVal pointsRequiredToWin As Integer)
            'Start new game
            BrowseTo("/Home/StartGame")
            WebBrowser.Page(Of StartGamePage).StartNewGame(pointsRequiredToWin.ToString)
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <Given("Kelly has (.*) points and Rob has (.*) points")> _
        Public Sub GivenKellyHasSomePointsAndRobHasSomePoints(ByVal KellyPoints As Integer, ByVal RobPoints As Integer)
            'Go to the add score page
            WebBrowser.Page(Of ViewScorePage).AddScoreLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Add scores
            'WebBrowser.Page(Of AddScorePage).SetScoreForPlayer(1, KellyPoints)
            'WebBrowser.Page(Of AddScorePage).SetScoreForPlayer(2, RobPoints)
            WebBrowser.Page(Of AddScorePage).Points(1).TypeText(KellyPoints.ToString)
            WebBrowser.Page(Of AddScorePage).Points(2).TypeText(RobPoints.ToString)

            'click submit
            WebBrowser.Page(Of AddScorePage).SubmitButton.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("the score for Kelly should read (.*)")>
        Public Sub ThenTheScoreForKellyShouldReadSomething(ByVal expectedScore As String)
            Assert.AreEqual(expectedScore, WebBrowser.Page(Of ViewScorePage).ReadPlayerScore(1))
        End Sub

        <[Then]("the score for Rob should read (.*)")>
        Public Sub ThenTheScoreForRobShouldReadSomething(ByVal expectedScore As String)
            Assert.AreEqual(expectedScore, WebBrowser.Page(Of ViewScorePage).ReadPlayerScore(2))
        End Sub

        <[When]("I click the view details link")>
        Public Sub WhenIClickTheViewDetailsLink()
            WebBrowser.Page(Of ViewScorePage).ViewScoreDetailsLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[When]("I click the add score link")>
        Public Sub WhenIClickTheAddScoreLink()
            WebBrowser.Page(Of ViewScorePage).AddScoreLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("I should be redirected to the View Score Details page")>
        Public Sub ThenIShouldBeRedirectedToTheViewScoreDetailsPage()
            Assert.IsTrue(WebBrowser.Page(Of ViewScoreDetailsPage).IsActivePage)
        End Sub

        <[Then]("I should be redirected to the Add Score page")>
        Public Sub ThenIShouldBeRedirectedToTheAddScorePage()
            Assert.IsTrue(WebBrowser.Page(Of AddScorePage).IsActivePage)
        End Sub

        <[Then]("the page should be in game over mode")>
        Public Sub ThenThePageShouldBeInGameOverMode()
            Assert.IsTrue(WebBrowser.Page(Of ViewScorePage).IsInGameOverMode)
        End Sub

        <[Then]("the page should not be in game over mode")>
        Public Sub ThenThePageShouldNotBeInGameOverMode()
            Assert.IsFalse(WebBrowser.Page(Of ViewScorePage).IsInGameOverMode)
        End Sub
    End Class

End Namespace
