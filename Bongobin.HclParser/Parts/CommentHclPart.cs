namespace Bongobin.HclParser.Parts;

public class CommentHclPart : HclPart
{
    public CommentHclPart(char root) : base(root)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return character != '\r' && character != '\n';
    }
}