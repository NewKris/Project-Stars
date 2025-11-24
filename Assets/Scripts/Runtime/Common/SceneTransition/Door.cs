using UnityEngine;

namespace Werehorse.Runtime.Common.SceneTransition {
    public class Door : MonoBehaviour {
        public void GoToScene(int sceneIndex) {
            SceneTransitionController.LoadScene(sceneIndex);
        }
    }
}
