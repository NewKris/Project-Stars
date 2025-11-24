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
        private Vector3 _velocity;
        private CharacterController _characterController;
        
        public override void OnEnter() {
            _mechYaw = new DampedAngle(thirdPersonCamera.CurrentYaw);
        }

        public override void OnExit() {
        }

        private void Update() {
            thirdPersonCamera.Look(PlayerMechController.Look);
            Strafe(Time.deltaTime);
            LookForward(Time.deltaTime);
        }

        private void Awake() {
            _characterController = GetComponent<CharacterController>();
        }
        
        private void Strafe(float dt) {
            Vector3 targetVel = PlayerMechController.Move.ProjectOnGround();
            targetVel.y = PlayerMechController.Lift;
            targetVel = thirdPersonCamera.StrafeSpace * targetVel.normalized * maxStrafeSpeed;
            
            Vector3 delta = targetVel - _velocity;
            _velocity += Vector3.ClampMagnitude(delta * dt, maxStrafeAcceleration);

            _characterController.Move(_velocity * dt);
        }

        private void LookForward(float dt) {
            _mechYaw.Target = thirdPersonCamera.CurrentYaw;
            transform.localRotation = Quaternion.Euler(0, _mechYaw.Tick(angularDamping, dt), 0);
        }
    }
}
