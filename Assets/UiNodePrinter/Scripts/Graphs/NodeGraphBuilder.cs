using System;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphBuilder {
        private readonly NodeGraph _graph = new NodeGraph();
        
        public NodeGraph Build () {
            return _graph;
        }

        public NodeGraphBuilder Add (string name, Sprite graphic, Action<INode> onClick) {
            _graph.AddNode(new Node {
                Name = name,
                Graphic = graphic,
                OnClick = onClick,
            });

            return this;
        }
    }
}