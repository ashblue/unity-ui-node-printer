using System;
using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        List<INode> Children { get; }
        string Name { get; set; }
        Action<bool> OnClick { get; set; }

        void AddChild (INode node);
    }
}