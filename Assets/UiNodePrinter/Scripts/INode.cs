using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        List<INode> Children { get; }
        
        void AddChild (INode node);
    }
}