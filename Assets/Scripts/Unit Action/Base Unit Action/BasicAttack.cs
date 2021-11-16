using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;
using System;

namespace FH_ActionModule
{
    public class BasicAttack : MonoBehaviour, IUnitAction
    {
        public string actionCode => "basicAttack";

        public virtual UnitObject owner => null;

        public virtual IEnumerator ProccessAction(UnitObject owner, Action onDoneAction, CheckComboResult comboResult)
        {
            yield break;
        }
    }
}
