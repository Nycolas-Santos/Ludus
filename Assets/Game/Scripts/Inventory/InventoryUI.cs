using System;
using Game.Scripts.Equipment;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySlotsUI inventorySlotsUI;
        [SerializeField] private InventoryInfoUI inventoryInfoUI;
        [SerializeField] private InventoryEquipmentUI inventoryEquipmentUI;
        [SerializeField] private TMP_Text cashAmountTMP;
        
        private InventoryHandler _inventory => GameManager.Instance.Player.InventoryHandler;
        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Terminate();
        }

        private void Initialize()
        {
            cashAmountTMP.text = $"${_inventory.Cash}";
            inventorySlotsUI.Initialize(_inventory.Items);
            inventoryEquipmentUI.Initialize();
        }

        private void Terminate()
        {
            cashAmountTMP.text = string.Empty;
            inventorySlotsUI.Terminate();
            inventoryEquipmentUI.Terminate();
        }
    }
}
