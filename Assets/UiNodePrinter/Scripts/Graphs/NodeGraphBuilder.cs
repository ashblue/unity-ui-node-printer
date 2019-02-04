using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            var parent = _pointer.Peek();
            var node = new Node {
                Name = name,
                Graphic = graphic,
            };

            node.Enabled = parent.Purchased;
            _graph.AddNode(Pointer.Peek(), node);
            _graph.Nodes.Add(node);
            _pointer.Push(node);

            return this;
        }

        public NodeGraphBuilder Add (string name, string description, Sprite graphic) {
            Add(name, graphic);
            _pointer.Peek().Description = description;
            
            return this;
        }

        public NodeGraphBuilder OnClickNode (UnityAction<INode> callback) {
            _pointer.Peek().OnClick.AddListener(callback);
            return this;
        }
        
        public NodeGraphBuilder OnPurchase (UnityAction<INode> callback) {
            _pointer.Peek().OnPurchase.AddListener(callback);
            return this;
        }

        public NodeGraphBuilder End () {
            _pointer.Pop();
            return this;
        }

        public NodeGraphBuilder Purchased (bool purchased) {
            _pointer.Peek().Purchased = purchased;
            return this;
        }

        public NodeGraphBuilder IsPurchasable (Func<INode, bool> callback) {
            _pointer.Peek().OnIsPurchasable = callback;
            return this;
        }
    }
}