using System;
using UnityEngine;
using Werehorse.Runtime.Utility;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechCamera {
    public class ThirdPersonCamera : CameraController {
        [Header("Pivots")] 
        public Transform yawPivot;
        public Transform pitchPivot;
        public Vector3 pitchOffset;
        
        [Header("Spring Arm")] 
        public Vector3 shoulderOffset;
        public float radius;
        public float length;
        public LayerMask obstacleMask;
        
        private Vector3 SpringArmOrigin => pitchPivot.TransformPoint(shoulderOffset);
        private Vector3 SpringArmDirection => -pitchPivot.forward;

        public override Vector3 GetCameraPosition() {
            return CalculateSpringArmEnd();
        }

        public override Quaternion GetCameraRotation() {
            return Quaternion.LookRotation(pitchPivot.forward, yawPivot.up);
        }

        public override bool Ready() {
            return yawPivot && pitchPivot;
        }

        private Vector3 CalculateSpringArmEnd() {
            Ray ray = new Ray(SpringArmOrigin, SpringArmDirection);
            
            if (Physics.SphereCast(ray, radius, out RaycastHit hit, length, obstacleMask)) {
                return ray.GetPoint(hit.distance);
            }

            return ray.GetPoint(length);
        }

        private void OnDrawGizmos() {
            if (!Ready()) {
                return;
            }

            Vector3 end = GetCameraPosition();
            HandlesProxy.DrawLine(SpringArmOrigin, end, 3, false, Color.red);
            HandlesProxy.DrawSphere(end, radius, true, Color.red, 3);
        }
    }
}
