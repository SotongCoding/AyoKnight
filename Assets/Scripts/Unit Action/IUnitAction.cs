using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;
namespace FH_ActionModule
{
    public interface IUnitAction
    {
        string actionCode { get; }
        UnitObject owner { get; }
        IEnumerator ProccessAction(UnitObject owner, System.Action onDoneAction, CheckComboResult comboResult);
    }
}

