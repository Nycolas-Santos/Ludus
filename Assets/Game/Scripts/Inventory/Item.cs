using System;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    [Serializable]
    public class Item
    {
        public ItemData ItemData;
        public int Amount;
        
        [SerializeField]
        private string guid;

        public string GUID // this is to diferentiate different instances of the same item, like items that are not stackable
        {
            get
            {
                if (string.IsNullOrEmpty(guid))
                {
                    guid = System.Guid.NewGuid().ToString();
                }
                return guid;
            }
        }
    }
}