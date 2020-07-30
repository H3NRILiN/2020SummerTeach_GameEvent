using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource m_AudioSource;
        public string m_SoundName;

        public void PlaySound()
        {
            m_AudioSource.PlayOneShot(SoundManager.m_Instance.GetClip(m_SoundName));
        }
    }
}