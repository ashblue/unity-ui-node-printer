using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class Node : INode {
        public Sprite Graphic { get; set; }
        public List<INode> Children { get; } = new List<INode>();
        public string Name { get; set; }
        public Action OnClick { get; set; }

        public void AddChild (INode node) {
            Children.Add(node);
        }
    }
}