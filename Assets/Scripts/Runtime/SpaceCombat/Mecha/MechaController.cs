using System;
using UnityEngine;
using Werehorse.Runtime.Utility.CommonObjects;
using Werehorse.Runtime.Utility.Extensions;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class MechaController : MonoBehaviour {
        public MechState strafeState;
        public MechState flyState;

        private MechState _currentState;
        
        private void Awake() {
            SetCursorVisibility(false);
            _currentState = strafeState;
            
            _currentState.OnEnter();
            _currentState.enabled = true;
            flyState.enabled = false;
        }

        private void SwitchState(MechState toState) {
            _currentState.OnExit();
            _currentState.enabled = false;
            
            _currentState = toState;
            
            _currentState.OnEnter();
            _currentState.enabled = true;
        }
        
        private void SetCursorVisibility(bool showCursor) {
            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = showCursor;
        }
    }
}
