using System.Diagnostics;

namespace Bongobin.HclParser.Parts;

[DebuggerDisplay("}")]
public class EndBlockHclPart : HclPart
{
    public EndBlockHclPart(char character) : base(character)
    {
    }

    public override bool ShouldAppend(char character)
    {
        return false; // Nothing other than the brace makes sense here.
    }
}