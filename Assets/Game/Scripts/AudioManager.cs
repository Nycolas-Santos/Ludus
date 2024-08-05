using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Data;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioData AudioData;
        public AudioSource AudioSource;

        private Dictionary<string, AudioClip> audioClips;

        protected override void Awake()
        {
            base.Awake();
            audioClips = new Dictionary<string, AudioClip>();

            foreach (var entry in AudioData.AudioEntries.Where(entry => !audioClips.ContainsKey(entry.Name)))
            {
                audioClips.Add(entry.Name, entry.Clip);
            }
        }

        public void PlayUISound(string sound)
        {
            if (audioClips.TryGetValue(sound, out AudioClip clip))
            {
                AudioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"Sound '{sound}' not found in AudioData.");
            }
        }
    }
}
