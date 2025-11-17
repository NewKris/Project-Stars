using UnityEngine;

namespace Werehorse.Runtime.Common.Ui {
    public class MenuController : MonoBehaviour {
        public void ExitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
