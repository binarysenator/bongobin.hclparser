using Bongobin.HclParser;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser.Nodes;

public class BlockNode : Node
{
    private StartBlockHclPart? StartBlockPart { get; set; }
    private EndBlockHclPart? EndBlockPart { get; set; }

    public BlockNode(HclPart root) : base(root)
    {

    }

    public IEnumerable<VariableAssignmentNode> VariableAssignments => Children.OfType<VariableAssignmentNode>();
    public VariableAssignmentCollection Variables => new VariableAssignmentCollection(VariableAssignments);
    public BlockCollection Blocks => new BlockCollection(VariableAssignments);

    public override Node Handle(HclPart part)
    {
        if (part is StartBlockHclPart startBlockPart)
        {
            if (StartBlockPart != null)
            {
                throw new NotSupportedException();
            }
            else
            {
                StartBlockPart = startBlockPart;
                return this;
            }
        }

        if (part is EndBlockHclPart endBlockPart)
        {
            if (StartBlockPart == null)
            {
                // Can't have an end block before a start block
                throw new NotSupportedException();
            }
            else
            {
                if (EndBlockPart != null)
                {
                    throw new NotSupportedException("Can't have another end block.");
                }
                else
                {
                    EndBlockPart = endBlockPart;
                    return this.Parent ?? this;
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
}