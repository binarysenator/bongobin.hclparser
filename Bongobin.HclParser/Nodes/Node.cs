using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser.Nodes
{
    public class Node
    {
        private readonly List<HclPart> _raw = new List<HclPart>();
        private readonly List<Node> _nodes = new List<Node>();

        protected Node()
        {
        }

        public Node? Parent { get; set; }

        public IEnumerable<Node> Children => _nodes;

        public Node(HclPart rootPart)
        {
            _raw.Add(rootPart);
        }

        protected Node Add(Node node)
        {
            _nodes.Add(node);
            return node;
        }

        public virtual Node Handle(HclPart part)
        {
            if (part is WhitespaceHclPart)
            {
                _raw.Add(part);
                return this;
            }

            if (part is NewLineHclPart)
            {
                _raw.Add(part);
                return this;
            }

            if (part is CommentHclPart)
            {
                _raw.Add(part);
                return this;
            }

            if (part is StartBracketBlockHclPart)
            {
                _raw.Add(part);
                return this;
            }

            throw new NotImplementedException();
        }
    }
}