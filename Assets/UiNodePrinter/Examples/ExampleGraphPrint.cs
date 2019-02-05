using System;
using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        private NodeGraph _graph;
        
        public NodeGraphPrinter printer;
        public NodeData root;
        public NodeContext context;
        public Text pointText;
        
        [Header("Stats")]
        
        public int currentLevel = 1;
        public int skillPoints = 2;
        public int abilityPoints = 2;

        private void Start () {
            var graphBuilder = new NodeGraphBuilder();
            root.children.ForEach(child => NodeRecursiveAdd(graphBuilder, child));

            _graph = graphBuilder.Build();
            printer.Build(_graph);

            UpdatePoints();
        }

        private void NodeRecursiveAdd (NodeGraphBuilder builder, NodeData data) {
            if (currentLevel < data.requiredLevel && data.hideRequiredLevel) return;
            
            builder.Add(data.displayName, data.description, data.graphic)
                .NodeType(data.type)
                .Purchased(data.purchased)
                .IsPurchasable(() => IsPurchasable(data))
                .OnPurchase(() => { ChangePoints(data, -1); })
                .OnRefund(() => { ChangePoints(data, 1); })
                .IsLocked(() => currentLevel < data.requiredLevel)
                .LockedDescription(() => $"Level {data.requiredLevel} is required.")
                .OnClickNode((node) => context.Open(node));
            data.children.ForEach(child => NodeRecursiveAdd(builder, child));
            builder.End();
        }

        private void ChangePoints (NodeData data, int amount) {
            switch (data.type) {
                case NodeType.Skill:
                    skillPoints += amount;
                    break;
                case NodeType.Ability:
                    abilityPoints += amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdatePoints();
        }

        private bool IsPurchasable (NodeData data) {
            switch (data.type) {
                case NodeType.Skill:
                    return skillPoints > 0;
                case NodeType.Ability:
                    return abilityPoints > 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdatePoints () {
            pointText.text = $"Ability Points: {abilityPoints}; Skill Points: {skillPoints}";
            
            if (skillPoints == 0) {
                _graph.Nodes.ForEach(n => {
                    if (!n.IsPurchased) n.Disable();
                });
            } else {
                _graph.Root.Enable();
            }
        }
    }
}