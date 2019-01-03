using System;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphBuilder {
        private readonly NodeGraph _graph = new NodeGraph();
        
        public NodeGraph Build () {
            return _graph;
        }

        public NodeGraphBuilder Add (string name, Action<bool> onClick) {
            _graph.AddNode(new Node {
                Name = name,
                OnClick = onClick
            });

            return this;
        }
    }
}