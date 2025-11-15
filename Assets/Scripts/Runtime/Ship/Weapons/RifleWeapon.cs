using System;
using UnityEngine;
using Werehorse.Runtime.Combat.Projectiles.SimpleProjectiles;

namespace Werehorse.Runtime.Ship.Weapons {
    public class RifleWeapon : Weapon {
        public float fireRate;
        public float convergeDistance;

        private bool _firing;
        private float _lastFireTime;
        
        private bool CanFire => Time.time > _lastFireTime + fireRate;
        
        public override void BeginFire() {
            _firing = true;
        }

        public override void EndFire() {
            _firing = false;
        }

        private void Update() {
            if (!_firing || !CanFire) {
                return;
            }
            
            _lastFireTime = Time.time;
            
            if (SimpleProjectileSystem.GetProjectile(out GameObject projectile)) {
                projectile.transform.position = transform.position;
                
                Ray ray = Camera.main.ScreenPointToRay(PlayerController.MousePosition);
                Vector3 convergePoint = ray.GetPoint(convergeDistance);
                Vector3 dir = convergePoint - projectile.transform.position;
                projectile.transform.rotation = Quaternion.LookRotation(dir);
                
                projectile.SetActive(true);
            }
        }
    }
}
