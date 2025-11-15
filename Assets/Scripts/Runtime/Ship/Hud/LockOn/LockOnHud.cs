using System;
using System.Collections.Generic;
using UnityEngine;

namespace Werehorse.Runtime.Ship.Hud.LockOn {
    public class LockOnHud : MonoBehaviour {
        public GameObject reticlePrefab;
        public LockOnPoint lockOnPoint;

        private Dictionary<Collider, LockOnReticle> _targetReticles;
        
        private void Awake() {
            LockOnPoint.OnTargetEnter += RegisterTarget;
            LockOnPoint.OnTargetExit += DeRegisterTarget;
            
            _targetReticles = new Dictionary<Collider, LockOnReticle>(20);
        }

        private void OnDestroy() {
            LockOnPoint.OnTargetEnter -= RegisterTarget;
            LockOnPoint.OnTargetExit -= DeRegisterTarget;
        }

        private void RegisterTarget(Collider target) {
            LockOnReticle reticle = Instantiate(reticlePrefab, transform).GetComponent<LockOnReticle>();
            reticle.target = target.transform;
            
            _targetReticles.Add(target, reticle);
        }

        private void DeRegisterTarget(Collider target) {
            Destroy(_targetReticles[target].gameObject);
            _targetReticles.Remove(target);
        }
    }
}
