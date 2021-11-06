using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_ActionModule
{
    public class UnitActionManager : MonoBehaviour
    {
        [SerializeField] string currentAction;
        public Dictionary<string, IUnitAction> avaiableAction = new Dictionary<string, IUnitAction>();

        private void Start()
        {
            foreach (var item in GetComponentsInChildren<IUnitAction>())
            {
                avaiableAction.Add(item.actionCode, item);
            }
        }

        public void CallState(string actionCode)
        {
            currentAction = actionCode;
            if (avaiableAction.ContainsKey(actionCode)) StartCoroutine((avaiableAction[actionCode].ProccessAction()));
            else Debug.Log("There are no such " + actionCode + " on this Object. Please Check Again");
        }
    }
}
