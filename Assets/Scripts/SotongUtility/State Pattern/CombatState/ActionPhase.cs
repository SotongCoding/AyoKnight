using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SotongUtility.StatePattern
{
    public abstract class ActionPhase : MonoBehaviour, ICombatState
    {
        public string stateCode => "actionPhase";

        public abstract IEnumerator BeginState();

        public abstract IEnumerator EndState();

        public abstract IEnumerator RunningState();
    }
}
