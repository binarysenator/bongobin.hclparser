namespace Bongobin.HclParser.Parts;

public class NewLineHclPart : HclPart
{
    public NewLineHclPart(char root) : base(root)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return character == '\r' || character == '\n';
    }
}