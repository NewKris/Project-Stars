using System;
using UnityEngine;

namespace Werehorse.Runtime.SpaceCombat.Weapons {
    [Serializable, CreateAssetMenu(menuName = "Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject {
        public int id;
        public GameObject prefab;
        public Sprite icon;
    }
}
