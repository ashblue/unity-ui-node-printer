using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphPrinter : MonoBehaviour {
        private List<NodePrint> _nodes = new List<NodePrint>();
        private HashSet<INode> _printedNodes = new HashSet<INode>();

        [Tooltip("Where nodes will be output in a Canvas")]
        [SerializeField]
        private RectTransform _nodeOutput;
        
        [SerializeField]
        private NodePrint _nodeSkillPrefab;
        
        [SerializeField]
        private NodePrint _nodeAbilityPrefab;

        public void Build (NodeGraph graph) {
            foreach (var child in graph.Root.Children) {
                RecursivePrint(child, _nodeOutput);
            }

            StartCoroutine(PostProcess());
        }

        private IEnumerator PostProcess () {
            yield return null;
            
            foreach (var node in _nodes) {
                node.OnGraphComplete();
            }
        }

        private void RecursivePrint (INode data, RectTransform output) {
            if (_printedNodes.Contains(data)) return;
            
            var node = Instantiate(GetPrefab(data), output);
            node.Setup(data);
            _printedNodes.Add(data);
            _nodes.Add(node);
            
            data.Children.ForEach(child => RecursivePrint(child, node.childOutput));
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