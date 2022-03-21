namespace Bongobin.HclParser.Parts;

public class EndBracketBlockHclPart : HclPart
{
    public EndBracketBlockHclPart(char character) : base(character)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return false; // Nothing other than the brace makes sense here.
    }
}