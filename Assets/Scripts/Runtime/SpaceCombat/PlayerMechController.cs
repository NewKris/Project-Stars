using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.SpaceCombat {
    public class PlayerMechController : MonoBehaviour {
        public static event Action OnToggleFlight;
        
        public int actionMapIndex = 2;

        private InputAction _boostAction;
        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _steerAction;

        private InputActionMap ActionMap => InputSystem.actions.actionMaps[actionMapIndex];
        
        public static bool Boost { get; private set; }
        public static Vector2 Look { get; private set; }
        public static Vector2 Move { get; private set; }
        public static Vector2 Steer { get; private set; }
        
        private void Awake() {
            _boostAction = ActionMap["Boost"];
            _lookAction = ActionMap["Look"];
            _moveAction = ActionMap["Move"];
            _steerAction = ActionMap["Steer"];
            
            ActionMap["Toggle Flight"].performed += _ => OnToggleFlight?.Invoke();
            
            ActionMap.Enable();
        }

        private void OnDestroy() {
            ActionMap.Dispose();
        }

        private void Update() {
            Boost = _boostAction.ReadValue<float>() != 0;
            Look = _lookAction.ReadValue<Vector2>();
            Move = _moveAction.ReadValue<Vector2>();
            Steer = _steerAction.ReadValue<Vector2>();
        }
    }
}
