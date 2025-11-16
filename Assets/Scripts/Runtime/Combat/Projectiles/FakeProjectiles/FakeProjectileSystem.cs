using System;
using System.Collections.Generic;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Combat.Projectiles.FakeProjectiles {
    public class FakeProjectileSystem : MonoBehaviour {
        private static FakeProjectileSystem Instance;
        private static HashSet<FakeProjectile> ActiveProjectiles = new HashSet<FakeProjectile>(20);

        public GameObject projectilePrefab;
        
        private PrefabPool _pool;
        
        public static void ShootFakeProjectile(Vector3 from, Vector3 to) {
            if (Instance._pool.GetObject(out GameObject projectileObj)) {
                FakeProjectile projectile = projectileObj.GetComponent<FakeProjectile>();
                projectile.from = from;
                projectile.to = to;
                projectile.currentTravelDuration = 0;
                
                Vector3 dir = to - from;
                projectileObj.transform.rotation = Quaternion.LookRotation(dir.normalized);
                projectileObj.gameObject.SetActive(true);
                
                ActiveProjectiles.Add(projectile);
            }
        }
        
        private void Awake() {
            Instance = this;
            _pool = new PrefabPool(projectilePrefab, transform, 20);
        }

        private void Update() {
            ActiveProjectiles.ForEach(Move);
            ActiveProjectiles.ForEach(Expire);
            ActiveProjectiles.RemoveWhere(IsExpired);
        }

        private void Move(FakeProjectile projectile) {
            float t = projectile.currentTravelDuration / projectile.maxTravelDuration;
            projectile.transform.position = Vector3.Lerp(projectile.from, projectile.to, t);
            projectile.currentTravelDuration += Time.deltaTime;
        }

        private void Expire(FakeProjectile projectile) {
            if (projectile.currentTravelDuration > projectile.maxTravelDuration) {
                projectile.gameObject.SetActive(false);
            }
        }

        private bool IsExpired(FakeProjectile projectile) {
            return !projectile.gameObject.activeSelf;
        }
    }
}
