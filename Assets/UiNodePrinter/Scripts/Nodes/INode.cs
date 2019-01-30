using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public interface INode {
        string Name { get; set; }
        Sprite Graphic { get; set; }
        List<INode> Children { get; }
        Action OnClick { get; set; }

        void AddChild (INode node);
    }
}