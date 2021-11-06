using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SotongUtility.StatePattern
{
    public interface IState
    {
        string stateCode { get; }
        IEnumerator BeginState();
        IEnumerator RunningState();
        IEnumerator EndState();
    }
}
