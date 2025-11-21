using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.SpaceCombat {
    public class PlayerShipController : MonoBehaviour {
        public static event Action OnBeginFire1;
        public static event Action OnEndFire1;
        public static event Action OnBeginFire2;
        public static event Action OnEndFire2;
        public static event Action OnTest;
        public static event Action OnPause;
        public static event Action OnToggleSteering;
        
        private static PlayerShipController Instance;

        public int shipActionMap = 0;

        private InputAction _aimAction;
        private InputAction _rollAction;
        private InputAction _yawAction;
        private InputAction _pitchAction;
        
        public static float Roll { get; private set; }
        public static float Yaw { get; private set; }
        public static float Pitch { get; private set; }
        public static Vector2 MousePosition { get; private set; }
        
        private void Awake() {
            _aimAction = InputSystem.actions.actionMaps[shipActionMap]["Aim"];
            _rollAction = InputSystem.actions.actionMaps[shipActionMap]["Roll"];
            _yawAction = InputSystem.actions.actionMaps[shipActionMap]["Yaw"];
            _pitchAction = InputSystem.actions.actionMaps[shipActionMap]["Pitch"];
            
            InputSystem.actions.actionMaps[shipActionMap]["Fire1"].performed += _ => OnBeginFire1?.Invoke();
            InputSystem.actions.actionMaps[shipActionMap]["Fire1"].canceled += _ => OnEndFire1?.Invoke();

            InputSystem.actions.actionMaps[shipActionMap]["Fire2"].performed += _ => OnBeginFire2?.Invoke();
            InputSystem.actions.actionMaps[shipActionMap]["Fire2"].canceled += _ => OnEndFire2?.Invoke();
            
            InputSystem.actions.actionMaps[shipActionMap]["Pause"].performed += _ => OnPause?.Invoke();
            
            InputSystem.actions.actionMaps[shipActionMap]["Test"].performed += _ => OnTest?.Invoke();
            
            InputSystem.actions.actionMaps[shipActionMap]["Toggle Steering"].performed += _ => OnToggleSteering?.Invoke();
            
            InputSystem.actions.actionMaps[shipActionMap].Enable();
        }

        private void OnDestroy() {
            InputSystem.actions.actionMaps[shipActionMap].Dispose();
        }

        private void Update() {
            MousePosition = _aimAction.ReadValue<Vector2>();
            Roll = _rollAction.ReadValue<float>();
            Yaw = _yawAction.ReadValue<float>();
            Pitch = _pitchAction.ReadValue<float>();
        }
    }
}
