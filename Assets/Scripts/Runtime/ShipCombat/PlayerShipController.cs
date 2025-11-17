using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Werehorse.Runtime.ShipCombat {
    public class PlayerShipController : MonoBehaviour {
        public static event Action OnBeginFire1;
        public static event Action OnEndFire1;
        public static event Action OnBeginFire2;
        public static event Action OnEndFire2;
        public static event Action OnTest;
        
        private static PlayerShipController Instance;

        public int shipActionMap = 0;

        private bool _isPaused;
        private InputAction _aimAction;
        private InputAction _rollAction;
        
        public static float Roll { get; private set; }
        public static Vector2 MousePosition { get; private set; }
        
        private void Awake() {
            _aimAction = InputSystem.actions["Aim"];
            _rollAction = InputSystem.actions["Roll"];
            
            InputSystem.actions["Fire1"].performed += _ => OnBeginFire1?.Invoke();
            InputSystem.actions["Fire1"].canceled += _ => OnEndFire1?.Invoke();

            InputSystem.actions["Fire2"].performed += _ => OnBeginFire2?.Invoke();
            InputSystem.actions["Fire2"].canceled += _ => OnEndFire2?.Invoke();
            
            InputSystem.actions["Pause"].performed += _ => PauseGame();
            
            InputSystem.actions["Test"].performed += _ => OnTest?.Invoke();
            
            InputSystem.actions.actionMaps[shipActionMap].Enable();
        }

        private void OnDestroy() {
            InputSystem.actions.actionMaps[shipActionMap].Dispose();
        }

        private void Update() {
            MousePosition = _aimAction.ReadValue<Vector2>();
            Roll = _rollAction.ReadValue<float>();
        }

        private void PauseGame() {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;
        }
    }
}
