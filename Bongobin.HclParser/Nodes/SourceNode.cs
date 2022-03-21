using Bongobin.HclParser;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser.Nodes;

public class SourceNode : BlockNode, INamed
{
    public SourceNode(HclPart rootPart) : base(rootPart) { }
    private QuotedTextHclPart? ResourceTypePart { get; set; }
    private QuotedTextHclPart? ResourceNamePart { get; set; }
    public string? ResourceType => ResourceTypePart?.Value;
    public string? ResourceName => ResourceNamePart?.Value;

    public override Node Handle(HclPart part)
    {
        if (part is QuotedTextHclPart textHclPart)
        {
            if (ResourceTypePart == null)
            {
                ResourceTypePart = textHclPart;
                return this;
            }
            else
            {
                if (ResourceNamePart == null)
                {
                    ResourceNamePart = textHclPart;
                    return this;
                }
                else
                {
                    throw new NotSupportedException("A resource node can only have a type and name part.");
                }
            }
        }

        if (part is TextHclPart textPart)
        {
            var variable = new VariableAssignmentNode(textPart)
            {
                Parent = this
            };
            return Add(variable);
        }

        return base.Handle(part);
    }

    public string Name => ResourceName ?? string.Empty;
}