using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphBuilder {
        private readonly NodeGraph _graph = new NodeGraph();
        private readonly Stack<INode> _pointer = new Stack<INode>();
        
        public Stack<INode> Pointer => _pointer;
        public INode Current => _pointer.Peek();

        public NodeGraphBuilder () {
            _pointer.Push(_graph.Root);
        }
        
        public NodeGraph Build () {
            return _graph;
        }

        public NodeGraphBuilder Add (string name, Sprite graphic) {
            var parent = Current;
            var node = new Node {
                Name = name,
                Graphic = graphic,
            };

            node.Parents.Add(parent);
            if (parent.IsPurchased) {
                node.Enable();
            }
            
            _graph.AddNode(Pointer.Peek(), node);
            _graph.Nodes.Add(node);
            _pointer.Push(node);

            return this;
        }

        public NodeGraphBuilder Description (string text) {
            Current.Description = text;
            return this;
        }

        public NodeGraphBuilder OnClickNode (UnityAction<INode> callback) {
            Current.OnClick.AddListener(callback);
            return this;
        }
        
        public NodeGraphBuilder OnPurchase (UnityAction callback) {
            Current.OnPurchase.AddListener(callback);
            return this;
        }

        public NodeGraphBuilder End () {
            _pointer.Pop();
            return this;
        }

        public NodeGraphBuilder EndGroup (string name, Sprite graphic) {
            var group = _pointer.Pop();
            
            Add(name, graphic);
            group.ExitChild = Current;
            Current.Parents[0].Children.Remove(Current);
            Current.IsGroupExit = true;
            Current.Parents.Clear();
            Current.Parents.Add(group);
            RecursiveEndChildInjector(group.Children, Current);
            
            return this;
        }

        private void RecursiveEndChildInjector (List<INode> children, INode end) {
            foreach (var child in children) {
                if (child == end) continue;
                if (child.Children.Count > 0) {
                    RecursiveEndChildInjector(child.Children, end);
                    continue;
                }
                
                child.Children.Add(end);
            }
        }

        public NodeGraphBuilder Purchased (bool purchase) {
            if (!purchase) return this;
            Current.Purchase();
            
            return this;
        }

        public NodeGraphBuilder IsPurchasable (Func<bool> callback) {
            Current.OnIsPurchasable = callback;
            return this;
        }

        public NodeGraphBuilder IsLocked (Func<bool> callback) {
            Current.OnIsLocked = callback;
            return this;
        }

        public NodeGraphBuilder LockedDescription (Func<string> callback) {
            Current.GetLockedDescription = callback;
            return this;
        }

        public NodeGraphBuilder OnRefund (UnityAction callback) {
            Current.OnRefund.AddListener(callback);
            return this;
        }

        public NodeGraphBuilder NodeType (NodeType type) {
            Current.NodeType = type;
            return this;
        }

        public NodeGraphBuilder AddGroup () {
            var parent = Current;
            var node = new Node {
                IsGroup = true,
            };

            node.Parents.Add(parent);
            if (parent.IsPurchased) {
                node.Enable();
            }
            
            _graph.AddNode(Pointer.Peek(), node);
            _graph.Nodes.Add(node);
            _pointer.Push(node);

            return this;
        }
    }
}