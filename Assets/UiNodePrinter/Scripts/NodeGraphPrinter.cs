using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGraphPrinter : MonoBehaviour {
        [Tooltip("Where nodes will be output in a Canvas")]
        [SerializeField]
        private RectTransform _nodeOutput;
        
        [Tooltip("Prefab used to output nodes")]
        [SerializeField] 
        private NodePrint _nodePrintPrefab;
        
        public void Build (NodeGraph graph) {
            foreach (var child in graph.Root.Children) {
                RecursivePrint(child, _nodeOutput);
            }
        }

        private void RecursivePrint (INode data, RectTransform output) {
            var node = Instantiate(_nodePrintPrefab, output);
            node.Setup(data);
            
            data.Children.ForEach(child => RecursivePrint(child, node.childOutput));
        }
    }
}