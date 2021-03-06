﻿using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraph {
        public INode Root { get; } = new Node();
        
        public List<INode> Nodes { get; } = new List<INode>();

        public NodeGraph () {
            Root.Enable();
            Root.Purchase();
            Root.IsRoot = true;
        }

        public void AddNode (INode parent, INode node) {
            parent.AddChild(node);
        }
    }
}

