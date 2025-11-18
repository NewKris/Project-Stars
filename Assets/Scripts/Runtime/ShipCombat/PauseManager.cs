using System;
using UnityEngine;

namespace Werehorse.Runtime.ShipCombat {
    public class PauseManager : MonoBehaviour {
        public static event Action<bool> OnPauseToggled; 
        
        public static bool IsPaused { get; private set; }

        public static void TogglePause() {
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0 : 1;
            OnPauseToggled?.Invoke(IsPaused);
        }

        public static void SetPause(bool paused) {
            IsPaused = paused;
            Time.timeScale = IsPaused ? 0 : 1;
            OnPauseToggled?.Invoke(IsPaused);
        }
        
        private void Awake() {
            PlayerShipController.OnPause += TogglePause;
        }

        private void OnDestroy() {
            Time.timeScale = 1;
            IsPaused = false;
            
            PlayerShipController.OnPause -= TogglePause;
        }
    }
}
