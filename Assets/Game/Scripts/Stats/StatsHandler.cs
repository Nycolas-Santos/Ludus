using System;
using System.Collections.Generic;
using Game.Scripts.Inventory;
using Game.Scripts.Player.Game.Scripts.Equipment;
using UnityEngine;

namespace Game.Scripts.Stats
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField]
        private List<Stat> statList = new List<Stat>();

        public Dictionary<string, Stat> Stats;

        public static Action OnStatsChange;

        private void Awake()
        {
            Stats = new Dictionary<string, Stat>();

            // Initialize the dictionary with stats from the list
            foreach (var stat in statList)
            {
                Stats[stat.Name] = stat;
            }
            
            EquipmentHandler.OnItemEquipped += OnItemEquipped;
            EquipmentHandler.OnItemUnequipped += OnItemUnequipped;
        }

        private void OnItemUnequipped(Item item)
        {
            if (item.ItemData is null) return;
            DecreaseStat(item.ItemData.EquipmentSettings.Stat.Name, item.ItemData.EquipmentSettings.Stat.Value);
            OnStatsChange?.Invoke();
        }

        private void OnItemEquipped(Item item)
        {
            if (item.ItemData is null) return;
            IncreaseStat(item.ItemData.EquipmentSettings.Stat.Name, item.ItemData.EquipmentSettings.Stat.Value);
            OnStatsChange?.Invoke();
        }

        public int GetStatValue(string statName)
        {
            if (Stats.TryGetValue(statName, out Stat stat))
            {
                return stat.Value;
            }
            Debug.LogWarning($"Stat {statName} not found.");
            return 0;
        }

        public void IncreaseStat(string statName, int amount)
        {
            if (Stats.TryGetValue(statName, out Stat stat))
            {
                stat.Increase(amount);
                Debug.Log($"Increased {statName} by {amount}, new value: {stat.Value}");
            }
            else
            {
                Debug.LogWarning($"Stat {statName} not found.");
            }
        }

        public void DecreaseStat(string statName, int amount)
        {
            if (Stats.TryGetValue(statName, out Stat stat))
            {
                stat.Decrease(amount);
                Debug.Log($"Decreased {statName} by {amount}, new value: {stat.Value}");
            }
            else
            {
                Debug.LogWarning($"Stat {statName} not found.");
            }
        }
    }
}

