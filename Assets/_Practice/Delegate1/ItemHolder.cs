using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemHolder : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image m_ItemImage;


    [SerializeField] CanvasGroup m_Group;

    float m_OnDragImageSize = 1.2f;

    Action<Transform, Sprite, Action> m_OnItemDrop;

    private void Start()
    {
        m_OnItemDrop += FindObjectOfType<Backpuck>().OnItemRecieve;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_Group.blocksRaycasts = false;

        m_ItemImage.rectTransform.localScale *= m_OnDragImageSize;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        m_ItemImage.rectTransform.localScale = Vector2.one;

        Transform match = null;
        if (eventData.hovered.Count > 0)
            match = eventData.hovered[0].transform;


        m_OnItemDrop(match, m_ItemImage.sprite, () => gameObject.SetActive(false));


        m_Group.blocksRaycasts = true;
    }
}


