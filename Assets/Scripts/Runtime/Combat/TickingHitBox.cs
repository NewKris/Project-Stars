using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Combat {
    public class TickingHitBox : MonoBehaviour {
        public int damage;
        public float tickRate;
        public LayerMask canHitFaction;
        public UnityEvent onHit;

        private float _lastTick;
        private readonly List<HurtBox> _hurtBoxes = new List<HurtBox>(10);
        
        private bool TickElapsed => Time.time - _lastTick > tickRate;
        
        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("Hit Box");
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out HurtBox hurtBox) && CanHurtFaction(hurtBox.gameObject.layer)) {
                _hurtBoxes.Add(hurtBox);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.TryGetComponent(out HurtBox hurtBox)) {
                RemoveFromList(hurtBox);
            }
        }

        private void OnEnable() {
            _hurtBoxes.Clear();
            HurtBox.OnDeath += RemoveFromList;
        }

        private void OnDisable() {
            HurtBox.OnDeath -= RemoveFromList;
        }

        private void Update() {
            if (!TickElapsed) {
                return;
            }

            for (int i = _hurtBoxes.Count - 1; i >= 0; i--) {
                _hurtBoxes[i].TakeDamage(damage);
                onHit?.Invoke();
            }
            
            _lastTick = Time.time;
        }

        private bool CanHurtFaction(LayerMask faction) {
            return canHitFaction.ContainstLayer(faction);
        }

        private void RemoveFromList(HurtBox hurtBox) {
            if (_hurtBoxes.Contains(hurtBox)) {
                _hurtBoxes.Remove(hurtBox);
            }
        }
    }
}
