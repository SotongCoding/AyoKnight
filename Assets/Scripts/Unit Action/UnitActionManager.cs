using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_BattleModule;
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
        public void ResetDoneAction()
        {
            doneActionRunning = false;
        }

        public void CallAction(UnitObject owner, string actionCode, CheckComboResult comboResult)
        {
            doneActionRunning = false;

            currentAction = actionCode;
            if (avaiableAction.ContainsKey(actionCode))
                StartCoroutine((avaiableAction[actionCode].ProccessAction(owner, DoneAction, comboResult)));
            else
                Debug.Log("There are no such " + actionCode + " on this Object. Please Check Again");
        }
        void DoneAction()
        {
            doneActionRunning = true;
        }
    }
}
