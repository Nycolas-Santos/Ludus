using Game.Scripts.Inventory;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private InventoryUI inventoryUI;
        
        public bool IsInventoryOpen => inventoryUI.gameObject.activeSelf;
        
        public void ShowInventoryUI()
        {
            inventoryUI.gameObject.SetActive(true);
        }
        
        public void CloseInventoryUI()
        {
            inventoryUI.gameObject.SetActive(false);
        }
    }
}
