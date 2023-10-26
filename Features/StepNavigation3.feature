Feature: Validate 404 page

Scenario: Open non-existent page on google.com
	Given I open the browser
	When I navigate to "https://www.google.com/nonexistentpage"
	Then I should see the text "404 Not Found"