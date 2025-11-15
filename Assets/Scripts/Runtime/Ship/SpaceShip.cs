using TMPro;
using UnityEngine;
using Werehorse.Runtime.Combat;
using Werehorse.Runtime.Ship.Equipment;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Ship {
    public class SpaceShip : MonoBehaviour {
        public float maxFlightSpeed;
        public float accelerationSpeed;

        [Header("Turning")] 
        public float maxPitchSpeed;
        public float maxYawSpeed;
        public float maxRollSpeed;
        public float maxTurnSpeed;
        public float maxTurnAngle;
        public AnimationCurve turnCurve;
        
        [Header("Miscs")]
        public ShipEquipper equipper;
        public Rigidbody rigidBody;
        public RectTransform reticle;

        private Vector2 _normalizedMousePosition;
        
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

        private void OnEnable() {
            Cursor.visible = false;
        }

        private void Update() {
            if (Time.timeScale == 0) {
                return;
            }
            
            reticle.position = PlayerController.MousePosition;
        }
        
        private void FixedUpdate() {
            if (Time.timeScale == 0) {
                return;
            }
            
            Rotate(Time.fixedDeltaTime);
            Move();
        }

        private void Rotate(float dt) {
            _normalizedMousePosition = GetNormalizedMousePosition(PlayerController.MousePosition);
            Vector3 forward = transform.forward;
            Vector3 up = transform.up;
            
            Quaternion pitch = Quaternion.AngleAxis(_normalizedMousePosition.y * -maxPitchSpeed * dt, transform.right);
            Quaternion yaw = Quaternion.AngleAxis(_normalizedMousePosition.x * maxYawSpeed * dt, transform.up);
            Quaternion roll = Quaternion.AngleAxis(PlayerController.Roll * -maxRollSpeed * dt, transform.forward);
            
            forward = yaw * pitch * forward;
            up = pitch * roll * up;
            
            Quaternion targetRot = Quaternion.LookRotation(forward, up);
            
            float angle = Quaternion.Angle(transform.rotation, targetRot);
            float t = angle / maxTurnAngle;
            float maxDelta = maxTurnSpeed * turnCurve.Evaluate(t) * dt;
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, maxDelta);
        }
        
        private void Move() {
            Vector3 targetVel = transform.forward * maxFlightSpeed;
            Vector3 deltaVel = targetVel - rigidBody.linearVelocity;
            rigidBody.AddForce(deltaVel, ForceMode.VelocityChange);            
        }

        private Vector2 GetNormalizedMousePosition(Vector2 mousePosition) {
            return new Vector2() {
                x = Mathf.Lerp(-1, 1, mousePosition.x / Screen.width),
                y = Mathf.Lerp(-1, 1, mousePosition.y / Screen.height)
            };
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
    }
}
