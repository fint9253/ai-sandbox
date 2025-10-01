using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("🤖 Hello Claude - First API Call Experiment");
Console.WriteLine("=============================================\n");

// Read from environment variable
var apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("❌ Error: ANTHROPIC_API_KEY environment variable not set!");
    Console.WriteLine("\nHow to set it:");
    Console.WriteLine("PowerShell: $env:ANTHROPIC_API_KEY=\"your-key-here\"");
    Console.WriteLine("\nThen run 'dotnet run' again.");
    return;
}

using var client = new HttpClient();
client.DefaultRequestHeaders.Add("x-api-key", apiKey);
client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

var requestBody = new
{
    model = "claude-sonnet-4-5-20250929",
    max_tokens = 1024,
    messages = new[]
    {
        new { role = "user", content = "Explain what you are in one sentence." }
    }
};

try
{
    Console.WriteLine("📡 Calling Claude API...\n");
    
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
        
        Console.WriteLine("✅ Success!\n");
        Console.WriteLine($"Claude says:\n\"{text}\"\n");
    }
    else
    {
        Console.WriteLine($"❌ Error: {response.StatusCode}");
        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(errorContent);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Exception: {ex.Message}");
}

Console.WriteLine("\n🎉 Experiment complete!");