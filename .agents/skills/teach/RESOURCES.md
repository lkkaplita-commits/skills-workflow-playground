# LLM Agent Skills Resources

## Knowledge

- [Matt Pocock — Skills repo](https://github.com/mattpocock/skills)
  The canonical source for Matt Pocock's skills framework. Contains the skills, docs, and examples.
  Use for: understanding the skills format, how skills are discovered and invoked.

- [Matt Pocock — "Introducing Skills"](https://mattpocock.com)
  Blog posts and announcements about the skills concept.
  Use for: the origin story and design rationale.

- [Anthropic — Tool Use Documentation](https://docs.anthropic.com/en/docs/build-with-claude/tool-use)
  Primary source on how Claude discovers and calls tools. Foundation for understanding function calling as the mechanism behind skills.
  Use for: the technical mechanism of how agents invoke skills.

- [OpenAI — Function Calling Documentation](https://platform.openai.com/docs/guides/function-calling)
  How OpenAI models accept and call functions. Parallel to Anthropic's approach.
  Use for: understanding the broader landscape of agent tool use.

- [Anthropic — Prompting with Tools](https://docs.anthropic.com/en/docs/build-with-claude/tool-use)
  How to structure system prompts that enable tool use. The "prompt engineering" side of skills.
  Use for: understanding how skills get injected into agent context.

- [ReAct Paper (Yao et al., 2022)](https://arxiv.org/abs/2210.03629)
  "ReAct: Synergizing Reasoning and Acting in Language Models." Foundational research on LLM agents that reason and act interleaved.
  Use for: the academic origin of the reasoning-acting loop that skills enable.

- [LangChain — Agent Patterns](https://python.langchain.com/docs/concepts/agents/)
  Overview of agent patterns (zero-shot, ReAct, plan-and-execute, etc.).
  Use for: historical context of agent architectures before the skills movement.

- [CrewAI — Multi-Agent Framework](https://docs.crewai.com/)
  Multi-agent orchestration framework. Shows how skills/composable agents scale.
  Use for: understanding the evolution from single-agent to multi-agent patterns.

## Wisdom (Communities)

- [r/LocalLLaMA](https://reddit.com/r/LocalLLaMA)
  High-signal community for LLM tooling discussions.
  Use for: staying current on agent frameworks and skills ecosystems.

- [Matt Pocock's Discord / Twitter](https://x.com/mattpocockuk)
  Primary community around the skills framework.
  Use for: announcements, discussions, and feedback on the skills ecosystem.

## Gaps

- No comprehensive timeline or history of the "skills" concept as a named pattern
- Limited academic literature specifically on "prompt-based skills" as a design pattern
- The Matt Pocock skills ecosystem is new — long-term best practices not yet established
