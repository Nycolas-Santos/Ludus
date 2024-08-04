using System;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySlotsUI inventorySlotsUI;
        [SerializeField] private InventoryInfoUI inventoryInfoUI;
        [SerializeField] private TMP_Text cashAmountTMP;


        private InventoryHandler _inventory => GameManager.Instance.Player.InventoryHandler;
        private void OnEnable()
        {
            inventorySlotsUI.Initialize(_inventory.Items);
            Initialize();
        }

        private void OnDisable()
        {
            inventorySlotsUI.Terminate();
            Terminate();
        }

        private void Initialize()
        {
            cashAmountTMP.text = $"${_inventory.Cash}";
        }

        private void Terminate()
        {
            cashAmountTMP.text = string.Empty;
        }
    }
}
