using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        string Name { get; }
        string Description { get; set; }
        Func<string> GetLockedDescription { get; set; }
        NodeType NodeType { get; set; }
        List<INode> Parents { get; }
        Sprite Graphic { get; set; }
        List<INode> Children { get; }
        bool IsPurchased { get; }
        bool IsEnabled { get; }
        bool IsPurchasable { get; }
        bool IsLocked { get; }
        bool IsRoot { get; set; }
        bool IsGroup { get; }

        UnityEvent<INode> OnClick { get; }
        UnityEvent OnPurchase { get; }
        UnityEvent OnDisable { get; }
        UnityEvent OnEnable { get; }
        Func<bool> OnIsPurchasable { set; }
        Func<bool> OnIsLocked { set; }
        UnityEvent OnRefund { get; }
        INode ExitChild { get; set; }
        bool IsGroupExit { get; set; }


        void AddChild (INode node);
        void Disable ();
        void Enable ();
        void Refund ();
        void Purchase ();
    }
}