using System;
using UnityEngine;

namespace Werehorse.Runtime.Ship.Weapons {
    [Serializable, CreateAssetMenu(menuName = "Weapon Data")]
    public class WeaponData : ScriptableObject {
        public int id;
        public GameObject prefab;
        public Sprite icon;
    }
}
