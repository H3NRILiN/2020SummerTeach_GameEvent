using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject m_DelegateTest;
        private void OnEnable()
        {
            var script = FindObjectOfType<DelegateTest>();
            if (script == null)
            {
                Instantiate(m_DelegateTest);
            }

            // var managers = FindObjectsOfType<GameManager>();
            // foreach (var m in managers)
            // {
            //     if (m != this)
            //     {
            //         Destroy(m.gameObject);
            //         continue;
            //     }
            // }
            // DontDestroyOnLoad(gameObject);

        }
        public void ResetGame()
        {

            SceneManager.LoadScene("1_Delegate_BaseExample");
        }

        public void NextScene()
        {
            SceneManager.LoadScene("1_Delegate_BaseExample_Scene2");
        }
    }
}
