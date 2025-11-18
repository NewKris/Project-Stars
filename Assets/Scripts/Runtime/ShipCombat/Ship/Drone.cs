using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;

namespace Werehorse.Runtime.ShipCombat.Ship {
    public class Drone : MonoBehaviour {
        public Transform target;
        public float defaultFollowDamping;
        public float defaultRotateDamping;

        private bool _initialized;
        private float _followDamping;
        private float _rotateDamping;
        private DampedRotation _rotation;
        private DampedVector _position;

        public void SetTarget(Transform newTarget, float overrideFollowDamping = -1, float overrideRotateDamping = -1, bool snapToTarget = false) {
            target = newTarget;

            if (snapToTarget) {
                _position = new DampedVector(target.position);
                _rotation = new DampedRotation(target.rotation);         
            }
            else {
                _position = new DampedVector(transform.position);
                _rotation = new DampedRotation(transform.rotation);
            }

            _followDamping = overrideFollowDamping < 0 ? defaultFollowDamping : overrideFollowDamping;
            _rotateDamping = overrideRotateDamping < 0 ? defaultRotateDamping : overrideRotateDamping;
            
            _initialized = true;
        }

        private void Start() {
            if (target && !_initialized) {
                SetTarget(target);
            }
        }

        private void Update() {
            _position.Target = target.position;
            transform.position = _position.Tick(_followDamping);
            
            _rotation.Target = target.rotation;
            transform.rotation = _rotation.Tick(_rotateDamping);
        }
    }
}
