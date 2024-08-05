using System;
using System.Collections.Generic;
using Game.Scripts.Player.Game.Scripts.Equipment;
using Game.Scripts.Stats;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public class InventoryEquipmentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSlotUI[] equipmentSlots;
        [SerializeField] private StatUI statPrefab;
        [SerializeField] private Transform statsContentParent;
        
        private List<StatUI> _statUis = new List<StatUI>();
        private EquipmentHandler _equipmentHandler => GameManager.Instance.Player.EquipmentHandler;
        private StatsHandler _statsHandler => GameManager.Instance.Player.StatsHandler;

        private void Awake()
        {
            EquipmentHandler.OnEquipmentChanged += Initialize;
            StatsHandler.OnStatsChange += Initialize;
        }

        private void OnDestroy()
        {
            EquipmentHandler.OnEquipmentChanged -= Initialize;
            StatsHandler.OnStatsChange -= Initialize;
        }

        public void Initialize()
        {
            ClearStats();
            var equippedItems = _equipmentHandler.EquippedItems;
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].Initialize(equippedItems[i]);
            }

            foreach (var stat in _statsHandler.Stats)
            {
                var statUI = Instantiate(statPrefab, statsContentParent);
                _statUis.Add(statUI);
                statUI.Initialize(stat.Value);
            }
        }

        public void Terminate()
        {
            foreach (var slot in equipmentSlots)
            {
                slot.Terminate();
            }
            ClearStats();
        }

        private void ClearStats()
        {
            foreach (var stat in _statUis)
            {
                Destroy(stat.gameObject);
            }
            _statUis.Clear();
        }
    }
}
