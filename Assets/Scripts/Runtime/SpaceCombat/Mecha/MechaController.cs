using System;
using UnityEngine;
using Werehorse.Runtime.SpaceCombat.Mecha.Flight;
using Werehorse.Runtime.SpaceCombat.Mecha.Strafe;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class MechaController : MonoBehaviour {
        public MechState strafeState;
        public MechState flyState;

        private MechState _currentState;
        
        private void Awake() {
            SetCursorVisibility(false);

            MechStrafeController.OnToggleFlight += ToggleFlight;
        }

        private void Start() {
            _currentState = strafeState;
            _currentState.OnEnter();
            _currentState.enabled = true;
            flyState.enabled = false;
            
            MechStrafeController.SetEnabled(true);
            MechFlightController.SetEnabled(false);
        }

        private void OnDestroy() {
            MechStrafeController.OnToggleFlight -= ToggleFlight;
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

        private void ToggleFlight() {
            if (_currentState == flyState) {
                SwitchState(strafeState);
                
                MechStrafeController.SetEnabled(true);
                MechFlightController.SetEnabled(false);
            }
            else {
                SwitchState(flyState);
                
                MechStrafeController.SetEnabled(false);
                MechFlightController.SetEnabled(true);
            }
        }
    }
}
