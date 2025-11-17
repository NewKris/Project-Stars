using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Combat {
    public static class KillZone {
        public static bool IsKillZone(LayerMask layer) {
            return LayerMask.NameToLayer("Kill Zone") == layer;
        }
    }
}
