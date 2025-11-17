using System;
using UnityEngine;
using Werehorse.Runtime.ShipCombat.Ship.Equipment;

namespace Werehorse.Runtime.Hangar {
    public class HangarController : MonoBehaviour {
        public EquipmentBlackBoard defaultEquipments;
        
        private void Awake() {
            Cursor.lockState = CursorLockMode.None;
            
            if (!EquipmentBlackBoard.HasEquipment) {
                EquipmentBlackBoard.SetCurrentEquipment(defaultEquipments);
            }
        }
    }
}
