using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Hub {
    public class FirstPersonCharacter : MonoBehaviour {
        public Transform playerCamera;

        [Header("Movement")] 
        public float maxMoveSpeed;
        public CharacterController characterController;
        
        [Header("Camera")]
        public Vector3 cameraOffset;
        public float sensitivity;
        public float yawScale;
        public float pitchScale;
        public float minPitch;
        public float maxPitch;
        public float lookDamping;

        [Header("Interaction")] 
        public float interactRange;
        public LayerMask interactionMask;
        public GameObject prompt;

        private DampedAngle _yaw;
        private DampedAngle _pitch;
        private Transform _yawPivot;
        private Transform _pitchPivot;
        private Interactable _interactable;

        private void OnValidate() {
            if (playerCamera) {
                playerCamera.position = transform.TransformPoint(cameraOffset);
            }
        }

        private void Awake() {
            Time.timeScale = 1;
            
            PlayerHubController.OnInteract += TryInteract;
        }

        private void OnDestroy() {
            PlayerHubController.OnInteract -= TryInteract;
        }

        private void Start() {
            SetupCamera();
        }

        private void Update() {
            if (Time.deltaTime <= 0) {
                // For some reason this can happen on rapid scene-switching, which causes NaNs on SmoothDamp
                return;
            }
            
            Look(Time.deltaTime);
            Move();
        }

        private void FixedUpdate() {
            _interactable = FindInteractable();
        }

        private void Move() {
            Vector3 velocity = PlayerHubController.Move.ProjectOnGround();
            velocity = _yawPivot.localRotation * velocity;
            
            characterController.SimpleMove(velocity * maxMoveSpeed);
        }

        private void Look(float dt) {
            float deltaYaw = PlayerHubController.Look.x * yawScale * sensitivity;
            _yaw.Target += deltaYaw;
            _yaw.Target %= 360;
            _yawPivot.localRotation = Quaternion.Euler(0, _yaw.Tick(lookDamping, dt), 0);
            
            float deltaPitch = PlayerHubController.Look.y * pitchScale * sensitivity;
            _pitch.Target -= deltaPitch;
            _pitch.Target = Mathf.Clamp(_pitch.Target, minPitch, maxPitch);
            _pitchPivot.localRotation = Quaternion.Euler(_pitch.Tick(lookDamping, dt), 0, 0);
        }
        
        private void SetupCamera() {
            _yawPivot = new GameObject("Yaw Pivot").transform;
            _pitchPivot = new GameObject("Pitch Pivot").transform;
            
            _yawPivot.SetParent(transform);
            _yawPivot.SetLocalPositionAndRotation(cameraOffset, Quaternion.identity);
            
            _pitchPivot.SetParent(_yawPivot);
            _pitchPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            playerCamera.SetParent(_pitchPivot);
            playerCamera.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            _yaw = new DampedAngle();
            _pitch = new DampedAngle();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private Interactable FindInteractable() {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool hitInteraction = Physics.Raycast(ray, out RaycastHit hit, interactRange,  interactionMask);
            prompt.SetActive(hitInteraction);
            
            return hitInteraction ? hit.collider.GetComponent<Interactable>() : null;
        }

        private void TryInteract() {
            if (_interactable) {
                _interactable.Interact();
            }
        }
    }
}
