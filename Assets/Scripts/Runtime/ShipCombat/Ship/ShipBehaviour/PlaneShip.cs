using System;
using UnityEngine;
using Werehorse.Runtime.ShipCombat.Ship.Weapons;

namespace Werehorse.Runtime.ShipCombat.Ship.ShipBehaviour {
    public class PlaneShip : MonoBehaviour {
        public float maxFlightSpeed;
        public float maxAcceleration;

        [Header("Turning")] 
        public float maxPitchSpeed;
        public float maxYawSpeed;
        public float maxRollSpeed;
        public float maxTurnSpeed;
        public float maxTurnAngle;
        public AnimationCurve turnCurve;

        [Header("Weapons")] 
        public Weapon weapon1;
        public Weapon weapon2;
        
        [Header("Miscs")]
        public Rigidbody rigidBody;
        public RectTransform reticle;
        public GameObject gameOverScreen;

        private bool _mouseSteering = true;
        private Vector2 _normalizedMousePosition;
        
        public void Die() {
            gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
            SetCursorVisibility(true);
        }
        
        private void Awake() {
            PlayerShipController.OnBeginFire1 += BeginFire1;
            PlayerShipController.OnEndFire1 += EndFire1;
            PlayerShipController.OnBeginFire2 += BeginFire2;
            PlayerShipController.OnEndFire2 += EndFire2;
            PlayerShipController.OnToggleSteering += ToggleSteerMode;

            PauseManager.OnPauseToggled += SetCursorVisibility;
            
            SetCursorVisibility(false);
        }

        private void OnDestroy() {
            PlayerShipController.OnBeginFire1 -= BeginFire1;
            PlayerShipController.OnEndFire1 -= EndFire1;
            PlayerShipController.OnBeginFire2 -= BeginFire2;
            PlayerShipController.OnEndFire2 -= EndFire2;
            PlayerShipController.OnToggleSteering -= ToggleSteerMode;
            
            PauseManager.OnPauseToggled -= SetCursorVisibility;
        }

        private void Update() {
            if (PauseManager.IsPaused) {
                return;
            }

            if (reticle) {
                reticle.position = PlayerShipController.MousePosition;
            }
        }
        
        private void FixedUpdate() {
            if (PauseManager.IsPaused) {
                return;
            }
            
            Steer(GetSteerValues(), Time.fixedDeltaTime);
            Move(Time.fixedDeltaTime);
        }

        private SteerValues GetSteerValues() {
            if (_mouseSteering) {
                return new SteerValues() {
                    pitch = _normalizedMousePosition.y,
                    yaw = _normalizedMousePosition.x,
                    roll = PlayerShipController.Roll
                };
            }
            
            return new SteerValues() {
                pitch = PlayerShipController.Pitch,
                yaw = PlayerShipController.Yaw,
                roll = PlayerShipController.Roll
            };
        }
        
        private void Steer(SteerValues steerValues, float dt) {
            _normalizedMousePosition = GetNormalizedMousePosition(PlayerShipController.MousePosition);
            Vector3 forward = transform.forward;
            Vector3 up = transform.up;
            
            Quaternion pitch = Quaternion.AngleAxis(steerValues.pitch * -maxPitchSpeed * dt, transform.right);
            Quaternion yaw = Quaternion.AngleAxis(steerValues.yaw * maxYawSpeed * dt, transform.up);
            Quaternion roll = Quaternion.AngleAxis(steerValues.roll * -maxRollSpeed * dt, transform.forward);
            
            forward = yaw * pitch * forward;
            up = pitch * roll * up;
            
            Quaternion targetRot = Quaternion.LookRotation(forward, up);
            
            float angle = Quaternion.Angle(transform.rotation, targetRot);
            float t = angle / maxTurnAngle;
            float maxDelta = maxTurnSpeed * turnCurve.Evaluate(t) * dt;
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, maxDelta);
        }
        
        private void Move(float dt) {
            Vector3 targetVel = transform.forward * maxFlightSpeed;
            Vector3 deltaVel = targetVel - rigidBody.linearVelocity;
            deltaVel = Vector3.ClampMagnitude(deltaVel, maxAcceleration * dt);
            
            rigidBody.AddForce(deltaVel, ForceMode.VelocityChange);            
        }

        private Vector2 GetNormalizedMousePosition(Vector2 mousePosition) {
            return new Vector2() {
                x = Mathf.Lerp(-1, 1, mousePosition.x / Screen.width),
                y = Mathf.Lerp(-1, 1, mousePosition.y / Screen.height)
            };
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

        private void SetCursorVisibility(bool showCursor) {
            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = showCursor;
        }

        private void ToggleSteerMode() {
            _mouseSteering = !_mouseSteering;
        }
        
        private struct SteerValues {
            public float pitch;
            public float yaw;
            public float roll;
        }
    }
}
