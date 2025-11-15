using UnityEngine;

namespace Werehorse.Runtime.Ship.Weapons {
    [CreateAssetMenu(menuName = "Weapon Database")]
    public class WeaponDatabase : ScriptableObject {
        public WeaponData[] weapons;
    }
}
