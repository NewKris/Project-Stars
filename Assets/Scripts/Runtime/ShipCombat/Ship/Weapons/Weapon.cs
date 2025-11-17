using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Ship.Weapons {
    public abstract class Weapon : MonoBehaviour {
        public abstract void BeginFire();
        public abstract void EndFire();
    }
}
