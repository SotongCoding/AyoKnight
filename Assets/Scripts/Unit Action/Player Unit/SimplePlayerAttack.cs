using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_ActionModule
{
    public class SimplePlayerAttack : BasicAttack
    {
        public override IEnumerator ProccessAction()
        {
            Debug.Log("Move Toward Enemys");
            yield return new WaitForSeconds(3);
            Debug.Log("Calculating damage");
            yield return new WaitForSeconds(3);
            Debug.Log("Play Anim Attack");
            yield return new WaitForSeconds(3);
            Debug.Log("Back to Position");
            yield return new WaitForSeconds(3);
            Debug.Log("Switch opponent Turn and set defense");
            yield return new WaitForSeconds(3);

        }
    }
}
