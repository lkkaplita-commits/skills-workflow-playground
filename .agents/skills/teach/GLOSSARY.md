# LLM Agent Skills Glossary

## Terms

**Agent skill**:
A self-contained, machine-readable instruction file (typically `.md`) that teaches an LLM agent how to perform a specific task or role. Skills are discovered, routed to, and invoked by the agent based on the user's request.
_Avoid_: Prompt, plugin, extension

**Skill discovery**:
The process by which an agent finds which skills are available in a workspace. Usually done by scanning `.agents/skills/`, `skills/`, or a configured directory for `SKILL.md` files.
_Avoid_: Plugin loading, module resolution

**Skill invocation**:
The mechanism by which an agent activates a skill — typically when the user's request matches the skill's `description` or `argument-hint`. The skill's content gets injected into the agent's system prompt.
_Avoid_: Plugin activation, function call

**Function calling**:
The underlying API mechanism (from OpenAI and adapted by Anthropic) that lets models output structured data to call functions. Skills are essentially function-calling wrappers around natural-language instructions.
_Avoid_: Tool use (ambiguous), API call

**Prompt chaining**:
A pattern where each step's output drives the next step's prompt. Skills often contain prompt chains that the agent executes sequentially.
_Avoid_: Sequential prompts, step-by-step prompts

**System prompt injection**:
How a skill's content enters the agent's context — it gets prepended or appended to the agent's system prompt when the skill is invoked.
_Avoid_: Prompt injection (confusing with security context), context loading

**ReAct pattern**:
Reasoning + Acting interleaved. The foundational agent pattern that skills build on: the model reasons about what to do, acts (calls a tool), observes the result, then reasons again.
_Avoid_: Think-act-observe (older term)

**Zone of proximal development**:
The range of tasks a learner can do with guidance. Used in the teaching framework to determine what to teach next based on what the user already knows.
_Avoid_: Learning zone, comfort zone (these mean different things)

**Fluency strength**:
The ability to retrieve and use knowledge in the moment. Built through practice and spaced repetition.
_Avoid_: Short-term memory, quick recall

**Storage strength**:
Long-term retention of knowledge that persists even without recent use. Built through desirable difficulty, not just exposure.
_Avoid_: Long-term memory (too broad), retention
