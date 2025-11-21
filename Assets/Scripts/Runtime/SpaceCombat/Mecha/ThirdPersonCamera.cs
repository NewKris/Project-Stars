using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class ThirdPersonCamera : MonoBehaviour {
        public bool isActive;
        public float sensitivity = 1;
        public float cameraDamping;

        [Header("Target")] 
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

        private bool _followTargetRotation;
        private float _pitch;
        private float _yaw;
        private Vector2 _axisScaling;
        private Vector2 _deltaMouse;
        private Vector3 _cameraVelocity;
        private Vector3 _aimTargetVelocity;
        private GameObject _root;
        private GameObject _pitchPivot;
        private GameObject _yawPivot;
        private GameObject _armEnd;
        
        private float PitchRotation {
            get => _pitchPivot.transform.localRotation.eulerAngles.x;
            set => _pitchPivot.transform.localRotation = Quaternion.Euler(value, 0, 0);
        }

        private float YawRotation {
            get => _yawPivot.transform.localRotation.eulerAngles.y;
            set => _yawPivot.transform.localRotation = Quaternion.Euler(0, value, 0);
        }

        private Vector3 CurrentPivotPosition => _pitchPivot.transform.position;
        private Vector3 TargetPivotPosition => target.position + target.TransformDirection(pivotOffset);
        private Vector3 PitchForward => _pitchPivot.transform.forward;
        private Vector3 TargetForward => target.forward;
        private Vector3 TargetUp => target.up;

        public void SetFollowTargetRotation(bool followParentRotation) {
            _followTargetRotation = followParentRotation;
        }
        
        public void SetIsActive(bool active) {
            Cursor.lockState = active ? CursorLockMode.Locked : CursorLockMode.None;
            isActive = active;
        }

        public void Look(Vector2 deltaMouse) {
            _deltaMouse = deltaMouse;
        }
        
        private void OnValidate() {
            if (!target) {
                return;
            }

            transform.position = CalculateArmEndPosition(TargetPivotPosition, -TargetForward, armLength);
            transform.LookAt(TargetPivotPosition);
        }

        private void Awake() {
            _root = new GameObject("Camera Root");
            _root.transform.SetParent(transform.parent);
            
            _yawPivot = new GameObject("Yaw Pivot");
            _yawPivot.transform.SetParent(transform.parent);
            
            if (target) {
                _yawPivot.transform.position = TargetPivotPosition;
            }
            
            _pitchPivot = new GameObject("Pitch Pivot");
            _pitchPivot.transform.SetParent(_yawPivot.transform);
            _pitchPivot.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            _armEnd = new GameObject("Arm End");
            
            _pitch = PitchRotation;
            _yaw = YawRotation;
            _axisScaling = new Vector2(pitchScaling, yawScaling) * sensitivity;
        }

        private void Update() {
            if (!isActive || !target) {
                return;
            }
            
            Move();
            
            if (_followTargetRotation) {
                FollowTargetRotation();
            }
            else {
                RotatePivots();
            }
            
            MoveCamera();
        }
        
        private void Move() {
            _root.transform.position = target.position;
        }

        private void FollowTargetRotation() {
            _root.transform.SetPositionAndRotation(target.position, target.rotation);
            
            _pitch = 0;
            _yaw = 0;
            PitchRotation = _pitch;
            YawRotation = _yaw;
        }

        private void MoveCamera() {
            _armEnd.transform.position = CalculateArmEndPosition(CurrentPivotPosition, -PitchForward, armLength);

            transform.position = Vector3.SmoothDamp(
                transform.position, 
                _armEnd.transform.position, 
                ref _cameraVelocity,
                cameraDamping
            );
            
            transform.LookAt(CurrentPivotPosition);
        }
        
        private void RotatePivots() {
            Vector2 velocity = Vector2.Scale(_deltaMouse, _axisScaling);

            _pitch -= velocity.y;
            _pitch = Mathf.Clamp(_pitch, minAngle, maxAngle);
            PitchRotation = _pitch;

            _yaw += velocity.x;
            _yaw %= 360;
            YawRotation = _yaw;
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

        private void OnDrawGizmos() {
            if (!target) {
                return;
            }
            
            Vector3 start = _pitchPivot == null ? TargetPivotPosition : CurrentPivotPosition;
            Vector3 forward = _pitchPivot == null ? TargetForward : PitchForward;
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(start, -forward * armLength);
            Gizmos.DrawWireSphere(CalculateArmEndPosition(start, -forward, armLength), radius);
        }
    }
}
