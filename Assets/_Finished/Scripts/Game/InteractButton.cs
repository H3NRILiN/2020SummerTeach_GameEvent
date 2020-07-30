using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ISU.Example
{
    [RequireComponent(typeof(Interactable))]
    public class InteractButton : SubInteractor
    {
        public override void OnInteract()
        {
            AchievementManager.m_Instance.AddCount("A_ButtonPress");
        }
    }
}