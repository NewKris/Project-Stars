using System;
using System.Collections.Generic;
using System.Linq;
using NewKris.Runtime.Utility.CommonObjects;
using UnityEngine;

namespace NewKris.Runtime.Combat.Projectiles {
    public enum MissileType {
        MISSILE
    }
    
    public class MissileProjectileSystem : MonoBehaviour {
        private static MissileProjectileSystem Instance;

        public GameObject missilePrefab;

        private PrefabPool _missilePool;
        private readonly Collider[] _inRangeColliders = new Collider[10];
        
        public static bool GetProjectile(out GameObject projectile, MissileType type) {
            return Instance.GetPool(type).GetObject(out projectile);
        }

        private void Awake() {
            Instance = this;
            _missilePool = new PrefabPool(missilePrefab, transform, 20);
        }

        private void Update() {
            float dt = Time.deltaTime;
            
            foreach (Missile missile in GetAllActiveProjectiles()) {
                IntegrateMissile(missile, dt);
            }
        }

        private void IntegrateMissile(Missile missile, float dt) {
            if (missile.target) {
                YawMissile(missile, dt);
            }
            else {
                missile.target = FindTarget(missile);
            }
            
            MoveMissile(missile, dt);
        }

        private void MoveMissile(Missile missile, float dt) {
            missile.speed += missile.accelerationSpeed * dt;
            missile.speed = Mathf.Clamp(missile.speed, 0, missile.maxFlightSpeed);
            missile.transform.position += missile.transform.forward * (missile.speed * dt);
        }
        
        private void YawMissile(Missile missile, float dt) {
            float maxDelta = missile.maxTurningSpeed * (missile.speed / missile.maxFlightSpeed);
            Vector3 dir = (missile.target.transform.position - missile.transform.position).normalized;
            Vector3 aimDir = Vector3.RotateTowards(
                missile.transform.forward, 
                dir, 
                maxDelta * dt, 
                0.0f
            );
                
            missile.transform.rotation = Quaternion.LookRotation(aimDir);

            if (!IsInsideCone(missile, missile.target)) {
                missile.target = null;
            }
        }
        
        private GameObject FindTarget(Missile missile) {
            int inRangeCount = Physics.OverlapSphereNonAlloc(
                missile.transform.position, 
                missile.detectionRange, 
                _inRangeColliders,
                missile.detectFaction
            );
            
            float closestDistance = float.MaxValue;
            int closestIndex = -1;
            
            for (int i = 0; i < inRangeCount; i++) {
                float distance = CalculateSqrDistance(missile, _inRangeColliders[i].gameObject);
                
                if (IsInsideCone(missile, _inRangeColliders[i].gameObject) && distance < closestDistance) {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex == -1 ? null : _inRangeColliders[closestIndex].gameObject;
        }
        
        private bool IsInsideCone(Missile missile, GameObject target) {
            Vector3 dir = (target.transform.position - missile.transform.position).normalized;
            float angle = Mathf.Abs(Vector3.Angle(missile.transform.forward, dir));
            return angle < missile.detectionAngle * 0.5f;
        }

        private float CalculateSqrDistance(Missile missile, GameObject target) {
            return Vector3.SqrMagnitude(target.transform.position - missile.transform.position);
        }
        
        private IEnumerable<Missile> GetAllActiveProjectiles() {
            return _missilePool.GetAllActiveObjects()
                .Select(x => x.GetComponent<Missile>());
        }

        private PrefabPool GetPool(MissileType type) {
            return type switch {
                MissileType.MISSILE => _missilePool,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
