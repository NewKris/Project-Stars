using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class PlayerMechController : MonoBehaviour {
        public static event Action OnToggleFlight;
        private static PlayerMechController Instance;
        
        public int actionMapIndex = 2;

        private InputAction _boostAction;
        private InputAction _rollAction;
        private InputAction _liftAction;
        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _steerAction;

        private InputActionMap ActionMap => InputSystem.actions.actionMaps[actionMapIndex];
        
        public static bool Boost { get; private set; }
        public static float Roll { get; private set; }
        public static float Lift { get; private set; }
        public static Vector2 Look { get; private set; }
        public static Vector2 Move { get; private set; }
        public static Vector2 Steer { get; private set; }

        public static void SetEnabled(bool enabled) {
            Instance.enabled = enabled;
        }
        
        private void Awake() {
            Instance = this;
            
            _boostAction = ActionMap["Boost"];
            _rollAction = ActionMap["Roll"];
            _liftAction = ActionMap["Lift"];
            _lookAction = ActionMap["Look"];
            _moveAction = ActionMap["Move"];
            _steerAction = ActionMap["Steer"];
            
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
            Boost = _boostAction.ReadValue<float>() != 0;
            Roll = _rollAction.ReadValue<float>();
            Lift = _liftAction.ReadValue<float>();
            Look = _lookAction.ReadValue<Vector2>();
            Move = _moveAction.ReadValue<Vector2>();
            Steer = _steerAction.ReadValue<Vector2>();
        }
    }
}
