using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Game/AudioData", order = 1)]
    public class AudioData : ScriptableObject
    {
        [System.Serializable]
        public class AudioEntry
        {
            public string Name;
            public AudioClip Clip;
        }

        public List<AudioEntry> AudioEntries = new List<AudioEntry>();
    }
}