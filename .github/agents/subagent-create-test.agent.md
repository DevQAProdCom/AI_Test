---
name: Create Test Agent
description: >
  Creates Playwright Test
model: copilot-default
---

# Subagent: Create Branch and Test

## Context

- Repository: `DevQAProdCom/AI_Test`
- Base branch: `feature/playwright-orchestrator`
- Test project: `Tests.AI_TEST.Playwright/`
- Test file to modify: `Tests.AI_TEST.Playwright/PlaywrightTest.cs`
- Project file: `Tests.AI_TEST.Playwright/Tests.AI_TEST.Playwright.csproj`

## Step-by-Step Instructions

### Step 1 — Inspect GitHub Homepage with Playwright MCP

Use the `playwright` MCP server to:
1. Launch a Chromium browser (headless is fine).
2. Navigate to `https://github.com/`.
3. Wait for the page to be fully loaded.
4. Select **one stable, visible element** that contains non-dynamic, readable text — good candidates include navigation links (e.g. "Sign in", "Features", "Enterprise", "Pricing") or a heading. Avoid elements whose text changes based on login state if possible; "Sign in" is a reliable choice.
5. Record:
   - The **Playwright locator** for that element (e.g. `page.GetByRole(AriaRole.Link, new() { Name = "Sign in" })` or `page.Locator("a.HeaderMenu-link[href='/login']")`)
   - The **exact text** the element contains (e.g. `"Sign in"`)
6. Close the browser.

### Step 2 — Add Microsoft.Playwright.NUnit Package

Checkout the new branch locally (or use the file API). Update `Tests.AI_TEST.Playwright/Tests.AI_TEST.Playwright.csproj` to add:

```xml
<PackageReference Include="Microsoft.Playwright.NUnit" Version="1.44.0" />
```

inside the existing `<ItemGroup>` that contains `PackageReference` elements.

### Step 3 — Write the Playwright Test

Replace the contents of `Tests.AI_TEST.Playwright/PlaywrightTest.cs` with the following template, filling in `LOCATOR_EXPRESSION` and `EXPECTED_TEXT` from Step 1:

```csharp
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
            var element = LOCATOR_EXPRESSION;
            await Expect(element).ToHaveTextAsync("EXPECTED_TEXT");
        }
    }
}
```

Replace:
- `LOCATOR_EXPRESSION` → the C# Playwright locator expression from Step 1 (using `Page.`)
- `"EXPECTED_TEXT"` → the exact text string from Step 1


