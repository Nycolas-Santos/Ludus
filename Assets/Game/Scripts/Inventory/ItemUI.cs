using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Scripts.Inventory
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
            
        public ItemData CurrentItemData; 
        public Item CurrentItem;
    }
}