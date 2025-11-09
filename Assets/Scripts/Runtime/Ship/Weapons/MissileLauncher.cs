using System;
using NewKris.Runtime.Projectiles;
using NewKris.Runtime.Utility.CommonObjects;
using UnityEngine;

namespace NewKris.Runtime.Ship.Weapons {
    public class MissileLauncher : Weapon {
        public float fireRate;
        public Transform missileSpawn;
        public AudioClip fireSound;
        
        private float _lastFire;
        private AudioSource _audio;
        
        public override void BeginFire() {
            if (Time.time - _lastFire < fireRate) {
                return;
            }

            if (MissileProjectileSystem.GetProjectile(out GameObject missile, MissileType.MISSILE)) {
                missile.transform.position = missileSpawn.position;
                missile.transform.rotation = Quaternion.identity;
                missile.SetActive(true);
                _audio.PlayOneShot(fireSound);
            }
            
            _lastFire = Time.time;
        }
        
        public override void EndFire() { }

        private void Awake() {
            _audio = GetComponent<AudioSource>();
        }
    }
}
