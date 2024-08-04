using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Inventory
{
    public class InventoryInfoUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemNameTMP;
        [SerializeField] private TMP_Text itemDescriptionTMP;
        [SerializeField] private TMP_Text itemAmountTMP;
        [SerializeField] private Image itemIconImage;

        private void Start()
        {
            ItemSlotUI.OnSelectSlot += Initialize;
        }

        private void OnDestroy()
        {
            ItemSlotUI.OnSelectSlot -= Initialize;
        }

        private void Initialize(ItemSlotUI slot)
        {
            if (slot.CurrentItem == null)
            {
                itemNameTMP.text = string.Empty;
                itemDescriptionTMP.text = string.Empty;
                itemIconImage.sprite = null;
                itemAmountTMP.text = string.Empty;
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
                itemNameTMP.text = slot.CurrentItem.ItemData.ItemName;
                itemDescriptionTMP.text = slot.CurrentItem.ItemData.ItemDescription;
                itemIconImage.sprite = slot.CurrentItem.ItemData.ItemIcon;
                itemAmountTMP.text = $"x{slot.CurrentItem.Amount}";
            }
        }
    }
}
