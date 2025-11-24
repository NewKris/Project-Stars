using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class PlayerFlightController : MonoBehaviour {
        public static event Action OnToggleFlight;
        
        private static PlayerFlightController Instance;

        public int shipActionMap = 0;

        private InputAction _aimAction;
        private InputAction _rollAction;
        private InputAction _yawAction;
        private InputAction _pitchAction;
        
        public static float Roll { get; private set; }
        public static float Yaw { get; private set; }
        public static float Pitch { get; private set; }
        public static Vector2 MousePosition { get; private set; }
        
        private InputActionMap ActionMap => InputSystem.actions.actionMaps[shipActionMap];
        
        public static void SetEnabled(bool enabled) {
            Instance.enabled = enabled;
        }
        
        private void Awake() {
            Instance = this;
            
            _aimAction = ActionMap["Aim"];
            _rollAction = ActionMap["Roll"];
            _yawAction = ActionMap["Yaw"];
            _pitchAction = ActionMap["Pitch"];
            
            ActionMap["Toggle Flight"].performed += _ => OnToggleFlight?.Invoke();
        }

        private void OnEnable() {
            ActionMap.Enable();
        }

        private void OnDisable() {
            ActionMap.Disable();
        }

        private void OnDestroy() {
            ActionMap.Dispose();
        }

        private void Update() {
            MousePosition = _aimAction.ReadValue<Vector2>();
            Roll = _rollAction.ReadValue<float>();
            Yaw = _yawAction.ReadValue<float>();
            Pitch = _pitchAction.ReadValue<float>();
        }
    }
}
