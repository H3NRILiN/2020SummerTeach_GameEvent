using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class ActionFuncTest : MonoBehaviour
    {
        [SerializeField] string m_TextContent;

        public Action m_OnDoThing;

        public Action<string, Color, Vector2> m_OnShowText;




        // void AMethod(int num, float numF)
        // {

        // }

        // Func<int> B;

        private void Start()
        {
            if (m_OnDoThing != null)
                m_OnDoThing();

            Debug.Log("num+1：" + DoMath(10, NumPlusOne));
            Debug.Log("num*num：" + DoMath(10, NumTimesNum));

            // Debug.Log("乘2 :" + DoMath(10, x => x * 2));
            // Debug.Log("除2 :" + DoMath(10, x => x / 2));
            // Debug.Log("平方 :" + DoMath(10, x => x * x));
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

        int DoMath(int num, Func<int, int> caculation)
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


        int SomeMethod(int x)
        {
            int result = x + x;
            return result;
        }
    }
}