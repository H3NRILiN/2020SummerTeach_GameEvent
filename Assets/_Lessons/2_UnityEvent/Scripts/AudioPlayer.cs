namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        AudioSource m_Source;
        private void Start()
        {
            m_Source = GetComponent<AudioSource>();
        }

        //播放音效
        public void PlayAudio(AudioClip clip)
        {
            m_Source.PlayOneShot(clip);
        }
    }
}