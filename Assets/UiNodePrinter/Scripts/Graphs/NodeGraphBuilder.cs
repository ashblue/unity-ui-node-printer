using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphBuilder {
        private readonly NodeGraph _graph = new NodeGraph();
        private readonly Stack<INode> _pointer = new Stack<INode>();
        
        public Stack<INode> Pointer => _pointer;

        public NodeGraphBuilder () {
            _pointer.Push(_graph.Root);
        }
        
        public NodeGraph Build () {
            return _graph;
        }

        public NodeGraphBuilder Add (string name, Sprite graphic) {
            _pointer.Push(new Node {
                Name = name,
                Graphic = graphic,
            });
            
            _graph.AddNode(Pointer.Peek());
            _graph.Nodes.Add(Pointer.Peek());

            return this;
        }

        public NodeGraphBuilder OnClickNode (Action<INode> callback) {
            _pointer.Peek().OnClick = callback;
            
            return this;
        }

        public NodeGraphBuilder End () {
            _pointer.Pop();

            return this;
        }
    }
}