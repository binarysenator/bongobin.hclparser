using Bongobin.HclParser.Nodes;

namespace Bongobin.HclParser;

public class BlockCollection : KeyedNodeCollection<VariableAssignmentNode>
{
    public BlockCollection(IEnumerable<VariableAssignmentNode> nodes) : base(nodes)
    {
    }

    protected override IEnumerable<VariableAssignmentNode> Queryable => base.Queryable.Where(n => n.IsBlock);
}