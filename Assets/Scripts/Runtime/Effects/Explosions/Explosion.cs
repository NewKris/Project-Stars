using UnityEngine;

namespace Werehorse.Runtime.Effects.Explosions {
    public class Explosion : MonoBehaviour {
        public float lifeTime;

        private float _spawnTime;

        private void OnEnable() {
            _spawnTime = Time.time;
            GetComponent<AudioSource>().Play();
        }

        private void Update() {
            if (Time.time - _spawnTime > lifeTime) {
                gameObject.SetActive(false);
            }
        }
    }
}
