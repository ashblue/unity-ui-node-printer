using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class NodePrint : MonoBehaviour {
        private Color _normalColor;

        [SerializeField] 
        private Text _name;

        [SerializeField]
        private Image _graphic;

        [SerializeField] 
        private Image _purchaseGraphic;

        [SerializeField] 
        private Button _button;

        public RectTransform childOutput;

        private void Awake () {
            _normalColor = _button.colors.normalColor;
        }

        public void Setup (INode node) {
            _name.text = node.Name;
            _graphic.sprite = node.Graphic;
            _button.onClick.AddListener(() => node.OnClick.Invoke(node));
            _purchaseGraphic.gameObject.SetActive(node.Purchased);

            node.OnPurchase.AddListener((n) => {
                _purchaseGraphic.gameObject.SetActive(true);
            });
            
            node.OnDisable.AddListener(() => {
                var colors = _button.colors;
                colors.normalColor = colors.disabledColor;

                _button.colors = colors;
            });

            node.OnEnable.AddListener(() => {
                var colors = _button.colors;
                colors.normalColor = _normalColor;

                _button.colors = colors;
            });

            if (!node.Enabled) {
                node.Disable();
            }
        }
    }
}
