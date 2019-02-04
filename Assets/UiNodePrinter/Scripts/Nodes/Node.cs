using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public class Node : INode {
        private class UnityEventNode : UnityEvent<INode> {}
        
        private bool _purchased;

        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Graphic { get; set; }
        public List<INode> Children { get; } = new List<INode>();
        public bool IsPurchasable => !Purchased && OnIsPurchasable(this);
        public bool Enabled { get; set; }

        public bool Purchased {
            get => _purchased;
            set {
                var oldValue = _purchased;
                _purchased = value;
                
                if (oldValue) return;
                OnPurchase.Invoke(this);
                EnableChildren();
            }
        }

        public UnityEvent<INode> OnClick { get; } = new UnityEventNode();
        public UnityEvent<INode> OnPurchase { get; } = new UnityEventNode();
        public UnityEvent OnDisable { get; } = new UnityEvent();
        public UnityEvent OnEnable { get; } = new UnityEvent();
        public Func<INode, bool> OnIsPurchasable { private get; set; } = (node) => true;

        public void AddChild (INode node) {
            Children.Add(node);
        }

        public void Disable () {
            OnDisable.Invoke();
        }

        public void Enable () {
            OnEnable.Invoke();
        }

        private void EnableChildren () {
            Children.ForEach(c => {
                if (c.IsPurchasable) c.Enable();
            });
        }
    }
}