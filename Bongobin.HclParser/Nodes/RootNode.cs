﻿using Bongobin.HclParser;
using Bongobin.HclParser.Parts;

namespace Bongobin.HclParser.Nodes;

public class RootNode : Node
{
    public RootNode()
    {
    }

    public IEnumerable<DataSourceNode> DataSourceNodes => Children.OfType<DataSourceNode>();
    public IEnumerable<ResourceSourceNode> ResourceNodes => Children.OfType<ResourceSourceNode>();
    public IEnumerable<VariableDeclarationNode> VariableNodes => Children.OfType<VariableDeclarationNode>();

    public ResourceCollection Resources => new ResourceCollection(ResourceNodes);

    public override Node Handle(HclPart part)
    {
        if (part is TextHclPart textPart)
        {
            if (part.Text == "data")
            {
                return Add(new DataSourceNode(part));
            }
            else if (part.Text == "resource")
            {
                return Add(new ResourceSourceNode(part));
            }
            else if (part.Text == "variable")
            {
                return Add(new VariableDeclarationNode(part));
            }
        }

        return base.Handle(part);
    }
}