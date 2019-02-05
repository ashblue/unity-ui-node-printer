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

        public NodeType type;

        [Tooltip("Set the node to purchased by default")]
        public bool purchased;
        
        [Tooltip("Failure to meet the required level automatically locks the skill")]
        public int requiredLevel;

        [Tooltip("This node will not be printed if it does not meet the required level")]
        public bool hideRequiredLevel;
    }
}