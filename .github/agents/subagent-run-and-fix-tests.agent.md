---
name: Run and Fix Tests
description: >
  Checks out the feature branch produced by the Create Branch and Test subagent,
  installs .NET Playwright browsers, runs dotnet test, and iteratively diagnoses
  and fixes failures until all tests pass (up to 5 iterations).
model: copilot-default
---

# Subagent: Run and Fix Tests

## Context

- Repository: `DevQAProdCom/AI_Test`
- Test project path: `Tests.AI_TEST.Playwright/`
- Test runner command: `dotnet test Tests.AI_TEST.Playwright`
- Browser install command: `pwsh Tests.AI_TEST.Playwright/bin/Debug/net8.0/playwright.ps1 install chromium`
- Maximum fix iterations: **5**

## Input Required

Receive from the orchestrator:
- `branch_name`: the branch created by the previous subagent (e.g. `feature/github-home-test-2026-12-01_14-30-00`)
- `locator_expression`: the Playwright C# locator used in the test
- `expected_text`: the asserted text string

## Step-by-Step Instructions

### Step 1 — Checkout Branch

Ensure the working directory is on `branch_name`. Pull the latest changes.

### Step 2 — Restore and Build

Run the following commands in sequence:
```
dotnet restore Tests.AI_TEST.Playwright
dotnet build Tests.AI_TEST.Playwright --configuration Debug
```

If the build fails, examine the error messages and apply the minimum fix needed:
- Missing `using` directives → add them
- API mismatches (e.g. wrong method signature) → correct the call
- Package version issues → adjust the `PackageReference` version in the `.csproj`

Commit any build fixes to `branch_name`.

### Step 3 — Install Playwright Browsers

After a successful build, install the Chromium browser binary:
```
pwsh Tests.AI_TEST.Playwright/bin/Debug/net8.0/playwright.ps1 install chromium --with-deps
```

If `playwright.ps1` is not present at the above path, try:
```
dotnet tool install --global Microsoft.Playwright.CLI
playwright install chromium --with-deps
```

### Step 4 — Run Tests (Iteration Loop)

Repeat up to **5 times**:

1. Run: `dotnet test Tests.AI_TEST.Playwright --logger "console;verbosity=detailed"`
2. If exit code is **0** → all tests passed. Stop and go to Step 5 (report).
3. If exit code is **non-zero**:
   - Read the test output carefully.
   - Identify the root cause (examples below).
   - Apply a targeted fix to `PlaywrightTest.cs` (or `.csproj` if needed).
   - Commit the fix to `branch_name`.
   - Continue to the next iteration.

**Common failure patterns and fixes:**

| Symptom | Likely Cause | Fix |
|---|---|---|
| `TimeoutError` or element not found | Locator too brittle or page not loaded | Add `await Page.WaitForLoadStateAsync(LoadState.NetworkIdle)` before assertion; consider using `GetByRole` or `GetByText` for resilience |
| Text mismatch | GitHub changed the element text | Update the `expected_text` in the test to the actual current text shown in the error |
| Build error: method not found | Playwright API version mismatch | Adjust method call to match `Microsoft.Playwright.NUnit` v1.44 API |
| `NUnit.Framework` conflict | Wrong base class | Ensure `Tests` extends `PageTest` from `Microsoft.Playwright.NUnit` |

After **5 failed iterations**, stop, do not make further changes, and report the final failure details.

### Step 5 — Report

Return to the orchestrator:
- Final branch name
- Number of fix iterations performed
- Final `dotnet test` exit code
- Summary of any fixes applied
- Whether the tests are now passing
