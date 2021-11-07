using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SotongUtility.StatePattern;
using FH_BattleModule;
namespace FH_StateModule
{
    public class CombatState_Practice_Standby : StandbyPhase
    {
        public override IEnumerator BeginState()
        {
            UIHandler.CombatUI.Debug("Call Combat Mnager for Set Enemy and Player");
            GameManager_BattleManager.Instance.NextSwitchUnit();
            GameManager_BattleManager.Instance.SetUnitPriority(true);
            yield return new WaitForSeconds(1);
            UIHandler.CombatUI.Debug("Call Running State");
            StartCoroutine(RunningState());
        }
        public override IEnumerator RunningState()
        {
            UIHandler.CombatUI.Debug("Here maybe Play some Movie if Some Condition if Fullfilled");
            yield return new WaitForSeconds(5);
            UIHandler.CombatUI.Debug("Call End State");
            StartCoroutine(EndState());
        }
        public override IEnumerator EndState()
        {
            UIHandler.CombatUI.Debug("Here End of Standby Phase");
            yield return new WaitForSeconds(1);
            UIHandler.CombatUI.Debug("Begin Call State Action");

            StateHelper.ChangeCombatState("actionPhase");
        }
    }
}
