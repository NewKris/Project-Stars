using System;
using UnityEngine;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.Combat.Projectiles {
    public class SimpleProjectile : MonoBehaviour {
        public static event Action<SimpleProjectile> ProjectileSpawned;
        public static event Action<SimpleProjectile> ProjectileDespawned;
        
        [ReadOnly] public float spawnedTime;
        public float lifeTime;
        public float maxSpeed;
        public Vector3 direction;

        public void Hit() {
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            spawnedTime = Time.time;
            ProjectileSpawned?.Invoke(this);
        }

        private void OnDisable() {
            ProjectileDespawned?.Invoke(this);
        }
    }
}
