using System;
using UnityEngine;
using Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechInput;
using Werehorse.Runtime.SpaceCombat.PlayerCharacter.MechStates;

namespace Werehorse.Runtime.SpaceCombat.PlayerCharacter {
    public class MechController : MonoBehaviour {
        public MechState strafeState;
        public MechState flightState;

        private MechState _currentState;

        private void Awake() {
            FlightInputListener.OnToggleFlight += ToggleFlight;
            StrafeInputListener.OnToggleFlight += ToggleFlight;
            
            _currentState = strafeState;
            _currentState.OnEnter();
        }

        private void OnDestroy() {
            FlightInputListener.OnToggleFlight -= ToggleFlight;
            StrafeInputListener.OnToggleFlight -= ToggleFlight;
        }

        private void ToggleFlight() {
            _currentState.OnExit();
            
            if (_currentState == flightState) {
                _currentState = strafeState;
            }
            else {
                _currentState = flightState;
            }
            
            _currentState.OnEnter();
        }
    }
}
