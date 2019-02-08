using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphPrinter : MonoBehaviour {
        [Tooltip("Where nodes will be output in a Canvas")]
        [SerializeField]
        private RectTransform _nodeOutput;
        
        [SerializeField]
        private NodePrint _nodeSkillPrefab;
        
        [SerializeField]
        private NodePrint _nodeAbilityPrefab;

        [SerializeField]
        private NodeGroupPrint _nodeGroupPrefab;

        public void Build (NodeGraph graph) {
            foreach (var child in graph.Root.Children) {
                RecursivePrint(child, _nodeOutput);
            }
        }

        private void RecursivePrint (INode data, RectTransform output) {
            if (data.IsGroup) {
                var group = Instantiate(_nodeGroupPrefab, output);
                group.Setup(data);
                data.Children.ForEach(child => RecursivePrint(child, group.childOutput));
                if (data.ExitChild != null) RecursivePrint(data.ExitChild, group.exitOutput);
                return;
            }
            
            var node = Instantiate(GetPrefab(data), output);
            node.Setup(data);
            
            data.Children.ForEach(child => {
                if (!child.IsGroupExit) {
                    RecursivePrint(child, node.childOutput);
                }
            });
        }

        private NodePrint GetPrefab (INode data) {
            switch (data.NodeType) {
                case NodeType.Skill:
                    return _nodeSkillPrefab;
                case NodeType.Ability:
                    return _nodeAbilityPrefab;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}