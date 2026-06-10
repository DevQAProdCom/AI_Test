using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace Tests.AI_TEST.Playwright
{
    [TestFixture]
    public class Tests : PageTest
    {
        [Test]
        public async Task GitHubHomePage_ElementHasExpectedText()
        {
            await Page.GotoAsync("https://github.com/");
            var element = Page.GetByLabel("Global").GetByRole(AriaRole.Link, new() { Name = "Pricing" });
            await Expect(element).ToHaveTextAsync("Pricing");
        }
    }
}