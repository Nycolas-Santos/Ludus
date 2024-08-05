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
        
        public ItemSlotUI CurrentSelectedSlot;
        
        private InventoryHandler _inventory => GameManager.Instance.Player.InventoryHandler;

        private void OnEnable()
        {
            ItemSlotUI.OnSelectSlot += SetCurrentSelectedSlot;
            InventoryInfoUI.OnInteractWithItem += UpdateInventoryHandler;
            ItemSlotUI.OnChangeSlot += UpdateInventoryHandler;
        }

        private void OnDisable()
        {
            ItemSlotUI.OnSelectSlot -= SetCurrentSelectedSlot;
            InventoryInfoUI.OnInteractWithItem -= UpdateInventoryHandler;
            ItemSlotUI.OnChangeSlot -= UpdateInventoryHandler;
        }
        
        public void UpdateSlots(Item[] items)
        {
            for (var i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].Initialize(items.ElementAtOrDefault(i));
            }
        }
        
        public void UpdateInventoryHandler()
        {
            var newItemPositions = itemSlots.Select(slot => slot.CurrentItem).ToList();
            _inventory.Items = newItemPositions.ToArray();
        }
        
        public void UpdateInventoryHandler(ItemSlotUI slot1, ItemSlotUI slot2)
        {
            var newItemPositions = itemSlots.Select(slot => slot.CurrentItem).ToList();
            _inventory.Items = newItemPositions.ToArray();
        }
        
        private void SetCurrentSelectedSlot(ItemSlotUI slot)
        {
            CurrentSelectedSlot = slot;
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
