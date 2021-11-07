using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_ActionModule
{
    public class SimplePlayerAttack : BasicAttack
    {
        public override IEnumerator ProccessAction(System.Action onActionDone)
        {
            UIHandler.CombatUI.Debug("Move Toward Enemys");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Calculating damage");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Play Anim Attack");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Back to Position");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("End of Action");
            
            onActionDone?.Invoke();
            yield return new WaitForSeconds(3);

        }
    }
}
