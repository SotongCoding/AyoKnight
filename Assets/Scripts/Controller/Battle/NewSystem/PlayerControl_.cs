using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;

namespace FH_PlayerControl
{
    public class PlayerControl_ : MonoBehaviour
    {
        int currentPickIndex; 
        private void Start() {
            GameManager_BattleManager.Instance.comboHandler.onCorretInput += CorrectArrowEvent;
            GameManager_BattleManager.Instance.comboHandler.onMissInput += MissArrowEvent;
            GameManager_BattleManager.Instance.comboHandler.OnSendedComboResult += SendedComboresutlEvent;

        }
        public void PickArrow(int code)
        {
            GameManager_BattleManager.Instance.comboHandler.CheckArrow(code);
            currentPickIndex++;
        }
        public void Submit(float timeRemaining)
        {
            currentPickIndex =0;
        }

        void MissArrowEvent(int arrowCode){
            UIHandler.CombatUI.generatedArrow[currentPickIndex].SetActive(false);
            Debug.Log("You miss "+ (ArrowType)arrowCode);
        }
        void CorrectArrowEvent(int arrowCode){
            UIHandler.CombatUI.generatedArrow[currentPickIndex].SetActive(true);
            Debug.Log("You Hit "+ (ArrowType)arrowCode);
        }

        void SendedComboresutlEvent(CheckComboResult result){
            Debug.Log("You gain : Correct "+result.corretAmout +" | miss : "+ result.missAmount+"| this is Perfect Combo? : "+ result.isPerfect);
            Submit(1);
        }
    }
}
