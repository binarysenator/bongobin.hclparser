namespace Bongobin.HclParser.Parts;

public class AssignmentHclPart : HclPart
{
    public AssignmentHclPart(char character) : base(character)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return false; // Nothing other than the equals makes sense here.
    }
}