using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lab04.Extensions.TagHelpers;

[HtmlTargetElement(Attributes = "generate-words")]
public class LoremIpsumTagHelper : TagHelper
{
    private static readonly Random _rnd = new Random();

    private static readonly string[] _words = new[]
    {
        "ac",
        "adipiscing",
        "aliquam",
        "amet",
        "amet",
        "ante",
        "arcu",
        "at",
        "auctor",
        "augue",
        "bibendum",
        "commodo",
        "congue",
        "consectetur",
        "convallis",
        "cras",
        "cum",
        "curabitur",
        "cursus",
        "diam",
        "dictum",
        "dignissim",
        "dis",
        "dolor",
        "donec",
        "duis",
        "efficitur",
        "egestas",
        "eget",
        "eleifend",
        "elementum",
        "elit",
        "enim",
        "erat",
        "eros",
        "est",
        "et",
        "etiam",
        "eu",
        "ex",
        "facilisis",
        "fames",
        "faucibus",
        "felis",
        "feugiat",
        "finibus",
        "fringilla",
        "fusce",
        "gravida",
        "habitant",
        "hendrerit",
        "iaculis",
        "imperdiet",
        "in",
        "integer",
        "interdum",
        "ipsum",
        "justo",
        "lacinia",
        "lacus",
        "laoreet",
        "lectus",
        "leo",
        "libero",
        "ligula",
        "lobortis",
        "lorem",
        "luctus",
        "maecenas",
        "magna",
        "magnis",
        "malesuada",
        "mattis",
        "mauris",
        "maximus",
        "metus",
        "mi",
        "molestie",
        "mollis",
        "montes",
        "morbi",
        "mus",
        "nam",
        "nascetur",
        "natoque",
        "nec",
        "neque",
        "netus",
        "nibh",
        "nisl",
        "non",
        "nulla",
        "nunc",
        "odio",
        "ornare",
        "parturient",
        "pellentesque",
        "penatibus",
        "pharetra",
        "phasellus",
        "placerat",
        "porta",
        "porttitor",
        "posuere",
        "praesent",
        "pretium",
        "primis",
        "proin",
        "quam",
        "quis",
        "quisque",
        "rhoncus",
        "ridiculus",
        "risus",
        "sagittis",
        "sapien",
        "scelerisque",
        "sed",
        "semper",
        "senectus",
        "sit",
        "sociis",
        "sodales",
        "suscipit",
        "suspendisse",
        "tellus",
        "tempor",
        "tempus",
        "tincidunt",
        "tortor",
        "tristique",
        "turpis",
        "ullamcorper",
        "ultrices",
        "ultricies",
        "urna",
        "ut",
        "varius",
        "vel",
        "velit",
        "venenatis",
        "vestibulum",
        "vitae",
        "vivamus",
        "viverra",
        "volutpat"
    };

    [HtmlAttributeName("generate-words")]
    public int Words { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (output.TagName == "lorem")
            output.SuppressOutput();
        output.Content.SetHtmlContent(GetWords());
    }

    private string GetWords()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < Words; i++)
        {
            sb.AppendFormat("{0} ", _words[_rnd.Next(_words.Length - 1)]);
        }
        sb[0] = char.ToUpper(sb[0]);
        return sb.ToString().TrimEnd() + ".";
    }
}