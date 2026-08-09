# Lesson 1: What Are Agent Skills, and Where Did They Come From?

## The Big Idea

An **agent skill** is a self-contained instruction file that teaches an AI agent how to perform a specific task. Think of it as a "role" or "persona" the agent can adopt when the situation calls for it.

In your workspace, you already have 24 skills installed from [mattpocock/skills](https://github.com/mattpocock/skills). Each one is a `SKILL.md` file that says: *when the user asks for X, read this file and follow these instructions.*

## How Skills Differ From Plain Prompts

A **prompt** is a one-off instruction. A **skill** is a reusable, structured, discoverable instruction set.

The difference matters because skills are:

- **Discoverable** — agents find them automatically by scanning directories
- **Scoped** — they live in specific repos or contexts
- **Versioned** — they change independently of the model
- **Composable** — one skill can reference another

## The History

The concept didn't appear in a vacuum. It evolved through several stages:

### Stage 1: The Research Foundation (2022)

The [ReAct paper](https://arxiv.org/abs/2210.03629) by Yao et al. showed that LLMs could interleave reasoning ("let me think about this") with action ("I'll call this tool") to solve complex tasks. This was the theoretical bedrock.

### Stage 2: Agent Frameworks (2023)

[LangChain](https://python.langchain.com/docs/concepts/agents/) and early frameworks introduced agent patterns — zero-shot agents, ReAct agents, plan-and-execute. These were programmable: you wrote Python code to define agents.

### Stage 3: Function Calling (Sep 2023)

OpenAI introduced [function calling](https://platform.openai.com/docs/guides/function-calling) — the ability for models to output structured JSON that calls specific functions. This was the technical breakthrough that made "skills" possible without custom code.

### Stage 4: Tool Use Goes Mainstream (2024)

Anthropic adapted the concept for Claude. Coding agents like [Aider](https://github.com/paul-gauthier/aider) and [Cursor](https://cursor.sh) proved that agents could reliably edit code, run tests, and iterate. The "agent" became a practical tool, not just a research demo.

### Stage 5: The Skills Movement (2025)

Matt Pocock and others noticed that the *instructions* for how an agent should behave are themselves a form of reusable knowledge — separate from the model, separate from the code. They formalized this as **skills**: prompt files that teach agents specific roles.

The key insight: *the intelligence isn't in the model — it's in the instructions you give it.*

## Why "Skills" Is the Right Word

In the learning sense, a skill is a learned capability that improves performance. For an agent:

- A **prompt** is a one-off instruction
- A **skill** is a reusable, structured, discoverable instruction set

## Your Workspace as an Example

Look at your `AGENTS.md`:

```markdown
## Agent skills

### Issue tracker
GitHub Issues are used for engineering work tracking. See `docs/agents/issue-tracker.md`.
```

This is a **routing rule** — it tells the agent: when you need issue tracker info, read this file. It's the simplest possible form of skill invocation. Your installed skills do the same thing but more richly, with structured metadata (`name`, `description`, `argument-hint`).

## What's Next

This lesson gave you the **what** and the **why**. The next lesson covers **how** skills actually get used — the conversation flow between agent and LLM.

## Primary Source

Read the [mattpocock/skills README](https://github.com/mattpocock/skills) for the official explanation of the framework. It's short and directly grounded in the concept we just covered.

## Follow-Up

Ask me anything about:
- How a specific skill in your workspace works
- The difference between skills and other patterns (plugins, extensions, fine-tuning)
- How to write your own skill
- Any part of this history that feels fuzzy

---

<div style="text-align: center; color: #888; font-size: 0.9em; margin-top: 2em;">
💡 Ask followup questions — I'm your teacher. Nothing here is set in stone.
</div>
