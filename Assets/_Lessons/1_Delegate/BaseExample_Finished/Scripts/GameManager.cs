using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            var manager = FindObjectOfType<GameManager>();
            if (manager != null && manager != this)
            {
                Destroy(manager.gameObject);
            }
            DontDestroyOnLoad(gameObject);

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
