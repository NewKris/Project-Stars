using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechCamera {
    public abstract class CameraController : MonoBehaviour {
        public abstract Vector3 GetCameraPosition();
        public abstract Quaternion GetCameraRotation();
        public abstract bool Ready();
    }
}
