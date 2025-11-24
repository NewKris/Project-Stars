using UnityEngine;
using UnityEngine.SceneManagement;
using Werehorse.Runtime.Utility;

namespace Werehorse.Runtime.Common.SceneTransition {
    public class SceneTransitionController : MonoBehaviour {
        private static SceneTransitionController Instance;

        public static void LoadScene(GameScene scene) {
            SceneManager.LoadScene((int)scene);
        }
        
        public static void LoadScene(int sceneId) {
            SceneManager.LoadScene(sceneId);
        }
        
        private void Awake() {
            Singleton.SetSingleton(ref Instance, this);
        }

        private void OnDestroy() {
            Singleton.UnsetSingleton(ref Instance, this);
        }
    }
}
