using NewKris.Runtime.Utility.CommonObjects;
using NewKris.Runtime.Utility.Extensions;
using UnityEngine;

namespace NewKris.Runtime.Ship {
    public class SpaceShip : MonoBehaviour {
        public float mouseSensitivity = 0.25f;
        
        [Header("Parameters")]
        public float maxMoveSpeed;
        public float moveDamping;

        [Header("Pitch")] 
        public float maxPitch;
        public float pitchDamping;
        
        [Header("Roll")]
        public float maxRoll;
        public float rollDamping;

        [Header("Weapons")] 
        public Weapon weapon1;
        public Weapon weapon2;
        
        [Header("Miscs")]
        public Transform modelPivot;
        public Boundary boundary;

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

            _position = new DampedVector(transform.position);
            Cursor.lockState = CursorLockMode.Locked;
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
            _position.Target += PlayerController.DeltaMouse.ProjectOnGround() * mouseSensitivity;
            _position.Target = Vector3.Max(boundary.MinCorner, _position.Target);
            _position.Target = Vector3.Min(boundary.MaxCorner, _position.Target);
            
            transform.position = _position.Tick(moveDamping, maxMoveSpeed);
            reticle.position = _position.Target;
        }

        private void RotateModel() {
            _roll.Target = GetTargetRoll();
            _pitch.Target = GetTargetPitch();

            modelPivot.localRotation = Quaternion.Euler(_pitch.Tick(pitchDamping), 0, -_roll.Tick(rollDamping));
        }

        private void BeginFire1() {
            weapon1?.BeginFire();
        }

        private void EndFire1() {
            weapon1?.EndFire();
        }

        private void BeginFire2() {
            weapon2?.BeginFire();
        }

        private void EndFire2() {
            weapon2?.EndFire();
        }

        private float GetTargetRoll() {
            return (_position.Velocity.x / maxMoveSpeed) * maxRoll;
        }

        private float GetTargetPitch() {
            return (_position.Velocity.z / maxMoveSpeed) * maxPitch;
        }
    }
}
