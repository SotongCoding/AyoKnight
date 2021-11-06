using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SotongUtility.StatePattern;
namespace FH_StateModule
{
    public class CombatState_Standby : StandbyPhase
    {
       public override IEnumerator BeginState()
        {
            Debug.Log("Call Combat Mnager for Set Enemy and Player");
            yield return new WaitForSeconds(1);
            Debug.Log("Call Running State");
            RunningState();
        }
        public override IEnumerator RunningState()
        {
           Debug.Log("Here maybe Play some Movie if Some Condition if Fullfilled");
            yield return new WaitForSeconds(5);
            Debug.Log("Call End State");
            EndState();
        }
        public override IEnumerator EndState()
        {
            Debug.Log("Here End of Standby Phase");
            yield return new WaitForSeconds(1);
            Debug.Log("Begin Call State Stand By Once more");

            StateHelper.ChangeCombatState("actionPhase");
        }
    }
}
