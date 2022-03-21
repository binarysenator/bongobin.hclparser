namespace Bongobin.HclParser;

public class KeyedNodeCollection<TNode> where TNode : INamed
{
    private readonly IEnumerable<TNode> _nodes;

    public KeyedNodeCollection(IEnumerable<TNode> nodes)
    {
        _nodes = nodes;
    }

    public int Count => _nodes.Count();

    public TNode this[string name]
    {
        get
        {
            return _nodes.First(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}