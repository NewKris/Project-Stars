using UnityEngine;

namespace Werehorse.Runtime.ShipCombat.Ship.Weapons {
    [CreateAssetMenu(menuName = "Weapon Database")]
    public class WeaponDatabase : ScriptableObject {
        public WeaponData[] weapons;
    }
}
