using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraph {
        public INode Root { get; } = new Node {
            Purchased = true,
        };
        
        public List<INode> Nodes { get; } = new List<INode>();

        public NodeGraph () {
            Root.Enable();
        }

        public void AddNode (INode parent, INode node) {
            parent.AddChild(node);
        }
    }
}

