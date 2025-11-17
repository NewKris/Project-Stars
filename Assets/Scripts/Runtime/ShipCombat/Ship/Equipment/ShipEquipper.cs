using System;
using System.Linq;
using UnityEngine;
using Werehorse.Runtime.ShipCombat.Ship.Weapons;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.ShipCombat.Ship.Equipment {
    public class ShipEquipper : MonoBehaviour {
        [Header("Weapons")] 
        [ReadOnly] public Weapon weapon1;
        [ReadOnly] public Weapon weapon2;
        public Transform weaponParent;
        public WeaponDatabase weaponDatabase;

        [Header("Overrides")] [InspectorButton(nameof(ClearEquipmentCache), "Clear Cache")] 
        public EquipmentBlackBoard defaultEquipments;
        
        private void Awake() {
            ApplyWeapons();
        }

        private void ApplyWeapons() {
            try {
                if (!EquipmentBlackBoard.HasEquipment) {
                    Debug.Log("Using default equipments");
                }
                
                EquipmentBlackBoard equipment = EquipmentBlackBoard.HasEquipment
                    ? EquipmentBlackBoard.CurrentEquipment
                    : defaultEquipments;
                
                int weaponId1 = equipment.weapon1Id;
                int weaponId2 = equipment.weapon2Id;

                if (weaponId1 >= 0) {
                    WeaponData weapon1Data = weaponDatabase.weapons.First(x => x.id == weaponId1);
                    GameObject weapon1Prefab = Instantiate(weapon1Data.prefab, weaponParent);
                    weapon1Prefab.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    weapon1 = weapon1Prefab.GetComponent<Weapon>();
                }

                if (weaponId2 >= 0) {
                    WeaponData weapon2Data = weaponDatabase.weapons.First(x => x.id == weaponId2);
                    GameObject weapon2Prefab = Instantiate(weapon2Data.prefab, weaponParent);
                    weapon2Prefab.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    weapon2 = weapon2Prefab.GetComponent<Weapon>();
                }
            }
            catch (Exception e) {
                Debug.LogError($"Failed to equip weapons. Reason: {e}");
            }
        }

        private void ClearEquipmentCache() {
            Debug.Log("Cleared equipment cache");
            EquipmentBlackBoard.SetCurrentEquipment(null);
        }
    }
}
