using UnityEngine;
using Werehorse.Runtime.Combat;
using Werehorse.Runtime.Ship.Equipment;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Ship {
    public class SpaceShip : MonoBehaviour {
        public float sensitivity;

        [Header("Parameters")] 
        [Range(0,1 )] public float moveSpeed;
        [Range(0,1 )] public float responsiveness;
        
        [Header("Movement")]
        public float maxMoveSpeed;
        public float minMoveSpeed;
        public float maxMoveDamping;
        public float minMoveDamping;

        [Header("Pitch")] 
        public float maxPitch;
        public float pitchDamping;
        
        [Header("Roll")]
        public float maxRoll;
        public float rollDamping;
        
        [Header("Miscs")]
        public ShipEquipper equipper;
        public Transform modelPivot;
        public Boundary boundary;
        public Transform reticle;

        private Vector3 _velocity;
        private DampedVector _position;
        private DampedAngle _roll;
        private DampedAngle _pitch;

        private float TargetSpeed => Mathf.Lerp(minMoveSpeed, maxMoveSpeed, moveSpeed);
        private float TargetDamping => Mathf.Lerp(minMoveDamping, maxMoveDamping, 1 - responsiveness);

        private void OnGUI() {
            GUILayout.BeginArea(new Rect(10, 10, 100, 100));
            GUILayout.Label($"HP: {GetComponent<HurtBox>().CurrentHealth}");
            GUILayout.EndArea();
        }

        private void Awake() {
            PlayerController.OnBeginFire1 += BeginFire1;
            PlayerController.OnEndFire1 += EndFire1;
            PlayerController.OnBeginFire2 += BeginFire2;
            PlayerController.OnEndFire2 += EndFire2;

            _position = new DampedVector(transform.position);
        }

        private void OnDestroy() {
            PlayerController.OnBeginFire1 -= BeginFire1;
            PlayerController.OnEndFire1 -= EndFire1;
            PlayerController.OnBeginFire2 -= BeginFire2;
            PlayerController.OnEndFire2 -= EndFire2;
        }

        private void OnEnable() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable() {
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update() {
            if (Time.timeScale == 0) {
                return;
            }
            
            Move();
            RotateModel();
        }

        private void Move() {
            Vector3 previousPosition = transform.position;
            
            _position.Target = MouseToTargetPosition();
            _position.Target = Vector3.Max(boundary.MinCorner, _position.Target);
            _position.Target = Vector3.Min(boundary.MaxCorner, _position.Target);
            
            transform.position = _position.Tick(TargetDamping, TargetSpeed);
            reticle.position = _position.Target;

            _velocity = (transform.position - previousPosition) / Time.deltaTime;
        }

        private void RotateModel() {
            _roll.Target = GetTargetRoll();
            _pitch.Target = GetTargetPitch();

            modelPivot.localRotation = Quaternion.Euler(_pitch.Tick(pitchDamping), 0, -_roll.Tick(rollDamping));
        }

        private void BeginFire1() {
            equipper.weapon1?.BeginFire();
        }

        private void EndFire1() {
            equipper.weapon1?.EndFire();
        }

        private void BeginFire2() {
            equipper.weapon2?.BeginFire();
        }

        private void EndFire2() {
            equipper.weapon2?.EndFire();
        }

        private float GetTargetRoll() {
            return (_velocity.x / TargetSpeed) * maxRoll;
        }

        private float GetTargetPitch() {
            return (_velocity.z / TargetSpeed) * maxPitch;
        }

        private Vector3 MouseToTargetPosition() {
            return _position.Target + PlayerController.MousePosition.ProjectOnGround() * sensitivity;
        }
    }
}
