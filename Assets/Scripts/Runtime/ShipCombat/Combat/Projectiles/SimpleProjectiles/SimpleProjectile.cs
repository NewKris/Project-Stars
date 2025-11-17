using System;
using UnityEngine;
using Werehorse.Runtime.ShipCombat.Utility.Attributes;

namespace Werehorse.Runtime.ShipCombat.Combat.Projectiles.SimpleProjectiles {
    public struct SimpleProjectileConfig {
        public Vector3 position;
        public Quaternion rotation;
        public int damage;
    }
    
    public class SimpleProjectile : MonoBehaviour {
        public static event Action<SimpleProjectile> OnSpawned;
        
        public float travelSpeed;
        public float maxDistance;
        [ReadOnly] public float travelDistance;

        public void Initialize(SimpleProjectileConfig config) {
            transform.position = config.position;
            transform.rotation = config.rotation;
            GetComponentInChildren<HitBox>().damage = config.damage;
            
            gameObject.SetActive(true);
        }
        
        private void OnEnable() {
            travelDistance = 0;
            OnSpawned?.Invoke(this);
        }
    }
}
