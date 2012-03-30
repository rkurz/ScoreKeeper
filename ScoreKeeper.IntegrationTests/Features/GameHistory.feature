Feature: Game History Page
	Shows a list of all completed or in-progress games.  Clicking on the header allows a user to delete or continue an in-progress game.

Scenario: Completed game has a blue divider
	Given I have completed a game
	And I am on the dashboard page
	When I click the Game History link
	Then the first game in the list should have a blue divider

Scenario: Incomplete game has yellow divider
    Given I have started a game
    And I am on the dashboard page
    When I click the Game History link
    Then the first game in the list should have a yellow divider

@ignore
Scenario: Deleting a game removes it from the list

Scenario: Resuming a game takes the user to the View Score page
    Given I have started a game
    And I am on the Game History page
    When I click the first game in the list
    And I click the resume game link
    Then I should be taken to the view score page