using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class ExampleGraphPrint : MonoBehaviour {
        public NodeGraphPrinter printer;
        public NodeData root;
        
        private void Start () {
            var graph = new NodeGraphBuilder();
            root.children.ForEach(n => graph.Add(n.displayName, n.graphic, () => Debug.Log("Click")));
            printer.Build(graph.Build());
        }
    }
}