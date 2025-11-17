using UnityEngine;
using UnityEngine.UI;

namespace Werehorse.Runtime.ShipCombat.Ship.Hud.LockOn {
    public class LockOnReticle : MonoBehaviour {
        public Transform target;

        private Color _defaultColor;
        private RectTransform _rectTransform;
        private Image _image;

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            _defaultColor = _image.color;
        }

        private void Update() {
            _rectTransform.position = Camera.main.WorldToScreenPoint(target.position);
            _image.color = _rectTransform.position.z < 0 ? Color.clear : _defaultColor;
        }
    }
}
