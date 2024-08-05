using Game.Scripts.Stats;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;
        private StatsHandler _statsHandler => GameManager.Instance.Player.StatsHandler;
        private void Start()
        {
            StatsHandler.OnStatsChange += OnStatsChange;   
        }

        private void OnStatsChange()
        {
            healthText.text = $"{_statsHandler.GetStatValue("MaxHealth")}/{_statsHandler.GetStatValue("MaxHealth")}";
        }
    }
}
