using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ISU.Lesson.GameEvent
{
    public class HandHoldDropZone : MonoBehaviour
    {
        [SerializeField] string m_MatchID;
        [SerializeField] LayerMask m_Layer;
        [SerializeField] MeshRenderer m_DisplayObject;
        [SerializeField] UnityEvent m_OnAnimationComplete;
        private void OnTriggerEnter(Collider other)
        {
            if (m_MatchID == string.Empty)
            {
                Debug.Log("No ID Setup");
                return;
            }
            if (other.gameObject.layer == 9)
            {
                var interact = other.GetComponent<HandHoldObject>();
                if (!interact || interact.m_MatchID == string.Empty)
                    return;
                if (interact.m_MatchID == m_MatchID)
                {
                    m_DisplayObject.enabled = false;
                    interact.OnDropZone(transform, m_OnAnimationComplete.Invoke);
                }
            }
        }
    }
}