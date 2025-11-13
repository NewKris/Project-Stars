using Unity.VisualScripting;
using UnityEngine;

namespace Werehorse.Runtime.Environment {
    public class ScrollingBackground : MonoBehaviour {
        public float scrollSpeed;
        public float spacing;
        public float flipThreshold;
        public Vector3 scrollDirection;
        public Vector3 backgroundNormal;
        public Sprite backgroundSprite;
        public Color tint;

        private Transform _child1;
        private Transform _child2;
        
        private void Start() {
            _child1 = CreateChild();
            _child2 = CreateChild();
            _child2.localPosition = SiblingOffset();
        }

        private void Update() {
            Vector3 vel = scrollDirection.normalized * (scrollSpeed * Time.deltaTime);
            _child1.localPosition += vel;
            _child2.localPosition += vel;

            if (_child1.localPosition.sqrMagnitude > flipThreshold * flipThreshold) {
                _child1.localPosition = _child2.localPosition + SiblingOffset();
            }
            
            if (_child2.localPosition.sqrMagnitude > flipThreshold * flipThreshold) {
                _child2.localPosition = _child1.localPosition + SiblingOffset();
            }
        }

        private Vector3 SiblingOffset() {
            return -scrollDirection.normalized * spacing;
        }

        private Transform CreateChild() {
            Transform child = new GameObject().transform;
            child.parent = transform;
            child.SetLocalPositionAndRotation(Vector3.zero, Quaternion.LookRotation(backgroundNormal));
            child.localScale = Vector3.one;
            SpriteRenderer childRenderer = child.AddComponent<SpriteRenderer>();
            
            childRenderer.sprite = backgroundSprite;
            childRenderer.color = tint;
            childRenderer.sortingLayerName = "Background";

            return child;
        }
    }
}
