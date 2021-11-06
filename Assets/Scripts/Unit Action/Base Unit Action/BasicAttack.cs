using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;

namespace FH_ActionModule
{
    public class BasicAttack : MonoBehaviour, IUnitAction
    {
        public string actionCode => "basicAttack" ;

        protected virtual IEnumerator PreProccess(){
            yield break;
        }
        public virtual IEnumerator ProccessAction(){
            PreProccess();
            yield break;
        }
    }
}
