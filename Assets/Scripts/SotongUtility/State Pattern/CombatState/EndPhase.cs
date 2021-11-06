using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SotongUtility.StatePattern
{
    public class EndPhase : MonoBehaviour, ICombatState
    {
        public string stateCode => "actionPhase";

        public IEnumerator BeginState()
        {
            yield break;
        }

        public IEnumerator EndState()
        {
            yield break;
        }

        public IEnumerator RunningState()
        {
            yield break;
        }
    }
}
