using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class NodePrint : MonoBehaviour {
        [SerializeField] 
        private Text _name;

        [SerializeField]
        private Image _graphic;

        [SerializeField] 
        private Image _purchaseGraphic;

        [SerializeField] 
        private Button _button;

        public RectTransform childOutput;

        public void Setup (INode node) {
            _name.text = node.Name;
            _graphic.sprite = node.Graphic;
            _button.onClick.AddListener(() => node.OnClick(node));
            _purchaseGraphic.gameObject.SetActive(node.Purchased);

            node.OnPurchaseChange.AddListener(() => {
                _purchaseGraphic.gameObject.SetActive(node.Purchased);
            });
            
            node.OnDisable.AddListener(() => {
                _button.interactable = false;
            });
        }
    }
}
