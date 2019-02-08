using UnityEngine;

namespace CleverCrow.UiNodeBuilder {
    public class NodeGroupPrint : MonoBehaviour {
        public RectTransform childOutput;
        public RectTransform exitOutput;
        public RectTransform endDivider;

        public void Setup (INode node) {
            endDivider.gameObject.SetActive(node.ExitChild != null);
        }
    }
}