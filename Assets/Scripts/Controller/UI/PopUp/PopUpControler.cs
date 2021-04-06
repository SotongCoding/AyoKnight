using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpControler : MonoBehaviour {
    public static PopUpControler _instance;
    public List<PopUpAction_Base> popUps;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (this != _instance) {
            Destroy (gameObject);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">use 'notice', 'winlose'</param>
    /// <param name="tittle"></param>
    /// <param name="mainMessages"></param>
    /// <param name="subMessages"></param>
    public static void CallPopUp (string name, string tittle, string mainMessages, string subMessages) {
        PopUpAction_Base pop = _instance.GetPopUp (name);
        pop.InitialData (tittle, mainMessages, subMessages);
        pop.SetData ();
        _instance.SetBlocker (pop);
        pop.SetEneble (true);
    }
    public static void CallPopUp (string name) {
        PopUpAction_Base pop = _instance.GetPopUp (name);
        pop.SetData ();
        _instance.SetBlocker (pop);
        pop.SetEneble (true);
    }
    void SetBlocker (PopUpAction_Base popUp) {
        Button blocker = popUp.blocker;

        blocker.onClick.RemoveAllListeners ();
        blocker.onClick.AddListener (delegate { popUp.ClickBtnBlocker (); });
    }

    PopUpAction_Base GetPopUp (string popName) {
        foreach (var item in popUps) {
            if (item.popUpName == popName) return item;
        }
        return null;
    }
}