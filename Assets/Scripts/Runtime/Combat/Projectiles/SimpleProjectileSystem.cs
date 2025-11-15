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
        private static HashSet<SimpleProjectile> ActiveProjectiles;

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
            ActiveProjectiles = new HashSet<SimpleProjectile>();
            
            _pelletPool = new PrefabPool(pelletPrefab, transform, 100);
            _riflePool = new PrefabPool(riflePrefab, transform, 10);
            _asteroidPool = new PrefabPool(asteroidPrefab, transform, 25);

            SimpleProjectile.ProjectileSpawned += RegisterProjectile;
        }

        private void OnDestroy() {
            SimpleProjectile.ProjectileSpawned -= RegisterProjectile;
        }

        private void Update() {
            float dt = Time.deltaTime;
            
            foreach (SimpleProjectile simpleProjectile in ActiveProjectiles) {
                MoveProjectile( simpleProjectile, dt);
                TimeOutProjectile(simpleProjectile);
            }

            ActiveProjectiles.RemoveWhere(IsInactive);
        }

        private void MoveProjectile(SimpleProjectile projectile, float dt) {
            projectile.transform.position += projectile.transform.forward * (projectile.maxSpeed * dt);
        }

        private void TimeOutProjectile(SimpleProjectile simpleProjectile) {
            if (Time.time - simpleProjectile.spawnedTime >= simpleProjectile.lifeTime) {
                simpleProjectile.gameObject.SetActive(false);
            }
        }

        private bool IsInactive(SimpleProjectile simpleProjectile) {
            return !simpleProjectile.gameObject.activeSelf;
        }

        private PrefabPool GetPool(ProjectileType type) {
            return type switch {
                ProjectileType.PELLET => _pelletPool,
                ProjectileType.RIFLE => _riflePool,
                ProjectileType.ASTEROID => _asteroidPool,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private void RegisterProjectile(SimpleProjectile simpleProjectile) {
            ActiveProjectiles.Add(simpleProjectile);
        }
    }
}
