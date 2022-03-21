using Bongobin.HclParser.Nodes;

namespace Bongobin.HclParser;

public class VariableAssignmentCollection : KeyedNodeCollection<VariableAssignmentNode>
{
    public VariableAssignmentCollection(IEnumerable<VariableAssignmentNode> nodes) : base(nodes)
    {
    }
}