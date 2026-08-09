# Lesson 7: Writing a Skill from Scratch

## The Complete Process

This lesson walks through the entire skill authoring process end-to-end. You've learned the patterns in isolation — now we put them together.

We'll write a skill for a real task in this workspace. The task: **creating a learning record** — something the teaching framework does but which doesn't have a dedicated skill yet.

## Step 1: Define the Trigger

Before writing a single line of the body, nail down the **frontmatter**. This is where most skills fail — a vague description means the skill never fires when needed.

### The Adverse Example Test

Write 5 requests before you write the skill. For each, ask: **should this skill fire?**

```
Request: "Create a learning record for today's session"
Expected fire: YES ← core purpose

Request: "Write a learning record about what I learned about seams"
Expected fire: YES ← same thing

Request: "Summarize what I learned"
Expected fire: NO ← that's a handoff, not a learning record

Request: "Create a new lesson"
Expected fire: NO ← different artifact

Request: "I want to update CONTEXT.md"
Expected fire: NO ← domain modeling, not learning records

Request: "Record that I now understand progressive disclosure"
Expected fire: YES ← same intent, different wording
```

### Drafting the Description

Based on the test, the description needs to cover:

- **What it does**: creates a learning record file
- **When to fire**: user wants to record something they've learned
- **What it produces**: a file in `learning-records/`
- **What it's NOT**: not a handoff, not a lesson summary, not a glossary entry

```yaml
---
name: learning-record
description: >
  Create a learning record — a short document capturing something the user
  has learned. Use when the user wants to record a lesson, insight, or
  understanding for future reference. Not for handoffs, lesson creation,
  or glossary entries.
argument-hint: "What was learned"
disable-model-invocation: true
---
```

Key choices:

- **`disable-model-invocation: true`** — this skill writes a file; it doesn't need the agent to do anything complex
- **`disable-model-invocation`** means the skill is only loaded when explicitly requested, not auto-triggered
- **The description uses `>`** (folded block scalar) to write a multi-line description that reads as one paragraph
- **Explicit negative constraint** prevents the most common confusion (handoff vs. learning record)

## Step 2: Design the Body

Now write the body. Apply the patterns from lessons 3–4:

### Pattern 1: Steps over reference

The skill is a **process**, not a reference document. Every line should be an action the agent takes.

### Pattern 2: Completion criteria

Every step needs a checkable "done" condition.

### Pattern 3: Leading words

Use familiar concepts: "insight," "non-obvious," "decision-grade."

### Draft

```markdown
# Learning Record

Create a learning record — a short document capturing something the user has learned.

## Process

### 1. Confirm what was learned

Ask the user to state what they learned in one sentence. If they've already stated it clearly, use their wording.

### 2. Determine the file path

Find the highest existing number in `./learning-records/` and increment by one. If no records exist, start with `0001`.

### 3. Write the record

Write the file to `./learning-records/<number>-<slug>.md` using the format below. Use dash-case for the slug derived from the core insight.

<record-template>

# {Short title of what was learned}

{1-3 sentences: what was learned and why it matters for future sessions.}

</record-template>

### 4. Report

Tell the user the file path and what was written.

## Rules

- Only write when the user explicitly asks to record something learned
- Keep records to 1-3 sentences — not essays
- If the record contradicts an earlier one, mark the old record as superseded
- Never duplicate content already in GLOSSARY.md
```

## Step 3: Apply the Lens Tests

### Lens 1: Trigger Precision

Re-test the 5 requests against the final description. Does any request produce an uncertain prediction? If so, tighten the description.

### Lens 2: Instruction Clarity

Walk through the steps silently:

- **Step 1** — completion criterion: "user has stated what they learned in one sentence." Checkable.
- **Step 2** — completion criterion: "file path determined." Checkable.
- **Step 3** — completion criterion: "file exists at the determined path." Checkable.
- **Step 4** — completion criterion: "user has been informed." Checkable.

### Lens 3: Pruning

For each line, ask: "Would removing this change the agent's behavior?"

- "Ask the user to state what they learned" — YES, changes behavior (don't remove)
- "If they've already stated it clearly, use their wording" — NO, implicit in "use what they give" → prune
- "Use dash-case for the slug" — YES, changes the output format → keep

## Step 4: Compare Against a Real Skill

Look at the [`handoff/SKILL.md`](../../handoff/SKILL.md) in your workspace. It's the closest sibling to our learning-record skill. Compare:

| Aspect | handoff | learning-record (draft) |
|---|---|---|
| Description scope | "Compact the current conversation" | "Create a learning record" |
| Negative constraints | None needed (very specific) | Needed (vs. handoff, lesson) |
| Body length | ~10 lines | ~20 lines |
| Template inline? | Yes (in prose) | Yes (in template block) |
| Rules section | No | Yes (why records are short) |
| `disable-model-invocation` | true | true |

The key difference: **handoff is more specific** because its purpose is narrower (compact a conversation). A learning record is broader (any learned insight), so it needs the negative constraint to avoid overlap.

## Step 5: Identify Where It Fits in the Workflow

The learning-record skill connects to other skills in the ecosystem:

```
teaching workflow:
  lesson delivered → user learns something → learning-record
  domain model evolves → domain-modeling → update CONTEXT.md
  session ends → handoff → summarize for next session
```

This is why the negative constraint in the description matters — it prevents the skill from firing when `handoff` or `domain-modeling` should fire instead.

## Your Turn

Now write a skill for a real task. Not a hypothetical one — pick something you actually do. Use this checklist:

### Pre-writing

- [ ] Write 5 adverse examples (3 should fire, 2 shouldn't)
- [ ] Write the description that handles all 5 correctly
- [ ] Decide: does this need `disable-model-invocation`?

### Writing

- [ ] Steps are ordered actions, not bullet points
- [ ] Each step has a completion criterion
- [ ] Leading words are used where appropriate
- [ ] Templates are inlined only if short (< 10 lines)

### Post-writing

- [ ] Re-test the 5 adverse examples against the final description
- [ ] Silent walkthrough: does each step have a clear done condition?
- [ ] Pruning: cut every line that doesn't change behavior
- [ ] Check overlap with existing skills — is there a negative constraint needed?

## Primary Source

Re-read the **Trigger Precision** section of [lesson 5](0005-testing-and-refining-skills.md) and the **Context pointers** section of [`writing-for-agents/SKILL.md`](../../writing-for-agents/SKILL.md). They cover the two most critical design decisions: the description and the cross-references.

## Follow-Up

Ask about:
- How to handle skills that need to work across different file formats or conventions
- How to version a skill over time (when to update vs. create a new version)
- How to test a skill with a real agent before committing it
- Any part of your skill draft that feels uncertain

---

<div style="text-align: center; color: #888; font-size: 0.9em; margin-top: 2em;">
💡 Ask followup questions — I'm your teacher. Nothing here is set in stone.
</div>
