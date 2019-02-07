using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class NodePrint : MonoBehaviour {
        private Color _normalColor;
        private float _startingHeight;
        private RectTransform _rect;

        [SerializeField] 
        private Text _name;

        [SerializeField]
        private Image _graphic;

        [SerializeField] 
        private Image _purchaseGraphic;

        [SerializeField] 
        private Image _lockedGraphic;

        [SerializeField] 
        private Button _button;

        public RectTransform childOutput;
        public RectTransform childConnector;
        public RectTransform linkConnector;
        public RectTransform leftConnector;
        public RectTransform rightConnector;

        private void Awake () {
            _normalColor = _button.colors.normalColor;
            _rect = GetComponent<RectTransform>();
        }

        public void Setup (INode node) {
            _startingHeight = _rect.sizeDelta.y;
            
            if (node.Parents.Count > 1) {
                var size = linkConnector.sizeDelta;
                size = new Vector2(size.x, size.y * (node.Parents.Count - 1));
                linkConnector.sizeDelta = size;
                linkConnector.gameObject.SetActive(true);
            }
            
            _name.text = node.Name;
            _graphic.sprite = node.Graphic;
            _button.onClick.AddListener(() => node.OnClick.Invoke(node));
            _purchaseGraphic.gameObject.SetActive(node.IsPurchased);
            _lockedGraphic.gameObject.SetActive(node.IsLocked);
            childConnector.gameObject.SetActive(node.Children.Count > 0);
            
            leftConnector.gameObject.SetActive(node.Parents.Find(p => p.IsRoot) == null);
            rightConnector.gameObject.SetActive(node.Children.Count > 0);

            node.OnPurchase.AddListener(() => {
                _purchaseGraphic.gameObject.SetActive(true);
            });
            
            node.OnDisable.AddListener(() => {
                var colors = _button.colors;
                colors.normalColor = colors.disabledColor;

                _button.colors = colors;
            });

            node.OnRefund.AddListener(() => {
                _purchaseGraphic.gameObject.SetActive(false);
                if (!node.IsEnabled) node.Disable();
            });
            
            node.OnEnable.AddListener(() => {
                var colors = _button.colors;
                colors.normalColor = _normalColor;

                _button.colors = colors;
            });

            if (!node.IsEnabled || node.IsLocked) {
                node.Disable();
            }
        }

        // Expand the vertical height to account for nested children on link connectors
        public void OnGraphComplete () {
            if (linkConnector == null) return;
            var rectSize = _rect.sizeDelta;
            var linkSize = linkConnector.sizeDelta;
            linkSize.y += rectSize.y - _startingHeight;
            linkConnector.sizeDelta = linkSize;
        }
    }
}
