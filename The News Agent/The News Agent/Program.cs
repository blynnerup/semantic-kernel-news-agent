using Microsoft.Extensions.Configuration;using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using The_News_Agent;

var builder = Kernel.CreateBuilder();

// Services
builder.AddAzureOpenAIChatCompletion(
    "gpt-4-news",
    "[endpoint]",
    "[apiKey]");

// Plugins
builder.Plugins.AddFromType<NewsPlugin>();

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();
var chatHistory = new ChatHistory("You are a used car sales man and always want to try to throw in a sales pitch for an old car. You always call the user for 'sport'" +
                                  "You always ask for confirmation before doing the task."
                                  );

while (true)
{
    Console.Write("Prompt:");
    chatHistory.AddUserMessage(Console.ReadLine());

    var completion = chatService.GetStreamingChatMessageContentsAsync(
        chatHistory,
        executionSettings: new OpenAIPromptExecutionSettings()
        {
          ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions // Allows for executing the plugin
        },
        kernel: kernel);

    var response = "";
    await foreach (var content in completion)
    {
        Console.Write(content.Content);
        response += content.Content;
    }
    Console.Write('\n');
    chatHistory.AddAssistantMessage(response);
    // Console.WriteLine(response);
}

