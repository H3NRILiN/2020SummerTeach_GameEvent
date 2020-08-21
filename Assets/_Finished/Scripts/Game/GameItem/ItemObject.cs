using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Example.Inventory
{
    [CreateAssetMenu(menuName = "_Finished/Item")]
    public class ItemObject : ScriptableObject
    {
        public string m_ItemName;

        [TextArea] public string m_Discription;

        public Color m_DisplayColor = Color.white;
        public int GetID() => this.GetInstanceID();
    }
}