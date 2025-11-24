using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha.Flight {
    public class FlyState : MechState {
        public ThirdPersonCamera thirdPersonCamera;
        public RectTransform reticle;
        
        public override void OnEnter() {
            thirdPersonCamera.ResetOrientation();
            transform.localRotation = Quaternion.Euler(0, thirdPersonCamera.CurrentYaw, 0);
        }

        public override void OnExit() {
            
        }

        private void Update() {
            if (reticle) {
                reticle.position = MechFlightController.MousePosition;
            }
        }
    }
}
