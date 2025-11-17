using UnityEngine;
using Werehorse.Runtime.ShipCombat.Utility.CommonObjects;

namespace Werehorse.Runtime.ShipCombat.Effects.Sparks {
    public class SparksSystem : MonoBehaviour {
        private static SparksSystem Instance;

        public GameObject vfxPrefab;
        public int capacity;

        private PrefabPool _pool;
        
        public static void PlaySparks(Vector3 point, Vector3 normal) {
            if (Instance._pool.GetObject(out GameObject spark)) {
                spark.transform.position = point;
                spark.transform.up = normal;
                spark.gameObject.SetActive(true);
            }
        }
        
        private void Awake() {
            Instance = this;
            _pool = new PrefabPool(vfxPrefab, transform, capacity);
        }
    }
}
