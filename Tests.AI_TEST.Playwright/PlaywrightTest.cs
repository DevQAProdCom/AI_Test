using Microsoft.Playwright;
using NUnit.Framework;

namespace Tests.AI_TEST.Playwright
{
    [TestFixture]
    public class Tests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new()
            {
                Headless = true
            });
        }

        [SetUp]
        public async Task SetupAsync()
        {
            // Create context with ignoreHTTPSErrors
            _context = await _browser.NewContextAsync(new()
            {
                IgnoreHTTPSErrors = true
            });
            _page = await _context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            if (_page != null)
                await _page.CloseAsync();
            if (_context != null)
                await _context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync()
        {
            if (_browser != null)
                await _browser.CloseAsync();
            if (_playwright != null)
                _playwright.Dispose();
        }

        [Test]
        public async Task GitHubHomePage_SignUpLinkExists()
        {
            await _page.GotoAsync("https://github.com/", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            
            var element = _page.GetByRole(AriaRole.Link, new() { Name = "Sign up" });
            await Assertions.Expect(element).ToHaveTextAsync("Sign up");
        }
    }
}