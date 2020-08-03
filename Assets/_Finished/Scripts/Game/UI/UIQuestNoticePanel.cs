using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIQuestNoticePanel : MonoBehaviour
{
    [SerializeField] ListQuestVariable m_TrackingQuests;
    [SerializeField] UIQuestNoticeBlock[] m_NoticeBlocks;

    #region Edtior用

    [SerializeField] RectTransform m_NoticeListContainer;
    [SerializeField] UIQuestNoticeBlock m_NoticeBlockPrefab;
    [SerializeField] IntVariable m_NoticeBlockCount;
    [SerializeField] ContentSizeFitter m_SizeFitter;
    [SerializeField] VerticalLayoutGroup m_LayoutGroup;

    [ContextMenu("生成Blocks")]
    private void SetBlocks()
    {
        StartCoroutine(SetBloacksRoutine());
    }

    /// <summary>
    /// 生成Block的Coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator SetBloacksRoutine()
    {
        //摧毀所有底下的物件
        foreach (var item in m_NoticeListContainer.GetComponentsInChildren<Transform>())
        {
            if (item == m_NoticeListContainer || item == null)
                continue;
            DestroyImmediate(item.gameObject);
        }
        ActiveLayout(true);
        yield return null;
        //建立List，實例化物件
        if (m_NoticeListContainer && m_NoticeBlockPrefab)
        {
            m_NoticeBlocks = new UIQuestNoticeBlock[m_NoticeBlockCount.value];
            for (int i = 0; i < m_NoticeBlockCount.value; i++)
            {
                //實例化，命名
                m_NoticeBlocks[i] = Instantiate(m_NoticeBlockPrefab, m_NoticeListContainer);
                m_NoticeBlocks[i].name = $"Block {m_NoticeBlockCount.value - i}";
            }
            //Linq依命名排序
            m_NoticeBlocks = m_NoticeBlocks.OrderBy(x => x.name).ToArray();
        }
        yield return null;

        ActiveLayout(false);
    }
    #endregion

    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// SizeFitter與GroupLayout開關
    /// </summary>
    /// <param name="on"></param>
    void ActiveLayout(bool on)
    {
        m_SizeFitter.enabled = on;
        m_LayoutGroup.enabled = on;
    }


    public void UpdateUI()
    {
        foreach (var item in m_TrackingQuests.value)
        {
            Debug.Log($"{item.name} : {item.currentCount}");
        }
        for (int i = 0; i < m_NoticeBlocks.Length; i++)
        {
            if (i >= m_TrackingQuests.value.Count || m_TrackingQuests.value[i] == null)
            {
                m_NoticeBlocks[i].UpdateInfo(null);
                continue;
            }
            if (!m_NoticeBlocks[i].m_CurrentQuestIDName.Equals(m_TrackingQuests.value[i].IDName))
            {
                m_NoticeBlocks[i].Notice(m_TrackingQuests.value[i]);
            }
            else
            {
                m_NoticeBlocks[i].UpdateInfo(m_TrackingQuests.value[i]);
            }
        }
    }
}
