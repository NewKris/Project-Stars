using System;

namespace Werehorse.Runtime.SpaceCombat.SpaceShip.Equipment {
    [Serializable]
    public class EquipmentBlackBoard {
        public static bool HasEquipment { get; private set; }
        public static EquipmentBlackBoard CurrentEquipment { get; private set; }

        public int shipBaseId;
        public int weapon1Id;
        public int weapon2Id;
        
        public static void SetCurrentEquipment(EquipmentBlackBoard equipment) {
            CurrentEquipment = equipment;
            HasEquipment = equipment != null;
        }
    }
}
