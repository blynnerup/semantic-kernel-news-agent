using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace The_News_Agent;

public class AchivePlugin
{
    [KernelFunction("archive_data")]
    [Description("Save data to a file on your computer.")]
    public async Task WriteData(Kernel kernel, string fileName, string data)
    {
        await File.WriteAllTextAsync(
            $@"D:\Source\Courses\Semantic Kernel\News Agent\semantic-kernel-news-agent\The News Agent\The News Agent\{fileName}.txt",
            data);
    }
}