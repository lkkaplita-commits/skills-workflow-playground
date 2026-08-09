# Lesson 4: Structuring Complex Skills

## When a Skill Grows Up

Lesson 3 showed you the basic anatomy: frontmatter + body. That works great for skills up to ~50 lines. Beyond that, you need **structure** — not just sections, but patterns for managing complexity.

There are two kinds of complexity:

1. **Branching** — the skill handles multiple scenarios differently (GitHub vs GitLab vs local markdown)
2. **Reference depth** — the skill needs lots of definitions, rules, or examples

Each requires a different pattern.

## Pattern 1: Branching Workflows

### The Problem

You write a skill that configures something. But the configuration depends on the user's environment. If you inline every branch, the skill becomes a maze of "if this, then that."

### The Solution: Explore → Present → Confirm → Execute

Look at [`setup-matt-pocock-skills/SKILL.md`](../../setup-matt-pocock-skills/SKILL.md). It handles three branches (GitHub, GitLab, local markdown) using a **four-phase pattern**:

```markdown
### 1. Explore
Read the environment to understand the starting state.

### 2. Present findings and ask
Summarise what's present. Ask the user one decision at a time.

### 3. Confirm and edit
Show the user a draft. Let them edit before writing.

### 4. Write
Execute based on the user's decisions.
```

This pattern works for any skill that needs to **adapt to the user's environment**. The key insight: **don't decide in the skill — decide with the user.** The skill defines the decision points; the user provides the answers.

### Designing Branch Points

Each branch point has three parts:

1. **The signal** — how to tell which branch applies (e.g., `git remote -v` points at GitHub)
2. **The default** — what to propose if the user doesn't decide (e.g., "propose GitHub")
3. **The alternatives** — what to offer if the default doesn't fit (e.g., GitLab, local markdown)

```
Explore → find signal → propose default → ask user → record decision
```

### When to Inline vs. Split Branches

- **Inline** the branch logic when there are ≤ 3 branches and the user needs to see them all to make a decision
- **Split** into a separate file when a branch has its own sub-workflow (e.g., GitHub setup has its own steps that would bloat the main flow)

## Pattern 2: Progressive Disclosure

### The Problem

A skill has 80+ lines. The agent reads it every time the skill fires. Half the content is reference material the agent only needs sometimes.

### The Solution: Push reference behind context pointers

A **context pointer** is a reference in the skill that says: *load this other file when you need it.* It's the agent equivalent of a hyperlink.

```markdown
See [glossary.md](glossary.md) for term definitions.
```

The agent reads `SKILL.md`, encounters the pointer, and loads the referenced file **only when needed**. This is **progressive disclosure** — material that's available but not always loaded.

### The Information Hierarchy

Every piece of content in a skill sits on a rung of the information hierarchy:

```
1. In-file step       → What the agent does, in order (always loaded)
2. In-file reference  → Rules/facts consulted during execution (always loaded)
3. Disclosed reference → Reference material in a separate file (loaded on demand)
```

**Rule of thumb:** if the agent only needs it for some runs of the skill, it belongs on rung 3 — in a separate file, reached by a pointer.

### Your Workspace Example: `tdd`

The [`tdd/SKILL.md`](../../tdd/SKILL.md) skill uses progressive disclosure:

```markdown
See [tests.md](tests.md) for examples and [mocking.md](mocking.md) for mocking guidelines.
```

The core skill (~60 lines) stays focused on the red-green loop rules. The examples and mocking strategies live in sibling files. The agent loads them only when it reaches that part of the workflow.

### When to Split

Split when:

- The skill exceeds ~80 lines
- A section is reference-heavy (definitions, rules, examples) rather than step-based
- The section is only needed for some branches

Don't split when:

- The skill is under ~60 lines
- Every branch needs every section
- The content is a rule the agent consults on every run

## Pattern 3: Completion Criteria

### The Problem

The agent doesn't know when a step is "done." It keeps iterating, or worse — it stops too early.

### The Solution: Every step ends on a checkable condition

Compare these two step descriptions:

- ❌ *"Understand the user's problem"* — vague, no clear done condition
- ✅ *"Summarise what's present and what's missing in one paragraph"* — checkable, specific

Look at [`writing-for-agents/SKILL.md`](../../writing-for-agents/SKILL.md). Notice how every step has a completion criterion:

```markdown
The session is done when the frontier is empty: every branch of the design tree visited, nothing left silently assumed.
```

This is a **checkable condition**. The agent can evaluate it: is the frontier empty? Yes or no.

### Designing Good Completion Criteria

A good criterion is both **checkable** and **exhaustive**:

| Property | What it means | Example |
|---|---|---|
| Checkable | The agent can evaluate true/false | "Every modified model accounted for" |
| Exhaustive | It requires thoroughness | "Every" not "some" or "a few" |

Bad criteria are either vague ("make it good") or non-exhaustive ("write a test for the main function" — leaves out edge cases).

## Pattern 4: Leading Words

### What They Are

A **leading word** is a familiar concept from the agent's training that the agent already "knows." Using it recruits the agent's pre-existing understanding instead of teaching from scratch.

Examples:

- "seam" — the agent already knows this from software engineering training
- "tracer bullet" — the agent knows this from agile/tdd training
- "frontier" — the agent knows this from algorithm training

### How to Use Them

In your skill, use leading words as **conceptual anchors**:

```markdown
A **seam** is the public boundary you test at. Tests live at seams, never against internals.
```

The definition is brief because the agent already knows the concept. The one-line definition just pins it to your specific usage.

### When NOT to Use Them

Don't coin new words. A made-up term recruits no priors — you pay in definition tokens what a pretrained word gives free. Reach for an existing word first.

## Putting It All Together: A Complex Skill

Here's how the patterns combine in a real skill:

```
setup-matt-pocock-skills/SKILL.md

1. Frontmatter — describes the skill and when to invoke it
2. Pattern 1 (Branching) — four-phase pattern with branch points
   - Branch 1: GitHub vs GitLab vs local (signal: git remote)
   - Branch 2: default labels vs custom
   - Branch 3: single-context vs multi-context
3. Pattern 2 (Progressive Disclosure) — references docs/agents/ files
4. Pattern 4 (Leading Words) — "default posture," "frontier," "seeds"
5. No Pattern 3 explicitly — the steps are short enough that the
   completion criteria are implicit
```

## Exercise

Take your skill from Lesson 3's exercise and ask:

1. **Does it have branches?** If yes, does the four-phase pattern (explore → present → confirm → execute) fit?
2. **Is it over 60 lines?** If yes, which sections could be disclosed reference?
3. **Does every step have a completion criterion?** Rewrite any that are vague.
4. **Are you using leading words?** Could any phrases be replaced with familiar concepts?

## Primary Source

Re-read [`writing-for-agents/SKILL.md`](../../writing-for-agents/SKILL.md). It's the authoritative reference for these patterns. Focus on:

- **Context pointers** section — how to design reliable references
- **Information hierarchy** — when to inline vs. disclose
- **Completion criteria** — what makes a criterion checkable and exhaustive
- **Leading words** — how to use familiar concepts as anchors

## Follow-Up

Ask about:
- How to design branch points that don't overwhelm the user
- How to decide between inlining vs. splitting at the 60-line boundary
- How to write completion criteria that are specific enough
- Any part of your skill draft that feels uncertain

---

<div style="text-align: center; color: #888; font-size: 0.9em; margin-top: 2em;">
💡 Ask followup questions — I'm your teacher. Nothing here is set in stone.
</div>
