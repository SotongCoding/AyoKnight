using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_ActionModule
{
    public class UnitActionManager : MonoBehaviour
    {
        [SerializeField] string currentAction;
        bool doneActionRunning;
        public bool IsCurrentActionDone() { return doneActionRunning; }
        public Dictionary<string, IUnitAction> avaiableAction = new Dictionary<string, IUnitAction>();

        private void Start()
        {
            foreach (var item in GetComponentsInChildren<IUnitAction>())
            {
                avaiableAction.Add(item.actionCode, item);
            }
        }

        public void CallAction(string actionCode)
        {
            doneActionRunning = false;

            currentAction = actionCode;
            if (avaiableAction.ContainsKey(actionCode)) StartCoroutine((avaiableAction[actionCode].ProccessAction(DoneAction)));
            else Debug.Log("There are no such " + actionCode + " on this Object. Please Check Again");
        }
        void DoneAction()
        {
            doneActionRunning = true;
        }
    }
}
