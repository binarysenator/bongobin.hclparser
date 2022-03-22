using Bongobin.HclParser.Nodes;

namespace Bongobin.HclParser;

public class VariableAssignmentCollection : KeyedNodeCollection<VariableAssignmentNode>
{
    public VariableAssignmentCollection(IEnumerable<VariableAssignmentNode> nodes) : base(nodes)
    {
    }

    protected override IEnumerable<VariableAssignmentNode> Queryable => base.Queryable.Where(n => !n.IsBlock);
}