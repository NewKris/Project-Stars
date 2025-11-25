using System;
using UnityEngine;

namespace Werehorse.Runtime.Utility.CommonBehaviours {
    public class MatchRotation : MonoBehaviour {
        [Range(0, 1)] public float speed;
        public Transform target;

        private void Awake() {
            if (target) {
                transform.rotation = target.rotation;
            }
        }

        private void Update() {
            if (target) {
                transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, speed);
            }
        }
    }
}
