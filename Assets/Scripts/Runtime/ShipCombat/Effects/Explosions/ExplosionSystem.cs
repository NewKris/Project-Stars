using UnityEngine;
using Werehorse.Runtime.ShipCombat.Combat;
using Werehorse.Runtime.ShipCombat.Utility.CommonObjects;

namespace Werehorse.Runtime.ShipCombat.Effects.Explosions {
    public class ExplosionSystem : MonoBehaviour {
        private static ExplosionSystem Instance;

        public GameObject explosionPrefab;
        
        private PrefabPool _explosionPool;
        
        public static void SpawnExplosion(Vector3 position, LayerMask targetFaction, int damage) {
            if (Instance._explosionPool.GetObject(out GameObject explosion)) {
                HitBox explosionHitBox = explosion.GetComponent<HitBox>();
                
                explosion.transform.position = position;
                explosionHitBox.canHitFaction = targetFaction;
                explosionHitBox.damage = damage;
                
                explosion.SetActive(true);
            }
        }

        private void Awake() {
            Instance = this;
            _explosionPool = new PrefabPool(explosionPrefab, transform, 25);
        }
    }
}
