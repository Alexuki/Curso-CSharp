using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Lab04.Extensions.TagHelpers;

[HtmlTargetElement("content-watcher")]
[HtmlTargetElement(Attributes="block-words")]
public class ContentWatcherTagHelper : TagHelper
{
    private string[] _words;
    public string BlockWords { get; set; }

    public override void Init(TagHelperContext context)
    {
        _words = BlockWords.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
        base.Init(context);
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (output.TagName.Equals("content-watcher", StringComparison.OrdinalIgnoreCase))
        {
            output.SuppressOutput();
        }
        var childContent = await output.GetChildContentAsync();
        string content = childContent.GetContent();
        for (var i = 0; i < _words.Length; i++)
        {
            var word = _words[i];
            content = content.Replace(word, new string('*', word.Length));
        }
        output.Content.SetContent(content);
    }
}