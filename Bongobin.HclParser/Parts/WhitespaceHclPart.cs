namespace Bongobin.HclParser.Parts;

public class WhitespaceHclPart : HclPart
{
    public WhitespaceHclPart(char root) : base(root)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return character != '\r' && character != '\n' && char.IsWhiteSpace(character);
    }
}