using System;
using System.Collections.Generic;

namespace CleverCrow.UiNodeBuilder {
    public class Node : INode {
        public List<INode> Children { get; } = new List<INode>();
        public string Name { get; set; }
        public Action<bool> OnClick { get; set; }

        public void AddChild (INode node) {
            Children.Add(node);
        }
    }
}