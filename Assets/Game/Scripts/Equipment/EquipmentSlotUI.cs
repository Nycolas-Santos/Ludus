using Game.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public class EquipmentSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text slotTypeText;
        [SerializeField] private Image equippedItemIcon;
        [FormerlySerializedAs("itemType")] [SerializeField] private EquipmentType equipmentType;

        public Item EquippedItem;

        public void Initialize(Item item)
        {
            if (item == null || item.ItemData == null)
            {
                slotTypeText.text = equipmentType.ToString();
                equippedItemIcon.sprite = null;
                equippedItemIcon.gameObject.SetActive(false);
                EquippedItem = null;
                return;
            }
            EquippedItem = item;
            equippedItemIcon.sprite = EquippedItem.ItemData.ItemIcon;
            equippedItemIcon.gameObject.SetActive(true);
        }
        
        public void Terminate()
        {
            EquippedItem = null;
            equippedItemIcon.sprite = null;
            equippedItemIcon.gameObject.SetActive(false);
        }
    }
}