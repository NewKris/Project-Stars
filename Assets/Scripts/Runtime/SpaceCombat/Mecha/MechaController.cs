using System;
using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public class MechaController : MonoBehaviour {
        public MechState strafeState;
        public MechState flyState;

        private MechState _currentState;
        
        private void Awake() {
            SetCursorVisibility(false);

            PlayerMechController.OnToggleFlight += ToggleFlight;
        }

        private void Start() {
            _currentState = strafeState;
            _currentState.OnEnter();
            _currentState.enabled = true;
            flyState.enabled = false;
            
            PlayerMechController.SetEnabled(true);
            PlayerFlightController.SetEnabled(false);
        }

        private void OnDestroy() {
            PlayerMechController.OnToggleFlight -= ToggleFlight;
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
                
                PlayerMechController.SetEnabled(true);
                PlayerFlightController.SetEnabled(false);
            }
            else {
                SwitchState(flyState);
                
                PlayerMechController.SetEnabled(false);
                PlayerFlightController.SetEnabled(true);
            }
        }
    }
}
