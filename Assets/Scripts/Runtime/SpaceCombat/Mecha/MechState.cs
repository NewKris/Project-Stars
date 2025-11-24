using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Mecha {
    public abstract class MechState : MonoBehaviour {
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
