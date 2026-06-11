using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace Tests.AI_TEST.Playwright
{
    [TestFixture]
    public class Tests : PageTest
    {
        [Test]
        public async Task GitHubHomePage_SignUpLinkExists()
        {
            await Page.GotoAsync("https://github.com/");
            var element = Page.GetByRole(AriaRole.Link, new() { Name = "Sign up" });
            await Expect(element).ToHaveTextAsync("Sign up");
        }
    }
}