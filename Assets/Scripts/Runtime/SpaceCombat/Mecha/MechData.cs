using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    [CreateAssetMenu(menuName = "Mechs/Mech Data")]
    public class MechData : ScriptableObject {
        public int id;
        public GameObject modelPrefab;
    }
}
