using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public class Node : INode {
        public List<INode> Children { get; } = new List<INode>();

        public void AddChild (INode node) {
            Children.Add(node);
        }
    }
}