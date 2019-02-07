using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    public interface ISkillNode {
        string DisplayName { get; }
        string Description { get; }
        Sprite Graphic { get; }
        
        NodeType NodeType { get; }
        List<ISkillNode> Children { get; }
        
        bool Purchased { get; }
        int RequiredLevel { get; }
        bool HideRequiredLevel { get; }
    }
}