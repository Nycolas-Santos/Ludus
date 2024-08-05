using System;
using System.Collections.Generic;
using Game.Scripts.Interaction;
using Unity.Collections;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        public Item[] Items;
        [ReadOnly] public int Cash;
        [SerializeField] private ItemInteraction _itemDropPrefab;
        
        private UIManager _uiManager => UIManager.Instance;
        private NotificationManager _notificationManager => NotificationManager.Instance;
        
        public static Action<Item> OnAddItem;
        public static Action<Item> OnRemoveItem;

        private void Update()
        {
            ProcessInputs();
        }

        private void ProcessInputs()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_uiManager.IsInventoryOpen)
                {
                    _uiManager.CloseInventoryUI();
                }
                else
                {
                    _uiManager.ShowInventoryUI();
                }
            }
        }
        
        public bool AddItem(Item item)
        {
            if (item?.ItemData is null)
            {
                _notificationManager.AddNotification("Invalid item");
                return false;
            }

            if (Items == null || Items.Length == 0)
            {
                _notificationManager.AddNotification("Inventory is empty or not initialized");
                return false;
            }

            var emptyIndex = Array.FindIndex(Items, i => i?.ItemData is null);

            if (item.ItemData.Stackable)
            {
                var index = Array.FindIndex(Items, i => i?.ItemData?.ItemName == item.ItemData.ItemName);
                if (index >= 0)
                {
                    Items[index].Amount += item.Amount;
                }
                else if (emptyIndex >= 0)
                {
                    Items[emptyIndex] = item;
                }
                else
                {
                    _notificationManager.AddNotification("Inventory is full");
                    return false;
                }
            }
            else if (emptyIndex >= 0)
            {
                Items[emptyIndex] = item;
            }
            else
            {
                _notificationManager.AddNotification("Inventory is full");
                return false;
            }
            OnAddItem?.Invoke(item);
            _notificationManager.AddNotification($"Collected: {item.ItemData.ItemName}");
            return true;
        }

        private void RemoveItem(Item item)
        {
            if (item == null || item.ItemData == null)
            {
                _notificationManager.AddNotification("Invalid item");
                return;
            }

            var index = Array.FindIndex(Items, i => i != null && i == item);
            if (index >= 0)
            {
                Items[index] = null;
                OnRemoveItem?.Invoke(item);
                _notificationManager.AddNotification($"Removed: {item.ItemData.ItemName}");
            }
            else
            {
                _notificationManager.AddNotification("Item not found in inventory");
            }
            
        }
        
        public void UseItem(Item item)
        {
            if (item?.ItemData is null)
            {
                _notificationManager.AddNotification("Invalid item");
                return;
            }

            if (item.ItemData.Stackable)
            {
                item.Amount--;
                if (item.Amount <= 0)
                {
                    RemoveItem(item);
                }
            }
            else
            {
                RemoveItem(item);
            }
        }
        
        public void AddCash(int amount)
        {
            Cash += amount;
            _notificationManager.AddNotification($"Cash added: ${amount}");
        }
        
        public void SubtractCash(int amount)
        {
            Cash -= amount;
            _notificationManager.AddNotification($"Cash removed: ${amount}");
        }
        
        public void SetCash(int amount)
        {
            Cash = amount;
        }
        
        public bool HasItem(Item item)
        {
            return Array.Exists(Items, i => i == item);
        }
    }
}