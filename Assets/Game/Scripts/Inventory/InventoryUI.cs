using System;
using Game.Scripts.Equipment;
using Game.Scripts.Extensions;
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
        public static Action OnRefreshInventory;

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_CANCEL);
            Initialize();
        }

        private void OnDisable()
        {
            AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_CANCEL);
            Terminate();
        }

        private void Initialize()
        {
            cashAmountTMP.text = $"${_inventory.Cash}";
            inventorySlotsUI.Initialize(_inventory.Items);
            inventoryInfoUI.Initialize(inventorySlotsUI.CurrentSelectedSlot);
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
