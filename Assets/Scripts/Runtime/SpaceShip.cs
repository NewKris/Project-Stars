using System;
using NewKris.Runtime.Utility.CommonObjects;
using UnityEngine;

namespace NewKris.Runtime {
    public class SpaceShip : MonoBehaviour {
        public float maxMoveSpeed;
        public float minMoveSpeed;
        public float moveDamping;

        public Transform reticle;

        private DampedVector _position;
        private readonly Plane _groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        private void Awake() {
            PlayerController.OnBeginFire1 += BeginFire1;
            PlayerController.OnEndFire1 += EndFire1;
            PlayerController.OnBeginFire2 += BeginFire2;
            PlayerController.OnEndFire2 += EndFire2;
        }

        private void OnDestroy() {
            PlayerController.OnBeginFire1 -= BeginFire1;
            PlayerController.OnEndFire1 -= EndFire1;
            PlayerController.OnBeginFire2 -= BeginFire2;
            PlayerController.OnEndFire2 -= EndFire2;
        }

        private void Update() {
            _position.Target = GetTargetPos();
            reticle.position = _position.Target;

            Vector3 nextPosition = _position.Tick(moveDamping);
            Vector3 vel = nextPosition - transform.position;
            
            transform.position += Vector3.ClampMagnitude(vel, maxMoveSpeed * Time.deltaTime);
        }

        private void BeginFire1() {
        }

        private void EndFire1() {
            
        }

        private void BeginFire2() {
            
        }

        private void EndFire2() {
            
        }

        private Vector3 GetTargetPos() {
            Ray camRay = Camera.main.ScreenPointToRay(PlayerController.MousePosition);
            
            if (_groundPlane.Raycast(camRay, out float distance)) {
                return camRay.GetPoint(distance);
            }

            return Vector3.zero;
        }
    }
}