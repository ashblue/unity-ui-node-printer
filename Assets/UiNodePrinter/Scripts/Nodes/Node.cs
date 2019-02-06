using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public class Node : INode {
        private class UnityEventNode : UnityEvent<INode> {}

        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Graphic { get; set; }
        public List<INode> Children { get; } = new List<INode>();
        public bool IsPurchasable => !IsPurchased && OnIsPurchasable() && !IsLocked;
        public bool IsLocked => OnIsLocked();
        public bool IsEnabled { get; private set; }
        public bool IsPurchased { get; private set; }

        public UnityEvent<INode> OnClick { get; } = new UnityEventNode();
        public UnityEvent OnPurchase { get; } = new UnityEvent();
        public UnityEvent OnDisable { get; } = new UnityEvent();
        public UnityEvent OnEnable { get; } = new UnityEvent();
        public UnityEvent OnRefund { get; } = new UnityEvent();
        public NodeType NodeType { get; set; }
        public List<INode> Parents { get; } = new List<INode>();
        public bool IsRoot { get; set; }
        public Func<bool> OnIsPurchasable { private get; set; } = () => true;
        public Func<bool> OnIsLocked { private get; set; } = () => false;
        public Func<string> GetLockedDescription { get; set; }

        public void Purchase () {
            if (IsPurchased) return;

            IsPurchased = true;
            OnPurchase.Invoke();
            EnableChildren();
        }
        
        public void AddChild (INode node) {
            Children.Add(node);
        }

        public void Disable () {
            IsEnabled = false;
            OnDisable.Invoke();
        }

        public void Enable () {
            if (IsLocked) return;

            IsEnabled = true;
            if (IsPurchased) {
                Children.ForEach(child => child.Enable());
            }
            
            OnEnable.Invoke();
        }

        public void Refund () {
            if (!IsPurchased) return;

            IsPurchased = false;
            Children.ForEach(child => {
                child.Refund();
                child.Disable();
            });
            OnRefund.Invoke();
        }

        private void EnableChildren () {
            Children.ForEach(c => {
                if (c.IsPurchasable) c.Enable();
            });
        }
    }
}