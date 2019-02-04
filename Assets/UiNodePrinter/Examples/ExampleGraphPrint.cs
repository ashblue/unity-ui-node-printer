using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        private NodeGraph _graph;
        
        public NodeGraphPrinter printer;
        public NodeData root;
        public NodeContext context;

        [SerializeField] 
        private int _currentLevel = 1;
        
        [Header("Skill Points")]
        
        [SerializeField]
        private int _skillPoints = 2;
        
        public Text skillPointText;

        private void Start () {
            var graphBuilder = new NodeGraphBuilder();
            root.children.ForEach(child => NodeRecursiveAdd(graphBuilder, child));

            _graph = graphBuilder.Build();
            printer.Build(_graph);

            UpdateSkillPoints();
        }

        private void NodeRecursiveAdd (NodeGraphBuilder builder, NodeData data) {
            if (_currentLevel < data.requiredLevel && data.hideRequiredLevel) return;
            
            builder.Add(data.displayName, data.description, data.graphic)
                .Purchased(data.purchased)
                .IsPurchasable(() => _skillPoints > 0)
                .OnPurchase(() => {
                    _skillPoints -= 1;
                    UpdateSkillPoints();
                })
                .OnRefund(() => {
                    _skillPoints += 1;
                    UpdateSkillPoints();
                })
                .IsLocked(() => _currentLevel < data.requiredLevel)
                .LockedDescription(() => $"Level {data.requiredLevel} is required.")
                .OnClickNode((node) => context.Open(node));
            data.children.ForEach(child => NodeRecursiveAdd(builder, child));
            builder.End();
        }

        private void UpdateSkillPoints () {
            skillPointText.text = $"Skill Points: {_skillPoints}";
            
            if (_skillPoints == 0) {
                _graph.Nodes.ForEach(n => {
                    if (!n.IsPurchased) n.Disable();
                });
            } else {
                _graph.Root.Enable();
            }
        }
    }
}