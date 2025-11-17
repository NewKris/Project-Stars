using UnityEngine;
using Werehorse.Runtime.ShipCombat.Utility.CommonObjects;

namespace Werehorse.Runtime.ShipCombat.Ship {
    public class Drone : MonoBehaviour {
        public Transform target;
        public float followDamping;
        public float rotateDamping;

        private DampedRotation _rotation;
        private DampedVector _position;

        private void Awake() {
            _position = new DampedVector(target.position);
            _rotation = new DampedRotation(target.rotation);
        }

        private void Update() {
            _position.Target = target.position;
            transform.position = _position.Tick(followDamping);
            
            _rotation.Target = target.rotation;
            transform.rotation = _rotation.Tick(rotateDamping);
        }
    }
}
