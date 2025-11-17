using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Utility.Extensions {
    public static class LayerMaskExtensions {
        public static bool ContainstLayer(this LayerMask mask, int layer) {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}
