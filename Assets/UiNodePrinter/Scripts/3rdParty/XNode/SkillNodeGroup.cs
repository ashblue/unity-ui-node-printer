namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    [NodeTint(0f, 1f, 1f)]
    [CreateNodeMenuAttribute("Skill Tree/Group")]
    public class SkillNodeGroup : SkillNodeBase {
        [Input]
        public Connection enter;

        [Output]
        public Connection children;
        
        [Output(connectionType = ConnectionType.Override)] 
        public Connection exit;

        public override bool IsGroup { get; } = true;
    }
}