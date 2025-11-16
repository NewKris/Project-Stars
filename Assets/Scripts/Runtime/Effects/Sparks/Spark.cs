using System;
using UnityEngine;

namespace Werehorse.Runtime.Effects.Sparks {
    public class Spark : MonoBehaviour {
        public float lifeTime;

        private float _spawnTime;
        
        private void OnEnable() {
            _spawnTime = Time.time;
        }

        private void Update() {
            if (Time.time > _spawnTime + lifeTime) {
                gameObject.SetActive(false);
            }
        }
    }
}
