using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace ISU.Sound
{
    [Serializable]
    public class SoundData
    {
        public string name = "";
        public string soundName = "";
        public AudioClip[] clips = new AudioClip[] { };
        public bool random = false;

        public AudioClip GetClip()
        {
            if (random)
            {
                return clips[Random.Range(0, clips.Length)];
            }

            return clips[0];
        }
    }

    [CreateAssetMenu(menuName = "_Finished/SoundCollection")]
    public class SoundCollection : ScriptableObject
    {
        public List<SoundData> m_SoundDatas = new List<SoundData>();
    }
}