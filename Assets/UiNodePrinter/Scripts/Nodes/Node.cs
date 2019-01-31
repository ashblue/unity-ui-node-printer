using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public class Node : INode {
        private bool _purchased;
        
        public Sprite Graphic { get; set; }
        public List<INode> Children { get; } = new List<INode>();
        public string Name { get; set; }
        public Action<INode> OnClick { get; set; }

        public bool Purchased {
            get => _purchased;
            set {
                _purchased = value;
                OnPurchaseChange.Invoke();
            }
        }

        public UnityEvent OnPurchaseChange { get; } = new UnityEvent();
        public UnityEvent OnDisable { get; } = new UnityEvent();

        public void AddChild (INode node) {
            Children.Add(node);
        }

        public void Disable () {
            if (!Purchased) {
                OnDisable.Invoke();
            }
        }
    }
}