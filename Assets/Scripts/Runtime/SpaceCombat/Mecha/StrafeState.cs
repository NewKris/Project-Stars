using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class StrafeState : MechState {
        public ThirdPersonCamera thirdPersonCamera;
        public float maxStrafeSpeed;
        public float maxStrafeAcceleration;
        public float angularDamping;
        
        private DampedAngle _mechYaw;
        private Rigidbody _rigidBody;
        
        public override void OnEnter() {
            _mechYaw = new DampedAngle(thirdPersonCamera.CurrentYaw);
        }

        public override void OnExit() {
        }

        private void Update() {
            thirdPersonCamera.Look(PlayerMechController.Look);
        }

        private void FixedUpdate() {
            Strafe(Time.fixedDeltaTime);
            LookForward(Time.fixedDeltaTime);
        }

        private void Awake() {
            _rigidBody = GetComponent<Rigidbody>();
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
    }
}
