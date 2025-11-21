using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class MechaController : MonoBehaviour {
        public ThirdPersonCamera thirdPersonCamera;
        
        [Header("Strafing")]
        public float maxStrafeSpeed;
        public float maxStrafeAcceleration;
        public float angularDamping;
        
        private DampedAngle _mechYaw;
        private Rigidbody _rigidBody;

        private void Awake() {
            _rigidBody = GetComponent<Rigidbody>();
            
            SetCursorVisibility(false);
        }

        private void Update() {
            thirdPersonCamera.Look(PlayerMechController.Look);
        }

        private void FixedUpdate() {
            Strafe(Time.fixedDeltaTime);
            LookForward(Time.fixedDeltaTime);
        }

        private void Strafe(float dt) {
            Vector3 velocity = PlayerMechController.Move.ProjectOnGround();
            velocity.y = PlayerMechController.Lift;
            velocity = thirdPersonCamera.StrafeSpace * velocity.normalized * maxStrafeSpeed;

            Vector3 delta = velocity - _rigidBody.linearVelocity;
            delta = Vector3.ClampMagnitude(delta, maxStrafeAcceleration * dt);
            
            _rigidBody.AddForce(delta, ForceMode.VelocityChange);
        }

        private void LookForward(float dt) {
            _mechYaw.Target = thirdPersonCamera.CurrentYaw;
            transform.localRotation = Quaternion.Euler(0, _mechYaw.Tick(angularDamping, dt), 0);
        }
        
        private void SetCursorVisibility(bool showCursor) {
            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = showCursor;
        }
    }
}
