using UnityEngine;
using Random = UnityEngine.Random;

namespace Werehorse.Runtime.Environment {
    public class Tumbler : MonoBehaviour {
        public float maxTumbleSpeed;
        public float minTumbleSpeed;
        public Transform pivot;

        private float _tumbleSpeed;
        private Vector3 _rotateAxis;

        private void OnEnable() {
            _tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed);
            _rotateAxis = Random.insideUnitSphere;
        }

        private void Update() {
            pivot.Rotate(_rotateAxis, _tumbleSpeed * Time.deltaTime);
        }
    }
}
