using System.Collections.Generic;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    public class SimpleProjectileSystem : MonoBehaviour {
        private static readonly HashSet<SimpleProjectile> Projectiles = new HashSet<SimpleProjectile>(300);
        
        public static void AddProjectile(SimpleProjectile projectile) {
            Projectiles.Add(projectile);
        }

        public static void RemoveProjectile(SimpleProjectile projectile) {
            Projectiles.Remove(projectile);
        }

        private void Update() {
            float dt = Time.deltaTime;
            foreach (SimpleProjectile simpleProjectile in Projectiles) {
                MoveProjectile( simpleProjectile, dt);
            }
        }

        private void MoveProjectile(SimpleProjectile projectile, float dt) {
            projectile.transform.position += projectile.direction.normalized * (projectile.maxSpeed * dt);
        }
    }
}
