using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewKris.Runtime.Combat {
    public class HurtBox : MonoBehaviour {
        public int maxHealth;
        public Faction isFaction;
        public UnityEvent onHurt;
        public UnityEvent onDeath;
        
        private int _health;

        public void TakeDamage(int damage) {
            _health -= damage;
            onHurt.Invoke();

            if (_health <= 0) {
                onDeath.Invoke();
            }
        }
        
        private void Reset() {
            gameObject.layer = LayerMask.NameToLayer("Hurt Box");
        }

        private void OnEnable() {
            _health = maxHealth;
        }
    }
}
