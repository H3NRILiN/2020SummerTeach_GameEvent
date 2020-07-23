using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ISUExample
{
    [RequireComponent(typeof(BoxCollider))]
    public class Interactable : MonoBehaviour
    {
        public string m_ObjectName;
        public Color m_TextColor = Color.white;
        public bool m_UseKeyPress = false;
        private void Reset()
        {
            this.tag = "Interactable";
        }
        void Start()
        {
            InteractionManager.m_Instance.Register(this);
        }

        // Update is called once per frame
        public void StartInteract()
        {
            Debug.Log($"與{m_ObjectName}互動");
        }
    }
}
