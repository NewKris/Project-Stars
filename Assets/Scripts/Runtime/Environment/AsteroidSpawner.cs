using System;
using NewKris.Runtime.Utility.CommonObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NewKris.Runtime.Environment {
    public class AsteroidSpawner : MonoBehaviour {
        public float radius;
        public float minSpawnRate;
        public float pollRate;
        [Range(0, 1)] public float spawnChance;
        public GameObject asteroidPrefab;
        
        private float _lastSpawnTime;
        private float _lastPollTime;
        private PrefabPool _asteroidPool;

        private bool CanTrySpawnAsteroid => Time.time - _lastSpawnTime >= minSpawnRate;
        private bool CanPollSpawn =>  Time.time - _lastPollTime >= pollRate;
        
        private void Awake() {
            _asteroidPool = new PrefabPool(asteroidPrefab, transform, 50, 10);
        }

        private void Update() {
            if (!CanTrySpawnAsteroid || !CanPollSpawn) {
                return;
            }
            
            _lastPollTime = Time.time;
            
            if (Random.value <= spawnChance) {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid() {
            if (_asteroidPool.GetObject(out GameObject asteroid)) {
                float xPos = Random.Range( -radius, radius);
                asteroid.transform.localPosition = new Vector3(xPos, 0, 0);
                asteroid.SetActive(true);
                _lastSpawnTime = Time.time;
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(radius * 2, 1, 1));
        }
    }
}
