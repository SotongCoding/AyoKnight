using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SotongUtility.StatePattern;

namespace FH_StateModule
{
    public class UnitSateHandler : MonoBehaviour
    {
        public string currentSate;
        public Dictionary<string, IUnitState> avaiableState = new Dictionary<string, IUnitState>();

        private void Start()
        {
            foreach (var item in GetComponentsInChildren<IUnitState>())
            {
                avaiableState.Add(item.stateCode, item);
            }

            CallState("basicAttack");
        }

        public void CallState(string stateCode)
        {
            currentSate = stateCode;
            if (avaiableState.ContainsKey(stateCode)) StartCoroutine(RunStateInOrder(avaiableState[stateCode]));
            else Debug.Log("There are no such "+ stateCode + " on this Object. Please Check Again");
        }

        IEnumerator RunStateInOrder(IUnitState state)
        {
            yield return state.BeginState();
            yield return state.RunningState();
            yield return state.EndState();
        }
    }
}
