using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ObjectImpactSound : MonoBehaviour
{

    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioClip m_ImpactSound;
    private void Reset()
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.spatialBlend = 0.7f;
        m_Source.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Player"))
            m_Source.PlayOneShot(m_ImpactSound);
    }
}
