using UnityEngine;
using Werehorse.Runtime.Common;

namespace Werehorse.Runtime.Ui {
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
