using System;
using NewKris.Runtime.Utility.CommonObjects;
using NewKris.Runtime.Utility.Timers;
using UnityEngine;
using UnityEngine.Pool;

namespace NewKris.Runtime.Ship.Weapons {
    public class MachineGun : Weapon {
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public float fireRate;
        
        private bool _firing;
        private Timer _fireCooldown;
        private PrefabPool _bulletPool;
            
        public override void BeginFire() {
            _firing = true;
        }
        
        public override void EndFire() {
            _firing = false;
        }

        private void Awake() {
            Transform projectileParent = GameObject.FindGameObjectWithTag("Projectile Parent").transform;
            _bulletPool = new PrefabPool(bulletPrefab, projectileParent, 100, 50);

            _fireCooldown = TimerManager.CreateTimer();
        }

        private void OnDestroy() {
            TimerManager.RemoveTimer(_fireCooldown);
        }

        private void Update() {
            if (!_firing || !_fireCooldown.Elapsed) {
                return;
            }
            
            _fireCooldown.SetTimer(fireRate);
            SpawnBullet();
        }

        private void SpawnBullet() {
            if (_bulletPool.GetObject(out GameObject bullet)) {
                bullet.transform.position = bulletSpawn.position;
                bullet.gameObject.SetActive(true);
            }
        }
    }
}
