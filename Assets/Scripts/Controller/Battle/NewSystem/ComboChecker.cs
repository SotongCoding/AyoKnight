using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_BattleModule
{
    public class ComboChecker
    {
        List<int> arrowCode = new List<int>();
        int checkCountdown = 0;

        #region Checker Event
        public System.Action<int> onCorretInput;
        public System.Action<int> onMissInput;
        public System.Action<int, bool> onCheckPerfectCombo;
        public System.Action<CheckComboResult> OnSendedComboResult;

        public void ResetEvent(System.Action<int> onCorretInput = null,
        System.Action<int> onMissInput = null,
        System.Action<int, bool> onCheckPerfectCombo = null,
        System.Action<CheckComboResult> OnSendedComboResult = null)

        {
            this.onCorretInput = onCorretInput;
            this.onMissInput = onMissInput;
            this.onCheckPerfectCombo = onCheckPerfectCombo;

            this.OnSendedComboResult = OnSendedComboResult;
        }
        #endregion

        #region  Check Arrow Data
        public int correctArrowAmount { private set; get; }
        public int missArrowAmount { private set; get; }
        public int perfectComboAmount { private set; get; }
        #endregion

        public void CheckArrow(int codeInput)
        {
            if (codeInput.Equals(arrowCode[checkCountdown]))
            {
                correctArrowAmount++;
                onCorretInput?.Invoke(codeInput);
            }
            else
            {
                missArrowAmount++;
                onMissInput?.Invoke(codeInput);
            }

            checkCountdown++;
            if (checkCountdown == arrowCode.Count)
            {
                bool perfectComboCheck = correctArrowAmount % arrowCode.Count == 0;
                if (perfectComboCheck)
                {
                    onCheckPerfectCombo?.Invoke(correctArrowAmount, perfectComboCheck);
                    perfectComboAmount++;
                }
                else
                {
                    SendCombo();
                }
            }
        }
        public void SendCombo()
        {
            OnSendedComboResult?.Invoke(new CheckComboResult(
            correctArrowAmount,
            missArrowAmount + (Mathf.Abs((perfectComboAmount + 1) * arrowCode.Count - (correctArrowAmount + missArrowAmount))),
            perfectComboAmount));

            correctArrowAmount = missArrowAmount = perfectComboAmount = 0;
        }
        public int[] SetCombo(int[] arrowVariation, int arrowAmount)
        {
            checkCountdown = 0;
            arrowCode.Clear();

            int[] tempResult = new int[arrowAmount];
            for (int i = 0; i < arrowAmount; i++)
            {
                int selectedArrow = arrowVariation[Random.Range(0, arrowVariation.Length)];
                arrowCode.Add(selectedArrow);
                tempResult[i] = selectedArrow;
            }

            return tempResult;
        }
    }

    public struct CheckComboResult
    {
        public int corretAmout { get; }
        public int missAmount { get; }
        public int perfectAmount { get; }
        public CheckComboResult(int corretAmout, int missAmount, int perfectAmount)
        {
            this.corretAmout = corretAmout;
            this.missAmount = missAmount;
            this.perfectAmount = perfectAmount;
        }

    }
}
