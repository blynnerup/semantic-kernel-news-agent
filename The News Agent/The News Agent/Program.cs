using Microsoft.Extensions.Configuration;using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = Kernel.CreateBuilder();

// Services
builder.AddAzureOpenAIChatCompletion(
    "gpt-4-news",
    "[endpoint]",
    "[apiKey]");

// Plugins

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();
var chatHistory = new ChatHistory();

while (true)
{
    Console.Write("Prompt:");
    chatHistory.AddUserMessage(Console.ReadLine());

    var completion = chatService.GetStreamingChatMessageContentsAsync(chatHistory, kernel: kernel);

    var response = "";
    await foreach (var content in completion)
    {
        Console.Write(content.Content);
        response += content.Content;
    }
    
    chatHistory.AddAssistantMessage(response);
    Console.WriteLine(response);
}

