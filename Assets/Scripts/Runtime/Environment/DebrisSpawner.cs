using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Werehorse.Runtime.Environment {
    public class DebrisSpawner : MonoBehaviour {
        public GameObject debrisPrefab;
        public int debrisCount;
        public float maxRadius;

        private void Start() {
            for (int i = 0; i < debrisCount; i++) {
                float randDist = Random.value * maxRadius;
                Vector3 randDir = Random.insideUnitSphere * randDist;

                Instantiate(debrisPrefab, transform.position + randDir, Quaternion.identity, transform);
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxRadius);
        }
    }
}
