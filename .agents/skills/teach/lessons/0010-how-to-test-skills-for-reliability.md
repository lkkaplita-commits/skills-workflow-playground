# Lesson 10: How to Test Skills for Reliability

## Prompt Evaluation, Regression Testing, and Building a Test Harness

*Previous: [Lesson 9 — Combining Skills with MCP Servers](0009-combining-skills-with-mcp-servers.html) · [Resources](../RESOURCES.md) · [Mission](../MISSION.md)*

---

## Why "It Works Sometimes" Isn't Good Enough

Lesson 5 taught you to evaluate a skill through three lenses: trigger precision, instruction clarity, and reference quality. That's the **one-shot** evaluation — does this skill work *today*?

But skills are living documents. They change. And every change risks breaking what already worked. Reliability testing answers a harder question: **does this skill produce the right output consistently, across versions, over time?**

This lesson covers three practices:

1. **Prompt evaluation harnesses** — a repeatable set of test prompts with expected outputs
2. **Regression testing** — catching skill updates that break existing behavior
3. **Versioned test suites** — storing tests alongside skills so they evolve together

---

## 1. Building a Prompt Evaluation Harness

A prompt evaluation harness is a structured collection of inputs and expected outputs. It turns the vague question "does this skill work?" into a concrete checklist.

### The Test Case Format

Each test case has three parts: the user prompt (what the user might say), the expected behavior (what the skill should do), and the evaluation criteria (how you judge success).

**Example: test-cases.yaml for a TDD Skill**

```yaml
cases:
  - id: tdd-001
    prompt: "Let's write a function for calculating fibonacci numbers, test-first"
    category: positive-fire
    expected:
      fires: true
      calls_tdd_skill: true
      asks_for_first_step: true

  - id: tdd-002
    prompt: "Can you refactor this function to be more readable?"
    category: negative-fire
    expected:
      fires: false
      explains_difference: true

  - id: tdd-003
    prompt: "I want to write unit tests for the login endpoint"
    category: positive-fire
    expected:
      fires: true
      asks_for_test_framework: true
      suggests_red_green_refactor: true

  - id: tdd-004
    prompt: "What's the difference between unit tests and integration tests?"
    category: negative-fire
    expected:
      fires: false
      provides_explanation: true

  - id: tdd-005
    prompt: "Red green refactor this bug fix"
    category: positive-fire
    expected:
      fires: true
      mentions_write_test_first: true
```

### Category Types

| Category | Purpose | What to check |
|---|---|---|
| **Positive-fire** | Should trigger the skill | Does it fire? Does it produce correct output? |
| **Negative-fire** | Should NOT trigger | Does it stay silent? Does it explain why? |
| **Edge-case** | Borderline scenarios | Does it handle ambiguity gracefully? |
| **Adverse** | Malicious or confusing input | Does it fail safely? |

### Evaluating Automatically

For simple skills, you can check the binary trigger behavior automatically: does the agent call the skill or not?

**Automated trigger check (pseudocode)**

```
function evaluate_skill(skill_file, test_case):
    agent = initialize_agent([skill_file])
    response = agent.invoke(test_case.prompt)
    
    # Check 1: Did the skill fire?
    skill_invoked = skill_file.path in response.tools_called
    
    if test_case.expected.fires == skill_invoked:
        return PASS("Trigger behavior matches expectation")
    else:
        return FAIL(f"Expected fires={test_case.expected.fires}, got {skill_invoked}")
```

For deeper evaluation (output quality, step ordering, etc.), you need manual review or a secondary LLM-as-judge.

---

## 2. Regression Testing for Skills

When you update a skill, how do you know you haven't broken it? Regression testing answers this by re-running the harness after every change.

```
Skill v1.0 (23 tests passed)
        │
        ▼
  ┌─────────────┐
  │ Edit Skill   │
  │ Modify SKILL │
  │ Update tests?│
  │ Re-run tests │
  └─────────────┘
        │
        ▼
  Skill v1.1 (run same 23 tests)
        │
        ▼
  23/23 PASS ✓
```

### What to Regression-Test

| What | How to test | Frequency |
|---|---|---|
| **Trigger behavior** | Does it fire on the right prompts? | Every change to description |
| **Step ordering** | Does the agent follow the process in the right order? | Every change to instructions |
| **Output quality** (human or LLM-judge) | Does the output meet quality criteria? | Weekly or on major updates |
| **Adverse cases** | Does it handle edge cases safely? | Monthly or when new edge cases found |

### The Golden Rule of Skill Regression Tests

> **Rule:** When you fix a bug in a skill's behavior, *add a new test case first* that would have caught that bug. Then fix the skill. Then verify the new test passes. This is the test-driven approach applied to skill development.

---

## 3. Versioned Test Suites

The most important structural decision: **where do tests live?**

### Option A: Tests Inside the Skill File

```yaml
---
name: my-skill
description: "Does X thing"
---

# My Skill

(standard skill content)

## Test Cases

See [test-cases/](../test-cases/) for the evaluation harness.
Run with: `copilot-skills test`
```

**Pros:** Tests live with the skill. Easy to discover. One file to version.
**Cons:** Tests clutter the skill. Hard to run programmatically.

### Option B: Tests in a Parallel Directory

```
skills/
  my-skill/
    SKILL.md
    test-cases/
      triggers.yaml          # Trigger precision tests
      output-quality.yaml    # Output quality tests
      adverse.yaml           # Adverse case tests
    tests/
      test_triggers.py       # Automated harness
      test_output.py         # LLM-as-judge harness
```

**Pros:** Clean separation. Easy to automate. Scales to many skills.
**Cons:** Tests are outside the skill. Need a runner tool.

### Option C: Tests as a Separate Skill

```
skills/
  evaluate-my-skill/
    SKILL.md  # A skill whose only job is to run the harness
    test-cases/
      ...
```

**Pros:** The agent can invoke evaluation just like any other skill. Tests become part of the workflow.
**Cons:** Adds complexity. Only useful for mature skill ecosystems.

### Recommendation

For a single skill or small set: **Option A** (tests in the skill file). For a growing collection: **Option B** (parallel directory). For an ecosystem of skills: **Option C** (test as a skill).

---

## Practical Exercise

### Exercise: Build a Test Harness for One of Your Skills

Take one skill from your workspace and create a prompt evaluation harness:

1. **Pick a skill** — choose one with a clear trigger description (e.g., the TDD skill or a domain-specific one).
2. **Write 5 test cases** — 3 positive-fire, 1 negative-fire, 1 edge-case. Use the format above.
3. **Run the tests manually** — paste each prompt into the agent and record whether the skill fired as expected.
4. **Find a failure** — at least one test should fail (or produce unexpected behavior). Note what changed.
5. **Fix the skill** — update the description or instructions to make that test pass.
6. **Add it to the harness** — the test that failed is now part of your regression suite. It won't fail again.

This is the skill equivalent of writing a failing test first. The harness grows with every bug you fix.

---

## Key Takeaways

- **A skill harness is a versioned checklist** of prompts and expected behaviors. It turns "does this work?" into a pass/fail score.
- **Regression testing prevents skill drift**. Every bug fix should add a test case that would have caught it first.
- **Automate what you can**. Trigger behavior is easy to check automatically. Output quality needs human or LLM-judge review.
- **Store tests where they'll survive**. In the skill file for simplicity. In a parallel directory for scale. As a skill for an ecosystem.

---

## Primary Source

If you want to go deeper on prompt evaluation, read:

- [ReAct: Synergizing Reasoning and Acting in Language Models](https://arxiv.org/abs/2210.03629) — the foundational paper on LLM agents that reason and act. Understanding the reasoning-acting loop is essential for evaluating whether a skill actually improves agent behavior.
- [Matt Pocock's Skills Repo](https://github.com/mattpocock/skills) — study how existing skills structure their descriptions for reliable triggering.

---

*Ask followup questions to your agent anytime — it's your teacher and can help with anything that's unclear.*
