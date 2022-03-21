using Bongobin.HclParser.Nodes;

namespace Bongobin.HclParser;

public class ResourceCollection : KeyedNodeCollection<ResourceSourceNode>
{
    public ResourceCollection(IEnumerable<ResourceSourceNode> nodes) : base(nodes)
    {
    }
}