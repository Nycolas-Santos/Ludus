using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        public Item[] Items;
        [ReadOnly] public int Cash;
        
        private UIManager _uiManager => UIManager.Instance;
        private NotificationManager _notificationManager => NotificationManager.Instance;

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

            if (item.ItemData.Stackable)
            {
                var index = Array.FindIndex(Items, i => i is { ItemData: not null } && i.ItemData.ItemName == item.ItemData.ItemName);
                if (index >= 0)
                {
                    Items[index].Amount += item.Amount;
                }
                else
                {
                    var emptyIndex = Array.FindIndex(Items, i => i.ItemData is null);
                    if (emptyIndex >= 0)
                    {
                        Items[emptyIndex] = item;
                    }
                    else
                    {
                        _notificationManager.AddNotification("Inventory is full");
                        return false;
                    }
                }
            }
            else
            {
                var emptyIndex = Array.FindIndex(Items, i => i.ItemData is null);
                if (emptyIndex >= 0)
                {
                    Items[emptyIndex] = item;
                }
                else
                {
                    _notificationManager.AddNotification("Inventory is full");
                    return false;
                }
            }

            _notificationManager.AddNotification($"Collected: {item.ItemData.ItemName}");
            return true;
        }
        
        public void RemoveItem(Item item)
        {
            Items[Array.FindIndex(Items, i => i == item)] = null;
            _notificationManager.AddNotification($"Removed: {item.ItemData.ItemName}");
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
    }
}