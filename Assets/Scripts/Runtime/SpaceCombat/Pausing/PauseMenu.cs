using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Pausing {
    public class PauseMenu : MonoBehaviour {
        public void ResumeGame() {
            PauseManager.TogglePause();
        }

        private void Awake() {
            PauseManager.OnPauseToggled += ToggleVisibility;
            gameObject.SetActive(false);
        }

        private void OnDestroy() {
            PauseManager.OnPauseToggled -= ToggleVisibility;
        }

        private void ToggleVisibility(bool isPaused) {
            gameObject.SetActive(isPaused);
        }
    }
}
