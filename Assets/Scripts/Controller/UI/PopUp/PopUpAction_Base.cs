using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class PopUpAction_Base : MonoBehaviour, IEquatable<PopUpAction_Base> {
    public int popUpID;
    public string popUpName;

    [Header ("UI Input")]
    public Text tittle;
    public Text mainMessage;
    public Text subMessage;
    protected PopUpEvent events;

    public void InitialData (string tittle, string mainMessage, string subMessage) {
        if (this.tittle != null) this.tittle.text = tittle;
        if (this.mainMessage != null) this.mainMessage.text = mainMessage;
        if (this.subMessage != null) this.subMessage.text = subMessage;
    }
    public abstract void SetData ();
    public void ClickBtnA () {
        events.CallBtn1 ();
    }
    public void ClickBtnB () {
        events.CallBtn2 ();
    }
    public void ClickBtnBlocker () {
        events.CallBtnBlock ();
    }
    public void SetEneble (bool isEnable) {
        this.transform.parent.gameObject.SetActive (isEnable);
        this.gameObject.SetActive (isEnable);
    }

    ///==========
    public override string ToString () {
        return "ID: " + popUpID + "   Name: " + popUpName;
    }
    public override bool Equals (object obj) {
        if (obj == null) return false;
        PopUpAction_Base objAsPart = obj as PopUpAction_Base;
        if (objAsPart == null) return false;
        else return Equals (objAsPart);
    }
    public override int GetHashCode () {
        return popUpID;
    }
    public bool Equals (PopUpAction_Base other) {
        if (other == null) return false;
        return (this.popUpID.Equals (other.popUpID));
    }

}
public struct PopUpEvent {
    event Action btnEvent_A;
    event Action btnEvent_B;
    event Action blockerEvent;

    public PopUpEvent (Action btnEvent_A, Action btnEvent_B, Action blockerEvent) {
        this.btnEvent_A = btnEvent_A;
        this.btnEvent_B = btnEvent_B;
        this.blockerEvent = blockerEvent;
    }

    public void CallBtn1 () {
        if (btnEvent_A != null) {
            btnEvent_A ();
        }
    }
    public void CallBtn2 () {
        if (btnEvent_B != null) {
            btnEvent_B ();
        }
    }
    public void CallBtnBlock () {
        if (blockerEvent != null) {
            blockerEvent ();
        }
    }
}