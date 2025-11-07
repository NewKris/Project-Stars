using NewKris.Runtime.Combat;
using NewKris.Runtime.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    public class Missile : MonoBehaviour {
        public int explosionDamage;

        [Header("Movement")] 
        public float accelerationSpeed;
        public float maxFlightSpeed;
        public float maxTurningSpeed;
        
        [Header("Detection")]
        public LayerMask detectFaction;
        public float detectionAngle;
        public float detectionRange;

        private float _speed;
        private GameObject _target;
        private readonly Collider[] _inRangeColliders = new Collider[10];

        public void Explode(Vector3 hitPoint) {
            ExplosionSystem.SpawnExplosion(
                hitPoint, 
                GetComponentInChildren<HitBox>().canHitFaction,
                explosionDamage
            );
            
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            _speed = 0;
            _target = null;
        }

        private void Update() {
            if (_target) {
                float maxDelta = maxTurningSpeed * (_speed / maxFlightSpeed);
                Vector3 dir = (_target.transform.position - transform.position).normalized;
                Vector3 aimDir = Vector3.RotateTowards(
                    transform.forward, 
                    dir, 
                    maxDelta * Time.deltaTime, 
                    0.0f
                );
                
                transform.rotation = Quaternion.LookRotation(aimDir);

                if (!InsideCone(_target)) {
                    _target = null;
                }
            }
            else {
                _target = FindTarget();
            }
            
            _speed += accelerationSpeed * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, 0, maxFlightSpeed);
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }

        private GameObject FindTarget() {
            int inRangeCount = Physics.OverlapSphereNonAlloc(
                transform.position, 
                detectionRange, 
                _inRangeColliders,
                detectFaction
            );
            
            float closestDistance = float.MaxValue;
            int closestIndex = -1;
            
            for (int i = 0; i < inRangeCount; i++) {
                float distance = CalculateSqrDistance(_inRangeColliders[i].GameObject());
                
                if (InsideCone(_inRangeColliders[i].gameObject) && distance < closestDistance) {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex == -1 ? null : _inRangeColliders[closestIndex].gameObject;
        }

        private bool InsideCone(GameObject target) {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Abs(Vector3.Angle(transform.forward, dir));
            return angle < detectionAngle * 0.5f;
        }

        private float CalculateSqrDistance(GameObject target) {
            return Vector3.SqrMagnitude(target.transform.position - transform.position);
        }
        
        private void OnDrawGizmos() {
            HandlesProxy.DrawArc(
                transform.position, 
                transform.forward, 
                Vector3.up, 
                detectionAngle, 
                detectionRange, 
                12, 
                3, 
                Color.red
            );
        }
    }
}
