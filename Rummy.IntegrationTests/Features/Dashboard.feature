Feature: Dashboard Page
	The dashboard page is the initial page users see when they navigate to the site.


Scenario: Navigating to site
	Given I have navigated to the site
	Then I should see the dashboard page

Scenario: Starting a new game
    Given I am on the dashboard page
    When I click the new game link
    Then I should be redirected to the start game page