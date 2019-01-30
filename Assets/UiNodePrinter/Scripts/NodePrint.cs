using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.UiNodeBuilder {
    public class NodePrint : MonoBehaviour {
        [SerializeField] 
        private Text _name;

        [SerializeField]
        private Image _graphic;

        [SerializeField] 
        private Button _button;
        
        public void Setup (INode child) {
            _name.text = child.Name;
            _graphic.sprite = child.Graphic;
            _button.onClick.AddListener(() => child.OnClick());
        }
    }
}
