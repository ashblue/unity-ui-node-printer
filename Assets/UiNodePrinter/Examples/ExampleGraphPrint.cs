using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        private NodeGraph _graph;
        
        public NodeGraphPrinter printer;
        public NodeData root;
        
        [Header("Skill Points")]
        
        [SerializeField]
        private int _skillPoints = 2;
        public Text skillPointText;

        private void Start () {
            var graphBuilder = new NodeGraphBuilder();
            root.children.ForEach(n => {
                graphBuilder.Add(n.displayName, n.graphic);
                graphBuilder.OnClickNode((node) => {
                    if (!node.Purchased && _skillPoints > 0) {
                        _skillPoints -= 1;
                        node.Purchased = true;
                        UpdateSkillPoints();
                    }
                });
                graphBuilder.End();
            });

            _graph = graphBuilder.Build();
            printer.Build(_graph);

            UpdateSkillPoints();
        }

        private void UpdateSkillPoints () {
            skillPointText.text = $"Skill Points: {_skillPoints}";
            
            if (_skillPoints == 0) {
                _graph.Nodes.ForEach(n => n.Disable());
            }
        }
    }
}