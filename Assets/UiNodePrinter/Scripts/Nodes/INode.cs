using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        string Name { get; }
        string Description { get; set; }
        Sprite Graphic { get; set; }
        List<INode> Children { get; }
        bool Purchased { get; set; }
        bool Enabled { get; set; }
        bool IsPurchasable { get; }

        UnityEvent<INode> OnClick { get; }
        UnityEvent<INode> OnPurchase { get; }
        UnityEvent OnPurchaseChange { get; }
        UnityEvent OnDisable { get; }
        UnityEvent OnEnable { get; }
        Func<INode, bool> OnIsPurchasable { set; }

        void AddChild (INode node);
        void Disable ();
        void Enable ();
    }
}