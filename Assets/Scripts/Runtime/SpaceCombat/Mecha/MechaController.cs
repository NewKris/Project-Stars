using System;
using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class MechaController : MonoBehaviour {
        public ThirdPersonCamera thirdPersonCamera;

        [Header("Strafing")]
        public float maxStrafeSpeed;
        public float maxRotateSpeed;
        
        [Header("Flying")]
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
        public Rigidbody rigidBody;
        public RectTransform reticle;
        
        private bool _flying = false;
        private Vector2 _normalizedMousePosition;

        private void Awake() {
            PlayerMechController.OnToggleFlight += ToggleFlight;
            thirdPersonCamera.SetIsActive(true);
        }

        private void OnDestroy() {
            PlayerMechController.OnToggleFlight -= ToggleFlight;
        }

        private void Update() {
            thirdPersonCamera.Look(PlayerMechController.Look);
        }

        private void ActivateFlightMode() {
            thirdPersonCamera.SetFollowTargetRotation(true);
        }
        
        private void ActivateStrafeMode() {
            thirdPersonCamera.SetFollowTargetRotation(false);
        }

        private void ToggleFlight() {
            _flying = !_flying;

            if (_flying) {
                ActivateFlightMode();
            }
            else {
                ActivateStrafeMode();
            }
        }
    }
}
