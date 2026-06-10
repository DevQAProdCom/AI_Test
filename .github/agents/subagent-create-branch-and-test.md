---
name: Create Branch and Test
description: >
  Creates a dated feature branch from feature/playwright-orchestrator, uses Playwright MCP
  to inspect https://github.com/, captures a stable element locator and its text, adds the
  Microsoft.Playwright.NUnit package, and writes a verified async NUnit Playwright test in
  PlaywrightTest.cs.
model: copilot-default
tools:
  - github
  - playwright
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
Construct the branch name as: `feature/github-home-test-{date}` (e.g. `feature/github-home-test-2026-12-01_14-30-00`).

### Step 2 — Create Branch via GitHub MCP

Use the `github` MCP server to create the new branch:
- Owner: `DevQAProdCom`
- Repo: `AI_Test`
- New branch name: `feature/github-home-test-{date}` (computed above)
- Source branch: `feature/playwright-orchestrator`

### Step 3 — Inspect GitHub Homepage with Playwright MCP

Use the `playwright` MCP server to:
1. Launch a Chromium browser (headless is fine).
2. Navigate to `https://github.com/`.
3. Wait for the page to be fully loaded.
4. Select **one stable, visible element** that contains non-dynamic, readable text — good candidates include navigation links (e.g. "Sign in", "Features", "Enterprise", "Pricing") or a heading. Avoid elements whose text changes based on login state if possible; "Sign in" is a reliable choice.
5. Record:
   - The **Playwright locator** for that element (e.g. `page.GetByRole(AriaRole.Link, new() { Name = "Sign in" })` or `page.Locator("a.HeaderMenu-link[href='/login']")`)
   - The **exact text** the element contains (e.g. `"Sign in"`)
6. Close the browser.

### Step 4 — Add Microsoft.Playwright.NUnit Package

Checkout the new branch locally (or use the file API). Update `Tests.AI_TEST.Playwright/Tests.AI_TEST.Playwright.csproj` to add:

```xml
<PackageReference Include="Microsoft.Playwright.NUnit" Version="1.44.0" />
```

inside the existing `<ItemGroup>` that contains `PackageReference` elements.

### Step 5 — Write the Playwright Test

Replace the contents of `Tests.AI_TEST.Playwright/PlaywrightTest.cs` with the following template, filling in `LOCATOR_EXPRESSION` and `EXPECTED_TEXT` from Step 3:

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
- `LOCATOR_EXPRESSION` → the C# Playwright locator expression from Step 3 (using `Page.`)
- `"EXPECTED_TEXT"` → the exact text string from Step 3

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
