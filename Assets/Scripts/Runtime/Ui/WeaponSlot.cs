using System;
using NewKris.Runtime.Combat.Weapons;
using NewKris.Runtime.Ship.Equipment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NewKris.Runtime.Ui {
    public enum WeaponHand {
        LEFT,
        RIGHT
    }
    
    public class WeaponSlot : MonoBehaviour {
        public WeaponHand weaponHand;
        public Image iconImage;
        public WeaponDatabase database;

        private int _selectedWeaponIndex;

        public void Increment() {
            _selectedWeaponIndex++;
            _selectedWeaponIndex %= database.weapons.Length;
            
            UpdateIcon();
            EquipWeapon();
        }

        public void Decrement() {
            _selectedWeaponIndex--;

            if (_selectedWeaponIndex < 0) {
                _selectedWeaponIndex = database.weapons.Length - 1;
            }
            
            UpdateIcon();
            EquipWeapon();
        }

        private void Awake() {
            _selectedWeaponIndex = GetEquippedWeaponIndex();
            
            UpdateIcon();
            EquipWeapon();
        }

        private void UpdateIcon() {
            iconImage.sprite = database.weapons[_selectedWeaponIndex].icon;
            iconImage.color = Color.white;
        }

        private int GetEquippedWeaponIndex() {
            int targetId = weaponHand == WeaponHand.LEFT 
                ? EquipmentBlackBoard.weapon1Id 
                : EquipmentBlackBoard.weapon2Id;
            
            return Array.FindIndex(database.weapons, x => x.id == targetId);;
        }
        
        private void EquipWeapon() {
            if (weaponHand == WeaponHand.LEFT) {
                EquipmentBlackBoard.weapon1Id = database.weapons[_selectedWeaponIndex].id;
            }
            else {
                EquipmentBlackBoard.weapon2Id = database.weapons[_selectedWeaponIndex].id;
            }
        }
    }
}
