---
name: Create Branch and Test
description: >
  Creates a dated feature branch from feature/playwright-orchestrator, uses Playwright MCP
  to inspect https://github.com/, captures a stable element locator and its text, adds the
  Microsoft.Playwright.NUnit package, and writes a verified async NUnit Playwright test in
  PlaywrightTest.cs.
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

### Step 1 — Generate Branch Name

Compute the current UTC timestamp in the format `yyyy-MM-dd_hh-mm-ss` (e.g. `2026-12-01_14-30-00`).
Construct the branch name as: `copilot/feature/github-test-{date}` (e.g. `copilot/feature/github-test-2026-12-01_14-30-00`).

### Step 2 — Create Branch via GitHub MCP

Use the `github` MCP server to create the new branch:
- Owner: `DevQAProdCom`
- Repo: `AI_Test`
- New branch name: `copilot/feature/github-test-{date}` (computed above)
- Source branch: `feature/playwright-orchestrator`

Make sure that new branch name starts with `copilot/` to avoid "repository branch creation restriction".

### Step 3 — Inspect GitHub Homepage with Playwright MCP

Use the `playwright` MCP server to:
1. Launch a Chromium browser (headless is fine).
2. Navigate to `https://github.com/`.
3. Wait for the page to be fully loaded.
4. Select  **randomly five stable, visible element** that contains non-dynamic, readable text. Don't select "Sign In". 
5. Save and Record:
   - The **Playwright locator** for that element (e.g. `page.GetByRole(AriaRole.Link, new() { Name = "Element Name" })` or `page.Locator("a.HeaderMenu-link[href='/some_names']")`)
   - The **exact text** the element contains (e.g. `"Element Name"`)
6. Close the browser.

### Step 4 — Add Microsoft.Playwright.NUnit Package

Checkout the new branch locally (or use the file API). Update `Tests.AI_TEST.Playwright/Tests.AI_TEST.Playwright.csproj` to add:

```xml
<PackageReference Include="Microsoft.Playwright.NUnit" Version="1.44.0" />
```

inside the existing `<ItemGroup>` that contains `PackageReference` elements.

### Step 5 — Write the Playwright Test

Replace the contents of `Tests.AI_TEST.Playwright/PlaywrightTest.cs` with the following template, filling in `LOCATOR_EXPRESSION` and `EXPECTED_TEXT` from Step 3. Do it for 1 randomly picked element:

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

### Step 6 — Commit and Push Changes

Use the `github` MCP server to commit both changed files to the new branch:
- Commit message: `feat: add Playwright GitHub homepage test`
- Files to commit:
  - `Tests.AI_TEST.Playwright/Tests.AI_TEST.Playwright.csproj`
  - `Tests.AI_TEST.Playwright/PlaywrightTest.cs`

### Step 7 — Report

Return the following information to the orchestrator:
- Branch name created
- Playwright locator expression used
- Expected text being asserted
- Confirmation that both files were pushed
