using System.Linq;
using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    [CreateAssetMenu(menuName = "Mechs/Database")]
    public class MechDatabase : ScriptableObject {
        public MechData[] mechs;

        public MechData GetMech(int id) {
            return mechs.First(x => x.id == id);
        }
    }
}
