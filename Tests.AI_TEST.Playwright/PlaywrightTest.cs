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
            var element = Page.GetByRole(AriaRole.Link, new() { Name = "GitHub" });
            await Expect(element).ToHaveTextAsync("GitHub");
        }
    }
}