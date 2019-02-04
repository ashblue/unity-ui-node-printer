using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    [CreateAssetMenu(fileName = "Skill", menuName = "Skill Tree/Skill")]
    public class NodeData : ScriptableObject {
        public string displayName;
        
        [TextArea]
        public string description;
        
        public Sprite graphic;
        public List<NodeData> children;
        
        [Tooltip("Failure to meet the required level automatically locks the skill")]
        public int requiredLevel;
    }
}
