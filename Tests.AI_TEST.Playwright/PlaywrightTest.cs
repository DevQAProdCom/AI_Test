using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace Tests.AI_TEST.Playwright
{
    [TestFixture]
    public class Tests : PageTest
    {
        [Test]
        public async Task GitHubHomePage_PlatformButtonHasExpectedText()
        {
            await Page.GotoAsync("https://github.com/");
            var platformButton = Page.GetByRole(AriaRole.Button, new() { Name = "Platform" });
            await Expect(platformButton).ToHaveTextAsync("Platform");
        }
    }
}