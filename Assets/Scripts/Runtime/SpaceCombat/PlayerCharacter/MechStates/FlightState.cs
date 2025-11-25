using Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechInput;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechStates {
    public class FlightState : MechState {
        public override void OnEnter() {
            playerCamera.SetCameraController(cameraController);
            FlightInputListener.Enable();
        }

        public override void OnExit() {
            FlightInputListener.Disable();
        }
    }
}
