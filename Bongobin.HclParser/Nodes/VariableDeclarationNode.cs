using Bongobin.HclParser;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser.Nodes;

public class VariableDeclarationNode : BlockNode
{
    private TextHclPart? NamePart { get; set; }
    public string Type => Children.OfType<VariableAssignmentNode>().FirstOrDefault(n => n.Name == "type")?.Value ?? string.Empty;
    public string Description => Children.OfType<VariableAssignmentNode>().FirstOrDefault(n => n.Name == "description")?.Value ?? string.Empty;

    public VariableDeclarationNode(HclPart part) : base(part)
    {
    }

    public override Node Handle(HclPart part)
    {
        if (part is QuotedTextHclPart textHclPart)
        {
            if (NamePart == null)
            {
                NamePart = textHclPart;
                return this;
            }
            else
            {
                throw new NotSupportedException("A variable declaration node can only have one name.");
            }
        }

        if (part is EndBlockHclPart endBlockHclPart)
        {
            return this.Parent ?? this;
        }

        return base.Handle(part);
    }
}