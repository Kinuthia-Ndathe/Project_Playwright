Feature: Search on Google

Scenario: Search for OpenAI on Google
	Given I open the browser
	When I navigate to "https://www.google.com"
	And I type "OpenAI" into the search box and press Enter
	Then I should see "OpenAI" in the search box