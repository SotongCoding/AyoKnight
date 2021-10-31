using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SotongUtility;

namespace FH_UIControl
{
    public class UIControl_MainCombatUI : MonoBehaviour
    {
       public CombatUI_Arrow [] generatedArrow{private set; get;}

        #region Arrow Button
        [SerializeField] Transform arrowPlaces;
        #endregion

        public void SetArrow(int[] arrowData)
        {
            generatedArrow = new CombatUI_Arrow[arrowData.Length];
            for (int i =0 ;i< arrowData.Length; i++)
            {
                
                CombatUI_Arrow arrow = PoolObjectSystem.Instance.RequestObject(PoolObjectSystem.PoolTag.UIArrow).GetComponent<CombatUI_Arrow>();
                generatedArrow[i] = arrow;

                arrow.transform.SetParent(arrowPlaces);
                arrow.transform.position = Vector3.zero;
                arrow.gameObject.SetActive(true);

                arrow.Initial(arrowData[i]);
            }

        }
    }
}
