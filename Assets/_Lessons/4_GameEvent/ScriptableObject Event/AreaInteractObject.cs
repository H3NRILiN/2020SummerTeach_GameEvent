using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteractObject : MonoBehaviour
{
    #region 物件集合
    static Dictionary<int, AreaInteractObject> m_Collection;
    public static AreaInteractObject GetObject(Transform _transform)
    {
        if (!_transform || !m_Collection.ContainsKey(_transform.GetInstanceID()))
            return null;
        return m_Collection[_transform.GetInstanceID()];
    }
    private void OnEnable()
    {
        if (m_Collection == null)
            m_Collection = new Dictionary<int, AreaInteractObject>();

        m_Collection.Add(m_InstanceID, this);
    }
    private void OnDisable()
    {
        m_Collection.Remove(m_InstanceID);
    }
    #endregion

    public bool m_UsingID;
    public string m_MatchID;
    public AreaInteractionState m_StateCanInteract;

    public int m_InstanceID { get => transform.GetInstanceID(); }

    public void Interact(AreaInteractObject last)
    {
        Debug.Log($"目前物件: {m_MatchID} | 結合物件: {(last == null ? "無" : last.m_MatchID) }");
    }

    public void UnInteract()
    {
        Debug.Log("取消互動");
    }

}
