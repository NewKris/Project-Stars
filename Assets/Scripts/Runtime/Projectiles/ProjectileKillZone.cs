using System;
using UnityEngine;

namespace NewKris.Runtime.Projectiles {
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileKillZone : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out Projectile projectile)) {
                projectile.gameObject.SetActive(false);
            }
        }
    }
}
