
Namespace Rummy.IntegrationTests

    <Binding()> _
    Public Class Dashboard
        Inherits StepDefinitionBase

        <Given("I have navigated to the site")>
        Public Sub GivenIHaveNavigatedToTheSite()
            BrowseTo("")
        End Sub

        <[Then]("I should see the dashboard page")>
        Public Sub ThenIShouldSeeTheDashboardPage()
            AssertUrl("/")
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
        End Sub
    End Class

End Namespace
