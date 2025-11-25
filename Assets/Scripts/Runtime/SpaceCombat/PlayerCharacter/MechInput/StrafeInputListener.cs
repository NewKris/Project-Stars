using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechInput {
    public class StrafeInputListener : MonoBehaviour {
        public static event Action OnToggleFlight;
        
        private static StrafeInputListener Instance;
        
        public int actionMapIndex;

        private InputAction _liftAction;
        private InputAction _rollAction;
        private InputAction _lookAction;
        private InputAction _moveAction;

        public static float Lift => Instance._liftAction.ReadValue<float>();
        public static float Roll => Instance._rollAction.ReadValue<float>();
        public static Vector2 Look => Instance._lookAction.ReadValue<Vector2>();
        public static Vector2 Move => Instance._moveAction.ReadValue<Vector2>();

        private InputActionMap ActionMap => InputSystem.actions.actionMaps[actionMapIndex];
        
        public static void Enable() {
            Instance.enabled = true;
        }

        public static void Disable() {
            Instance.enabled = false;
        }
        
        private void Awake() {
            Instance = this;
            
            _liftAction = ActionMap["Lift"];
            _rollAction = ActionMap["Roll"];
            _lookAction = ActionMap["Look"];
            _moveAction = ActionMap["Move"];
            
            ActionMap["Toggle Flight"].performed += _ => OnToggleFlight?.Invoke();
        }

        private void OnDestroy() {
            ActionMap.Dispose();
        }
    }
}
