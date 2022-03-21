using System.Diagnostics;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser;

[DebuggerDisplay("TextBlock: {Text}")]
public class TextHclPart : HclPart
{
    private TextHclPart() : base()
    {
    }

    public TextHclPart(char character) : base(character)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return char.IsLetterOrDigit(character) || character == '_' || character == '-' || character == '$' || character == '.'
               || character == ',' || character == '-' || character == '\"' || character == '[' || character == ']';
    }

    public static TextHclPart From(StartBracketBlockHclPart startBracketBlockHclPart)
    {
        return new TextHclPart();
    }

    public void AppendPart(StartBracketBlockHclPart startPart)
    {
        throw new NotImplementedException();
    }
}