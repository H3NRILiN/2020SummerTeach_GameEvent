using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISUExample
{
    public class InteractionManager : MonoBehaviour
    {
        Dictionary<int, Interactable> m_Interactables;
        public static InteractionManager m_Instance;

        private void Awake()
        {
            m_Instance = this;

            m_Interactables = new Dictionary<int, Interactable>();
        }

        public void Register(Interactable interactable)
        {
            m_Interactables.Add(interactable.transform.GetInstanceID(), interactable);
        }

        public void UnRegister(Interactable interactable)
        {
            m_Interactables.Remove(interactable.transform.GetInstanceID());
        }

        public Interactable Access(int id)
        {
            if (m_Interactables.ContainsKey(id))
                return m_Interactables[id];
            else
                return (new Interactable() { name = "Null" });
        }
    }
}
