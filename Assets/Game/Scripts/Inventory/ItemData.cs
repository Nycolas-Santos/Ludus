using UnityEngine;

namespace Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string ItemName;
        public string ItemDescription;
        public bool Stackable;
        public Sprite ItemIcon;
    }
}