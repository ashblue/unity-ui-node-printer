using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    public interface ISkillNode {
        string DisplayName { get; }
        string Description { get; }
        Sprite Graphic { get; }
        int Priority { get; }
        
        NodeType NodeType { get; }
        List<ISkillNode> Children { get; }
        
        bool IsPurchased { get; }
        bool IsGroup { get; }
        int RequiredLevel { get; }
        bool Hide { get; }
        ISkillNode GroupEnd { get; }

        List<ISkillNode> GetSortedChildren ();
    }
}