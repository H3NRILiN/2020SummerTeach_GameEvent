using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class ResetButton : MonoBehaviour
    {
        [SerializeField] Button m_Button;
        // Start is called before the first frame update
        void Start()
        {
            m_Button.onClick.AddListener(() => FindObjectOfType<GameManager>().ResetGame());
        }
    }
}

