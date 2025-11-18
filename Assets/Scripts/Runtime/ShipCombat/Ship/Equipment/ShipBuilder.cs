using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Werehorse.Runtime.ShipCombat.Ship.ShipBases;
using Werehorse.Runtime.ShipCombat.Ship.ShipBehaviour;
using Werehorse.Runtime.ShipCombat.Ship.Weapons;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.ShipCombat.Ship.Equipment {
    public class ShipBuilder : MonoBehaviour {
        public Transform spawnPoint;
        public Drone shipCamera;
        public RectTransform reticle;
        public ShipDatabase shipDatabase;
        public WeaponDatabase weaponDatabase;
        public GameObject gameOverScreen;

        [Header("Overrides")] [InspectorButton(nameof(ClearEquipmentCache), "Clear Cache")] 
        public EquipmentBlackBoard defaultEquipments;

        private void Awake() {
            EquipmentBlackBoard equipment = EquipmentBlackBoard.HasEquipment ? EquipmentBlackBoard.CurrentEquipment : defaultEquipments;
            
            PlaneShip ship = SpawnBaseShip(equipment.shipBaseId);
            ship.weapon1 = SpawnWeapon(equipment.weapon1Id, ship.transform);
            ship.weapon2 = SpawnWeapon(equipment.weapon2Id, ship.transform);

            shipCamera.SetTarget(ship.transform, true);
        }

        private PlaneShip SpawnBaseShip(int id) {
            GameObject shipPrefab = shipDatabase.GetShipData(id).shipBasePrefab;
            PlaneShip instance = Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation, transform).GetComponent<PlaneShip>();
            instance.reticle = reticle;
            instance.gameOverScreen = gameOverScreen;
            
            return instance;
        }

        private Weapon SpawnWeapon(int weaponId, Transform parent) {
            if (weaponId < 0) {
                return null;
            }

            GameObject weaponPrefab = weaponDatabase.GetWeaponData(weaponId).prefab;
            GameObject instance = Instantiate(weaponPrefab, parent);
            instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            return instance.GetComponent<Weapon>();
        }

        private void ClearEquipmentCache() {
            Debug.Log("Cleared equipment cache");
            EquipmentBlackBoard.SetCurrentEquipment(null);
        }
    }
}
