using System;
using UnityEngine;
using UnityEngine.UI;
using Werehorse.Runtime.SpaceCombat.Weapons;

namespace Werehorse.Runtime.Hangar {
    public enum WeaponHand {
        LEFT,
        RIGHT
    }
    
    public class WeaponSlot : MonoBehaviour {
        public WeaponHand weaponHand;
        public Image iconImage;
        public WeaponDatabase database;

        private int _selectedWeaponIndex;
    }
}
