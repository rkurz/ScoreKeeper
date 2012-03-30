
Namespace Rummy.IntegrationTests

    <Binding()> _
    Public Class EditPlayer
        Inherits StepDefinitionBase

        'TODO - If we run the integration tests against an empty db (which we should) then this step won't be needed.
        <TearDown()>
        Public Sub TearDown()
            Dim db As RummyEntities
            Dim oc As ObjectContextAdapter
            Dim playerService As PlayerService
            Dim players As List(Of Player)
            Dim targetPlayer As Player

            db = New RummyEntities
            oc = New ObjectContextAdapter(db)

            playerService = New PlayerService(oc)
            players = playerService.FindAll

            targetPlayer = players.Where(Function(p) p.Name = "Eric")
            If targetPlayer IsNot Nothing Then
                playerService.DeletePlayer(targetPlayer)
            End If
            targetPlayer = players.Where(Function(p) p.Name = "Stan")
            If targetPlayer IsNot Nothing Then
                playerService.DeletePlayer(targetPlayer)
            End If
        End Sub

        <Given("I am on the edit player page")>
        Public Sub GivenIAmOnTheEditPlayerPage()
            BrowseTo("/Home/EditPlayer")
        End Sub

        <Given("I have entered (.*) into the name field")>
        Public Sub GivenIHaveEnteredSomethingIntoTheNameField(ByVal name As String)
            WebBrowser.Page(Of EditPlayerPage).PlayerNameTextfield.TypeText(name)
        End Sub

        <[When]("I press the Save button")>
        Public Sub WhenIPressTheSaveButton()
            WebBrowser.Page(Of EditPlayerPage).SubmitButton.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[When]("I click the Cancel link")>
        Public Sub WhenIClickTheCancelLink()
            WebBrowser.Page(Of EditPlayerPage).CancelLink.Click()
            System.Threading.Thread.Sleep(2000)
            WebBrowser.WaitForComplete()
        End Sub

        <[Then]("I will be redirected to the Start Game page")>
        Public Sub ThenIWillBeRedirectedToTheStartGamePage()
            Assert.IsTrue(WebBrowser.Page(Of StartGamePage).IsActivePage)
        End Sub

        <[Then]("(.*) will be in the list of players")>
        Public Sub ThenSomethingWillBeInTheListOfPlayers(ByVal name As String)
            Assert.IsTrue(WebBrowser.Page(Of StartGamePage).PlayerLabel(name).Exists)
        End Sub

        <[Then]("(.*) will not be in the list of players")>
        Public Sub ThenSomethingWillNotBeInTheListOfPlayers(ByVal name As String)
            Assert.IsFalse(WebBrowser.Page(Of StartGamePage).PlayerLabel(name).Exists)
        End Sub

        <[Then]("I will remain on the edit player page")>
        Public Sub ThenIWillRemainOnTheEditPlayerPage()
            Assert.IsTrue(WebBrowser.Page(Of EditPlayerPage).IsActivePage)
        End Sub

        <[Then]("I will be shown an error message")>
        Public Sub ThenIWillBeShownAnErrorMessage()
            Assert.IsTrue(WebBrowser.Page(Of EditPlayerPage).ErrorMessage.Exists)
        End Sub
    End Class

End Namespace
