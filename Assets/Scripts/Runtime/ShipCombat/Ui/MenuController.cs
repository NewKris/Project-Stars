using UnityEngine;
using Werehorse.Runtime.ShipCombat.Common;

namespace Werehorse.Runtime.ShipCombat.Ui {
    public class MenuController : MonoBehaviour {
        public void GoToScene(int gameScene) {
            SceneTransitionController.LoadScene((GameScene)gameScene);
        }

        public void ExitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
