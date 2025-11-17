using UnityEngine;
using UnityEngine.SceneManagement;
using Werehorse.Runtime.ShipCombat.Utility;

namespace Werehorse.Runtime.ShipCombat.Common {
    public class SceneTransitionController : MonoBehaviour {
        private static SceneTransitionController Instance;

        public static void LoadScene(GameScene scene) {
            SceneManager.LoadScene((int)scene);
        }
        
        private void Awake() {
            Singleton.SetSingleton(ref Instance, this);
        }

        private void OnDestroy() {
            Singleton.UnsetSingleton(ref Instance, this);
        }
    }
}
