using System;
using Game.Scripts.Player.Game.Scripts.Equipment;
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
        [SerializeField] private Button equipButton;
        [SerializeField] private Image itemIconImage;
        
        public ItemSlotUI CurrentSlot;
        
        private EquipmentHandler _equipmentHandler => GameManager.Instance.Player.EquipmentHandler;

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
                CurrentSlot = null;
                itemNameTMP.text = string.Empty;
                itemDescriptionTMP.text = string.Empty;
                itemIconImage.sprite = null;
                itemAmountTMP.text = string.Empty;
                TerminateEquipButton();
                ItemSlotUI.OnChangeSlot -= OnChangeSlot;
                gameObject.SetActive(false);
                return;
            }
            else
            {
                CurrentSlot = slot;
                gameObject.SetActive(true);
                itemNameTMP.text = slot.CurrentItem.ItemData.ItemName;
                itemDescriptionTMP.text = slot.CurrentItem.ItemData.ItemDescription;
                itemIconImage.sprite = slot.CurrentItem.ItemData.ItemIcon;
                ItemSlotUI.OnChangeSlot += OnChangeSlot;
                InitializeEquipButton(slot);
                itemAmountTMP.text = $"x{slot.CurrentItem.Amount}";
            }
        }

        private void OnChangeSlot(ItemSlotUI oldSLot, ItemSlotUI newSlot)
        {
            if (oldSLot == CurrentSlot)
            {
                Initialize(newSlot);
            }
        }

        private void InitializeEquipButton(ItemSlotUI slot)
        {
            equipButton.gameObject.SetActive(slot.CurrentItem is not null && slot.CurrentItem.ItemData.ItemType == ItemType.Equipment);
            if (_equipmentHandler.IsItemEquipped(slot.CurrentItem))
            {
                equipButton.GetComponentInChildren<TMP_Text>().text = "Unequip";
                equipButton.onClick.RemoveAllListeners();
                equipButton.onClick.AddListener(() =>
                {
                    _equipmentHandler.UnequipItem(slot.CurrentItem.ItemData.EquipmentSettings.EquipmentType);
                    Initialize(slot);
                });
            }
            else
            {
                equipButton.GetComponentInChildren<TMP_Text>().text = "Equip";
                equipButton.onClick.RemoveAllListeners();
                equipButton.onClick.AddListener(() =>
                {
                    _equipmentHandler.EquipItem(slot.CurrentItem, slot.CurrentItem.ItemData.EquipmentSettings.EquipmentType);
                    Initialize(slot);
                });
            }
        }

        private void TerminateEquipButton()
        {
            equipButton.gameObject.SetActive(false);
        }
    }
}
