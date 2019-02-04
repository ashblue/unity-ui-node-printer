using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class NodeContext : MonoBehaviour {
        [SerializeField] 
        private Text _title;

        [SerializeField] 
        private Text _description;
        
        [SerializeField]
        private Button _buttonPurchase;

        private void Awake () {
            gameObject.SetActive(false);
        }

        public void Open (INode node) {
            gameObject.SetActive(true);
            
            _title.text = node.Name;
            _description.text = node.Description;

            _buttonPurchase.gameObject.SetActive(node.Enabled && node.IsPurchasable);
            _buttonPurchase.onClick.RemoveAllListeners();
            _buttonPurchase.onClick.AddListener(() => {
                node.Purchased = true;
                _buttonPurchase.gameObject.SetActive(false);
            });
        }
    }
}