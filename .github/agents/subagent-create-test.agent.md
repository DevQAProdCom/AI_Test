---
name: Create Test Agent
description: >
  Creates Playwright Test
model: copilot-default
---

# Subagent: Create Test

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
4. Select  **randomly five stable, visible element** that contains non-dynamic, readable text. Don't select "Sign In". 
5. Save and Record:
   - The **Playwright locator** for that element (e.g. `page.GetByRole(AriaRole.Link, new() { Name = "Element Name" })` or `page.Locator("a.HeaderMenu-link[href='/some_names']")`)
   - The **exact text** the element contains (e.g. `"Element Name"`)
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


