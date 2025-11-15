using System;
using UnityEngine;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.Combat.Projectiles.SimpleProjectiles {
    public class SimpleProjectile : MonoBehaviour {
        public static event Action<SimpleProjectile> OnSpawned;
        
        public float travelSpeed;
        public float maxDistance;
        [ReadOnly] public float travelDistance;

        private void OnEnable() {
            travelDistance = 0;
            OnSpawned?.Invoke(this);
        }
    }
}
