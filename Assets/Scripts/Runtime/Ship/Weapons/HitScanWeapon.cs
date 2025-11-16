using System;
using UnityEngine;
using Werehorse.Runtime.Combat;
using Werehorse.Runtime.Effects.Sparks;
using Random = UnityEngine.Random;

namespace Werehorse.Runtime.Ship.Weapons {
    public class HitScanWeapon : Weapon {
        public float fireRate;
        public float maxRange;
        public int damage;
        public LayerMask targetMasks;
        public AudioClip[] shootSounds;

        private bool _firing;
        private float _lastFireTime;
        private AudioSource _audio;
        
        private bool CanFire => Time.time > _lastFireTime + fireRate;
        
        public override void BeginFire() {
            _firing = true;
        }

        public override void EndFire() {
            _firing = false;
        }

        private void Awake() {
            _audio = GetComponent<AudioSource>();
        }

        private void Update() {
            if (!_firing || !CanFire) {
                return;
            }
            
            _lastFireTime = Time.time;
            _audio.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);

            Ray ray = Camera.main.ScreenPointToRay(PlayerController.MousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxRange, targetMasks)) {
                SparksSystem.PlaySparks(hit.point, hit.normal);

                if (hit.collider.TryGetComponent(out HurtBox hurtBox)) {
                    hurtBox.TakeDamage(damage);
                }
            }
        }
    }
}
