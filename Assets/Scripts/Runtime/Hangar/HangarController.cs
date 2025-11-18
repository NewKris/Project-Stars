using System;
using UnityEngine;
using Werehorse.Runtime.ShipCombat.Ship.Equipment;
using Werehorse.Runtime.Utility.Attributes;

namespace Werehorse.Runtime.Hangar {
    public class HangarController : MonoBehaviour {
        [InspectorButton(nameof(ClearEquipmentCache), "Clear Cache")]
        public EquipmentBlackBoard defaultEquipments;
        
        private void Awake() {
            Cursor.lockState = CursorLockMode.None;
            
            if (!EquipmentBlackBoard.HasEquipment) {
                EquipmentBlackBoard.SetCurrentEquipment(defaultEquipments);
            }
        }
        
        private void ClearEquipmentCache() {
            Debug.Log("Cleared equipment cache");
            EquipmentBlackBoard.SetCurrentEquipment(null);
        }
    }
}
