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
        public System.Action<CheckComboResult> OnSendedComboResult;

        public void ResetEvent(System.Action<int> onCorretInput = null, System.Action<int> onMissInput = null, System.Action<CheckComboResult> OnSendedComboResult = null)
        {
            this.onCorretInput = onCorretInput;
            this.onMissInput = onMissInput;
            this.OnSendedComboResult = OnSendedComboResult;
        }
        #endregion

        #region  Check Arrow Data
        public int correctArrowAmount { private set; get; }
        public int missArrowAmount { private set; get; }
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
            if (checkCountdown >= arrowCode.Count)
            {
                OnSendedComboResult?.Invoke(new CheckComboResult(correctArrowAmount, missArrowAmount,
                correctArrowAmount == arrowCode.Count && missArrowAmount == 0));
            }
        }
        public int[] SetCombo(int[] arrowVariation, int arrowAmount)
        {
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
        public bool isPerfect { get; }
        public CheckComboResult(int corretAmout, int missAmount, bool isPerfect)
        {
            this.corretAmout = corretAmout;
            this.missAmount = missAmount;
            this.isPerfect = isPerfect;
        }

    }
}
