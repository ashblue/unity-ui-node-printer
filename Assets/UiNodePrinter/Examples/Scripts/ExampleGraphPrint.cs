using System;
using System.Collections.Generic;
using CleverCrow.UiNodeBuilder.ThirdParty.XNodes;
using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        private NodeGraph _graph;
        
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
            data.root.GetSortedChildren().ForEach(child => NodeRecursiveAdd(graphBuilder, child));

            _graph = graphBuilder.Build();
            printer.Build(_graph);

            UpdatePoints();
        }

        private void NodeRecursiveAdd (NodeGraphBuilder builder, ISkillNode data) {
            if (data.Hide) return;

            if (data.IsGroup) {
                builder.AddGroup();
                data.GetSortedChildren().ForEach(child => NodeRecursiveAdd(builder, child));

                if (data.GroupEnd != null) {
                    builder.EndGroup(data.GroupEnd.DisplayName, data.GroupEnd.Graphic);
                    SetNodeDetails(builder, data.GroupEnd);
                    data.GroupEnd.GetSortedChildren().ForEach(child => NodeRecursiveAdd(builder, child));
                } 
                
                builder.End();
            } else {
                builder.Add(data.DisplayName, data.Graphic);
                SetNodeDetails(builder, data);
                
                data.GetSortedChildren().ForEach(child => NodeRecursiveAdd(builder, child));
                builder.End();
            }
        }

        private void SetNodeDetails (NodeGraphBuilder builder, ISkillNode data) {
            builder.Description(data.Description)
                .NodeType(data.NodeType)
                .Purchased(data.IsPurchased)
                .IsPurchasable(() => IsPurchasable(data))
                .OnPurchase(() => { ChangePoints(data, -1); })
                .OnRefund(() => { ChangePoints(data, 1); })
                .IsLocked(() => currentLevel < data.RequiredLevel)
                .LockedDescription(() => $"Level {data.RequiredLevel} is required.")
                .OnClickNode((node) => context.Open(node));
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