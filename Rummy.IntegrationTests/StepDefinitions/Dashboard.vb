
Namespace Rummy.IntegrationTests

    <Binding()> _
    Public Class Dashboard
        Inherits StepDefinitionBase

        <Given("I have navigated to the site")>
        Public Sub GivenIHaveNavigatedToTheSite()
            BrowseTo("/Home/Index")
        End Sub

        <[Then]("I should see the dashboard page")>
        Public Sub ThenIShouldSeeTheDashboardPage()

            'NOTE: The page's data-url property is always "" or "/" ... so it never contains the actual url path.
            '       I think this is related to the route setup ... not sure though.
            'Assert.IsTrue(WebBrowser.Page(Of IndexPage).IsActivePage)
            AssertUrl("/Home/Index")
            Assert.IsTrue(WebBrowser.ContainsText("Rummy"), "Could not find text 'Rummy'")
            Assert.IsTrue(WebBrowser.ContainsText("Player"))
            Assert.IsTrue(WebBrowser.ContainsText("Wins"))
            Assert.IsTrue(WebBrowser.ContainsText("Losses"))
        End Sub

        <Given("I am on the dashboard page")>
        Public Sub GivenIAmOnTheDashboardPage()
            BrowseTo("/Home/Index")
        End Sub

        <[When]("I click the new game link")>
        Public Sub WhenIClickTheNewGameLink()
            WebBrowser.Page(Of IndexPage).StartNewGame()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("I should be redirected to the start game page")>
        Public Sub ThenIShouldBeRedirectedToTheStartGamePage()
            Assert.IsTrue(WebBrowser.Page(Of StartGamePage).IsActivePage)
            'Assert.IsTrue(WebBrowser.Page(Of StartGamePage).PointsRequiredToWin.Exists)
        End Sub

        <Given("I have finished a game")>
        Public Sub GivenIHaveFinishedAGame()
            'Start new game
            BrowseTo("/Home/StartGame")
            WebBrowser.Page(Of StartGamePage).StartNewGame("10")
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Go to the add score page
            WebBrowser.Page(Of ViewScorePage).AddScoreLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Add scores
            WebBrowser.Page(Of AddScorePage).Points(1).TypeText("5")
            WebBrowser.Page(Of AddScorePage).Points(2).TypeText("0")

            'click submit
            WebBrowser.Page(Of AddScorePage).SubmitButton.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Go to the add score page
            WebBrowser.Page(Of ViewScorePage).AddScoreLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Add scores
            WebBrowser.Page(Of AddScorePage).Points(1).TypeText("15")
            WebBrowser.Page(Of AddScorePage).Points(2).TypeText("7")

            'click submit
            WebBrowser.Page(Of AddScorePage).SubmitButton.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            
        End Sub

        <Given("I am back on the dashboard page")>
        Public Sub GivenIAmBackOnTheDashboardPage()
            WebBrowser.Page(Of ViewScorePage).StandingsLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub
    End Class

End Namespace
