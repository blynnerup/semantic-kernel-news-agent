using System.ComponentModel;
using Microsoft.SemanticKernel;
using SimpleFeedReader;

namespace The_News_Agent;

public class NewsPlugin
{
    [KernelFunction("get_news")]
    [Description("Gets news items for current date.")]
    [return: Description("A list of current news stories.")]
    public List<FeedItem> GetNews(Kernel kernel)
    {
        var reader = new FeedReader();

        return reader.RetrieveFeed($"https://www.berlingske.dk/content/66/rss")
            .Take(5)
            .ToList();
    }
}