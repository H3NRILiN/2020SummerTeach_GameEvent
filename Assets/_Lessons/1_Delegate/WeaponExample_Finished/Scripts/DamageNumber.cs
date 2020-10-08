using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.Delegate.WeaponExample
{
    [RequireComponent(typeof(Text))]
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField] Vector3 m_TargetOffset;

        Sequence m_CurrentSequence;
        Text m_Text;
        private void Awake()
        {
            m_CurrentSequence = DOTween.Sequence();
            m_Text = GetComponent<Text>();
        }

        public void OnSpawn(float amount)
        {
            m_Text.text = amount.ToString();

            float duration = 1;

            m_CurrentSequence
            .Insert(0, transform.DOMove(transform.position + m_TargetOffset, duration))
            .Insert(0, m_Text.DOFade(0, duration))
            .OnComplete(() => Destroy(gameObject));
        }
    }
}