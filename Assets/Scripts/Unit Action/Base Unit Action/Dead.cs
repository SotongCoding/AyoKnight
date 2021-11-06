using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SotongUtility.StatePattern;

namespace FH_StateModule
{
    public class Dead : IUnitState
    {
        public string stateCode => "Dead";
        [SerializeField] protected Animator unitAnimator;

        public virtual IEnumerator BeginState()
        {
            yield break;
        }

        public virtual IEnumerator EndState()
        {
            yield break;
        }

        public virtual IEnumerator RunningState()
        {
            yield break;
        }
    }
}
