using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;

namespace Werehorse.Runtime.Combat.Projectiles {
    public enum ProjectileType {
        PELLET,
        RIFLE,
        ASTEROID
    }
    
    public class SimpleProjectileSystem : MonoBehaviour {
        private static SimpleProjectileSystem Instance;

        public GameObject riflePrefab;
        public GameObject pelletPrefab;
        public GameObject asteroidPrefab;

        private PrefabPool _pelletPool;
        private PrefabPool _riflePool;
        private PrefabPool _asteroidPool;

        public static bool GetProjectile(out GameObject projectile, ProjectileType type) {
            return Instance.GetPool(type).GetObject(out projectile);
        }

        private void Awake() {
            Instance = this;
            _pelletPool = new PrefabPool(pelletPrefab, transform, 100);
            _riflePool = new PrefabPool(riflePrefab, transform, 10);
            _asteroidPool = new PrefabPool(asteroidPrefab, transform, 25);
        }

        private void Update() {
            float dt = Time.deltaTime;
            
            foreach (SimpleProjectile simpleProjectile in GetAllActiveProjectiles()) {
                MoveProjectile( simpleProjectile, dt);
            }
        }

        private void MoveProjectile(SimpleProjectile projectile, float dt) {
            projectile.transform.position += projectile.direction.normalized * (projectile.maxSpeed * dt);
        }

        private IEnumerable<SimpleProjectile> GetAllActiveProjectiles() {
            return _pelletPool.GetAllActiveObjects()
                .Concat(_riflePool.GetAllActiveObjects())
                .Concat(_asteroidPool.GetAllActiveObjects())
                .Select(x => x.GetComponent<SimpleProjectile>());
        }

        private PrefabPool GetPool(ProjectileType type) {
            return type switch {
                ProjectileType.PELLET => _pelletPool,
                ProjectileType.RIFLE => _riflePool,
                ProjectileType.ASTEROID => _asteroidPool,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
