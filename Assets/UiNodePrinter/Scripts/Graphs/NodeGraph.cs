namespace CleverCrow.UiNodeBuilder {
    public class NodeGraph {
        public INode Root { get; } = new Node();

        public void AddNode (INode node) {
            Root.AddChild(node);
        }
    }
}

