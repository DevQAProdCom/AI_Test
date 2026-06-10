---
name: Create Branch
description: >
  Creates a dated feature branch from feature/playwright-orchestrator, uses Playwright MCP
  to inspect https://github.com/, captures a stable element locator and its text, adds the
  Microsoft.Playwright.NUnit package, and writes a verified async NUnit Playwright test in
  PlaywrightTest.cs.
model: copilot-default
tools:
  - github
---

# Subagent: Create Branch

## Context

- Repository: `DevQAProdCom/AI_Test`
- Base branch: `feature/playwright-orchestrator`
- Test project: `Tests.AI_TEST.Playwright/`
- Test file to modify: `Tests.AI_TEST.Playwright/PlaywrightTest.cs`
- Project file: `Tests.AI_TEST.Playwright/Tests.AI_TEST.Playwright.csproj`

## Step-by-Step Instructions
fhome
### Step 1 — Generate Branch Name

Compute the current UTC timestamp in the format `yyyy-MM-dd_hh-mm-ss` (e.g. `2026-12-01_14-30-00`).
Construct the branch name as: `feature/github-test-{date}` (e.g. `feature/github-test-2026-12-01_14-30-00`).

### Step 2 — Create Branch via GitHub MCP

Use the `github` MCP server to create the new branch:
- Owner: `DevQAProdCom`
- Repo: `AI_Test`
- New branch name: `feature/github-test-{date}` (computed above)
- Source branch: `feature/playwright-orchestrator`

### Step 3 — Push Changes
Use the `github` MCP server to switch to the new branch
Use the `github` MCP server to push new branch to remote repo

### Step 4 — Report

Return the following information to the orchestrator:
- Branch name created

