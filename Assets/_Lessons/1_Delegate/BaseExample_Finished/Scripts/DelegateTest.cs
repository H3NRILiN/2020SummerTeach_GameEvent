using UnityEngine;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class DelegateTest : MonoBehaviour
    {
        [SerializeField] string m_TextContent;

        public delegate void OnDoThing();
        public delegate void OnShowText(string content, Color color, Vector2 scale);


        public OnDoThing m_OnDoThing;
        public OnShowText m_OnShowText;

        delegate int Caculation(int num);


        private void Start()
        {



            if (m_OnDoThing != null)
                m_OnDoThing();


            Debug.Log("乘2 :" + DoMath(10, x => x * 2));
            Debug.Log("除2 :" + DoMath(10, x => x / 2));
            Debug.Log("平方 :" + DoMath(10, x => x * x));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                m_OnShowText?.Invoke(m_TextContent, Random.ColorHSV(), new Vector2(1 + Random.value, 1 + Random.value));

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (m_OnDoThing != null)
                    m_OnDoThing();
            }
        }

        int DoMath(int num, Caculation caculation)
        {
            return caculation(num);
        }


    }
}