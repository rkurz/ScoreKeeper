Feature: StartGame
	The StartGame page allows the user to set the number of points required to win the game.

Scenario: Start a game with a valid point requirement
	Given I am on the start game page
	And I have entered 500 into the points required to win field
	When I press the Start button
	Then I will be redirected to the view score page

@ignore
Scenario: Start a game with a negative point requirement returns an error message
    Given I am on the start game page
	And I have entered -10 into the points required to win field
	When I press the Start button
	Then I will be shown an error message indicating an invalid point value

Scenario: Cancel a game before it starts
    Given I am on the start game page
	And I have entered 20 into the points required to win field
	When I press the Cancel button
	Then I will be redirected to the dashboard