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

        GUIStyle m_WindowTitleTextStyle = new GUIStyle();
        GUIStyle m_LessonTitleTextStyle = new GUIStyle();
        GUIStyle m_ButtonStyle = new GUIStyle();
        GUIStyle m_SceneButtonStyle = new GUIStyle();
        GUIStyle m_LinkButtonStyle = new GUIStyle();
        GUIStyle m_DescriptionTextStyle = new GUIStyle();

        GUIContent m_LinkIconContent;

        [SerializeField] Vector2 m_MainAreaScroll;

        string m_ActiveScenePath;

        bool m_AlreadyStyleSet;
        bool m_CommentWindowShow;

        Rect m_CommentWindowRect;

        [MenuItem("Tools/ISU教學/幫助視窗")]
        static void Init()
        {
            var window = GetWindow<StartupWindow>("ISU教學幫助視窗");
            window.minSize = new Vector2(690, 840);
        }

        private void OnEnable()
        {
            m_LinkIconContent = EditorGUIUtility.IconContent("BuildSettings.Web.Small");
            m_CommentWindowRect = new Rect(10, 50, 300, 600);

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

            m_WindowTitleTextStyle = new GUIStyle(EditorStyles.largeLabel);
            m_WindowTitleTextStyle.fontStyle = FontStyle.Bold;
            m_WindowTitleTextStyle.fontSize = 25;
            m_WindowTitleTextStyle.alignment = TextAnchor.MiddleCenter;

            m_LessonTitleTextStyle = new GUIStyle(EditorStyles.miniButton);
            m_LessonTitleTextStyle.fontSize = 20;
            m_LessonTitleTextStyle.fixedHeight = EditorGUIUtility.singleLineHeight * 2;
            m_LessonTitleTextStyle.alignment = TextAnchor.MiddleLeft;

            m_ButtonStyle = new GUIStyle(GUI.skin.button);
            m_ButtonStyle.fontSize = 15;

            m_SceneButtonStyle = new GUIStyle(GUI.skin.button);
            m_SceneButtonStyle.fontSize = 25;

            m_LinkButtonStyle = new GUIStyle(GUI.skin.button);
            m_LinkButtonStyle.alignment = TextAnchor.MiddleCenter;

            m_AlreadyStyleSet = true;
        }

        private void OnGUI()
        {
            BeginWindows();
            StylesSet();

            WindowTitle();
            LinksArea();


            GUILayout.Space(10);
            m_MainAreaScroll = GUILayout.BeginScrollView(m_MainAreaScroll);

            if (!m_Settings)
                return;

            SceneSet set;
            for (int i = 0; i < m_Settings.m_SceneSettings.Length; i++)
            {
                set = m_Settings.m_SceneSettings[i];

                //場景專案位置
                m_ActiveScenePath = AssetDatabase.GetAssetPath(set.scene);

                EditorGUI.BeginDisabledGroup(!set.active);
                SceneSelectionArea(set, set.folder, ref set.moreInfoOpen, ref set.lessonOpen);
                EditorGUI.EndDisabledGroup();
            }

            GUILayout.Space(15);
            GUILayout.EndScrollView();

            if (m_CommentWindowShow)
                m_CommentWindowRect = GUILayout.Window(-99, m_CommentWindowRect, CommentWindow, "");

            EndWindows();

        }

        void CommentWindow(int windowID)
        {
            var style = new GUIStyle(GUI.skin.window);

            var rect = EditorGUILayout.BeginVertical(style, GUILayout.Height(20));
            GUILayout.Space(10);
            GUILayout.Label("留言區");
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
            GUI.DragWindow(rect);
        }

        void WindowTitle()
        {
            Separator();

            GUILayout.Label("教學幫助視窗", m_WindowTitleTextStyle);

            Separator();
        }

        void LinksArea()
        {
            BeginCenterLayout();
            LinkButton("專案原始碼(Github)", "https://github.com/H3NRILiN/2020SummerTeach_GameEvent");
            LinkButton("聯絡我");
            GUILayout.Space(10);
            SendCommentButton();
            EndCenterLayout();
        }
        void LinkButton(string label, string url = "")
        {
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(url));

            var content = new GUIContent(m_LinkIconContent);
            content.text = label;
            content.tooltip = $"連接到：{url}";
            if (GUILayout.Button(content, m_LinkButtonStyle))
            {
                if (EditorUtility.DisplayDialog("開啟連結",
                $@"是否連結到：{url}
這個動作會使用你目前的預設瀏覽器與瀏覽器視窗",
                "確定",
                "取消"))
                    Application.OpenURL(url);
            }
            EditorGUI.EndDisabledGroup();
        }
        void SendCommentButton()
        {
            var content = new GUIContent(m_LinkIconContent);
            content.text = "意見發送";

            EditorGUI.BeginDisabledGroup(true);
            if (GUILayout.Button(content, m_LinkButtonStyle))
            {
                m_CommentWindowShow = !m_CommentWindowShow;
            }
            EditorGUI.EndDisabledGroup();
        }

        float m_BannerWidth;
        void SceneSelectionArea(SceneSet set, Object folder, ref AnimBool moreInfoOpen, ref AnimBool lessonOpen)
        {
            //課程區域之間的距離
            GUILayout.Space(10);

            BeginHorSpaceLayout(5);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                //定義區
                var buttonWidth = 525;
                var buttonContent = EditorGUIUtility.IconContent("UnityEditor.SceneHierarchyWindow@2x");
                var tooltip = $"點下查看{set.name}內容";
                var tooltip_indev = "製作中";
                var alreadyInSceneTip = "目前在這個場景中";

                //與邊界的距離
                GUILayout.Space(10);

                bool alreadyInScene = EditorSceneManager.GetActiveScene().path == m_ActiveScenePath;

                #region 開啟課程區域的按鈕

                buttonContent.text = set.name;
                buttonContent.tooltip = set.active ? tooltip : tooltip_indev;
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(buttonContent, m_LessonTitleTextStyle, GUILayout.Width(buttonWidth))) lessonOpen.target = !lessonOpen.target;
                if (alreadyInScene) EditorGUILayout.HelpBox(alreadyInSceneTip, MessageType.Info);
                GUILayout.EndHorizontal();

                #endregion

                if (EditorGUILayout.BeginFadeGroup(lessonOpen.faded))
                {
                    //圖片與按鈕的距離 
                    GUILayout.Space(10);

                    //橫幅
                    BannerArea(set.banner);

                    Separator();

                    LoadSceneButtonArea(alreadyInScene);

                    //顯示更多資訊
                    moreInfoOpen = MoreInfoArea(moreInfoOpen, set.description);

                    Separator();

                    //提醒資料夾位置
                    FolderLocationArea(folder);

                    GUILayout.Space(15);

                }
                EditorGUILayout.EndFadeGroup();


                //提示
                var tipStyle = new GUIStyle(EditorStyles.label);
                tipStyle.normal.textColor = set.active ? Color.cyan : Color.magenta;
                if (!lessonOpen.target) GUILayout.Label(set.active ? tooltip : tooltip_indev, tipStyle);

            }

            EditorGUILayout.EndVertical();
            EndHorSpaceLayout(5);

            //課程區域之間的距離
            GUILayout.Space(10);
        }
        private void BannerArea(Texture2D pic)
        {
            //原始1920x600，比例為1:0.3125
            m_BannerWidth = 650;//= EditorGUILayout.Slider(m_BannerWidth, 0, 1000);
            float hieght = m_BannerWidth * 0.3125f;
            //橫幅
            BeginCenterLayout();
            {
                if (pic) GUILayout.Label(pic, GUILayout.Width(m_BannerWidth), GUILayout.Height(hieght));
            }
            EndCenterLayout();
        }
        private void LoadSceneButtonArea(bool alreadyInScene)
        {
            //若已經在場景就取消按鈕互動
            EditorGUI.BeginDisabledGroup(alreadyInScene);
            {
                var content = new GUIContent(EditorGUIUtility.IconContent("UnityLogo"));
                content.text = "進入場景";
                if (GUILayout.Button(content, m_ButtonStyle, GUILayout.Height(30)))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(m_ActiveScenePath);
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }
        void FolderLocationArea(Object folder)
        {
            BeginCenterLayout();
            if (GUILayout.Button("尋找資料夾", GUILayout.Width(200)))
            {
                EditorUtility.FocusProjectWindow();
                if (folder)
                    EditorGUIUtility.PingObject(folder);
            }
            GUILayout.EndHorizontal();
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
        /// <summary>
        /// 置中用
        /// </summary>
        private static void EndCenterLayout()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 置中用
        /// </summary>
        private static void BeginCenterLayout()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// 左右空白
        /// </summary>
        private static void EndHorSpaceLayout(float space)
        {
            GUILayout.Space(space);
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// 左右空白
        /// </summary>
        private static void BeginHorSpaceLayout(float space)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(space);
        }
        void Separator()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        #endregion
    }
}