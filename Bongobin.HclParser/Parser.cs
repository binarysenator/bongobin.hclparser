using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser;

public class Parser
{
    public HclDocument Parse(string input)
    {
        var document = new HclDocument();
        HclPart? context = null;

        foreach (var character in input)
        {
            if (context != null)
            {
                if (context.ShouldAppend(character))
                {
                    context.Append(character);
                }
                else
                {
                    context = CreateFrom(character);
                    document.Add(context);
                }
            }
            else
            {
                context = CreateFrom(character);
                document.Add(context);
            }
        }

        document.Build();
        return document;
    }

    private HclPart CreateFrom(char character)
    {
        if (character == '#')
        {
            return new CommentHclPart(character);
        }



        if (character == '\r')
        {
            return new NewLineHclPart(character);
        }

        if (char.IsWhiteSpace(character))
        {
            return new WhitespaceHclPart(character);
        }

        if (character == '=')
        {
            return new AssignmentHclPart(character);
        }

        if (character == '"')
        {
            return new QuotedTextHclPart(character);
        }

        if (character == '{')
        {
            return new StartBlockHclPart(character);
        }

        if (character == '}')
        {
            return new EndBlockHclPart(character);
        }

        if (character == '(')
        {
            return new StartBracketBlockHclPart(character);
        }

        if (character == ')')
        {
            return new EndBracketBlockHclPart(character);
        }

        //if (Char.IsLetterOrDigit(character))
        //{
        // Everything else is text?
        return new TextHclPart(character);
        //}


        //throw new NotImplementedException();
    }
}