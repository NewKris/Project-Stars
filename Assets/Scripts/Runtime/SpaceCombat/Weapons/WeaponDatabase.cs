using System.Linq;
using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Weapons {
    [CreateAssetMenu(menuName = "Weapon Database")]
    public class WeaponDatabase : ScriptableObject {
        public WeaponData[] weapons;

        public WeaponData GetWeaponData(int id) {
            return weapons.First(data => data.id == id);
        }
    }
}
