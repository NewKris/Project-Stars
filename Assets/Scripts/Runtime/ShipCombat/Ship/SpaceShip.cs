using UnityEngine;
using Werehorse.Runtime.ShipCombat.Ship.Equipment;

namespace Werehorse.Runtime.ShipCombat.Ship {
    public class SpaceShip : MonoBehaviour {
        public float maxFlightSpeed;
        public float maxAcceleration;

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
            PlayerShipController.OnBeginFire1 += BeginFire1;
            PlayerShipController.OnEndFire1 += EndFire1;
            PlayerShipController.OnBeginFire2 += BeginFire2;
            PlayerShipController.OnEndFire2 += EndFire2;
        }

        private void OnDestroy() {
            PlayerShipController.OnBeginFire1 -= BeginFire1;
            PlayerShipController.OnEndFire1 -= EndFire1;
            PlayerShipController.OnBeginFire2 -= BeginFire2;
            PlayerShipController.OnEndFire2 -= EndFire2;
        }

        private void OnEnable() {
            Cursor.visible = false;
        }

        private void Update() {
            if (Time.timeScale == 0) {
                return;
            }
            
            reticle.position = PlayerShipController.MousePosition;
        }
        
        private void FixedUpdate() {
            if (Time.timeScale == 0) {
                return;
            }
            
            Rotate(Time.fixedDeltaTime);
            Move(Time.fixedDeltaTime);
        }

        private void Rotate(float dt) {
            _normalizedMousePosition = GetNormalizedMousePosition(PlayerShipController.MousePosition);
            Vector3 forward = transform.forward;
            Vector3 up = transform.up;
            
            Quaternion pitch = Quaternion.AngleAxis(_normalizedMousePosition.y * -maxPitchSpeed * dt, transform.right);
            Quaternion yaw = Quaternion.AngleAxis(_normalizedMousePosition.x * maxYawSpeed * dt, transform.up);
            Quaternion roll = Quaternion.AngleAxis(PlayerShipController.Roll * -maxRollSpeed * dt, transform.forward);
            
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
