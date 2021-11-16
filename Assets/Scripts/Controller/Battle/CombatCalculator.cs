using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_BattleModule
{
    public class CombatCalculator
    {
        public static int CalculateAttackPhaseDamage(IUnitBasicParameter unitParamater, IUnitBasicParameter opponent, CheckComboResult comboResult)
        {
            return
            (int)(

            (float)unitParamater.Attack *
            ((float)comboResult.corretAmout / (float)(comboResult.corretAmout + comboResult.missAmount)) *
            (float)(1 + comboResult.perfectAmount) *
            (float)((float)unitParamater.Attack / (float)(unitParamater.Attack + opponent.Defense))

            );
        }
    }
}
