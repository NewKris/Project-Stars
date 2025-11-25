using UnityEngine;
using Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechCamera;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechStates {
    public abstract class MechState : MonoBehaviour {
        public CameraController cameraController;
        public PlayerCamera playerCamera;
        
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
