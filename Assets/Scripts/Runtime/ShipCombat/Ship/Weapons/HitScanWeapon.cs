using UnityEngine;
using Werehorse.Runtime.ShipCombat.Combat;
using Werehorse.Runtime.ShipCombat.Combat.Projectiles.FakeProjectiles;
using Werehorse.Runtime.ShipCombat.Effects.Sparks;
using Random = UnityEngine.Random;

namespace Werehorse.Runtime.ShipCombat.Ship.Weapons {
    public class HitScanWeapon : Weapon {
        public float fireRate;
        public float maxRange;
        public int damage;
        public LayerMask targetMasks;
        public AudioClip[] shootSounds;
        public Transform[] bulletSources;

        private bool _firing;
        private float _lastFireTime;
        private int _lastBulletSource;
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

            Vector3 hitPos;
            Ray ray = Camera.main.ScreenPointToRay(PlayerShipController.MousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, maxRange, targetMasks)) {
                SparksSystem.PlaySparks(hit.point, hit.normal);
                hitPos = hit.point;

                if (hit.collider.TryGetComponent(out HurtBox hurtBox)) {
                    hurtBox.TakeDamage(damage);
                }
            }
            else {
                hitPos = ray.GetPoint(maxRange);
            }
            
            FakeProjectileSystem.ShootFakeProjectile(bulletSources[_lastBulletSource].position, hitPos);
            _lastBulletSource = (_lastBulletSource + 1) % bulletSources.Length;
        }
    }
}
