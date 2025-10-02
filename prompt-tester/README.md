# Prompt Tester

**Created**: 2025-10-02  
**Time Spent**: 1 hour  
**Phase**: 1, Week 1, Day 2  
**Status**: ✅ Complete

## Purpose

Compare how different prompt styles affect Claude's responses.

## What I Learned

- Prompt engineering dramatically changes responses
- "ELI5" produces simpler language
- Technical prompts get more detailed answers
- Same question, totally different outputs!

## How to Run

Interactive mode (will prompt you for question):
```bash
cd PromptTester

dotnet run

OR

dotnet run "What is a neural network?"
```

## Observations from Testing
Question Tested: "What is a GPT"

**Direct Response**

Gave a balanced explanation with structure
Used headers and bullet points
Listed GPT versions chronologically
Middle ground between simple and technical

**ELI5 Prompt Response**

Used metaphors ("robot friend who read every book")
Extremely simple vocabulary
Relatable comparisons (phone autocomplete)
Avoided all technical jargon
Made it concrete and imaginative

**Technical Prompt Response**

Included actual code examples
Used proper ML terminology (tokens, unsupervised learning, pre-training)
More formal academic structure
Assumed technical background

## Key Learnings
**Prompt Engineering is Powerful:**

The same question produced dramatically different responses
Prompt framing controls vocabulary, depth, and approach
"Show, don't tell" - asking for "technical with examples" actually gave code

**Context Matters:**

"ELI5" triggered simple language and metaphors
"Technical explanation with examples" triggered formal terminology
The model adapts its entire communication style based on the prompt

**Practical Application:**

For learning: Use ELI5 prompts
For documentation: Use direct prompts
For implementation: Use technical prompts with examples
Match prompt style to your audience

**Limitations Noticed:**

All responses cut off at 200 tokens (max_tokens setting)
Would need higher limit for complete answers
Cost vs completeness tradeoff

**Next Experiments to Try**

- [ ] Test with different max_tokens values (500, 1000)
- [ ] Add a "concise" prompt style (under 50 words)
 - [ ] Test with code-related questions
 - [ ] Compare different temperature settings
 - [ ] Try few-shot examples (provide examples in prompt)