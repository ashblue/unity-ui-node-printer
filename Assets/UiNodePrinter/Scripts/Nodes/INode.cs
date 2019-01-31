using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        string Name { get; set; }
        Sprite Graphic { get; set; }
        List<INode> Children { get; }
        bool Purchased { get; set; }
        bool Enabled { get; set; }

        Action<INode> OnClick { get; set; }
        UnityEvent OnPurchaseChange { get; }
        UnityEvent OnDisable { get; }
        UnityEvent OnEnable { get; }

        void AddChild (INode node);
        void Disable ();
        void Enable ();
    }
}