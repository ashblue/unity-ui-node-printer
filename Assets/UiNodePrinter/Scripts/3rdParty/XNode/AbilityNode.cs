namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    [CreateNodeMenuAttribute("Skill Tree/Ability")]
    [NodeTint(1f, 1f, 1f)]
    public class AbilityNode : SkillNode {
        public override NodeType NodeType => NodeType.Ability;
    }
}