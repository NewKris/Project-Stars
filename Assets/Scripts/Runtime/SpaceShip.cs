using System;
using NewKris.Runtime.Utility;
using NewKris.Runtime.Utility.CommonObjects;
using UnityEngine;

namespace NewKris.Runtime {
    public class SpaceShip : MonoBehaviour {
        public float maxMoveSpeed;
        public float moveDamping;
        public Transform modelPivot;

        [Header("Pitch")] 
        public float maxPitch;
        public float pitchDamping;
        
        [Header("Roll")]
        public float maxRoll;
        public float rollDamping;

        public Transform reticle;

        private DampedVector _position;
        private DampedAngle _roll;
        private DampedAngle _pitch;
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
            Move();
            RotateModel();
        }

        private void Move() {
            _position.Target = GetTargetPos();
            transform.position = _position.Tick(moveDamping, maxMoveSpeed);
            reticle.position = _position.Target;
        }

        private void RotateModel() {
            _roll.Target = GetTargetRoll();
            _pitch.Target = GetTargetPitch();

            modelPivot.localRotation = Quaternion.Euler(_pitch.Tick(pitchDamping), 0, -_roll.Tick(rollDamping));
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

        private float GetTargetRoll() {
            return (_position.Velocity.x / maxMoveSpeed) * maxRoll;
        }

        private float GetTargetPitch() {
            return (_position.Velocity.z / maxMoveSpeed) * maxPitch;
        }
    }
}
