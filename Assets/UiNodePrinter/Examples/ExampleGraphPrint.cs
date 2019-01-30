using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        public NodeGraphPrinter printer;
        public NodeData root;
        
        [Header("Skill Points")]
        public int skillPoints = 3;
        public Text skillPointText;
        
        private void Start () {
            var graph = new NodeGraphBuilder();
            root.children.ForEach(n => graph.Add(n.displayName, n.graphic, (node) => {
                if (!node.Purchased && skillPoints > 0) {
                    skillPoints -= 1;
                    node.Purchased = true;
                    UpdateSkillPointText();
                }
            }));
            printer.Build(graph.Build());

            UpdateSkillPointText();
        }

        private void UpdateSkillPointText () {
            skillPointText.text = $"Skill Points: {skillPoints}";
        }
    }
}