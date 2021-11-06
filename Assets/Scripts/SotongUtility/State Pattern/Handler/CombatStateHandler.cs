using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SotongUtility.StatePattern
{
    public class CombatStateHandler : MonoBehaviour
    {
        [SerializeField] string currentSate;
       public Dictionary<string, ICombatState> avaiableState = new Dictionary<string, ICombatState>();

        private void Start()
        {
            foreach (var item in GetComponentsInChildren<ICombatState>())
            {
                avaiableState.Add(item.stateCode, item);
            }

            CallState("basicAttack");
        }

        public void CallState(string stateCode)
        {
            currentSate = stateCode;
            if (avaiableState.ContainsKey(stateCode)) StartCoroutine(avaiableState[stateCode].BeginState());
            else Debug.Log("There are no such "+ stateCode + " on this Object. Please Check Again");
        }
    }
}
