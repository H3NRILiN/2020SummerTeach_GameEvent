using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;
using UnityEditor.AnimatedValues;

namespace ISU.Common.HelpWindow
{
    public class StartupWindow : EditorWindow
    {

        [SerializeField] StartupWindowConfiguration m_Settings;

        GUIStyle m_SceneLoadTitle = new GUIStyle();
        GUIStyle m_LessonTitle = new GUIStyle();
        GUIStyle m_ButtonStyle = new GUIStyle();
        GUIStyle m_DescriptionLabel = new GUIStyle();

        [SerializeField] AnimBool[] m_MoreInfoAreaOpens;

        [SerializeField] Vector2 m_MainAreaScroll;

        bool m_AlreadyStyleSet;

        [MenuItem("Tools/ISU教學/幫助視窗")]
        static void Init()
        {
            var window = GetWindow<StartupWindow>("ISU教學幫助視窗");
            window.minSize = new Vector2(690, 420);
        }

        private void OnEnable()
        {
            GetSetting();
        }

        [InitializeOnLoadMethod]
        void GetSetting()
        {
            if (m_Settings == null)
            {
                var guid = AssetDatabase.FindAssets("t:StartupWindowConfiguration").FirstOrDefault();
                var path = AssetDatabase.GUIDToAssetPath(guid);

                m_Settings = (StartupWindowConfiguration)AssetDatabase.LoadAssetAtPath(path, typeof(StartupWindowConfiguration));
            }

            m_MoreInfoAreaOpens = new AnimBool[m_Settings.m_SceneSettings.Length];

            for (int i = 0; i < m_MoreInfoAreaOpens.Length; i++)
            {
                m_MoreInfoAreaOpens[i] = new AnimBool();
                m_MoreInfoAreaOpens[i].target = true;
                m_MoreInfoAreaOpens[i].valueChanged.AddListener(Repaint);
            }
        }
        void StylesSet()
        {
            if (m_AlreadyStyleSet)
                return;

            m_SceneLoadTitle = new GUIStyle(EditorStyles.label);
            m_SceneLoadTitle.fontSize = 18;
            m_SceneLoadTitle.alignment = TextAnchor.MiddleCenter;

            m_ButtonStyle = new GUIStyle(GUI.skin.button);
            m_ButtonStyle.fontSize = 15;

            m_AlreadyStyleSet = true;
        }

        private void OnGUI()
        {
            StylesSet();

            m_MainAreaScroll = GUILayout.BeginScrollView(m_MainAreaScroll);

            if (!m_Settings)
                return;

            FolderSet folderSet;
            SceneSet set;
            AnimBool moreInfoOpen;

            for (int i = 0; i < m_Settings.folder.Length; i++)
            {
                folderSet = m_Settings.folder[i];

                for (int j = 0; j < m_Settings.m_SceneSettings.Length; j++)
                {
                    set = m_Settings.m_SceneSettings[j];
                    moreInfoOpen = m_MoreInfoAreaOpens[j];

                    if (folderSet.lesson != set.lesson)
                        continue;


                    SceneSelectionArea(set, folderSet.folder, ref moreInfoOpen);
                }
            }
            GUILayout.Space(15);
            GUILayout.EndScrollView();
        }

        float m_BannerWidth;
        void SceneSelectionArea(SceneSet set, Object folder, ref AnimBool moreInfoOpen)
        {
            GUILayout.Space(15);
            var rect = EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                GUILayout.Space(10);
                //場景名稱


                GUILayout.Label(set.name, m_SceneLoadTitle);

                GUILayout.Space(10);

                //原始1920x600
                m_BannerWidth = 650;//= EditorGUILayout.Slider(m_BannerWidth, 0, 1000);
                float hieght = m_BannerWidth * 0.3125f;

                //橫幅

                //置中用
                BeginCenterLayout();
                if (set.banner)
                    GUILayout.Label(set.banner, GUILayout.Width(m_BannerWidth), GUILayout.Height(hieght));
                EndCenterLayout();

                Separator();

                //場景專案位置
                var scenePath = AssetDatabase.GetAssetPath(set.scene);

                //若已經在場景就取消按鈕互動
                EditorGUI.BeginDisabledGroup(EditorSceneManager.GetActiveScene().path == scenePath);
                if (GUILayout.Button("進入場景", m_ButtonStyle))
                {

                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(scenePath);
                    }
                }
                EditorGUI.EndDisabledGroup();
                //顯示更多資訊
                moreInfoOpen = MoreInfoArea(moreInfoOpen, set.description);

                Separator();

                //提醒資料夾位置
                FolderLocationArea(folder);

                GUILayout.Space(15);
            }
            EditorGUILayout.EndVertical();
        }



        void FolderLocationArea(Object folder)
        {
            if (GUILayout.Button("尋找資料夾"))
            {
                EditorUtility.FocusProjectWindow();
                EditorGUIUtility.PingObject(folder);
            }
        }

        AnimBool MoreInfoArea(AnimBool open, string description)
        {
            open.target = EditorGUILayout.Foldout(open.target, "顯示詳細說明", toggleOnLabelClick: true);

            if (EditorGUILayout.BeginFadeGroup(open.faded))
            {
                GUILayout.BeginVertical(GUI.skin.box);

                GUILayout.Label("說明：");
                GUILayout.Label(description);


                GUILayout.EndVertical();


            }

            EditorGUILayout.EndFadeGroup();

            return open;
        }

        #region 我就懶
        private static void EndCenterLayout()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private static void BeginCenterLayout()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
        }
        void Separator()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        #endregion
    }
}