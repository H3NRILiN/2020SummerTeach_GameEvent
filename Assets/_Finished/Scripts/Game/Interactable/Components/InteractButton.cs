using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ISU.Example
{
    [RequireComponent(typeof(Interactable))]
    public class InteractButton : SubInteractor
    {
        Tween m_PressTween;
        private void Start()
        {
            GetComponent<Interactable>().m_OnSubInteract = OnInteract;
        }
        public override void OnInteract()
        {
            if (m_PressTween != null)
                m_PressTween.Kill();
            m_PressTween = transform.DOMoveY(0.93f, 0.1f).OnComplete(() => transform.DOMoveY(1, 0.2f));
        }
    }
}