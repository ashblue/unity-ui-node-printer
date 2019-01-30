using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    [CreateAssetMenu(fileName = "Skill", menuName = "Skill Tree/Skill")]
    public class NodeData : ScriptableObject {
        public string displayName;
        public Sprite graphic;
        public List<NodeData> children;
    }
}