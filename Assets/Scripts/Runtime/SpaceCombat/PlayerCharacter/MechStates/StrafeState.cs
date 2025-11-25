using UnityEngine;
using Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechCamera;
using Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechInput;
using Werehorse.Runtime.Utility.CommonBehaviours;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechStates {
    public class StrafeState : MechState {
        public MatchRotation mechYawFollow;
        
        public override void OnEnter() {
            playerCamera.SetCameraController(cameraController);
            StrafeInputListener.Enable();
            mechYawFollow.enabled = true;
        }

        public override void OnExit() {
            StrafeInputListener.Disable();
            mechYawFollow.enabled = false;
        }
    }
}
