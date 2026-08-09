# Lesson 5: Testing and Refining Skills

## The Feedback Loop

Writing a skill is a drafting process. The real test is whether it **works reliably** — fires when it should, doesn't fire when it shouldn't, and produces consistent results.

This lesson covers how to evaluate and refine your skills using three lenses:

1. **Trigger precision** — does the description fire on the right things?
2. **Instruction clarity** — does the agent follow the skill as intended?
3. **Reference quality** — does the supporting material help or hinder?

## Lens 1: Trigger Precision

### The Core Problem

The skill's `description` field is its **trigger**. If it's too vague, it fires on everything. If it's too narrow, it never fires when it should.

### The Test: Adverse Examples

Write 5 user requests and ask: **would this skill fire for each?**

```
Request: "Can you help me with testing?"
Expected fire: YES (broad testing need)

Request: "Fix the build error in CI"
Expected fire: NO (build issue, not testing)

Request: "Let's do red-green-refactor on this bug"
Expected fire: YES (explicit TDD trigger)

Request: "Write integration tests for the API"
Expected fire: YES (testing scope)

Request: "What's the best testing framework?"
Expected fire: NO (question, not action)
```

If the skill fires on requests 2 or 5, the description is too broad. If it doesn't fire on request 1 or 4, it's too narrow.

### Tuning the Description

Iterate the description until the test passes:

- **Too broad?** Add negative constraints: `"Use for test-first workflows. Not for testing questions or framework comparisons."`
- **Too narrow?** Add more trigger patterns: `"Use when the user wants to build features or fix bugs test-first, mentions 'red-green-refactor', wants integration tests, or asks about testing strategies."`
- **Ambiguous?** Add the argument hint: `"argument-hint: 'Describe the feature or bug to test-first'"`

### Leading Words in the Description

The description should use **leading words** — concepts the agent already knows — to trigger more reliably:

```markdown
# Too vague
description: "Help with testing and quality"

# Better — uses leading words as triggers
description: "Test-driven development. Use when the user wants to build features or fix bugs test-first, mentions 'red-green-refactor', or wants integration tests."
```

The second version fires because "test-driven development," "red-green-refactor," and "integration tests" are all concepts the agent recognizes from training.

## Lens 2: Instruction Clarity

### The Test: Silent Execution

Give the skill to an agent (or yourself pretending to be one) and watch it execute. Look for:

- **Ambiguous steps** — the agent hesitates or asks clarifying questions
- **Missing completion criteria** — the agent doesn't know when a step is done
- **Branch confusion** — the agent picks the wrong branch
- **Premature completion** — the agent stops early

### Common Clarity Bugs

| Bug | Symptom | Fix |
|---|---|---|
| Vague criterion | Agent loops or stops early | Make the done condition checkable |
| Missing defaults | Agent asks obvious questions | Set a default posture |
| Branch overlap | Agent picks wrong branch | Make branches mutually exclusive |
| Implicit assumptions | Agent does the wrong thing | Make every assumption explicit |

### The Branch Overlap Test

List every branch in your skill. For each pair, write a request that should trigger one but not the other:

```
Branch A: GitHub issues
Branch B: GitLab issues

Request: "Create an issue for this bug"
Expected: Branch A (default to GitHub)

Request: "Create a GitLab issue for this bug"
Expected: Branch B (explicit GitLab)
```

If you can't write such a request, the branches overlap and the agent will get confused.

## Lens 3: Reference Quality

### The Test: Load vs. Use

For each reference file your skill points to:

1. **Does the agent actually load it during execution?** If not, remove the pointer.
2. **Does the agent load it but ignore it?** The pointer wording may be too vague.
3. **Does the agent load it and use it correctly?** Good — keep it.

### The Pruning Test

For each line in your skill, ask:

> Would removing this line change the agent's behavior?

If no, cut it. Even if it's "nice to know."

### The Co-location Test

For each concept in your skill, check: are all its pieces together?

```
Bad:
## Rules
- Rule 1
- Rule 2

## Definitions
- Rule 1 definition is here

Good:
## Rules
### Rule 1
Definition and rule together.

### Rule 2
Definition and rule together.
```

Scattered concepts force the agent to jump around, increasing cognitive load and missing context.

## Practical Exercise: Debug Your Skill

Take your skill from Lesson 3/4 and run these tests:

### Test 1: Trigger Precision (5 minutes)

Write 5 requests. For each, predict whether the skill should fire. If any prediction feels uncertain, revise the description.

### Test 2: Silent Execution (10 minutes)

Walk through your skill step by step. For each step:

- Is the completion criterion clear?
- Is there a default if the user doesn't provide info?
- Would a confused agent misinterpret this step?

### Test 3: Pruning (5 minutes)

For each line, ask: "Would removing this change behavior?" Cut anything that doesn't.

### Test 4: Branch Overlap (5 minutes)

If your skill has branches, write one request per branch pair that should trigger one but not the other. If you can't, the branches need sharpening.

## When to Stop Refining

You've refined enough when:

- The trigger test passes (5 requests, clear yes/no for each)
- A silent execution produces no confusion
- Every line passes the pruning test
- All branches are mutually exclusive

Don't refine beyond that. A skill is never "perfect" — it's a living document that evolves as the agent uses it.

## Your Workspace Examples

### Good: `grilling/SKILL.md`

The description is precise: `"Grill the user relentlessly about a plan, decision, or idea. Use when the user wants to stress-test their thinking, or uses any 'grill' trigger phrases."`

- Trigger words: "stress-test," "grill trigger phrases"
- Negative constraint: only for plans/decisions, not for general Q&A
- The body is short (~30 lines) — every line is a step or rule

### Needs Work: Some Skills in mattpocock/skills

Many community skills have descriptions that are too broad: `"Help with X"` instead of `"Use when the user does Y specific thing related to X."` The trigger precision test catches this immediately.

## Primary Source

Re-read the **Pruning** section of [`writing-for-agents/SKILL.md`](../../writing-for-agents/SKILL.md). It covers the core principle: cut everything that doesn't change the agent's behavior.

## Follow-Up

Ask about:
- How to design better trigger descriptions
- How to handle skills that need to work across different agent platforms
- How to version or iterate on a skill over time
- Any part of your skill that still feels uncertain

---

<div style="text-align: center; color: #888; font-size: 0.9em; margin-top: 2em;">
💡 Ask followup questions — I'm your teacher. Nothing here is set in stone.
</div>
