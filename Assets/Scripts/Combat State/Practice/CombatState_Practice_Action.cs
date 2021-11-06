using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SotongUtility.StatePattern;

namespace FH_StateModule
{
    public class CombatState_Practice_Action : ActionPhase
    {
        public override IEnumerator BeginState()
        {
            Debug.Log("Check Manager what should I Do : Attack or Defense. Then Call Unit Action for Do that");
            yield return new WaitForSeconds(1);
            Debug.Log("Call Combo Arrow");
            yield return new WaitForSeconds(1);
            Debug.Log("Call Running State");
        }
        public override IEnumerator RunningState()
        {
            Debug.Log("Call Unit State to Do something like Manager Said");
            Debug.Log("Enable UI and Start Combo Timer");
            yield return new WaitForSeconds(1);
            Debug.Log("Here wait until all Action is True");
            Debug.Log("Like animation and other");

            yield return new WaitForSeconds(5);
            Debug.Log("Call End State");
            EndState();
        }
        public override IEnumerator EndState()
        {
            Debug.Log("Here End of Action State.");
            yield return new WaitForSeconds(1);
            Debug.Log("Begin Call State Stand By Once more");

            StateHelper.ChangeCombatState("standbyPhase");
        }


    }
}
