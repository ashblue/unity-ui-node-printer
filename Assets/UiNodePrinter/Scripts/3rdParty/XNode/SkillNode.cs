using UnityEngine;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    [CreateNodeMenuAttribute("Skill Tree/Skill")]
    [NodeTint(1f, 1f, 0f)]
    public class SkillNode : SkillNodeBase {
        [Output(connectionType = ConnectionType.Override)]
        public Connection children;
        
        
        [Input]
        public Connection enter;
        
        [SerializeField] private string _displayName;
        [TextArea]
        [SerializeField] private string _description;
        [SerializeField] private Sprite _graphic;
        [SerializeField] private int _priority;
        [SerializeField] private bool _hide;
        
        [Header("Details")]
        [SerializeField] private bool _purchased;
        [SerializeField] private int _requiredLevel;

        public override string DisplayName => _displayName;
        public override string Description => _description;
        public override Sprite Graphic => _graphic;
        public override NodeType NodeType => NodeType.Skill;
        public override bool IsPurchased => _purchased;
        public override int RequiredLevel => _requiredLevel;
        public override bool Hide => _hide;
        public override int Priority => _priority;
    }
}