using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string ItemName;
        public string ItemDescription;
        public bool Stackable;
        public Sprite ItemIcon;
        public ItemType ItemType;
        public EquipmentSettings EquipmentSettings;
    }
    
    [Serializable]
    public struct EquipmentSettings
    {
        public EquipmentType EquipmentType;
    }
    
    public enum EquipmentType
    {
        Head,
        Chest,
        Boots,
        Weapon
    }
    
    public enum ItemType
    {
        Generic,
        Consumable,
        Equipment
    }
}