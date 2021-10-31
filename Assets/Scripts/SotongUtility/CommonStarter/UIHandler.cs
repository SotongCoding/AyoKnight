using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_UIControl;

public class UIHandler
{
    static UIControl_MainCombatUI _combatUI;
    public static UIControl_MainCombatUI CombatUI{
        get{
            if(_combatUI == null){
                _combatUI = GameObject.FindObjectOfType<UIControl_MainCombatUI>();
            }

            return _combatUI;
        }
    }
}
