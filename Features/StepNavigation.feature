Feature: Navigate to Google

Scenario: Open Google Homepage
	Given I open the browser
	When I navigate to "https://www.google.com"
	Then I should see the title "Google"