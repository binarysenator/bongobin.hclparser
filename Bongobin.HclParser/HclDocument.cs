using System.Text;
using Bongobin.HclParser.Nodes;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser;

public class HclDocument
{
    private readonly List<HclPart> _parts = new List<HclPart>();

    public static HclDocument Parse(string raw)
    {
        var parser = new Parser();
        return parser.Parse(raw);
    }

    public HclDocument()
    {
        Root = new RootNode();
    }

    public RootNode Root { get; }
    public IEnumerable<HclPart> Parts => _parts;
    public string Text
    {
        get
        {
            var builder = new StringBuilder();
            _parts.ForEach(p => builder.Append(p.Text));
            return builder.ToString();
        }
    }


    public void Add(HclPart context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        _parts.Add(context);
    }

    /// <summary>
    /// Put together all parts and decide if the document is valid and well structured.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Build()
    {
        Node parent = Root;
        var partIndex = 0;

        foreach (var part in Parts)
        {
            parent = parent.Handle(part);
            partIndex++;
        }
    }
}