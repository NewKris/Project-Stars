using UnityEngine;
using Werehorse.Runtime.ShipCombat.Combat.Projectiles.SimpleProjectiles;
using Random = UnityEngine.Random;

namespace Werehorse.Runtime.ShipCombat.Ship.Weapons {
    public class RifleWeapon : Weapon {
        public int damage;
        public float fireRate;
        public float convergeDistance;
        public Transform[] bulletSources;
        public AudioClip[] shootSounds;

        private bool _firing;
        private float _lastFireTime;
        private int _lastSource;
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
            _lastSource = 0;
        }

        private void Update() {
            if (!_firing || !CanFire) {
                return;
            }
            
            _lastFireTime = Time.time;
            
            if (SimpleProjectileSystem.GetProjectile(out SimpleProjectile projectile)) {
                Vector3 spawnPos = GetNextSpawnPosition();
                Quaternion rotation = GetBulletRotation(spawnPos);
                
                projectile.Initialize(new SimpleProjectileConfig() {
                    position = spawnPos,
                    rotation = rotation,
                    damage = damage
                });
                
                _audio.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);
            }
        }

        private Quaternion GetBulletRotation(Vector3 spawnPos) {
            Ray ray = Camera.main.ScreenPointToRay(PlayerShipController.MousePosition);
            Vector3 convergePoint = ray.GetPoint(convergeDistance);
            Vector3 dir = convergePoint - spawnPos;
            
            return Quaternion.LookRotation(dir);
        }

        private Vector3 GetNextSpawnPosition() {
            Vector3 spawnPos = bulletSources[_lastSource].position;
            _lastSource = (_lastSource + 1) % bulletSources.Length;

            return spawnPos;
        }
    }
}
