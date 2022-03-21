namespace Bongobin.HclParser;

public class KeyedNodeCollection<TNode> where TNode : INamed
{
    private readonly IEnumerable<TNode> _nodes;

    public KeyedNodeCollection(IEnumerable<TNode> nodes)
    {
        _nodes = nodes;
    }

    public int Count => _nodes.Count();

    protected virtual IEnumerable<TNode> Queryable => _nodes;

    public TNode this[string name]
    {
        get
        {
            return Queryable.First(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}