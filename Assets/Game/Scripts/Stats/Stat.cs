using UnityEngine;

namespace Game.Scripts.Stats
{
    [System.Serializable]
    public class Stat
    {
        public string Name;
        public int Value;

        public Stat(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public void Increase(int amount)
        {
            Value += amount;
        }

        public void Decrease(int amount)
        {
            Value = Mathf.Max(Value - amount, 0);
        }
    }
}