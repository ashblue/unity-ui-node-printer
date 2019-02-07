using UnityEngine;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    [CreateNodeMenuAttribute("Skill Tree/Skill")]
    [NodeTint(1f, 1f, 0f)]
    public class SkillNode : SkillNodeBase {
        [Input]
        public Connection enter;
        
        [SerializeField] private string _displayName;
        [TextArea]
        [SerializeField] private string _description;
        [SerializeField] private Sprite _graphic;
        [SerializeField] private bool _purchased;
        [SerializeField] private int _requiredLevel;
        [SerializeField] private bool _hideRequiredLevel;

        public override string DisplayName => _displayName;
        public override string Description => _description;
        public override Sprite Graphic => _graphic;
        public override NodeType NodeType => NodeType.Skill;
        public override bool Purchased => _purchased;
        public override int RequiredLevel => _requiredLevel;
        public override bool HideRequiredLevel => _hideRequiredLevel;
    }
}