using XNode;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes {
    [CreateNodeMenuAttribute("Skill Tree/Root")]
    [NodeTint(0f, 1f, 0f)]
    public class RootNode : SkillNodeBase {
        [Output(connectionType = ConnectionType.Multiple)]
        public Connection children;
    }
}
