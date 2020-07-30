using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "_Finished/Item")]
public class ItemObject : ScriptableObject
{
    public string m_ItemName;

    [TextArea] public string m_Discription;

    public int GetID() => this.GetInstanceID();
}
