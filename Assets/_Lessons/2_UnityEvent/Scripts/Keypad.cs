namespace ISU.Lesson.UNITYEvent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using ISU.Common;
    public class Keypad : MonoBehaviour, IInteractable
    {
        public int m_Password;
        [SerializeField] Text m_InputText;
        [SerializeField] UnityEvent m_OnInteract;
        [SerializeField] UnityEvent m_OnExit;
        [SerializeField] UnityEvent m_OnUnlock;

        [SerializeField] CameraAnimationTarget camAnimTarget;
        public CameraAnimationTarget m_CamAnimTarget { get => camAnimTarget; set => camAnimTarget = value; }

        int m_InputedNumbers;
        public void OnExitInteraction()
        {
            m_OnExit.Invoke();

        }

        public void OnInteract()
        {
            m_OnInteract.Invoke();
        }

        public void OnUnlockCheck()
        {
            if (m_InputedNumbers == m_Password)
            {
                m_OnUnlock.Invoke();
            }
        }

        private void Start()
        {
            StartCoroutine(GenerateKeypad());
        }
        void InputNumber(int number)
        {
            Debug.Log("Press " + number);
            m_InputedNumbers = m_InputedNumbers * 10 + number;
            m_InputText.text = m_InputedNumbers.ToString();


        }
        void ClearNumbers()
        {
            m_InputedNumbers = 0;
            m_InputText.text = "";
        }



        #region 自動生成按鍵
        [Header("自動生成按鍵")]
        [SerializeField] KeypadNumber m_KeypadNumberReference;
        [SerializeField] Transform m_KeypadNumberParent;
        [SerializeField] GridLayoutGroup m_GridLayout;
        [SerializeField] UnityEvent m_OnKeyInput;
        int m_NumbersCount = 12;


        public IEnumerator GenerateKeypad()
        {
            return GenerateKeypadNumbers(m_KeypadNumberReference, m_KeypadNumberParent, m_GridLayout, m_NumbersCount);
        }

        IEnumerator GenerateKeypadNumbers(KeypadNumber reference, Transform parent, GridLayoutGroup layout, int count)
        {
            layout.enabled = true;
            yield return null;
            for (int i = 0; i < count; i++)
            {
                var keynum = Instantiate(reference, parent);
                if (i == 11)
                {
                    keynum.ToEmptyObject();
                }

                int curNum = i + 1;
                if (curNum == 11)
                    curNum = 0;

                if (i == 9)
                {
                    keynum.SetText("清除");
                    keynum.m_OnKeyPress = ClearNumbers;
                    continue;
                }

                keynum.SetText(curNum);

                keynum.m_OnKeyPress = () => InputNumber(curNum);
            }
            Destroy(reference.gameObject);
            yield return null;

            layout.enabled = false;
        }



        #endregion
    }
}