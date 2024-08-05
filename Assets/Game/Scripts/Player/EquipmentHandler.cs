using System;
using Game.Scripts.Inventory;

namespace Game.Scripts.Player
{
    using UnityEngine;

    namespace Game.Scripts.Equipment
    {
        public class EquipmentHandler : MonoBehaviour
        {
            public Item[] EquippedItems = new Item[System.Enum.GetValues(typeof(EquipmentType)).Length];
            
            public static Action<Item> OnItemEquipped;
            public static Action<Item> OnItemUnequipped;
            public static Action OnEquipmentChanged;
            private NotificationManager _notificationManager => NotificationManager.Instance;
            
            public bool IsItemEquipped(Item item)
            {
                return Array.Exists(EquippedItems, i => i == item);
            }

            public bool EquipItem(Item item, EquipmentType type)
            {
                if (item == null || item.ItemData == null)
                {
                    _notificationManager.AddNotification("Invalid item");
                    return false;
                }

                EquippedItems[(int)type] = item;
                _notificationManager.AddNotification($"Equipped: {item.ItemData.ItemName} to {type}");
                OnItemEquipped?.Invoke(item);
                OnEquipmentChanged?.Invoke();
                return true;
            }

            public bool UnequipItem(EquipmentType type)
            {
                if (EquippedItems[(int)type] == null)
                {
                    _notificationManager.AddNotification($"{type} slot is already empty");
                    return false;
                }

                var item = EquippedItems[(int)type];
                EquippedItems[(int)type] = null;
                _notificationManager.AddNotification($"Unequipped: {item.ItemData.ItemName} from {type}");
                OnItemUnequipped?.Invoke(item);
                OnEquipmentChanged?.Invoke();
                return true;
            }
        }
    }
}