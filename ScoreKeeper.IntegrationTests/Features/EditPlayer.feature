Feature: Edit Player
	The Edit Player page allows the user to add new players or edit existing players

Scenario: Add new player 
	Given I am on the edit player page
	And I have entered Eric into the name field
	When I press the Save button
    Then Eric will be in the list of players

Scenario: Not providing a name value results in error message
    Given I am on the edit player page
    When I press the Save button
    Then I will remain on the edit player page
    And I will be shown an error message

Scenario: Clicking Cancel button does not save player
    Given I am on the edit player page
    And I have entered Stan into the name field
    When I click the Cancel link
    Then I will be redirected to the Start Game page
    And Stan will not be in the list of players