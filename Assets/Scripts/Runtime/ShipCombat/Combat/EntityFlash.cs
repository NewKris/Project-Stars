using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Combat {
    public class EntityFlash : MonoBehaviour {
        private static readonly int FlashStrength = Shader.PropertyToID("_Flash_Strength");

        public float flashDecreaseSpeed = 15;
        public MeshRenderer[] meshRenderers;

        private float _flashStrength;
        private MaterialPropertyBlock _materialPropertyBlock;
        
        public void Flash() {
            _flashStrength = 1;
        }

        private void Awake() {
            _materialPropertyBlock = new MaterialPropertyBlock();
            _materialPropertyBlock.SetFloat(FlashStrength, 0);
            UpdateAllRenderers();
        }

        private void Update() {
            _flashStrength -= flashDecreaseSpeed * Time.deltaTime;
            _flashStrength = Mathf.Clamp01(_flashStrength);
            
            _materialPropertyBlock.SetFloat(FlashStrength, _flashStrength);
            UpdateAllRenderers();
        }

        private void UpdateAllRenderers() {
            foreach (MeshRenderer meshRenderer in meshRenderers) {
                meshRenderer.SetPropertyBlock(_materialPropertyBlock);
            }
        }
    }
}
