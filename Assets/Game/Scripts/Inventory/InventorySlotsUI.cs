using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Inventory
{
    public class InventorySlotsUI : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlots;
        
        private ItemSlotUI _currentSelectedSlot;
        
        private InventoryHandler _inventory => GameManager.Instance.Player.InventoryHandler;

        private void OnEnable()
        {
            ItemSlotUI.OnSelectSlot += SetCurrentSelectedSlot;
            ItemSlotUI.OnChangeItem += UpdateInventoryHandler;
        }

        private void OnDisable()
        {
            ItemSlotUI.OnSelectSlot -= SetCurrentSelectedSlot;
            ItemSlotUI.OnChangeItem -= UpdateInventoryHandler;
        }
        
        private void UpdateInventoryHandler(ItemSlotUI itemSlotUI)
        {
            var newItemPositions = itemSlots.Select(slot => slot.CurrentItem).ToList();
            _inventory.Items = newItemPositions.ToArray();
        }
        
        private void SetCurrentSelectedSlot(ItemSlotUI slot)
        {
            _currentSelectedSlot = slot;
        }

        public void Initialize(Item[] items)
        {
            for (var i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].Initialize(items.ElementAtOrDefault(i));
            }
        }

        public void Terminate()
        {
            foreach (var slot in itemSlots)
            {
               slot.Terminate(); 
            }
        }
    }
}
