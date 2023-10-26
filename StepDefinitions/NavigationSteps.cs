using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using Xunit;

/*
class Program
{
    public static async Task Main()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        await page.GotoAsync("https://www.google.com/");

        await page.GetByRole(AriaRole.Button, new() { Name = "Reject all" }).ClickAsync();

        await page.GetByLabel("Search", new() { Exact = true }).ClickAsync();

        await page.GetByLabel("Search", new() { Exact = true }).PressAsync("CapsLock");

        await page.GetByLabel("Search", new() { Exact = true }).FillAsync("S");

        await page.GetByLabel("Search", new() { Exact = true }).PressAsync("CapsLock");

        await page.GetByLabel("Search", new() { Exact = true }).FillAsync("Superman");

    }
}
*/
[Binding]
public class NavigationSteps
{
    private IPage? _page; 
    /*
    - IPage? represents a single tab or window  within a browser. In terms of navigation, the IPage helps in reverting to the browser's history.
    - It provides methods to navigate to URLs, reloads the current page and go back/forward in browser's history
    - '?' indicates that the _page is a nullable reference, meaning it can be either an instance of IPage or null
    - INTERACTION: IPage also allows you to interact with the contents of the webpage e.g. clicking on elements, typing into input, submitting forms
    - EVALUATION: you can execute Javascript within the context of the page which can be used in scrapping data, manipulating page's state or performing checks.
    */

    [Given("I open the browser")]
    public async Task GivenIOpenTheBrowser()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {  Headless = false});
        _page = await browser.NewPageAsync();
    }

    //[When(@"I navigate to ""(.*)""")] //The '.*' is used as a placeholder. It is used for passing Dynamic Values.
    [When(@"I navigate to Google")]
    public async Task WhenINavigateToGoogle() //(string url)
    {
        await _page.GotoAsync("https://www.google.com");
    }

    //[When(@"I type ""(.*)"" into the search box and press Enter")]
    [When("I search for Superman")]
    public async Task WhenISearchForSuperman()//WhenITyoeIntoTheSearchBoxAndPressEnter(string searchText)
    {
        var searchBox = await _page.WaitForSelectorAsync("input [name=q]");
        await searchBox.TypeAsync("Superman");
        await searchBox.PressAsync("Enter");
        /*
        The method '_page.WaitForSelectorAsync' used to input the text in the search box
        This method allows the automation script to pause and wait until a specified element becomes available on the page.
        - In line 35 above, the 'WaitForSelectorAsync' method is used to wait for an input element with the attribute name set to 'q' to appear on the page.
        - Once it is available, a handle to that element is stored in the searchBox variable, which can then be used for further interactions
        */
    }
    [When(@"I open the first search results")]
    public async Task WhenIOpenTheFirstSearchResults()
    {
        await _page.WaitForTimeoutAsync(2000); //wait for 2 seconds for search results to load
        var firstResult = await _page.WaitForSelectorAsync("h3"); //Usually, the title of the first results
        await firstResult.ClickAsync();
    }

    [Then(@"I should see the title ""(.*)""")]
    public async Task ThenIShouldSeeTheTitle(string expectedTitle)
    {
        var actualTitle = await _page.TitleAsync();
        Assert.Equal(expectedTitle, actualTitle);
    }
    
    /*
    [Then(@"I should see ""(.*)"" in the search results")]
    public async Task ThenIShouldSeeInTheSearchResults(string expectedText)
    {
        await _page.WaitForTimeoutAsync(2000); //Wait for 2 seconds for the search results to load 
        var content = await _page.TextContentAsync("body");
        Assert.Contains(expectedText, content);
    }
    */

    [Then(@"I should see the text ""(.*)""")]
    public async Task ThenIShouldSeeTheText(string expectedText)
    {
        var content = await _page.TextContentAsync("body");
        Assert.Contains(expectedText, content);
    }
}