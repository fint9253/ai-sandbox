using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("🧪 Prompt Tester - Compare Different Prompt Styles");
Console.WriteLine("===================================================\n");

var apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("❌ Error: ANTHROPIC_API_KEY not set!");
    return;
}

// Get question from user
string baseQuestion;
if (args.Length > 0)
{
    // Use command line argument if provided
    baseQuestion = string.Join(" ", args);
}
else
{
    // Ask user interactively
    Console.Write("Enter your question: ");
    baseQuestion = Console.ReadLine() ?? "What is machine learning?";
}

if (string.IsNullOrWhiteSpace(baseQuestion))
{
    baseQuestion = "What is machine learning?";
}

// Three different prompt styles
var prompts = new Dictionary<string, string>
{
    ["Direct"] = baseQuestion,
    
    ["ELI5"] = $"Explain like I'm 5 years old: {baseQuestion}",
    
    ["Technical"] = $"Provide a technical explanation with examples: {baseQuestion}"
};

Console.WriteLine($"\nBase Question: \"{baseQuestion}\"\n");
Console.WriteLine("Testing 3 different prompt styles...\n");
Console.WriteLine("═══════════════════════════════════════════════════\n");

using var client = new HttpClient();
client.DefaultRequestHeaders.Add("x-api-key", apiKey);
client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

foreach (var (style, prompt) in prompts)
{
    Console.WriteLine($"📝 Style: {style}");
    Console.WriteLine($"Prompt: \"{prompt}\"\n");
    
    var requestBody = new
    {
        model = "claude-sonnet-4-5-20250929",
        max_tokens = 200,
        messages = new[]
        {
            new { role = "user", content = prompt }
        }
    };

    try
    {
        var response = await client.PostAsJsonAsync(
            "https://api.anthropic.com/v1/messages",
            requestBody
        );

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            var text = result
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();
            
            Console.WriteLine($"Response:\n{text}\n");
        }
        else
        {
            Console.WriteLine($"❌ Error: {response.StatusCode}\n");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Exception: {ex.Message}\n");
    }
    
    Console.WriteLine("═══════════════════════════════════════════════════\n");
}

Console.WriteLine("🎉 Experiment complete!");
Console.WriteLine("\nNotice how different prompt styles produce different responses!");