using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraph {
        public INode Root { get; } = new Node();
        public List<INode> Nodes { get; } = new List<INode>();

        public void AddNode (INode node) {
            Root.AddChild(node);
        }
    }
}

