using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SotongUtility.StatePattern;

using FH_BattleModule;
namespace FH_StateModule
{
    public class CombatState_Practice_Action : ActionPhase
    {
        public override IEnumerator BeginState()
        {
            UIHandler.CombatUI.Debug("Begin Of Action Phase of Current Player and Enemys");
            yield return new WaitForSeconds(3);
            StartCoroutine(RunningState());
        }
        public override IEnumerator RunningState()
        {
            // Player
            GameManager_BattleManager.Instance.SetUnitPriority(true);
            StartCoroutine(SetAction()); //Call Arrow Control
            UIHandler.CombatUI.Debug("Begin Current Player Action and Animation");
            yield return new WaitUntil(GameManager_BattleManager.Instance.currentPlayerPlay.actionManager.IsCurrentActionDone);

            // Enemy
            GameManager_BattleManager.Instance.SetUnitPriority(false);
            StartCoroutine(SetAction()); // Call Arrow Control
            UIHandler.CombatUI.Debug("Begin Current Enemy Action and Animation");
            yield return new WaitUntil(GameManager_BattleManager.Instance.currentEnemyPlay.actionManager.IsCurrentActionDone);

            yield return new WaitForSeconds(1);
            UIHandler.CombatUI.Debug("Call End State");
            StartCoroutine(EndState());
        }
        public override IEnumerator EndState()
        {
            UIHandler.CombatUI.Debug("Here End of Action State.");
            yield return new WaitForSeconds(1);
            UIHandler.CombatUI.Debug("Begin Call State Stand By Once more");

            StateHelper.ChangeCombatState("standbyPhase");
        }

        IEnumerator SetAction()
        {
            UIHandler.CombatUI.Debug("Call Unit State to Do something like Manager Said");
            UIHandler.CombatUI.Debug("Enable UI and Start Combo Timer");

            GameManager_BattleManager.Instance.SetCombo();
            GameManager_BattleManager.Instance.SetPickArrowTime();

            UIHandler.CombatUI.ShowActionUI(true);
            GameManager_BattleManager.Instance.BeginPickArrowTime();
            UIHandler.CombatUI.Debug("Player doing combo");

            yield return null;
        }


    }
}
