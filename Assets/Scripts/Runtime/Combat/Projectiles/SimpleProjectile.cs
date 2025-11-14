using UnityEngine;

namespace Werehorse.Runtime.Combat.Projectiles {
    public class SimpleProjectile : MonoBehaviour {
        public float maxSpeed;
        public Vector3 direction;

        public void Hit() {
            gameObject.SetActive(false);
        }
    }
}
