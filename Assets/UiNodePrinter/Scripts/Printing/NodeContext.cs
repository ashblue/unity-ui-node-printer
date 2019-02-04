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
        
        [SerializeField]
        private Button _buttonRefund;

        private void Awake () {
            gameObject.SetActive(false);
        }

        public void Open (INode node) {
            gameObject.SetActive(true);
            
            _title.text = node.Name;
            
            _description.text = node.Description;
            if (node.IsLocked) {
                _description.text += $"\n {node.GetLockedDescription()}";
            }

            SetupPurchase(node);
            SetupRefund(node);
        }

        private void SetupRefund (INode node) {
            _buttonRefund.gameObject.SetActive(node.IsPurchased);
            _buttonRefund.onClick.RemoveAllListeners();
            _buttonRefund.onClick.AddListener(() => {
                node.Refund();
                _buttonRefund.gameObject.SetActive(false);
                SetupPurchase(node);
            });
        }

        private void SetupPurchase (INode node) {
            _buttonPurchase.gameObject.SetActive(node.IsEnabled && node.IsPurchasable);
            _buttonPurchase.onClick.RemoveAllListeners();
            _buttonPurchase.onClick.AddListener(() => {
                node.Purchase();
                _buttonPurchase.gameObject.SetActive(false);
                SetupRefund(node);
            });
        }
    }
}