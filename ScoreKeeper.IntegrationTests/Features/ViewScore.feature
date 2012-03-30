Feature: ViewScore
	The main page while a game is in progress.  Displays the current score for each player.

Scenario: Display correct score
	Given A game is in progress that requires 500 points to win
    And Kelly has 40 points and Rob has 100 points
	Then the score for Kelly should read 40
    And the score for Rob should read 100
    And the page should not be in game over mode

Scenario: View Details Link
    Given A game is in progress that requires 500 points to win
    When I click the view details link
    Then I should be redirected to the View Score Details page

Scenario: Add Score Link
    Given A game is in progress that requires 500 points to win
    When I click the add score link
    Then I should be redirected to the Add Score page

Scenario: Game Over
    Given A game is in progress that requires 500 points to win
    And Kelly has 40 points and Rob has 550 points
    Then the page should be in game over mode
