using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Backpuck : MonoBehaviour, IDropHandler
{
    [SerializeField] Transform m_Bubble;
    [SerializeField] Image m_ItemImage;

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnItemRecieve(Transform obj, Sprite image, Action OnBackpuckFound)
    {
        if (!obj && obj != transform)
            return;
        OnBackpuckFound();
        m_ItemImage.sprite = image;

        m_Bubble.localScale = Vector3.zero;

        var tween = m_Bubble.
                 DOScale(Vector3.one, 0.5f).
                 SetEase(Ease.OutBounce);


    }
}
