using UnityEngine;

namespace Werehorse.Runtime.Combat.Weapons {
    [CreateAssetMenu(menuName = "Weapon Database")]
    public class WeaponDatabase : ScriptableObject {
        public WeaponData[] weapons;
    }
}
