using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FH_UIControl
{

    public class UIControl_UnitUI : MonoBehaviour
    {
        public Image healthBar;

        public Image atk, def;

        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        }

        public void UpdatePriorityIcon(bool playerPriority)
        {
            atk.gameObject.SetActive(playerPriority);
            def.gameObject.SetActive(!playerPriority);
        }
        public void TurnPriorityUI(bool turnOn){
            atk.gameObject.SetActive(turnOn);
            def.gameObject.SetActive(false);
        }
    }
}
