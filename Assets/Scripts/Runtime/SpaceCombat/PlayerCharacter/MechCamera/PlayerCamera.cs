using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechCamera {
    public class PlayerCamera : MonoBehaviour {
        public float followDamping;
        public float rotateSpeed;
        public CameraController currentCameraController;

        private DampedVector _position;
        
        public void SetCameraController(CameraController cameraController) {
            currentCameraController = cameraController;
        }
        
        private void OnValidate() {
            if (!currentCameraController || !currentCameraController.Ready()) {
                return;
            }

            transform.position = currentCameraController.GetCameraPosition();
            transform.rotation = currentCameraController.GetCameraRotation();
        }

        private void Update() {
            if (!currentCameraController) {
                return;
            }
            
            _position.Target = currentCameraController.GetCameraPosition();
            transform.position = _position.Tick(followDamping);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentCameraController.GetCameraRotation(), rotateSpeed * Time.deltaTime);
        }
    }
}
