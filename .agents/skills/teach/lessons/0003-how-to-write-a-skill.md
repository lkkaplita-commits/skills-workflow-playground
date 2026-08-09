# Lesson 3: Writing Your Own Skill

## The Anatomy of a Skill

A skill is one file: `SKILL.md`. That's it. But the file has two distinct parts, each with a job:

### Part 1: The Frontmatter (the index)

```yaml
---
name: my-skill
description: What this skill does and when to invoke it.
argument-hint: "What the user should provide"
disable-model-invocation: true
---
```

The frontmatter is the skill's **business card**. It answers three questions for the agent router:

| Field | Purpose |
|---|---|
| `name` | Unique identifier. Used in routing and cross-references. |
| `description` | **What it does AND when to use it.** This is the most important field — it determines whether the skill fires for a given user request. |
| `argument-hint` | What the user should supply when invoking. Guides the user. |
| `disable-model-invocation` | Optional. Set to `true` when the skill is purely informational (a reference) rather than a workflow to run. |

**The description is the skill's trigger.** It needs to cover both the *what* and the *when*. Compare:

- ❌ `"Help with testing"` — too vague, fires on everything about tests
- ✅ `"Test-driven development. Use when the user wants to build features or fix bugs test-first, mentions 'red-green-refactor', or wants integration tests."` — precise, fires on specific patterns

### Part 2: The Body (the instructions)

Everything after the frontmatter is the skill's **instruction text**. The LLM reads this when the skill fires. It's not a specification for humans — it's a script for the agent to follow.

Here's the structure that works:

```markdown
# Skill Name

One-line summary of what the skill does.

## Goal

What the skill produces or achieves.

## Process

### Step 1: Do this
Instructions for the first action.

### Step 2: Then this
Instructions for the next action.

## Rules

- Rule 1
- Rule 2

## Output Format

How the result should look.
```

## Writing Skills: A Live Example

Let's build a skill together. Imagine you want a skill that helps you **create a new feature branch with a ticket**.

### Step 1: The Frontmatter

```yaml
---
name: new-feature
description: Create a new feature branch linked to a tracker item. Use when the user wants to start work on a new feature, task, or bug fix.
argument-hint: "The feature or task name"
---
```

### Step 2: The Body

```markdown
# New Feature Branch

Create a new feature branch linked to a tracker item.

## Process

### 1. Confirm the tracker item

Ask the user for the ticket/issue number or description if not provided.

### 2. Create the branch

Run `git checkout -b feature/<ticket>/<short-description>` using a dash-case name derived from the ticket.

### 3. Create the scaffold

Create `src/<feature>/` with:
- `README.md` — one-line description
- `index.ts` — placeholder export

### 4. Commit

Run `git add .` and `git commit -m "feat: scaffold <feature>"`.

## Rules

- Branch names must be dash-case and under 50 characters
- Never push to `main` or `develop`
- Always ask for confirmation before pushing

## Output

Show the branch name, commit hash, and a link to the tracker item.
```

### Now Try It Yourself

Pick a task you do repeatedly in this workspace. It could be:

- A skill to generate a learning record (you've been doing that for this course!)
- A skill to scaffold a new domain model
- A skill to run a specific test suite and summarize failures

Write the frontmatter and the body. I'll review it with you.

## Key Principles

### 1. Write for the agent, not for humans

Every line should answer: *does this help the agent do the task?* If a line is purely explanatory, cut it. Agents don't need your reasoning — they need your instructions.

### 2. Steps over reference

The most reliable skills are **step-based**: ordered actions the agent follows. Reference (definitions, rules, glossaries) belongs in the skill only if the agent needs to consult it during execution.

### 3. Frontmatter descriptions are triggers, not summaries

A description like `"Testing helper"` will fire on almost anything about testing. A description like `"Test-driven development. Use when the user wants to build features or fix bugs test-first"` fires only on the right moments.

Look at the real skills in your workspace:

- [`tdd/SKILL.md`](../../tdd/SKILL.md) — description covers the pattern (red-green) and triggers (test-first, integration tests)
- [`grilling/SKILL.md`](../../grilling/SKILL.md) — description covers the mode (stress-test) and trigger phrases
- [`writing-for-agents/SKILL.md`](../../writing-for-agents/SKILL.md) — description covers the document types and the edit actions

### 4. Use the `writing-for-agents` skill

When writing a skill, the [`writing-for-agents`](../../writing-for-agents/SKILL.md) skill in your workspace is the authoritative guide. It covers:

- **Context pointers** — how other docs find your skill
- **Information hierarchy** — what goes inline vs. in separate files
- **Leading words** — using familiar concepts so the agent "gets it"
- **Pruning** — keeping the skill short and relevant

### 5. Progressive disclosure

If a skill grows beyond ~100 lines, it's time to split:

- Keep the core workflow in `SKILL.md`
- Move reference material to `tests.md`, `glossary.md`, etc.
- Use cross-references (`See [tests.md](tests.md)`)

## Your Workspace Example: The `tdd` Skill

Look at [`tdd/SKILL.md`](../../tdd/SKILL.md). It's a great model:

1. **Frontmatter** — the description covers the pattern name (red-green) and specific triggers
2. **Body structure** — clear sections: what a good test is, seams (the most important concept), anti-patterns, rules of the loop
3. **Cross-references** — links to `tests.md` and `mocking.md` for reference material
4. **Leading words** — "seam," "tracer bullet," "red-green" — concepts the agent already knows from training
5. **Pruned** — no history, no examples of how to install TDD. Every line is either a step or a rule the agent needs during execution

## Exercise

Take a repetitive task from your workflow and write a skill for it. Use this checklist:

- [ ] Frontmatter has `name`, `description` (with what + when), `argument-hint`
- [ ] Body starts with a one-line summary
- [ ] Process is ordered steps, not a bullet list
- [ ] Each step has a clear completion criterion (how do you know it's done?)
- [ ] Rules section for non-negotiables
- [ ] Output format specified
- [ ] No explanatory fluff — every line is actionable
- [ ] Cross-references to reference files instead of inline text

## Primary Source

Read the [`setup-matt-pocock-skills/SKILL.md`](../../setup-matt-pocock-skills/SKILL.md) in your workspace. It's a longer skill that shows how a multi-step workflow skill is structured — exploration, presentation, confirmation, execution.

## Follow-Up

Ask about:
- How to design the frontmatter description for maximum precision
- How to structure a skill that has many branches (different outcomes)
- How to test whether a skill fires reliably
- Any part of your draft skill you're unsure about

---

<div style="text-align: center; color: #888; font-size: 0.9em; margin-top: 2em;">
💡 Ask followup questions — I'm your teacher. Nothing here is set in stone.
</div>
