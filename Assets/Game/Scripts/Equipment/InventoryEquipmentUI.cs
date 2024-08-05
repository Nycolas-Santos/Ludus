using System;
using Game.Scripts.Player.Game.Scripts.Equipment;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public class InventoryEquipmentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSlotUI[] equipmentSlots;
        private EquipmentHandler _equipmentHandler => GameManager.Instance.Player.EquipmentHandler;

        private void Awake()
        {
            EquipmentHandler.OnEquipmentChanged += Initialize;
        }

        private void OnDestroy()
        {
            EquipmentHandler.OnEquipmentChanged -= Initialize;
        }

        public void Initialize()
        {
            var equippedItems = _equipmentHandler.EquippedItems;
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].Initialize(equippedItems[i]);
            }
        }

        public void Terminate()
        {
            foreach (var slot in equipmentSlots)
            {
                slot.Terminate();
            }
        }
    }
}
