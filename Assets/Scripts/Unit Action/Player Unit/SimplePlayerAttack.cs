using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;
namespace FH_ActionModule
{
    public class SimplePlayerAttack : BasicAttack
    {
        int currentDamage;
        UnitObject target;
        public override IEnumerator ProccessAction(UnitObject owner, System.Action onActionDone, CheckComboResult comboResult)
        {
            if (owner.GetType() == typeof(PlayerObject))
            {
                target = GameManager_BattleManager.Instance.currentEnemyPlay;
            }
            else if (owner.GetType() == typeof(EnemyObject))
            {
                target = GameManager_BattleManager.Instance.currentPlayerPlay;
            }

            if(target == null) {onActionDone?.Invoke(); yield break;}

            UIHandler.CombatUI.Debug("Initialize Action : " + actionCode);
            yield return new WaitForSeconds(3);
            currentDamage = CombatCalculator.CalculateAttackPhaseDamage(
               owner.unitParameter.finalParameter,
               target.unitParameter.finalParameter,
                comboResult);

            UIHandler.CombatUI.Debug("Move Toward Enemys");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Calculating damage");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Giving Damage to : " + target.gameObject.name + " Amount : " + currentDamage);
            target.TakeDamage(currentDamage);

            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Play Anim Attack");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("Back to Position");
            yield return new WaitForSeconds(3);
            UIHandler.CombatUI.Debug("End of Action");

            onActionDone?.Invoke();

        }
    }
}
