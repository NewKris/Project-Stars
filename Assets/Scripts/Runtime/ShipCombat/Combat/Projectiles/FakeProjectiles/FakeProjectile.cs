using UnityEngine;
using Werehorse.Runtime.ShipCombat.Utility.Attributes;

namespace Werehorse.Runtime.ShipCombat.Combat.Projectiles.FakeProjectiles {
    public class FakeProjectile : MonoBehaviour {
        public float maxTravelDuration;
        [ReadOnly] public Vector3 from;
        [ReadOnly] public Vector3 to;
        [ReadOnly] public float currentTravelDuration;
    }
}
