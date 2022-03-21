using Bongobin.HclParser;

namespace Bongobin.HclParser.Parts;

public class StartBracketBlockHclPart : HclPart
{
    public StartBracketBlockHclPart(char character) : base(character)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return character != ')'; // Nothing other than the brace makes sense here.
    }

    public TextHclPart AsTextPart()
    {
        return TextHclPart.From(this);
    }
}