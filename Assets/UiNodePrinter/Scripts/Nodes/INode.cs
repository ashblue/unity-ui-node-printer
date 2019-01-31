using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        string Name { get; set; }
        Sprite Graphic { get; set; }
        List<INode> Children { get; }
        Action<INode> OnClick { get; set; }
        bool Purchased { get; set; }
        
        UnityEvent OnPurchaseChange { get; }
        UnityEvent OnDisable { get; }

        void AddChild (INode node);
        void Disable ();
    }
}