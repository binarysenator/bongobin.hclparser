using Bongobin.HclParser;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser.Nodes;

public class VariableAssignmentNode : Node, INamed
{
    private TextHclPart? NamePart { get; set; }
    private AssignmentHclPart? AssignmentPart { get; set; }
    private readonly List<HclPart> _valueParts = new List<HclPart>();

    public VariableAssignmentNode(TextHclPart part) : base(part)
    {
        NamePart = part;
    }

    public override Node Handle(HclPart part)
    {
        if (part is AssignmentHclPart assignment)
        {
            if (AssignmentPart == null)
            {
                AssignmentPart = assignment;
                return this;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        if (part is TextHclPart valuePart)
        {
            _valueParts.Add(valuePart);
            return this;
        }

        // The variable assignment is actually a block.
        if (part is StartBlockHclPart startBlock)
        {
            // Change this type as this is actually a block?
            _valueParts.Add(startBlock);

            return this;
        }

        if (part is EndBlockHclPart endBlock)
        {
            // Change this type as this is actually a block?
            _valueParts.Add(endBlock);
            return Parent ?? this;
        }

        if (part is StartBracketBlockHclPart startPart)
        {
            _valueParts.Add(startPart);
            return this;
        }

        if (part is EndBracketBlockHclPart endPart)
        {
            _valueParts.Add(endPart);
            return this;
        }

        if (part is NewLineHclPart)
        {
            return Parent ?? this; // Fishy
        }

        //if (part is EndBracketBlockHclPart)
        //{
        //    return this.Parent ?? this; // Fishy
        //}

        return base.Handle(part);
    }

    public string Name => NamePart?.Text ?? string.Empty;
    public string? Value => string.Join("", _valueParts.Select(v => v.Text));
}