using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] SoundCollection[] m_SoundConllections;

        Dictionary<string, SoundData> m_ConllectionContainer;
        public static SoundManager m_Instance;

        private void Awake()
        {
            m_Instance = this;

            m_ConllectionContainer = new Dictionary<string, SoundData>();

            foreach (var collection in m_SoundConllections)
            {
                if (collection == null) continue;

                foreach (var data in collection.m_SoundDatas)
                {
                    if (data == null) continue;
                    m_ConllectionContainer.Add(data.soundName, data);
                }
            }
        }

        public AudioClip GetClip(string name)
        {
            return m_ConllectionContainer[name].GetClip();
        }
    }
}