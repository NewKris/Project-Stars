using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class ThirdPersonCamera : MonoBehaviour {
        public float sensitivity = 1;
        public float cameraDamping;
        public float lookDamping;

        [Header("Drone")] 
        public Transform target;
        public Vector3 pivotOffset;
        
        [Header("Yaw")]
        public float yawScaling = 1;

        [Header("Pitch")]
        public float pitchScaling = 1;
        public float minAngle = -80;
        public float maxAngle = 80;

        [Header("Spring Arm")] 
        public float armLength;
        public float radius;
        public LayerMask collisionMask;

        private float _pitch;
        private float _yaw;
        private Vector2 _axisScaling;
        private Vector2 _deltaMouse;
        private DampedVector _position;
        
        private Transform _pitchPivot;
        private Transform _yawPivot;
        private Transform _armEnd;

        public float CurrentYaw => _yawPivot.localRotation.eulerAngles.y;
        public Quaternion StrafeSpace => _yawPivot.rotation;
        
        private float PitchRotation {
            get => _pitchPivot.localRotation.eulerAngles.x;
            set => _pitchPivot.localRotation = Quaternion.Euler(value, 0, 0);
        }

        private float YawRotation {
            get => _yawPivot.localRotation.eulerAngles.y;
            set => _yawPivot.localRotation = Quaternion.Euler(0, value, 0);
        }

        private Vector3 CurrentPivotPosition => _pitchPivot.position;
        private Vector3 TargetPivotPosition => target.position + pivotOffset;
        private Vector3 SpringArmDirection => -_pitchPivot.forward;
        
        public void Look(Vector2 deltaMouse) {
            _deltaMouse = deltaMouse;
        }
        
        private void OnValidate() {
            if (!target) {
                return;
            }
            
            transform.position = CalculateArmEndPosition(TargetPivotPosition, -target.forward, armLength);
            transform.LookAt(TargetPivotPosition);
        }

        private void Awake() {
            BuildHierarchy();
            ResetCameraTransform();
            
            _pitch = PitchRotation;
            _yaw = YawRotation;
            _axisScaling = new Vector2(pitchScaling, yawScaling) * sensitivity;
            
            if (target) {
                _yawPivot.position = target.position;
            }
        }

        private void Update() {
            Rotate();
        }

        private void FixedUpdate() {
            Move();
            Look();
        }

        private void Rotate() {
            Vector2 velocity = Vector2.Scale(_deltaMouse, _axisScaling);

            _pitch -= velocity.y;
            _pitch = Mathf.Clamp(_pitch, minAngle, maxAngle);
            PitchRotation = _pitch;

            _yaw += velocity.x;
            _yaw %= 360;
            YawRotation = _yaw;

            _armEnd.position = CalculateArmEndPosition(CurrentPivotPosition, SpringArmDirection, armLength);
            _position.Target = _armEnd.position;
        }

        private void Look() {
            transform.LookAt(CurrentPivotPosition);
        }
        
        private void Move() {
            if (target) {
                _yawPivot.position = target.position;
            }
            
            transform.position = _position.Tick(cameraDamping, Time.fixedDeltaTime, Mathf.Infinity);
        }

        private Vector3 CalculateArmEndPosition(Vector3 start, Vector3 dir, float maxLength) {
            return start + dir * CalculateArmLength(start, dir, maxLength);
        }
        
        private float CalculateArmLength(Vector3 start, Vector3 dir, float maxLength) {
            Ray ray = new Ray(start, dir);

            if (Physics.SphereCast(ray, radius, out RaycastHit hit, maxLength, collisionMask)) {
                return hit.distance;
            }

            return maxLength;
        }

        private void BuildHierarchy() {
            _yawPivot = new GameObject("Yaw Pivot").transform;
            _yawPivot.SetParent(transform.parent);
            _yawPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            _pitchPivot = new GameObject("Pitch Pivot").transform;
            _pitchPivot.SetParent(_yawPivot.transform);
            _pitchPivot.SetLocalPositionAndRotation(pivotOffset, Quaternion.identity);
            
            _armEnd = new GameObject("Arm End").transform;
            _armEnd.SetParent(transform.parent);
            _armEnd.localRotation = Quaternion.identity;
            _armEnd.position = CalculateArmEndPosition(CurrentPivotPosition, SpringArmDirection, armLength);
        }

        private void ResetCameraTransform() {
            _position = new DampedVector(_armEnd.position);
            transform.position = _position.Current;
            transform.LookAt(CurrentPivotPosition);
        }
        
        private void OnDrawGizmos() {
            if (!target) {
                return;
            }
            
            Vector3 start = _pitchPivot == null ? TargetPivotPosition : CurrentPivotPosition;
            Vector3 dir = _pitchPivot == null ? -target.forward : SpringArmDirection;
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(start, dir * armLength);
            Gizmos.DrawWireSphere(CalculateArmEndPosition(start, dir, armLength), radius);
        }
    }
}
