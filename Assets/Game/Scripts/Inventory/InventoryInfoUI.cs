using System;
using Game.Scripts.Extensions;
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
        private InventoryHandler _inventoryHandler => GameManager.Instance.Player.InventoryHandler;
        
        public static event Action OnInteractWithItem;

        private void Start()
        {
            ItemSlotUI.OnSelectSlot += Initialize;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            ItemSlotUI.OnSelectSlot -= Initialize;
        }

        public void Initialize(ItemSlotUI slot)
        {
            if (slot.CurrentItem is null)
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
                InitializeInteractButton(slot);
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

        private void InitializeInteractButton(ItemSlotUI slot)
        {
            equipButton.onClick.RemoveAllListeners();
            switch (slot.CurrentItem.ItemData.ItemType)
            {
                case ItemType.Generic:
                    equipButton.gameObject.SetActive(false);
                    break;
                case ItemType.Consumable:
                    equipButton.GetComponentInChildren<TMP_Text>().text = "Use";
                    equipButton.gameObject.SetActive(true);
                    equipButton.onClick.AddListener(() =>
                    {
                        _inventoryHandler.UseItem(slot.CurrentItem);
                        slot.CurrentItem = _inventoryHandler.HasItem(slot.CurrentItem) ? slot.CurrentItem : null;
                        AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_CONFIRM);
                        Initialize(slot);
                        OnInteractWithItem?.Invoke();
                    });
                    break;
                case ItemType.Equipment:
                    equipButton.gameObject.SetActive(true);
                    if (_equipmentHandler.IsItemEquipped(slot.CurrentItem))
                    {
                        equipButton.GetComponentInChildren<TMP_Text>().text = "Unequip";
                        equipButton.onClick.RemoveAllListeners();
                        equipButton.onClick.AddListener(() =>
                        {
                            _equipmentHandler.UnequipItem(slot.CurrentItem.ItemData.EquipmentSettings.EquipmentType);
                            AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_CONFIRM);
                            Initialize(slot);
                            OnInteractWithItem?.Invoke();
                        });
                    }
                    else
                    {
                        equipButton.GetComponentInChildren<TMP_Text>().text = "Equip";
                        equipButton.onClick.RemoveAllListeners();
                        equipButton.onClick.AddListener(() =>
                        {
                            _equipmentHandler.EquipItem(slot.CurrentItem, slot.CurrentItem.ItemData.EquipmentSettings.EquipmentType);
                            AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_CONFIRM);
                            Initialize(slot);
                            OnInteractWithItem?.Invoke();
                        });
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        private void TerminateEquipButton()
        {
            equipButton.gameObject.SetActive(false);
        }
    }
}
