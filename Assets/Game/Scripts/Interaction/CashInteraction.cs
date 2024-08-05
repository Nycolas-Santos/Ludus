using Game.Scripts.Extensions;
using Game.Scripts.Inventory;
using UnityEngine;

namespace Game.Scripts.Interaction
{
    public class CashInteraction : InteractableBase
    {
        public int Amount;
        private InventoryHandler _inventoryHandler => GameManager.Instance.Player.InventoryHandler;
        public override void Interact()
        {
           Debug.Log("Cash Collected: " + Amount);
           _inventoryHandler.AddCash(Amount);
           AudioManager.Instance.PlayUISound(Constants.AudioStrings.UI_CASH);
           Destroy(gameObject);
        }
    }
}