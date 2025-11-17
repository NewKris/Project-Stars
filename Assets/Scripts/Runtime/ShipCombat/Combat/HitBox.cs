using UnityEngine;
using UnityEngine.Events;
using Werehorse.Runtime.ShipCombat.Utility.Extensions;

namespace Werehorse.Runtime.ShipCombat.Combat {
    public class HitBox : MonoBehaviour {
        public int damage;
        public LayerMask canHitFaction;
        public UnityEvent<Collider> onHit;

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out HurtBox hurtBox) && CanHurtFaction(hurtBox.gameObject.layer)) {
                hurtBox.TakeDamage(damage);
                onHit.Invoke(other);
            }
        }

        private bool CanHurtFaction(LayerMask faction) {
            return canHitFaction.ContainstLayer(faction) || KillZone.IsKillZone(faction);
        }
    }
}
