using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePU : PopUpAction_Base {
    public override void SetData () {
        events = new PopUpEvent (CloseUI, CloseUI, CloseUI);
    }

    void CloseUI () {
        SetEneble (false);
    }
}