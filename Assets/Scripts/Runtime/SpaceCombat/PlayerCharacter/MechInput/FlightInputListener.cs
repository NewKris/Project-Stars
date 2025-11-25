using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechInput {
    public class FlightInputListener : MonoBehaviour {
        public static event Action OnToggleFlight;
        
        private static FlightInputListener Instance;

        public int actionMapIndex;

        private InputAction _rollAction;
        private InputAction _aimAction;

        public static float Roll => Instance._rollAction.ReadValue<float>();
        public static Vector2 Aim => Instance._aimAction.ReadValue<Vector2>();
        
        private InputActionMap ActionMap => InputSystem.actions.actionMaps[actionMapIndex];

        public static void Enable() {
            Instance.enabled = true;
            Instance.ActionMap.Enable();
        }

        public static void Disable() {
            Instance.enabled = false;
            Instance.ActionMap.Disable();
        }
        
        private void Awake() {
            Instance = this;
            
            _rollAction = ActionMap["Roll"];
            _aimAction = ActionMap["Aim"];
            
            ActionMap["Toggle Flight"].performed += _ => OnToggleFlight?.Invoke();
        }
        
        private void OnDestroy() {
            ActionMap.Dispose();
        }
    }
}
