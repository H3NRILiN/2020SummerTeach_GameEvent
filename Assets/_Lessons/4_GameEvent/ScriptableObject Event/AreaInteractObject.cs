using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteractObject : MonoBehaviour
{
    #region 物件集合
    static Dictionary<int, AreaInteractObject> m_Collection;
    public static AreaInteractObject GetObject(Transform transform)
    {
        if (!transform || !m_Collection.ContainsKey(transform.GetInstanceID()))
            return null;
        return m_Collection[transform.GetInstanceID()];
    }
    private void OnEnable()
    {
        if (m_Collection == null)
            m_Collection = new Dictionary<int, AreaInteractObject>();

        m_Collection.Add(transform.GetInstanceID(), this);
    }
    private void OnDisable()
    {
        m_Collection.Remove(transform.GetInstanceID());
    }
    #endregion

    public AreaInteractionState m_StateCanInteract;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
