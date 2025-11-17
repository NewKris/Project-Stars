using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Common {
    public class SplashScreenController : MonoBehaviour {
        public float waitTime;

        private bool _done = false;
        private float _timer = 0;

        private void Update() {
            _timer += Time.deltaTime;

            if (_timer >= waitTime && !_done) {
                _done = true;
                SceneTransitionController.LoadScene(GameScene.MAIN_MENU);
            }
        }
    }
}
