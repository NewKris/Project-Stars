using NewKris.Runtime.Ship.Weapons;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    public class SimpleProjectile : Projectile {
        public float maxSpeed;
        public Vector3 direction;
        
        private void OnEnable() {
            SimpleProjectileSystem.AddProjectile(this);
        }

        private void OnDisable() {
            SimpleProjectileSystem.RemoveProjectile(this);
        }
    }
}
