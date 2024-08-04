using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Inventory
{
    public class ItemSlotUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Button itemSlotButton;
        [SerializeField] private Image itemIcon;
        public static event Action<ItemSlotUI> OnSelectSlot;
        public static event Action<ItemSlotUI> OnChangeItem; 
        

        public Item CurrentItem;

        private void OnEnable()
        {
            itemSlotButton.onClick.AddListener( () => OnSelectSlot?.Invoke(this));
        }

        private void OnDisable()
        {
            itemSlotButton.onClick.RemoveListener(() => OnSelectSlot?.Invoke(this));
        }

        public void Initialize(Item item)
        {
            if (item != null && item.ItemData != null)
            {
                CurrentItem = item;
                itemIcon.sprite = item.ItemData.ItemIcon;
                itemIcon.color = Color.white;
                OnChangeItem?.Invoke(this);
            }
            else
            {
                CurrentItem = null;
                itemIcon.sprite = null;
                itemIcon.color = Color.clear;
            }
        }
        
        public void Terminate()
        {
            CurrentItem = null;
            itemIcon.sprite = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemIcon.transform.SetParent(itemIcon.transform.parent.parent.parent);
            itemIcon.transform.SetAsLastSibling();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetIconToDefaultPosition();
        }

        public void OnDrag(PointerEventData eventData)
        {
            itemIcon.transform.position = eventData.position;
        }

        private void ResetIconToDefaultPosition()
        {
            itemIcon.transform.SetParent(transform);
            itemIcon.transform.localPosition = Vector3.zero;
        }


        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            var itemSlot = eventData.pointerDrag.GetComponent<ItemSlotUI>();
            if (itemSlot == null) return;
            var item = itemSlot.CurrentItem;
            itemSlot.Initialize(CurrentItem);
            Initialize(item);
        }
    }
}
