using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha.Database {
    [CreateAssetMenu(menuName = "Mechs/Mech Data")]
    public class MechData : ScriptableObject {
        public int id;
        public GameObject modelPrefab;
    }
}
