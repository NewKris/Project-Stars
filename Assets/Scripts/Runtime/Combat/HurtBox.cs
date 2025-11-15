using System;
using UnityEngine;
using UnityEngine.Events;

namespace Werehorse.Runtime.Combat {
    public class HurtBox : MonoBehaviour {
        public static event Action<HurtBox> OnDeath; 
        
        public int maxHealth;
        public UnityEvent onHurt;
        public UnityEvent onDeath;
        
        private int _health;

        public int CurrentHealth => _health;

        public void TakeDamage(int damage) {
            _health -= damage;
            onHurt.Invoke();

            if (_health <= 0) {
                OnDeath?.Invoke(this);
                onDeath.Invoke();
            }
        }

        private void OnEnable() {
            _health = maxHealth;
        }
    }
}
