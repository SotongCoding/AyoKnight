using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;
using System;

namespace FH_ActionModule
{
    public class BasicAttack : MonoBehaviour, IUnitAction
    {
        public string actionCode => "basicAttack" ;
        public virtual IEnumerator ProccessAction(System.Action onActionDone){
           
            yield return new WaitForSeconds(1);
            onActionDone?.Invoke();
        }
    }
}
