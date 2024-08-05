using TMPro;
using UnityEngine;

namespace Game.Scripts.Stats
{
    public class StatUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text statText;

        public void Initialize(Stat stat)
        {
            statText.text = $"{stat.Name}: {stat.Value}";
        }
    }
}
