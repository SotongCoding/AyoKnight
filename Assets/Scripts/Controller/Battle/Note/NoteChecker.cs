using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteChecker : MonoBehaviour {
    public int noteCode;
    [SerializeField] Image notePict;
    [SerializeField] Image noteBG;
    [SerializeField] Color falseColor, trueColor;
    [SerializeField] Image blocker;
    [SerializeField] Sprite[] noteImage;

    public void InitialNote (int noteCode) {
        this.noteCode = noteCode;
        SetVisualNote ();
    }
    public void Check (int code, out bool result) {
        if (code == noteCode) {
            CallTrueNote ();
            result = true;
        }
        else {
            CallFalseNote ();
            result = false;
        }
    }

    void SetVisualNote () {
        notePict.sprite = noteImage[noteCode - 1];
    }

    void CallTrueNote () {
        noteBG.color = trueColor;
        blocker.color = new Color32 (255, 255, 255, 0);
    }
    void CallFalseNote () {
        noteBG.color = falseColor;
        blocker.color = new Color32 (0, 0, 0, 100);
    }

}