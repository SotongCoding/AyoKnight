using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SotongUtility;

namespace FH_UIControl
{
    public class UIControl_MainCombatUI : MonoBehaviour
    {
        public List<CombatUI_Arrow> generatedArrow { private set; get; } = new List<CombatUI_Arrow>();
        [SerializeField] GameObject actionUI;
        [SerializeField] GameObject playerControlUI;
        [SerializeField] TextMeshProUGUI debugText;

        public void Debug(string messege){
            debugText.text = messege;
        }

        #region Arrow Button
        [SerializeField] Transform arrowPlaces;
        #endregion

        #region Timer UI
        [SerializeField] Image fill_timePickArrow;
        #endregion
        public void SetArrow(int[] arrowData)
        {
            generatedArrow.Clear();
            for (int i = 0; i < arrowData.Length; i++)
            {

                CombatUI_Arrow arrow = PoolObjectSystem.Instance.RequestObject(PoolObjectSystem.PoolTag.UIArrow).GetComponent<CombatUI_Arrow>();
                generatedArrow.Add(arrow);

                arrow.transform.SetParent(arrowPlaces);
                arrow.transform.position = Vector3.zero;
                arrow.gameObject.SetActive(true);

                arrow.Initial(arrowData[i]);
            }

        }
        public void ResetArrow(){
            foreach (var item in generatedArrow)
            {
                item.gameObject.SetActive(false);
                PoolObjectSystem.Instance.StoreObject(item);
            }
        }
        internal void SetTimerAmount(float currentPickTime, float pickArrowTime)
        {
            fill_timePickArrow.fillAmount = currentPickTime / pickArrowTime;
        }
        public void ShowActionUI(bool showIt){
            actionUI.SetActive(showIt);
        }
        public void ShowPayerControlUI(bool showIt){
            playerControlUI.SetActive(showIt);
        }
    }
}
