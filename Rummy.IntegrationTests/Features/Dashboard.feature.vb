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
     NUnit.Framework.DescriptionAttribute("Dashboard Page")>  _
    Partial Public Class DashboardPageFeature
        
        Private Shared testRunner As TechTalk.SpecFlow.ITestRunner
        
        <NUnit.Framework.TestFixtureSetUpAttribute()>  _
        Public Overridable Sub FeatureSetup()
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner
            Dim featureInfo As TechTalk.SpecFlow.FeatureInfo = New TechTalk.SpecFlow.FeatureInfo(New System.Globalization.CultureInfo("en-US"), "Dashboard Page", "The dashboard page is the initial page users see when they navigate to the site.", GenerationTargetLanguage.VB, CType(Nothing,String()))
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
         NUnit.Framework.DescriptionAttribute("Navigating to site")>  _
        Public Overridable Sub NavigatingToSite()
            Dim scenarioInfo As TechTalk.SpecFlow.ScenarioInfo = New TechTalk.SpecFlow.ScenarioInfo("Navigating to site", CType(Nothing,String()))
            Me.ScenarioSetup(scenarioInfo)
            testRunner.Given("I have navigated to the site")
            testRunner.Then("I should see the dashboard page")
            testRunner.CollectScenarioErrors
        End Sub
        
        <NUnit.Framework.TestAttribute(),  _
         NUnit.Framework.DescriptionAttribute("Starting a new game")>  _
        Public Overridable Sub StartingANewGame()
            Dim scenarioInfo As TechTalk.SpecFlow.ScenarioInfo = New TechTalk.SpecFlow.ScenarioInfo("Starting a new game", CType(Nothing,String()))
            Me.ScenarioSetup(scenarioInfo)
            testRunner.Given("I am on the dashboard page")
            testRunner.When("I click the new game link")
            testRunner.Then("I should be redirected to the start game page")
            testRunner.CollectScenarioErrors
        End Sub
    End Class
End Namespace
#End Region
