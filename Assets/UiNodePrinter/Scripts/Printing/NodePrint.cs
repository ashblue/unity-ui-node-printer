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
            _button.onClick.AddListener(() => node.OnClick.Invoke(node));
            _purchaseGraphic.gameObject.SetActive(node.Purchased);

            node.OnPurchase.AddListener((n) => {
                _purchaseGraphic.gameObject.SetActive(true);
            });
            
            node.OnDisable.AddListener(() => {
                _button.interactable = false;
            });

            node.OnEnable.AddListener(() => {
                _button.interactable = true;
            });

            if (!node.Enabled) {
                node.Disable();
            }
        }
    }
}
