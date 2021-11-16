using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;

namespace FH_PlayerControl
{
    public class PlayerControl_ : MonoBehaviour
    {
        public static PlayerControl_ Instance;
        int currentPickIndex;
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            GameManager_BattleManager.Instance.comboHandler.onCorretInput += CorrectArrowEvent;
            GameManager_BattleManager.Instance.comboHandler.onMissInput += MissArrowEvent;
            GameManager_BattleManager.Instance.comboHandler.OnSendedComboResult += SendedComboresultEvent;
            GameManager_BattleManager.Instance.comboHandler.onCheckPerfectCombo += CheckComboPerfect;

        }
        public void PickArrow(int code)
        {
            if (currentPickIndex < UIHandler.CombatUI.generatedArrow.Count)
            {
                GameManager_BattleManager.Instance.comboHandler.CheckArrow(code);
            }
        }
        public void Submit(CheckComboResult comboResult)
        {
            if (GameManager_BattleManager.Instance.playerPriority)
            {
                GameManager_BattleManager.Instance.currentPlayerPlay.ActionManager.
                CallAction(GameManager_BattleManager.Instance.currentPlayerPlay, "basicAttack", comboResult);
            }
            else
            {
                GameManager_BattleManager.Instance.currentEnemyPlay.ActionManager.
                CallAction(GameManager_BattleManager.Instance.currentEnemyPlay, "basicAttack", comboResult);
            }
            bool playerPriority = GameManager_BattleManager.Instance.playerPriority;

            GameManager_BattleManager.Instance.currentPlayerPlay.unitUI.UpdatePriorityIcon(playerPriority);
            GameManager_BattleManager.Instance.currentEnemyPlay.unitUI.UpdatePriorityIcon(!playerPriority);

            UIHandler.CombatUI.ShowActionUI(false);
            UIHandler.CombatUI.ResetArrow();
            
            currentPickIndex = 0;
        }

        void MissArrowEvent(int arrowCode)
        {
            UIHandler.CombatUI.generatedArrow[currentPickIndex].SetCorrect(false);
            UIHandler.CombatUI.Debug("You miss " + (ArrowType)arrowCode);
            currentPickIndex++;
        }
        void CorrectArrowEvent(int arrowCode)
        {
            UIHandler.CombatUI.generatedArrow[currentPickIndex].SetCorrect(true);
            UIHandler.CombatUI.Debug("You Hit " + (ArrowType)arrowCode);
            currentPickIndex++;
        }

        void CheckComboPerfect(int correctAmount, bool isPerfect)
        {
            if (isPerfect)
            {
                UIHandler.CombatUI.ResetArrow();
                GameManager_BattleManager.Instance.SetCombo();
                currentPickIndex = 0;
            }
            else
            {
                GameManager_BattleManager.Instance.comboHandler.SendCombo();
            }
        }
        void SendedComboresultEvent(CheckComboResult result)
        {
            GameManager_BattleManager.Instance.EndPickArrowTime();
            StartCoroutine(SendComboEvent(result));
        }

        IEnumerator SendComboEvent(CheckComboResult result)
        {
            UIHandler.CombatUI.Debug("You gain : Correct " + result.corretAmout + " | miss : " + result.missAmount + "| Perfect Combo : " + result.perfectAmount);
            yield return new WaitForSeconds(2f);
            Submit(result);
        }
    }
}
