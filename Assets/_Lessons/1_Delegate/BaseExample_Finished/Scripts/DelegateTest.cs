using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class DelegateTest : MonoBehaviour
    {
        [SerializeField] string m_TextContent;

        public delegate void OnDoThing();
        public OnDoThing m_OnDoThing;


        public delegate void OnShowText(string content, Color color, Vector2 scale);
        public OnShowText m_OnShowText;


        delegate int Caculation(int num);


        private void Start()
        {
            m_OnDoThing += DoThings;
            m_OnDoThing += DoThings2;

            if (m_OnDoThing != null)
                m_OnDoThing();

            // Debug.Log("num++：" + DoMath(10, NumPlusOne));
            //Debug.Log("num*num：" + DoMath(10, NumTimesNum));
            Debug.Log("乘2 :" + DoMath(10, x => x * 2));
            Debug.Log("除2 :" + DoMath(10, x => x / 2));
            Debug.Log("平方 :" + DoMath(10, x => x * x));

            // Debug.Log(DoMath(10, (num) => num + 120));
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

        int NumPlusOne(int num)
        {
            return num + 1;
        }
        int NumTimesNum(int num)
        {
            return num * num;
        }

        void DoThings()
        {
            Debug.Log("DoThings");
        }
        void DoThings2()
        {
            Debug.Log("DoThings2");
        }

        void LamdaMethod() => Debug.Log("123");
    }
}