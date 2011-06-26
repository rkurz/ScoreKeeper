
Namespace Rummy.IntegrationTests

    <Binding()> _
    Public Class SharedSteps
        Inherits StepDefinitionBase
        
        <Given("I am on the dashboard page")>
        Public Sub GivenIAmOnTheDashboardPage()
            BrowseTo("/Home/Index")
        End Sub

    End Class

End Namespace
