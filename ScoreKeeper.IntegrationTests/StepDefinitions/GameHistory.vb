
Namespace Rummy.IntegrationTests

    <Binding()> _
    Public Class GameHistory
        Inherits StepDefinitionBase

        <Given("I have completed a game")>
        Public Sub GivenIHaveCompletedAGame()
            'Start Game
            BrowseTo("/Home/StartGame")
            WebBrowser.Page(Of StartGamePage).StartNewGame("50")
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Go to the add score page
            WebBrowser.Page(Of ViewScorePage).AddScoreLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()

            'Add scores
            WebBrowser.Page(Of AddScorePage).Points(1).TypeText("55")
            WebBrowser.Page(Of AddScorePage).Points(2).TypeText("40")

            'click submit
            WebBrowser.Page(Of AddScorePage).SubmitButton.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[When]("I click the Game History link")>
        Public Sub WhenIClickTheNewGameLink()
            WebBrowser.Page(Of IndexPage).GameHistoryLink.Click()
            System.Threading.Thread.Sleep(3000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("the first game in the list should have a blue divider")>
        Public Sub ThenTheFirstGameInTheListShouldHaveABlueDivider()
            Assert.AreEqual("b", WebBrowser.Page(Of GameHistoryPage).FindFirstGame.ListItem(Find.First).GetAttributeValue("data-theme"))
        End Sub

        <[Then]("the first game in the list should have a yellow divider")>
        Public Sub ThenTheFirstGameInTheListShouldHaveAYellowDivider()
            Assert.AreEqual("e", WebBrowser.Page(Of GameHistoryPage).FindFirstGame.ListItem(Find.First).GetAttributeValue("data-theme"))
        End Sub

        <Given("I have started a game")>
        Public Sub GivenIHaveStartedAGame()
            'Start Game
            BrowseTo("/Home/StartGame")
            WebBrowser.Page(Of StartGamePage).StartNewGame("50")
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <Given("I am on the Game History page")>
        Public Sub GivenIAmOnTheGameHistoryPage()
            'Start Game
            BrowseTo("/Home/GameHistory")
            System.Threading.Thread.Sleep(3000)
            WebBrowser.WaitForComplete()
        End Sub

        <[When]("I click the first game in the list")>
        Public Sub WhenIClickTheFirstGameInTheList()
            WebBrowser.Page(Of GameHistoryPage).FindFirstGame.Link(Find.First).Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[When]("I click the resume game link")>
        Public Sub WhenIClickTheResumeGameLink()
            WebBrowser.Page(Of ManageGamePage).ResumeGameLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("I should be taken to the view score page")>
        Public Sub ThenIShouldBeTakenToTheViewScorePage()
            Assert.IsTrue(WebBrowser.Page(Of ViewScorePage).IsActivePage)
        End Sub
    End Class

End Namespace
