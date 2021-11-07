using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SotongUtility.StatePattern
{
    public class StateHelper
    {
        static CombatStateHandler _combatState;
        public static void InitialHelper(CombatStateHandler combatState = null)
        {
            _combatState = combatState;
        }
        #region  Unit State
        public IUnitState GetUnitState(object target)
        {
            if (target is IUnitState)
            {
                return (IUnitState)target;
            }
            else return null;
        }
        #endregion

        #region  Combat State
        public static void ChangeCombatState(string stateCode)
        {
            if (_combatState == null)
                _combatState = MonoBehaviour.FindObjectOfType<CombatStateHandler>();

            _combatState.CallState(stateCode);
        }
        #endregion
    }
}
