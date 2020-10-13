using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ISU.Common.HelpWindow
{
    public enum Lessons
    {
        Delegate,
        UnityEvent,
        DesignPattern,
        GameEvent
    }

    [Serializable]
    public class SceneSet
    {
        public string name;
        public Lessons lesson;
        public SceneAsset scene;
        public Texture2D banner;
        [TextArea(5, 25)] public string description;

        public AnimBool m_LeesonOpen = new AnimBool(false);
        public AnimBool m_MoreInfoOpen = new AnimBool(false);
    }

    [Serializable]
    public class FolderSet
    {
        public Lessons lesson;
        public Object folder;
    }

    [CreateAssetMenu]
    public class StartupWindowConfiguration : ScriptableObject
    {
        public SceneSet[] m_SceneSettings;

        public FolderSet[] folder;

        private void OnEnable()
        {
            // var lessons = Enum.GetNames(typeof(Lessons));
            // folder = new FolderSet[lessons.Length];

            // for (int i = 0; i < lessons.Length; i++)
            // {
            //     folder[i].lesson = (Lessons)Enum.Parse(typeof(Lessons), lessons[i]);
            // }
        }
    }
}
