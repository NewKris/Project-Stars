using UnityEngine;

namespace Werehorse.Runtime.Combat.Weapons {
    public abstract class Weapon : MonoBehaviour {
        public abstract void BeginFire();
        public abstract void EndFire();
    }
}
