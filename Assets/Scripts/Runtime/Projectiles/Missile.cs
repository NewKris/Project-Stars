using NewKris.Runtime.Combat;
using NewKris.Runtime.Utility;
using NewKris.Runtime.Utility.Attributes;
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

        [Header("Read Only")]
        [ReadOnly] public float speed;
        [ReadOnly] public GameObject target;
        
        public void Explode(Collider otherCollider) {
            if (KillZone.IsKillZone(otherCollider.gameObject.layer)) {
                gameObject.SetActive(false);
                return;
            }
            
            ExplosionSystem.SpawnExplosion(
                otherCollider.ClosestPoint(transform.position), 
                GetComponentInChildren<HitBox>().canHitFaction,
                explosionDamage
            );
            
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            speed = 0;
            target = null;
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
