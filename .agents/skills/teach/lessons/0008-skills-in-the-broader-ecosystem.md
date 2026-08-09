# Lesson 8: Skills in the Broader Ecosystem

## The Landscape

Matt Pocock's skills are one approach among several for teaching agents to do specific tasks. Understanding where skills fit relative to other patterns helps you choose the right tool and participate in the broader ecosystem.

## Skills vs. MCP Servers

### What MCP Is

[Model Context Protocol (MCP)](https://modelcontextprotocol.io) is a standardized way for LLM applications to connect to external tools and data sources. An MCP server is a program that exposes functions (like "search the web" or "query a database") to an LLM.

### The Key Difference

| | Skills | MCP Servers |
|---|---|---|
| **What they are** | Text instructions | Executable programs |
| **Language** | Natural language (Markdown) | Code (TypeScript, Python, etc.) |
| **Execution** | LLM follows instructions | Agent calls functions via JSON-RPC |
| **Scope** | How the agent *thinks* | What the agent *can do* |
| **Update cycle** | Edit a file | Restart the server |
| **Version control** | Plain text in git | Compiled artifacts + source |

### Where They Overlap

Both solve the same problem: **extending what an agent can do**. But they extend in different dimensions:

- **Skills** extend *how the agent reasons* — they teach the agent a mental model
- **MCP servers** extend *what the agent can access* — they give the agent new tools

They're complementary, not competing. You might use a skill to teach the agent *how* to debug a bug, and an MCP server to give the agent *access* to the debugger.

### When to Choose Which

| Choose skills when... | Choose MCP when... |
|---|---|
| The task is cognitive (design, review, plan) | The task needs external data or execution |
| You want to version control the instructions | You need real-time access to a service |
| The knowledge is textual (rules, patterns, examples) | The knowledge is computational (algorithms, APIs) |
| You want to share knowledge with a team | You want to integrate with an external API |

## Skills vs. VS Code Custom Instructions

### What They Are

VS Code's [custom instructions](https://code.visualstudio.com/docs/editor/chat-in-code) (`.vscode/.copilot/instructions.md` or similar) are files that get injected into every Copilot session. They're the simplest possible form of agent extensibility.

### Comparison

| | Skills | Custom Instructions |
|---|---|---|
| **Scope** | Invoked on demand | Loaded every session |
| **Granularity** | Per-task (one skill per role) | Global (one set of instructions) |
| **Version control** | Per-skill files in git | One file (or a few) |
| **Maintainability** | Modular, easy to update | Monolithic, hard to manage |
| **Discovery** | Agent scans and routes | Always loaded, no routing |

### The Relationship

Custom instructions are the **simplest possible form of skill** — a single file that's always loaded. Skills are what you get when you scale past one file: you need routing, versioning, and modularity.

**Rule of thumb:** if you have fewer than 3 roles to teach an agent, use custom instructions. If you have more, you've outgrown them and need skills.

## Skills vs. Plugins / Extensions

### What They Are

VS Code extensions and browser plugins are **compiled programs** that add functionality to an application. They run code, not text.

### Comparison

| | Skills | Plugins/Extensions |
|---|---|---|
| **Format** | Plain text (Markdown) | Compiled code + manifests |
| **Installation** | Copy files or git clone | Marketplace or `npm install` |
| **Update cycle** | Edit a file, changes immediately | Publisher releases, user updates |
| **Runtime** | LLM reads and follows | Executes in a sandbox |
| **Permissions** | Whatever the agent can do | Explicit permission model |
| **Security** | Text can't execute code | Code can do anything it's allowed |

### Why Skills Are Different

Skills don't run code — they **teach the LLM** how to use existing tools. A skill that says "run tests" doesn't execute tests itself; it tells the LLM to use its `run_in_terminal` tool to run tests.

This is the key insight: **skills are instructions for the agent, not for the machine.**

## Skills vs. Fine-Tuning

### What Fine-Tuning Is

Fine-tuning trains a model on a custom dataset to change its behavior. It's the oldest approach to agent customization.

### Comparison

| | Skills | Fine-Tuning |
|---|---|---|
| **Cost** | Free (text files) | Expensive (compute + data) |
| **Update time** | Instant (edit a file) | Days (retrain + deploy) |
| **Granularity** | Per-task, on demand | Model-wide |
| **Interpretability** | Readable Markdown | Black box (weights) |
| **Scope** | Specific roles | General behavior |

### When Fine-Tuning Makes Sense

Fine-tuning is appropriate when:

- You need the model to **always** behave a certain way (not just when a skill fires)
- The behavior is too complex to express in text
- You're building a product where skills can't be loaded at runtime

For the vast majority of use cases, **skills are a better choice** — they're cheaper, faster to update, and more interpretable.

## The Matt Pocock Skills Position

Matt Pocock's skills sit at a specific point in this landscape:

```
                    Complexity
                      ↑
                      │
         MCP Servers  │  Skills
          (code)      │  (text)
                      │
    Fine-tuning       │  Custom Instructions
    (model-wide)      │  (global text)
                      │
                      └─────────────────→
                   Granularity (narrow ← → broad)
```

Skills are **text-based, on-demand, narrow-scope** — the sweet spot between custom instructions (always loaded, broad scope) and MCP servers (code-based, tool access).

## The Emerging Ecosystem

Beyond Matt Pocock's framework, several other approaches exist:

### 1. Prompt Templates

Frameworks like [LangChain's prompt templates](https://python.langchain.com/docs/concepts/prompt_templates/) and [DSPy's modules](https://dspy.ai/) use programmatic prompt composition. They're more flexible than skills but require writing code.

### 2. Agent Frameworks

[LangChain agents](https://python.langchain.com/docs/concepts/agents/), [CrewAI](https://docs.crewai.com/), and [AutoGen](https://microsoft.github.io/autogen/) define agent behaviors in code. They're powerful but less accessible than text-based skills.

### 3. System Prompt Libraries

Projects like [promptfoo](https://promptfoo.dev/) and [Instructor](https://www.instructor.dev/) focus on prompt quality and reliability. They're complementary to skills — you can use promptfoo to test your skills.

### 4. Agent Operating Systems

Projects like [AutoGPT](https://autogpt.net/), [BabyAGI](https://github.com/yoheinakajima/babyagi), and [OpenHands](https://github.com/All-Hands-AI/OpenHands) try to build full agent orchestration platforms. They're broader than skills — they're trying to replace the agent framework itself.

## Why Skills Matter in This Landscape

Skills are notable because they:

1. **Democratize agent customization** — anyone who can write Markdown can create a skill
2. **Make agent behavior version-controllable** — skills live in git, so you can diff, review, and roll back
3. **Enable knowledge sharing** — a skill is a piece of knowledge that anyone can copy and adapt
4. **Decouple knowledge from code** — the instructions are separate from the tools the agent uses

This last point is the key insight: **the intelligence isn't in the model — it's in the instructions you give it.** Skills make those instructions first-class artifacts.

## Exercise: Map Your Own Tools

Take the tools you use to extend your agents and map them:

| Tool | Skills? | MCP? | Custom Instructions? | Other? |
|---|---|---|---|---|
| Example: test runner skill | ✅ | | | |
| Your example | | | | |
| Your example | | | | |

For each, ask:
1. What am I trying to teach the agent?
2. Is it cognitive (how to think) or operational (what to access)?
3. Does it need to be on-demand or always-loaded?

## Primary Source

Read the [MCP documentation](https://modelcontextprotocol.io/introduction) for the official explanation of the protocol. Compare its design to the skills framework — what problems does each solve, and where do they overlap?

## Follow-Up

Ask about:
- How to combine skills with MCP servers in a single workflow
- How to test skills for reliability (prompt evaluation, regression testing)
- The future of the skills ecosystem — who's building on top of it
- Any comparison that feels unclear

---

<div style="text-align: center; color: #888; font-size: 0.9em; margin-top: 2em;">
💡 Ask followup questions — I'm your teacher. Nothing here is set in stone.
</div>
