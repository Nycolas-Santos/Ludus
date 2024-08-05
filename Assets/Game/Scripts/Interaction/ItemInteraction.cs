using Game.Scripts.Extensions;
using Game.Scripts.Inventory;
using UnityEngine;

namespace Game.Scripts.Interaction
{
    public class ItemInteraction : InteractableBase
    {
        public Item Item;
        private InventoryHandler _inventoryHandler => GameManager.Instance.Player.InventoryHandler;
        public override void Interact()
        {
            if (_inventoryHandler.AddItem(Item))
            {
                Debug.Log($"Collected Item x{Item.Amount} {Item.ItemData.name}");
                AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_ITEM);
                Destroy(gameObject);
            }
        }
    }
}