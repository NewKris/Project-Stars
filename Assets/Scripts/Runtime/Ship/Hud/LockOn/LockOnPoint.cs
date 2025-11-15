using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.Ship.Hud.LockOn {
    public class LockOnPoint : MonoBehaviour {
        public static event Action<Collider> OnTargetEnter; 
        public static event Action<Collider> OnTargetExit; 
        
        public int maxLockOnTargets;
        
        [Header("Detection")]
        public LayerMask detectFaction;

        private HashSet<Collider> _inRangeTarget;

        private void Awake() {
            _inRangeTarget = new HashSet<Collider>(20);
        }

        private void OnTriggerEnter(Collider other) {
            if (detectFaction.ContainstLayer(other.gameObject.layer)) {
                _inRangeTarget.Add(other);
                OnTargetEnter?.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (_inRangeTarget.Contains(other)) {
                _inRangeTarget.Remove(other);
                OnTargetExit?.Invoke(other);
            }
        }

        private void Update() {
            _inRangeTarget.Where(IsInactive).ForEach(target => OnTargetExit?.Invoke(target));
            _inRangeTarget.RemoveWhere(IsInactive);
        }

        private bool IsInactive(Collider target) {
            return !target.gameObject.activeSelf;
        }
    }
}
