using Bongobin.HclParser;

namespace Bongobin.HclParser.Parts;

public class QuotedTextHclPart : TextHclPart
{
    public QuotedTextHclPart(char character) : base(character)
    {
    }

    public string? Value => Text?.Trim('"');

    public override bool ShouldAppend(char character)
    {
        return character == '"' || base.ShouldAppend(character) || character == '{' || character == '}';
    }
}