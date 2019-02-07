using System;
using System.Collections.Generic;
using CleverCrow.UiNodeBuilder.ThirdParty.XNodes;
using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        private NodeGraph _graph;
        private Dictionary<ISkillNode, INode> _dataToNode = new Dictionary<ISkillNode, INode>();
        
        public NodeGraphPrinter printer;
        public SkillTreeGraph data;
        public NodeContext context;
        public Text pointText;
        
        [Header("Stats")]
        
        public int currentLevel = 1;
        public int skillPoints = 2;
        public int abilityPoints = 2;

        private void Start () {
            var graphBuilder = new NodeGraphBuilder();
            data.root.Children.ForEach(child => NodeRecursiveAdd(graphBuilder, child));

            _graph = graphBuilder.Build();
            printer.Build(_graph);

            UpdatePoints();
        }

        private void NodeRecursiveAdd (NodeGraphBuilder builder, ISkillNode data) {
            if (currentLevel < data.RequiredLevel && data.HideRequiredLevel) return;

            if (_dataToNode.ContainsKey(data)) {
                builder.AddExistingNode(_dataToNode[data]);
            } else {
                builder.Add(data.DisplayName, data.Description, data.Graphic)
                    .NodeType(data.NodeType)
                    .Purchased(data.Purchased)
                    .IsPurchasable(() => IsPurchasable(data))
                    .OnPurchase(() => { ChangePoints(data, -1); })
                    .OnRefund(() => { ChangePoints(data, 1); })
                    .IsLocked(() => currentLevel < data.RequiredLevel)
                    .LockedDescription(() => $"Level {data.RequiredLevel} is required.")
                    .OnClickNode((node) => context.Open(node));
                
                _dataToNode[data] = builder.Current;
                data.Children.ForEach(child => NodeRecursiveAdd(builder, child));
                builder.End();
            }
        }

        private void ChangePoints (ISkillNode data, int amount) {
            switch (data.NodeType) {
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

        private bool IsPurchasable (ISkillNode data) {
            switch (data.NodeType) {
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
            
            if (abilityPoints > 0 || skillPoints > 0) {
                _graph.Root.Enable();
            }
            
            if (abilityPoints == 0 || skillPoints == 0) {
                _graph.Nodes.ForEach(n => {
                    if (!n.IsPurchased && !n.IsPurchasable) n.Disable();
                });
            } 
        }
    }
}