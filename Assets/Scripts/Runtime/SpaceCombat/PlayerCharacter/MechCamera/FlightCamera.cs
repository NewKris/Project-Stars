using System;
using UnityEngine;
using Werehorse.Runtime.Utility;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechCamera {
    public class FlightCamera : CameraController {
        public Transform target;
        public Vector3 offset;
        public float distance;
        
        public override Vector3 GetCameraPosition() {
            return target.TransformPoint(offset) - target.forward * distance;
        }

        public override Quaternion GetCameraRotation() {
            return target.rotation;
        }

        public override bool Ready() {
            return target != null;
        }

        private void OnDrawGizmos() {
            if (!Ready()) {
                return;
            }

            Vector3 pos = GetCameraPosition();
            HandlesProxy.DrawLine(target.TransformPoint(offset), pos, 3, false, Color.green);
            HandlesProxy.DrawSphere(pos, 0.5f, true, Color.green, 3);
        }
    }
}
