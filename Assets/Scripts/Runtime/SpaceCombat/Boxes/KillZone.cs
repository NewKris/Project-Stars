using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Boxes {
    public static class KillZone {
        public static bool IsKillZone(LayerMask layer) {
            return LayerMask.NameToLayer("Kill Zone") == layer;
        }
    }
}
