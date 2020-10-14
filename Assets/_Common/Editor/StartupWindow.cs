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

        GUIStyle m_LessonLoadTitle = new GUIStyle();
        GUIStyle m_LessonTitle = new GUIStyle();
        GUIStyle m_ButtonStyle = new GUIStyle();
        GUIStyle m_DescriptionLabel = new GUIStyle();

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

            foreach (var set in m_Settings.m_SceneSettings)
            {
                set.lessonOpen.valueChanged.AddListener(Repaint);
                set.moreInfoOpen.valueChanged.AddListener(Repaint);
            }
        }

        [InitializeOnLoadMethod]
        void GetSetting()
        {
            m_AlreadyStyleSet = false;
            if (m_Settings == null)
            {
                var guid = AssetDatabase.FindAssets("t:StartupWindowConfiguration").FirstOrDefault();
                var path = AssetDatabase.GUIDToAssetPath(guid);

                m_Settings = (StartupWindowConfiguration)AssetDatabase.LoadAssetAtPath(path, typeof(StartupWindowConfiguration));
            }
        }
        void StylesSet()
        {
            if (m_AlreadyStyleSet)
                return;

            m_LessonLoadTitle = new GUIStyle(EditorStyles.miniButton);

            m_LessonLoadTitle.fontSize = 20;
            m_LessonLoadTitle.fixedHeight = EditorGUIUtility.singleLineHeight * 2;
            m_LessonLoadTitle.alignment = TextAnchor.MiddleCenter;

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

            SceneSet set;
            for (int i = 0; i < m_Settings.m_SceneSettings.Length; i++)
            {
                set = m_Settings.m_SceneSettings[i];
                SceneSelectionArea(set, set.folder, ref set.moreInfoOpen, ref set.lessonOpen);
            }

            GUILayout.Space(15);
            GUILayout.EndScrollView();
        }

        float m_BannerWidth;
        void SceneSelectionArea(SceneSet set, Object folder, ref AnimBool moreInfoOpen, ref AnimBool lessonOpen)
        {
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                GUILayout.Space(10);

                //場景名稱
                var buttonContent = lessonOpen.target ? new GUIContent() : EditorGUIUtility.IconContent("d_icon dropdown@2x");
                buttonContent.text = set.name;
                buttonContent.tooltip = $"點下查看{set.name}內容";
                if (GUILayout.Button(buttonContent, m_LessonLoadTitle))
                {
                    lessonOpen.target = !lessonOpen.target;
                }
                //GUILayout.Label(set.name, m_SceneLoadTitle);
                GUILayout.Space(10);

                if (EditorGUILayout.BeginFadeGroup(lessonOpen.faded))
                {
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
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndVertical();
        }



        void FolderLocationArea(Object folder)
        {
            if (GUILayout.Button("尋找資料夾", GUILayout.Width(200)))
            {
                EditorUtility.FocusProjectWindow();
                if (folder)
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