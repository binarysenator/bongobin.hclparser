using System.Text;

namespace Bongobin.HclParser.Parts;

public abstract class HclPart
{
    private readonly StringBuilder _builder = new StringBuilder();

    protected HclPart()
    {
    }

    protected HclPart(char root)
    {
        Append(root);
    }

    public string Text => _builder.ToString();

    public void Append(char character)
    {
        _builder.Append(character);
    }

    public virtual bool ShouldAppend(char character)
    {
        return true;
    }
}