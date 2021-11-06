using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_ActionModule
{
    public interface IUnitAction
    {
        string actionCode {get;} 
        IEnumerator ProccessAction();
    }
}

