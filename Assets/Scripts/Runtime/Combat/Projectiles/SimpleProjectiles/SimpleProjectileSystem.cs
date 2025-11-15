using System.Collections.Generic;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Combat.Projectiles.SimpleProjectiles {
    public class SimpleProjectileSystem : MonoBehaviour {
        private static SimpleProjectileSystem Instance;
        private static HashSet<SimpleProjectile> ActiveProjectiles = new HashSet<SimpleProjectile>(50);

        public GameObject projectilePrefab;
        
        private PrefabPool _pool;

        public static bool GetProjectile(out SimpleProjectile projectile) {
            bool foundProjectile = Instance._pool.GetObject(out GameObject projectileObj);
            projectile = projectileObj.GetComponent<SimpleProjectile>();
            
            return foundProjectile;
        }
        
        private void Awake() {
            Instance = this;
            ActiveProjectiles.Clear();
            _pool = new PrefabPool(projectilePrefab, transform);
            SimpleProjectile.OnSpawned += RegisterProjectile;
        }

        private void OnDestroy() {
            SimpleProjectile.OnSpawned -= RegisterProjectile;
        }

        private void Update() {
            ActiveProjectiles.ForEach(MoveProjectile);
            ActiveProjectiles.ForEach(ExpireProjectile);
            ActiveProjectiles.RemoveWhere(IsInactive);
        }

        private bool IsInactive(SimpleProjectile projectile) {
            return !projectile.gameObject.activeSelf;
        }

        private void MoveProjectile(SimpleProjectile projectile) {
            float distance = projectile.travelSpeed * Time.deltaTime;
            Vector3 velocity = projectile.transform.forward * distance;
            
            projectile.transform.position += velocity;
            projectile.travelDistance += distance;
        }

        private void ExpireProjectile(SimpleProjectile projectile) {
            if (projectile.travelDistance > projectile.maxDistance) {
                projectile.gameObject.SetActive(false);
            }
        }
        
        private void RegisterProjectile(SimpleProjectile projectile) {
            ActiveProjectiles.Add(projectile);
        }
    }
}
