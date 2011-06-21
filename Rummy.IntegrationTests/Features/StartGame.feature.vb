'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by SpecFlow (http://www.specflow.org/).
'     SpecFlow Version:1.4.0.0
'     Runtime Version:4.0.30319.225
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
#Region "Designer generated code"
Imports TechTalk.SpecFlow

Namespace Rummy.IntegrationTests.Features
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.4.0.0"),  _
     System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     NUnit.Framework.TestFixtureAttribute(),  _
     NUnit.Framework.DescriptionAttribute("StartGame")>  _
    Partial Public Class StartGameFeature
        
        Private Shared testRunner As TechTalk.SpecFlow.ITestRunner
        
        <NUnit.Framework.TestFixtureSetUpAttribute()>  _
        Public Overridable Sub FeatureSetup()
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner
            Dim featureInfo As TechTalk.SpecFlow.FeatureInfo = New TechTalk.SpecFlow.FeatureInfo(New System.Globalization.CultureInfo("en-US"), "StartGame", "The StartGame page allows the user to set the number of points required to win th"& _ 
                    "e game.", GenerationTargetLanguage.VB, CType(Nothing,String()))
            testRunner.OnFeatureStart(featureInfo)
        End Sub
        
        <NUnit.Framework.TestFixtureTearDownAttribute()>  _
        Public Overridable Sub FeatureTearDown()
            testRunner.OnFeatureEnd
            testRunner = Nothing
        End Sub
        
        Public Overridable Sub ScenarioSetup(ByVal scenarioInfo As TechTalk.SpecFlow.ScenarioInfo)
            testRunner.OnScenarioStart(scenarioInfo)
        End Sub
        
        <NUnit.Framework.TearDownAttribute()>  _
        Public Overridable Sub ScenarioTearDown()
            testRunner.OnScenarioEnd
        End Sub
        
        <NUnit.Framework.TestAttribute(),  _
         NUnit.Framework.DescriptionAttribute("Start a game with a valid point requirement")>  _
        Public Overridable Sub StartAGameWithAValidPointRequirement()
            Dim scenarioInfo As TechTalk.SpecFlow.ScenarioInfo = New TechTalk.SpecFlow.ScenarioInfo("Start a game with a valid point requirement", CType(Nothing,String()))
            Me.ScenarioSetup(scenarioInfo)
            testRunner.Given("I am on the start game page")
            testRunner.And("I have entered 500 into the points required to win field")
            testRunner.When("I press the Start button")
            testRunner.Then("I will be redirected to the view score page")
            testRunner.CollectScenarioErrors
        End Sub
        
        <NUnit.Framework.TestAttribute(),  _
         NUnit.Framework.DescriptionAttribute("Start a game with a negative point requirement returns an error message")>  _
        Public Overridable Sub StartAGameWithANegativePointRequirementReturnsAnErrorMessage()
            Dim scenarioInfo As TechTalk.SpecFlow.ScenarioInfo = New TechTalk.SpecFlow.ScenarioInfo("Start a game with a negative point requirement returns an error message", CType(Nothing,String()))
            Me.ScenarioSetup(scenarioInfo)
            testRunner.Given("I am on the start game page")
            testRunner.And("I have entered -10 into the points required to win field")
            testRunner.When("I press the Start button")
            testRunner.Then("I will remain on the start game page")
            testRunner.And("I will be shown an error message indicating an invalid point value")
            testRunner.CollectScenarioErrors
        End Sub
        
        <NUnit.Framework.TestAttribute(),  _
         NUnit.Framework.DescriptionAttribute("Cancel a game before it starts")>  _
        Public Overridable Sub CancelAGameBeforeItStarts()
            Dim scenarioInfo As TechTalk.SpecFlow.ScenarioInfo = New TechTalk.SpecFlow.ScenarioInfo("Cancel a game before it starts", CType(Nothing,String()))
            Me.ScenarioSetup(scenarioInfo)
            testRunner.Given("I am on the start game page")
            testRunner.And("I have entered 20 into the points required to win field")
            testRunner.When("I press the Cancel button")
            testRunner.Then("I will be redirected to the dashboard")
            testRunner.CollectScenarioErrors
        End Sub
    End Class
End Namespace
#End Region
