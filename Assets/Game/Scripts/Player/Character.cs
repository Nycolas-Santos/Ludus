using Game.Scripts.Inventory;
using Game.Scripts.Player.Game.Scripts.Equipment;
using Game.Scripts.Stats;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class Character : MonoBehaviour
    {
        public MovementHandler MovementHandler;
        public AnimationHandler AnimationHandler;
        public InventoryHandler InventoryHandler;
        public EquipmentHandler EquipmentHandler;
        public StatsHandler StatsHandler;
    }
}
